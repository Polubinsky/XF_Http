using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UkrGo.Model
{
    public class DetailData
    {
        public string Caption { get; set; }
        public string[] DetailString { get; set; }

        public string Region
        {
            get
            {
                if (DetailString.Length >= 4)
                    return Regex.Match(DetailString[4], @"\(([^()]*)\)").Groups[1].Value;
                return string.Empty;
            }
        }

        public string[] Details
        {
            get
            {
                if (DetailString.Length >= 5)
                    return DetailString[5].Replace("<b>", "").Replace("</b>", "").Replace("<br>", "\n").Split('\n');
                return null;
            }
        }

        public string Info => $"{Details[1]} {Details[2]} {Details[3]}";

        public string Text { get; set; }

        public string Contact { get; set; }

        public ObservableCollection<ImageData> ImageLinks { get; set; }

        public int Position { get; set; }
    }
}
