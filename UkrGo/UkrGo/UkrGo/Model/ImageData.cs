using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UkrGo.Model
{
    public class ImageData
    {
        public string ImageLink { get; set; }
        public string Picture => ImageLink.Replace(@"./", @"http://kiev.ukrgo.com/");
    }
}
