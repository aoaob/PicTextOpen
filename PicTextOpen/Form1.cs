using Microsoft.VisualBasic.Devices;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Data.Common;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PicTextOpen
{
    public partial class Form1 : Form
    {
        private List<string> fonts;
        private List<textgroup> textgroups;
        private BindingSource bindingSource;
        private string targetFolderPath; // 获取之前选择的文件夹路径
        private object Lock = new object();
        private List<Label> labels = new List<Label>();

        public Form1()
        {
            InitializeComponent();
            fonts = new List<string>(System.Drawing.FontFamily.Families.Select(f => f.Name));
            textgroups = new List<textgroup>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Load");
            fonttype.DataSource = fonts;
            fonttype.DefaultCellStyle.NullValue = "微软雅黑"; // 或其他默认字体名


            // 初始化DataGridView列
            dataGridView1.AutoGenerateColumns = false;
            //dataGridView1.Columns.Add("Text", "文本");
            //dataGridView1.Columns.Add("FontType", "字体类型");
            //dataGridView1.Columns.Add("Position", "位置");
            //dataGridView1.Columns.Add("Size", "字体大小");

            //text, fonttype, posi, size

            // 设置DataGridViewTextBoxColumn的DataPropertyName以绑定到textgroup类的属性
            dataGridView1.Columns["text"].DataPropertyName = "text";
            dataGridView1.Columns["fonttype"].DataPropertyName = "fonttype";
            dataGridView1.Columns["posiX"].DataPropertyName = "posiX";
            dataGridView1.Columns["posiY"].DataPropertyName = "posiY";

            // 对于“字体大小”列，由于它是数值类型，需要使用DataGridViewTextBoxColumn并手动处理数据格式
            dataGridView1.Columns["size"].DefaultCellStyle.Format = "0.00"; // 格式化为两位小数
            dataGridView1.Columns["size"].DataPropertyName = "size";

            // 添加数据
            AddData();


            // 创建BindingSource并绑定到数据
            bindingSource = new BindingSource();
            bindingSource.ListChanged += bindingSource_ListChanged;
            bindingSource.CurrentItemChanged += bindingSource_CurrentItemChanged;
            bindingSource.DataSource = textgroups;

            // 设置数据源
            dataGridView1.DataSource = bindingSource;

            yulan();

        }

        private void bindingSource_ListChanged(object? sender, ListChangedEventArgs e)
        {
            yulan();
        }

        private void bindingSource_CurrentItemChanged(object? sender, EventArgs e)
        {
            yulan();
        }

        private void yulan()
        {
            lock (Lock)
            {

                if (textgroups.Any() && pictureBox1.Image != null)
                {


                    pictureBox2.Image = pictureBox1.Image;
                    pictureBox2.BackColor = Color.Thistle;

                    double scaleX = (double)pictureBox2.Width / pictureBox2.Image.Width;
                    double scaleY = (double)pictureBox2.Height / pictureBox2.Image.Height;
                    var chaX = 0d;
                    var chaY = 0d;
                    var model = scaleX;
                    if (scaleX > scaleY)
                    {
                        model = scaleY;
                        chaX = (pictureBox1.Width - pictureBox1.Image.Width * model) / 2.0;
                        chaY = 0;

                    }
                    else {
                        chaX = 0;
                        chaY = (pictureBox1.Height - pictureBox1.Image.Height * model) / 2.0;
                    }


                    if (labels.Any())
                    {
                        removeLabels();
                    }
                    foreach (var item in textgroups)
                    {
                        if (item.isNotNull())
                        {
                            var label = new Label();
                            label.Text = item.text;
                            label.Font = new Font(item.fonttype, (float)(item.size * model));
                            label.ForeColor = Color.Black;
                            label.BackColor = Color.Transparent;
                            label.Location = new Point((int)(item.posiX * model+ chaX), (int)(item.posiY * model+ chaY)); // 设置X轴起始位置及动态的Y轴位置
                            label.AutoSize = true; // 自动调整大小以适应文本内容
                            label.UseCompatibleTextRendering = false; 


                            splitContainer2.Panel2.Controls.Add(label);
                            splitContainer2.Panel2.Controls.SetChildIndex(label, 0);


                            labels.Add(label);
                        }
                    }
                    splitContainer2.Panel2.PerformLayout();
                    splitContainer2.Panel2.Controls.SetChildIndex(downButton, 0);
                    //MessageBox.Show("qingyulan");
                    pictureBox2.BackColor = Color.Yellow;
                }
            }
        }

        private void removeLabels()
        {
            //MessageBox.Show(labels.Count.ToString());
            foreach (var label in labels)
            {
                splitContainer2.Panel2.Controls.Remove(label);
            }
            labels = new List<Label>();
        }

        private void AddData()
        {
            // 手动添加数据
            textgroups.Add(new textgroup { text = "示例文本1", fonttype = "宋体", posiX = 100, posiY = 100, size = 52.5f });
            textgroups.Add(new textgroup { text = "示例文本2", fonttype = "宋体", posiX = 200, posiY = 200, size = 52.5f });
            textgroups.Add(new textgroup { text = "示例文本3", fonttype = "宋体", posiX = 300, posiY = 300, size = 52.5f });
            textgroups.Add(new textgroup { text = "示例文本4", fonttype = "宋体", posiX = 400, posiY = 400, size = 52.5f });
            textgroups.Add(new textgroup { text = "示例文本5", fonttype = "黑体", posiX = 500, posiY = 500, size = 54.0f });
        }


        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = dialog.FileName;
            }
        }


        // 若需要在非UI线程中更新数据源，请使用Invoke或BeginInvoke方法确保在UI线程中更新
        private void UpdateDataSource(List<textgroup> newData)
        {
            if (this.dataGridView1.InvokeRequired)
            {
                this.dataGridView1.BeginInvoke(new Action<List<textgroup>>(UpdateDataSource), newData);
            }
            else
            {
                bindingSource.DataSource = null; // 先断开原有数据源（避免循环引用）
                bindingSource.DataSource = newData; // 再连接新的数据源
            }
        }
        // X轴线起点和终点
        //private Point xAxisStart, xAxisEnd;
        // Y轴线起点和终点
        //private Point yAxisStart, yAxisEnd;


        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //// 更新X轴线，让其始终通过鼠标当前位置且平行于PictureBox的顶部和底部
            //xAxisStart = new Point(0, e.Y);
            //xAxisEnd = new Point(pictureBox1.Width, e.Y);

            //// 更新Y轴线，让其始终通过鼠标当前位置且平行于PictureBox的左侧和右侧
            //yAxisStart = new Point(e.X, 0);
            //yAxisEnd = new Point(e.X, pictureBox1.Height);

            //// 重绘PictureBox以显示新位置的轴线
            //pictureBox1.Invalidate();

            //首先判断,宽填充满，还是高填充满



            int chaX, chaY;
            double model;
            ingxy(out chaX, out chaY, out model);

            label1.Text = string.Format("x：{0} y：{1} trueX：{2} trueY：{3}", e.X, e.Y, (int)(e.X / model) - chaX, (int)(e.Y / model) - chaY);



        }

        private void ingxy(out int chaX, out int chaY, out double model)
        {
            double scaleX = (double)pictureBox1.Width / pictureBox1.Image.Width;
            double scaleY = (double)pictureBox1.Height / pictureBox1.Image.Height;
            //var chazhi = Math.Abs(scaleX - scaleX);
            chaX = 0;
            chaY = 0;
            model = scaleX;
            if (scaleX > scaleY)
            {
                model = scaleY;

                chaX = (int)((pictureBox1.Width / model - pictureBox1.Image.Width) / 2.0);
                chaY = 0;

            }
            else
            {

                chaX = 0;
                chaY = (int)((pictureBox1.Height / model - pictureBox1.Image.Height) / 2.0);
            }
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {

            int chaX, chaY;
            double model;
            ingxy(out chaX, out chaY, out model);


            int selectedRowIndex = -1;

            if (dataGridView1.CurrentRow != null)
            {
                selectedRowIndex = dataGridView1.CurrentRow.Index;
            }



            if (selectedRowIndex > -1)
            {
                var currentFD = textgroups[selectedRowIndex];
                currentFD.posiX = (int)(e.X / model) - chaX;
                currentFD.posiY = (int)(e.Y / model) - chaY;
                //MessageBox.Show("xx " + currentFD.posiX + ", yy "+currentFD.posiY + textgroups.ToJSON());
                dataGridView1.Refresh();
                yulan();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {


            //FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            //folderBrowserDialog.Description = "请选择文件保存的文件夹";

            //if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            //{
            //    targetFolderPath = folderBrowserDialog.SelectedPath;
            //}


            if (DateTime.UtcNow > new DateTime(2024, 7, 1, 0, 0, 0, DateTimeKind.Utc))
            {
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = targetFolderPath; // 设置初始目录为之前选择的文件夹
            saveFileDialog.Filter = "PTO文件 (*.pto)|*.pto|所有文件 (*.*)|*.*"; // 设置文件类型过滤器
            saveFileDialog.Title = "保存文件";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                targetFolderPath = saveFileDialog.FileName.Substring(0, saveFileDialog.FileName.LastIndexOf('\\'));
                string filePathToSave = saveFileDialog.FileName;
                // 这里应该包含实际的保存文件逻辑
                try
                {
                    File.WriteAllText(filePathToSave, new pto { setting = textgroups, image = pictureBox1.Image.ImageToBase64(ImageFormat.Bmp) }.ToJSON()); // 示例：保存文本内容到文件
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存文件时发生错误: " + ex.Message);
                }
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = targetFolderPath; // 设置初始目录为之前选择的文件夹
            openFileDialog.Filter = "PTO文件 (*.pto)|*.pto|所有文件 (*.*)|*.*"; // 设置文件类型过滤器
            openFileDialog.Title = "打开文件";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePathToSave = openFileDialog.FileName;
                var jsonString = File.ReadAllText(filePathToSave);
                var jj = NewJsonHelper<pto>.Deserialize(jsonString);
                bindingSource.DataSource = null;
                textgroups = [.. jj.setting];
                bindingSource.DataSource = textgroups;
                pictureBox1.Image = jj.image.Base64ToImage();

                dataGridView1.Refresh();

                yulan();
            }
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            var down = new pto { setting = textgroups};
            var image = down.makeNewImage(pictureBox1.Image);
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = targetFolderPath; // 设置初始目录为之前选择的文件夹
            saveFileDialog.Filter = "PNG文件 (*.png)|*.png|所有文件 (*.*)|*.*"; // 设置文件类型过滤器
            saveFileDialog.Title = "保存图片";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePathToSave = saveFileDialog.FileName;
                image.Save(filePathToSave,ImageFormat.Png);
            }
        }
    }
}
