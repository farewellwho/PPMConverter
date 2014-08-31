using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PPMConverter
{
    public class PPMInfo
    {
        public string Format { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public byte[][] RgbData { get; set; }

        public static PPMInfo FromPPMFile(string ppmfile)
        {
            try
            {
	            FileStream fs = new FileStream(ppmfile, FileMode.Open, FileAccess.Read);
	            StreamReader sr = new StreamReader(fs);
	            string format = sr.ReadLine(); // 图像格式
	            string sizeStr = sr.ReadLine();
	            string[] widthheight = sizeStr.Split(' ');
	            int width = int.Parse(widthheight[0]); // 宽度（像素）
	            int height = int.Parse(widthheight[1]); // 高度（像素）
	            int maxValue = int.Parse(sr.ReadLine()); // 最大像素值
	            if (format.ToLower().Equals("p6"))
	            {
	                // 初始化rgb字节数组
	                byte[][] rgbData = new byte[height][];
	                for (int i = 0; i < height; i++ )
	                {
	                    rgbData[i] = new byte[width * 3]; // 每个像素3字节
	                }
                    // 按行读取图像数据
	                for (int row = 0; row < height; row++ )
	                {
	                    fs.Read(rgbData[row], 0, width * 3);
	                }
                    PPMInfo ppminfo = new PPMInfo();
                    ppminfo.Format = format;
                    ppminfo.Width = width;
                    ppminfo.Height = height;
                    ppminfo.RgbData = rgbData;
                    return ppminfo;
	            }
	            return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
    }
}
