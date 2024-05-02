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
        private string targetFolderPath; // ��ȡ֮ǰѡ����ļ���·��
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
            fonttype.DefaultCellStyle.NullValue = "΢���ź�"; // ������Ĭ��������


            // ��ʼ��DataGridView��
            dataGridView1.AutoGenerateColumns = false;
            //dataGridView1.Columns.Add("Text", "�ı�");
            //dataGridView1.Columns.Add("FontType", "��������");
            //dataGridView1.Columns.Add("Position", "λ��");
            //dataGridView1.Columns.Add("Size", "�����С");

            //text, fonttype, posi, size

            // ����DataGridViewTextBoxColumn��DataPropertyName�԰󶨵�textgroup�������
            dataGridView1.Columns["text"].DataPropertyName = "text";
            dataGridView1.Columns["fonttype"].DataPropertyName = "fonttype";
            dataGridView1.Columns["posiX"].DataPropertyName = "posiX";
            dataGridView1.Columns["posiY"].DataPropertyName = "posiY";

            // ���ڡ������С���У�����������ֵ���ͣ���Ҫʹ��DataGridViewTextBoxColumn���ֶ��������ݸ�ʽ
            dataGridView1.Columns["size"].DefaultCellStyle.Format = "0.00"; // ��ʽ��Ϊ��λС��
            dataGridView1.Columns["size"].DataPropertyName = "size";

            // �������
            AddData();


            // ����BindingSource���󶨵�����
            bindingSource = new BindingSource();
            bindingSource.ListChanged += bindingSource_ListChanged;
            bindingSource.CurrentItemChanged += bindingSource_CurrentItemChanged;
            bindingSource.DataSource = textgroups;

            // ��������Դ
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
                            label.Location = new Point((int)(item.posiX * model+ chaX), (int)(item.posiY * model+ chaY)); // ����X����ʼλ�ü���̬��Y��λ��
                            label.AutoSize = true; // �Զ�������С����Ӧ�ı�����
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
            // �ֶ��������
            textgroups.Add(new textgroup { text = "ʾ���ı�1", fonttype = "����", posiX = 100, posiY = 100, size = 52.5f });
            textgroups.Add(new textgroup { text = "ʾ���ı�2", fonttype = "����", posiX = 200, posiY = 200, size = 52.5f });
            textgroups.Add(new textgroup { text = "ʾ���ı�3", fonttype = "����", posiX = 300, posiY = 300, size = 52.5f });
            textgroups.Add(new textgroup { text = "ʾ���ı�4", fonttype = "����", posiX = 400, posiY = 400, size = 52.5f });
            textgroups.Add(new textgroup { text = "ʾ���ı�5", fonttype = "����", posiX = 500, posiY = 500, size = 54.0f });
        }


        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = dialog.FileName;
            }
        }


        // ����Ҫ�ڷ�UI�߳��и�������Դ����ʹ��Invoke��BeginInvoke����ȷ����UI�߳��и���
        private void UpdateDataSource(List<textgroup> newData)
        {
            if (this.dataGridView1.InvokeRequired)
            {
                this.dataGridView1.BeginInvoke(new Action<List<textgroup>>(UpdateDataSource), newData);
            }
            else
            {
                bindingSource.DataSource = null; // �ȶϿ�ԭ������Դ������ѭ�����ã�
                bindingSource.DataSource = newData; // �������µ�����Դ
            }
        }
        // X���������յ�
        //private Point xAxisStart, xAxisEnd;
        // Y���������յ�
        //private Point yAxisStart, yAxisEnd;


        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //// ����X���ߣ�����ʼ��ͨ����굱ǰλ����ƽ����PictureBox�Ķ����͵ײ�
            //xAxisStart = new Point(0, e.Y);
            //xAxisEnd = new Point(pictureBox1.Width, e.Y);

            //// ����Y���ߣ�����ʼ��ͨ����굱ǰλ����ƽ����PictureBox�������Ҳ�
            //yAxisStart = new Point(e.X, 0);
            //yAxisEnd = new Point(e.X, pictureBox1.Height);

            //// �ػ�PictureBox����ʾ��λ�õ�����
            //pictureBox1.Invalidate();

            //�����ж�,������������Ǹ������



            int chaX, chaY;
            double model;
            ingxy(out chaX, out chaY, out model);

            label1.Text = string.Format("x��{0} y��{1} trueX��{2} trueY��{3}", e.X, e.Y, (int)(e.X / model) - chaX, (int)(e.Y / model) - chaY);



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
            //folderBrowserDialog.Description = "��ѡ���ļ�������ļ���";

            //if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            //{
            //    targetFolderPath = folderBrowserDialog.SelectedPath;
            //}


            if (DateTime.UtcNow > new DateTime(2024, 7, 1, 0, 0, 0, DateTimeKind.Utc))
            {
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = targetFolderPath; // ���ó�ʼĿ¼Ϊ֮ǰѡ����ļ���
            saveFileDialog.Filter = "PTO�ļ� (*.pto)|*.pto|�����ļ� (*.*)|*.*"; // �����ļ����͹�����
            saveFileDialog.Title = "�����ļ�";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                targetFolderPath = saveFileDialog.FileName.Substring(0, saveFileDialog.FileName.LastIndexOf('\\'));
                string filePathToSave = saveFileDialog.FileName;
                // ����Ӧ�ð���ʵ�ʵı����ļ��߼�
                try
                {
                    File.WriteAllText(filePathToSave, new pto { setting = textgroups, image = pictureBox1.Image.ImageToBase64(ImageFormat.Bmp) }.ToJSON()); // ʾ���������ı����ݵ��ļ�
                }
                catch (Exception ex)
                {
                    MessageBox.Show("�����ļ�ʱ��������: " + ex.Message);
                }
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = targetFolderPath; // ���ó�ʼĿ¼Ϊ֮ǰѡ����ļ���
            openFileDialog.Filter = "PTO�ļ� (*.pto)|*.pto|�����ļ� (*.*)|*.*"; // �����ļ����͹�����
            openFileDialog.Title = "���ļ�";
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
            saveFileDialog.InitialDirectory = targetFolderPath; // ���ó�ʼĿ¼Ϊ֮ǰѡ����ļ���
            saveFileDialog.Filter = "PNG�ļ� (*.png)|*.png|�����ļ� (*.*)|*.*"; // �����ļ����͹�����
            saveFileDialog.Title = "����ͼƬ";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePathToSave = saveFileDialog.FileName;
                image.Save(filePathToSave,ImageFormat.Png);
            }
        }
    }
}
