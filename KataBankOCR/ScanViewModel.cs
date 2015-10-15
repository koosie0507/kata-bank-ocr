using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace KataBankOCR
{
    public class ScanViewModel : INotifyPropertyChanged
    {
        private static readonly Dictionary<string, char> GlyphsToChars = new Dictionary<string, char>
                                                                             {
                                                                                 {" _ | ||_|", '0'},
                                                                                 {"     |  |", '1'},
                                                                                 {" _  _||_ ", '2'},
                                                                                 {" _  _| _|", '3'},
                                                                                 {"   |_|  |", '4'},
                                                                                 {" _ |_  _|", '5'},
                                                                                 {" _ |_ |_|", '6'},
                                                                                 {" _   |  |", '7'},
                                                                                 {" _ |_||_|", '8'},
                                                                                 {" _ |_| _|", '9'}
                                                                             };
        private IEnumerable<string> accountNumbers;


        public ScanViewModel()
        {
            this.AccountNumbers = Enumerable.Empty<string>();
        }

        public IEnumerable<string> AccountNumbers
        {
            get
            {
                return this.accountNumbers;
            }
            private set
            {
                this.accountNumbers = value;
                this.RaisePropertyChanged();
            }
        }

        public void Scan(string fileContents)
        {
            this.AccountNumbers = this.ParseAccountNumbers(fileContents).ToList();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private IEnumerable<string> GetGlyphs(string[] entryLines)
        {
            for (int j = 0; j < 27; j += 3)
            {
                var glyphs = string.Join(
                    string.Empty,
                    entryLines[0].Substring(j, 3),
                    entryLines[1].Substring(j, 3),
                    entryLines[2].Substring(j, 3));
                yield return glyphs;
            }
        }

        private IEnumerable<string> ParseAccountNumbers(string fileContents)
        {
            string[] lines = fileContents.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < lines.Length-2; i += 4)
            {
                char[] accountNumberChars = this.GetGlyphs(new[] {lines[i], lines[i+1], lines[i+2]})
                    .Select(glyph => GlyphsToChars[glyph])
                    .ToArray();
                yield return new string(accountNumberChars);
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if(handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}