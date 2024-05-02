using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicTextOpen
{
    internal static class jsonhelper
    {
        public static string ToJSON(this object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            return JsonConvert.SerializeObject(obj).ConvertJsonString();
        }
        public static string ConvertJsonString(this string str)
        {
            //格式化json字符串
            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            jsetting.NullValueHandling = NullValueHandling.Ignore;

            JsonSerializer serializer = JsonSerializer.Create(jsetting);
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' ',
                };
                serializer.Serialize(jsonWriter, obj);
                var result = textWriter.ToString();
                result = result.Replace(" null", " \"\"");
                return result;
            }
            else
            {
                return str;
            }
        }

        public static string ImageToBase64(this Image image, ImageFormat format)
        {
            using (var stream = new MemoryStream())
            {
                // 将Image保存到内存流中
                image.Save(stream, format);

                // 将内存流转换为字节数组
                var imageData = stream.ToArray();

                // 使用Convert类将字节数组转换为Base64编码的字符串
                return Convert.ToBase64String(imageData);
            }
        }
        public static Image Base64ToImage(this string base64String)
        {
            // 先确保Base64字符串是有效的图片数据，不含"data:image/png;base64,"这样的前缀
            if (base64String.StartsWith("data:image/"))
            {
                // 如果有"data:image/..."前缀，则剥离掉
                base64String = base64String.Substring(base64String.IndexOf(',') + 1);
            }

            // 将Base64字符串解码为字节数组
            byte[] imageBytes = Convert.FromBase64String(base64String);

            // 使用内存流加载字节数组
            using (var ms = new MemoryStream(imageBytes))
            {
                // 从内存流中创建Image对象
                return Image.FromStream(ms);
            }
        }

    }
}
