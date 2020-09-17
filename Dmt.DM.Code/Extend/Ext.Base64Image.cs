using System;
using System.Drawing;
using System.IO;

namespace Dmt.DM.Code
{
    public static partial class Ext
    {
        ///// <summary>
        ///// base64 转 Image
        ///// </summary>
        ///// <param name="base64"></param>
        //public static void Base64ToImage(string base64)
        //{
        //    base64 = base64.Replace("data:image/png;base64,", "").Replace("data:image/jgp;base64,", "").Replace("data:image/jpg;base64,", "").Replace("data:image/jpeg;base64,", "");//将base64头部信息替换
        //    byte[] bytes = Convert.FromBase64String(base64);
        //    MemoryStream memStream = new MemoryStream(bytes);
        //    Image mImage = Image.FromStream(memStream);
        //    Bitmap bp = new Bitmap(mImage);
        //    bp.Save("C:/Users/Administrator/Desktop/" + DateTime.Now.ToString("yyyyMMddHHss") + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);//注意保存路径
        //}

        /// <summary>
        /// Image 转成 base64
        /// </summary>
        /// <param name="fileFullName"></param>
        public static string ImageToBase64(string fileFullName,string fileType = "jpg")
        {
            Bitmap bmp = new Bitmap(fileFullName);
            MemoryStream ms = new MemoryStream();
            switch (fileType.ToLower())
            {
                case "jpg":
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                case "bmp":
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;
                case "gif":
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                    break;
                case "png":
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    break;
            }

            byte[] arr = new byte[ms.Length]; ms.Position = 0;
            ms.Read(arr, 0, (int)ms.Length); ms.Close();
            return Convert.ToBase64String(arr);
        }
    }
}
