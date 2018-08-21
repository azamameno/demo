using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Video;
using Accord.Video.FFMPEG;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace TestPush
{
    public class VideoMaker
    {
        public static void Run()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Img");
            string videoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Video");
            if (Directory.Exists(path))
            {
                string scalePath = Path.Combine(path, "Scale");
                if (Directory.Exists(scalePath))
                {
                    var dirScale = new DirectoryInfo(scalePath);
                    var scaleImgs = dirScale.GetFiles();
                    foreach (var scaleImg in scaleImgs)
                        scaleImg.Delete();
                }
                else
                    Directory.CreateDirectory(scalePath);

                if (!Directory.Exists(videoPath))
                    Directory.CreateDirectory(videoPath);

                var dir = new DirectoryInfo(path);
                var imgs = dir.GetFiles();

                int width = 1920;
                int height = 1080;

                VideoFileWriter writer = new VideoFileWriter();
                writer.Open(Path.Combine(videoPath, "test.mov"), width, height, 1, VideoCodec.MPEG4, 1000000);

                foreach (var img in imgs)
                {
                    //writer.WriteVideoFrame(new Bitmap(BitmapScale(img.FullName, width, height)));
                    writer.WriteVideoFrame(BitmapScaleV2(img.FullName, width, height));
                    //writer.WriteVideoFrame(new Bitmap(img.FullName));
                }

                writer.Close();

            }
        }

        public static string BitmapScale(string file, int width, int height)
        {
            var brush = new SolidBrush(Color.Black);
            FileInfo fileInfo = new FileInfo(file);
            var image = new Bitmap(file);
            float scale = Math.Min((float)width / (float)image.Width, (float)height / (float)image.Height);

            var bmp = new Bitmap((int)width, (int)height);
            var graph = Graphics.FromImage(bmp);

            var scaleWidth = (int)(image.Width * scale);
            var scaleHeight = (int)(image.Height * scale);

            graph.FillRectangle(brush, new RectangleF(0, 0, width, height));
            graph.DrawImage(image, ((int)width - scaleWidth) / 2, ((int)height - scaleHeight) / 2, scaleWidth, scaleHeight);

            string outName = Path.Combine(fileInfo.DirectoryName, "Scale", fileInfo.Name);
            bmp.Save(outName);

            return outName;
        }

        public static Bitmap BitmapScaleV2(string file, int width, int height)
        {
            var brush = new SolidBrush(Color.Black);
            var image = new Bitmap(file);

            float scale = Math.Min((float)width / (float)image.Width, (float)height / (float)image.Height);
            var scaleWidth = (int)(image.Width * scale);
            var scaleHeight = (int)(image.Height * scale);

            var bmp = new Bitmap((int)width, (int)height, PixelFormat.Format32bppRgb);
            using (Graphics gr = Graphics.FromImage(bmp))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.FillRectangle(brush, new RectangleF(0, 0, width, height));
                gr.DrawImage(image, new Rectangle(0, 0, width, height));
            }
            
            return bmp;
        }
    }
    
}
