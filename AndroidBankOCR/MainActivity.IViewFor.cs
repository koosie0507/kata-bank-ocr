namespace AndroidBankOCR
{
    using System.Collections.Generic;
    using System.IO;

    using Android.App;
    using Android.Content.Res;
    using Android.OS;
    using Android.Views;
    using Android.Widget;

    using KataBankOCR;

    using ReactiveUI;

    partial class MainActivity: IViewFor<ScanViewModel>
    {
        object IViewFor.ViewModel
        {
            get
            {
                return this.ViewModel;
            }
            set
            {
                this.ViewModel = (ScanViewModel)value;
            }
        }

        public ScanViewModel ViewModel { get; set; }
    }
}