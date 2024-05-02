using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicTextOpen
{
    public class pto
    {
        public List<textgroup> setting { get; set; }
        public string image { get; set; }

        public Image makeNewImage()
        {
            var newimage = image.Base64ToImage();
            foreach (var item in setting)
            {
                if (item.isNotNull())
                {
                    newimage = AddTextToImage(newimage, item.text, new Font(item.fonttype, item.size, FontStyle.Regular), new SolidBrush(Color.Black), new PointF(item.posiX, item.posiY));
                }
            }
            return newimage;
        }

        public Image makeNewImage(Image newimagef)
        {
            var newimage = newimagef;
            foreach (var item in setting)
            {
                if (item.isNotNull())
                {
                    newimage = AddTextToImage(newimage, item.text, new Font(item.fonttype, item.size, FontStyle.Regular), new SolidBrush(Color.Black), new PointF(item.posiX, item.posiY));
                }
            }
            return newimage;
        }

        private Bitmap AddTextToImage(Image originalImage, string text, Font font, Brush brush, PointF location)
        {
            // 将Image转换为Bitmap以便进行图形操作
            Bitmap bitmap = new Bitmap(originalImage);

            // 创建Graphics对象
            Graphics graphics = Graphics.FromImage(bitmap);

            // 设置质量等其他绘图选项（可选）
            //graphics.SmoothingMode = SmoothingMode.AntiAlias;
            //graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            graphics.SmoothingMode = SmoothingMode.None;
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics.PixelOffsetMode = PixelOffsetMode.None;


            // 绘制文本
            graphics.DrawString(text, font, brush, location);

            // 清理资源
            graphics.Dispose();

            // 返回带有文字的Bitmap对象
            return bitmap;
        }
    }
}
