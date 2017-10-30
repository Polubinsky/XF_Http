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
