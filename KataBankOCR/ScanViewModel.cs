using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace KataBankOCR
{
    public class ScanViewModel : INotifyPropertyChanged
    {
        public IEnumerable<string> AccountNumbers { get; private set; }

        public ScanViewModel()
        {
            AccountNumbers = Enumerable.Empty<string>();
        }

        private IEnumerable<string> ParseAccountNumbers(string fileContents)
        {
            yield return "000000000";
        }

        public void Scan(string fileContents)
        {
            AccountNumbers = ParseAccountNumbers(fileContents);
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}