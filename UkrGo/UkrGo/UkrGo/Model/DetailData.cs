using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
                try
                {
                    if (DetailString.Length >= 4)
                        return Regex.Match(DetailString[4], @"\(([^()]*)\)").Groups[1].Value;
                }
                catch (Exception )
                {
                    Debug.WriteLine(DetailString.ToString());
                }
                return string.Empty;
            }
        }

        public string[] Details
        {
            get
            {
                try
                {
                    if (DetailString.Length >= 5)
                    return DetailString[5].Replace("<b>", "").Replace("</b>", "").Replace("<br>", "\n").Split('\n');
                }
                catch (Exception)
                {
                    Debug.WriteLine(DetailString.ToString());
                }
                return null;
            }
        }

        public string Info
        {
            get
            {
                if (Details.Length > 4)
                    return $"{Details[1]} {Details[2]} {Details[3]}";
                else return string.Empty;
            }
        } 

        public string Text { get; set; }

        public string Contact { get; set; }

        public ObservableCollection<ImageData> ImageLinks { get; set; }

        public int Position { get; set; }
    }
}
