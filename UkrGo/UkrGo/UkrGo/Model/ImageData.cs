namespace UkrGo.Model
{
    public class ImageData
    {
        public string ImageLink { get; set; }
        public string Picture => ImageLink.Replace(@"./", @"http://kiev.ukrgo.com/");
        public long? ImageSize { get; set; }
    }
}
