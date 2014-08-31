using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PPMConverter;

namespace PPMConverterForm
{
    public partial class PPMConverterForm : Form
    {
        private string selectedPPMFile = null;
        public PPMConverterForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //PPMConverter.ConvertToBitmap("d:\\MathPic.ppm");
            /*
            int heigth = 200;
            int width = 400;
            byte[][] rgbData = new byte[heigth][];
            for (int i = 0; i < heigth; i++ )
            {
                rgbData[i] = new byte[width * 3];
            }
            for (int row = 0; row < heigth; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    if (row >= heigth / 3 && row <= heigth / 2)
                    {
                        rgbData[row][3 * col] = 0;
                        rgbData[row][3 * col + 1] = 0;
                        rgbData[row][3 * col + 2] = 0;
                    }
                    else
                    {
                        rgbData[row][3 * col] = 255;
                        rgbData[row][3 * col + 1] = 255;
                        rgbData[row][3 * col + 2] = 255;
                    }
                }
            }
            string filepath = "d:\\bitmap.bmp";
            Bitmap bitmap = PPMConverter.BitmapFromRgbData(rgbData, width, heigth);
            bitmap.Save(filepath);
            */
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            OpenFileDialog filedialog = new OpenFileDialog();
            filedialog.Multiselect = false;
            filedialog.Filter = "PPM 文件 (*.ppm)|*.ppm";
            filedialog.CheckFileExists = true;
            filedialog.CheckPathExists = true;
            DialogResult dialogResult = filedialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                selectedPPMFile = filedialog.FileName;
                this.txtPPMFile.Text = selectedPPMFile;
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {

            
            string ppmfile = this.txtPPMFile.Text.Trim();
            // 检查是否选择文件
            if (ppmfile.Equals(""))
            {
                MessageBox.Show("请选择要转换的.ppm文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // 检查所选文件是否为.ppm格式
            int pointIndex = ppmfile.LastIndexOf('.');
            if (!ppmfile.Substring(pointIndex + 1, ppmfile.Length - pointIndex - 1).ToLower().Equals("ppm"))
            {
                MessageBox.Show("只能转换.ppm格式文件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // 检查所选文件是否存在
            if (!File.Exists(ppmfile))
            {
                MessageBox.Show("指定的文件不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // 准备转换
            string bitmapfile = PPMConverter.Util.changeSuffix(ppmfile, "bmp");
            if (File.Exists(bitmapfile))
            {
                DialogResult dialogResult = MessageBox.Show("与该.ppm文件同名的.bmp文件已经存在，是否覆盖？", "文件已存在", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }
            }
            // 执行转换并成功
            if (PPMConverter.PPMConverter.ConvertToBitmap(ppmfile))
            {
                MessageBox.Show("转换完成", "转换成功", MessageBoxButtons.OK);
            }
            // 转换失败，输出提示信息
            else
            {
                MessageBox.Show("文件不存在或格式损坏，转换失败", "转换失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
            
    }
}
