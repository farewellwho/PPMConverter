using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace PPMConverter
{
    public class PPMConverter
    {
        public static bool ConvertToBitmap(string ppmfile)
        {
            try
            {
                PPMInfo ppminfo = PPMInfo.FromPPMFile(ppmfile); // 读取ppm文件信息
                if (ppminfo != null)
                {
                    string bitmapfile = ppmfile.Substring(0, ppmfile.LastIndexOf('.')) + ".bmp"; // bmp文件名
                    Bitmap bitmap = BitmapFromRgbData(ppminfo.RgbData, ppminfo.Width, ppminfo.Height);
                    bitmap.Save(bitmapfile); // 保存为bmp文件
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        private static Bitmap BitmapFromRgbData(byte[][] rgbData, int width, int height)
        {
            MemoryStream ms = new MemoryStream();
            //FileStream fs = new FileStream(outPath, FileMode.OpenOrCreate);
            BinaryWriter bw = new BinaryWriter(ms);
            int bpp = 24;
            int storeWidth = ((bpp * width + 31) >> 5) << 2;
            // bmp文件头， 14字节
            bw.Write((byte)'B'); bw.Write((byte)'M'); // 文件类型，2字节
            bw.Write(14 + 40 + storeWidth * height);// 位图文件大小，4字节
            bw.Write((short)0); // 保留，2字节
            bw.Write((short)0); // 保留，2字节
            bw.Write(14 + 40); // 数据偏移量，4字节

            // 位图信息头，40字节
            bw.Write(40); // 信息头字节数，4字节
            bw.Write(width); // 宽度（像素），4字节
            bw.Write(height); // 高度（像素），4字节
            bw.Write((short)1); // 颜色平面数（1）， 2字节
            bw.Write((short)bpp); // 位数（1、4、8、16、24或32），2字节
            bw.Write(0); // 压缩格式（0、1、2、3、4、5），4字节
            bw.Write(0); // 图像大小，压缩为0时，可设置为0。4字节
            bw.Write(0); // 水平分辨率，缺省为0，4字节
            bw.Write(0); // 垂直分辨率，缺省为0,4字节
            bw.Write(0); // 颜色索引数，4字节
            bw.Write(0); // 有重要影响的颜色索引数，0表示都重要，4字节。

            // 位图数据
            int fill = storeWidth - width * 3; // 每行需要补0的字节数
            for (int row = height - 1; row >= 0; row-- ) // 从最后一行开始存储数据
            {
                for (int col = 0; col < width; col++ )
                {
                    bw.Write(rgbData[row][3 * col + 2]); // B
                    bw.Write(rgbData[row][3 * col + 1]); // G
                    bw.Write(rgbData[row][3 * col]); // R
                }
                // 每行补0
                for (int i = 0; i < fill; i++ )
                {
                    bw.Write((byte)0);
                }
            }
            bw.Flush();
            return new Bitmap(ms);
        }


    }
}
