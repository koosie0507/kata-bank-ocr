using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace KataBankOCR
{
    using System;
    using System.Diagnostics;

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

        internal const int MachineLineWidth = 27;

        public IEnumerable<string> AccountNumbers { get; private set; }

        public ScanViewModel()
        {
            AccountNumbers = Enumerable.Empty<string>();
        }

        private IEnumerable<string> GetGlyphs(string[] entryLines)
        {
            for (int j = 0; j < MachineLineWidth; j += 3)
            {
                var glyphs = string.Join(
                    string.Empty,
                    entryLines[0].Substring(j, 3),
                    entryLines[1].Substring(j, 3),
                    entryLines[2].Substring(j, 3));
                Debug.WriteLine(glyphs.Substring(0,9));
                yield return glyphs;
            }
        }

        private IEnumerable<string> ParseAccountNumbers(string fileContents)
        {
            string[] lines = fileContents.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < lines.Length; i += 4)
            {
                char[] accountNumberChars = this.GetGlyphs(new[] {lines[i], lines[i+1], lines[i+2]})
                    .Select(glyph => GlyphsToChars[glyph])
                    .ToArray();
                yield return new string(accountNumberChars);
            }
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