namespace Wp8BankOCR
{
    using System;
    using System.Collections.Generic;

    using Windows.Storage;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    using KataBankOCR;

    public sealed partial class MainPage
    {
        private readonly Dictionary<string, string> files;

        private readonly ScanViewModel viewModel;

        public MainPage()
        {
            this.viewModel = new ScanViewModel();
            this.InitializeComponent();
            this.files = new Dictionary<string, string>();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            this.DataContext = this.viewModel;
        }

        private Uri GetResourceName(string fileName)
        {
            return new Uri(string.Format("ms-appx:///Files/{0}", fileName));
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            string[] fileNames = { "TextFile1.txt", "TextFile2.txt", "TextFile3.txt" };
            foreach (var fileName in fileNames)
            {
                var resourceUri = this.GetResourceName(fileName);
                var file = await StorageFile.GetFileFromApplicationUriAsync(resourceUri);
                this.files.Add(file.Name, await FileIO.ReadTextAsync(file));
                this.FilePicker.Items.Add(fileName);
            }
        }

        private void OnFilePickerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.viewModel.Scan(this.files[(string)((ComboBox)sender).SelectedValue]);
        }
    }
}