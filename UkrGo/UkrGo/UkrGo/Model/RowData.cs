using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UkrGo.Model
{
    public class RowData
    {
        public ImageData MainImage { get; set; }
        public string Link { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
       
        public RowData()
        {
            MainImage = new ImageData();
        }

    }
}
