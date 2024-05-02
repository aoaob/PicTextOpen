
namespace PicTextOpen
{
    public class textgroup
    {
        public string text { get; set; }
        public string fonttype { get; set; }
        public int posiX { get; set; }
        public int posiY { get; set; }
        public float size { get; set; }
        public textgroup()
        {
            this.text = "新的水印文字";
            this.fonttype = "微软雅黑";
            this.posiX = 100;
            this.posiY = 100;
            this.size = 9.0f;
        }

        internal bool isNotNull()
        {
            return !string.IsNullOrEmpty(this.text) && !string.IsNullOrEmpty(this.fonttype) && posiX > 0 && posiY > 0 && size > 0;
        }
    }
}