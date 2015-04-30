namespace AndroidBankOCR
{
    using System.Collections.Generic;
    using System.IO;

    using Android.App;
    using Android.Content.Res;
    using Android.OS;
    using Android.Widget;

    using KataBankOCR;

    using ReactiveUI;

    [Activity(Label = "AndroidBankOCR", MainLauncher = true, Icon = "@drawable/icon")]
    public partial class MainActivity : Activity
    {
        private readonly ReactiveList<string> accountNumbers;

        public MainActivity()
        {
            this.ViewModel = new ScanViewModel();
            this.accountNumbers = new ReactiveList<string>();
        }

        public IEnumerable<string> AccountNumbers
        {
            get
            {
                return this.accountNumbers;
            }
            set
            {
                this.accountNumbers.Clear();
                this.accountNumbers.AddRange(value);
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.Main);

            this.InitFilePicker();
            this.InitListView();

            this.OneWayBind(this.ViewModel, vm => vm.AccountNumbers, v => v.AccountNumbers);
        }

        private void InitListView()
        {
            var accountNumbersListView = this.FindViewById<ListView>(Resource.Id.accountNumbersListView);
            accountNumbersListView.Adapter = new ReactiveListAdapter<string>(
                this.accountNumbers,
                (_, g) => new TextView(g.Context),
                (s, v) => ((TextView)v).Text = s);
        }

        private void InitFilePicker()
        {
            var filePicker = this.FindViewById<Spinner>(Resource.Id.filePicker);
            var adapter = ArrayAdapter.CreateFromResource(
                this,
                Resource.Array.fileNames,
                Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            filePicker.Adapter = adapter;

            filePicker.ItemSelected += this.OnFileSelected;
        }

        private void OnFileSelected(object sender, AdapterView.ItemSelectedEventArgs evt)
        {
            if (evt.Position < 0)
            {
                return;
            }
            this.ViewModel.Scan(this.GetFileContents(((Spinner)sender).SelectedItem.ToString()));
        }

        private string GetFileContents(string fileName)
        {
            using (var str = this.Assets.Open(fileName, Access.Buffer)) using (var rdr = new StreamReader(str)) return rdr.ReadToEnd();
        }
    }
}