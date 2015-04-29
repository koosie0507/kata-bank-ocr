using System.IO;
using System.Text;

using KataBankOCR;

using Microsoft.Win32;

namespace WpfBankOCR
{
    public partial class ScanWindow
    {
        private readonly ScanViewModel viewModel;

        public ScanWindow()
        {
            this.viewModel = new ScanViewModel();
            this.InitializeComponent();
            this.DataContext = this.viewModel;
        }

        public void OpenFile()
        {
            var fileDialog = new OpenFileDialog
                                 {
                                     Filter = "Text files (*.txt)|*.txt",
                                     Title = "Open machine output file...",
                                     ValidateNames = true
                                 };
            if (!fileDialog.ShowDialog(this) ?? false)
            {
                return;
            }

            string fileContents;
            using (var reader = new StreamReader(fileDialog.FileName, Encoding.UTF8))
            {
                fileContents = reader.ReadToEnd();
            }
            this.viewModel.Scan(fileContents);
        }
    }
}