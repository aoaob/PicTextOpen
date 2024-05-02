
namespace PicTextOpen
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            splitContainer1 = new SplitContainer();
            splitContainer3 = new SplitContainer();
            dataGridView1 = new DataGridView();
            text = new DataGridViewTextBoxColumn();
            fonttype = new DataGridViewComboBoxColumn();
            posiX = new DataGridViewTextBoxColumn();
            posiY = new DataGridViewTextBoxColumn();
            size = new DataGridViewTextBoxColumn();
            loadButton = new Button();
            saveButton = new Button();
            splitContainer2 = new SplitContainer();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            downButton = new Button();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new Size(1502, 999);
            splitContainer1.SplitterDistance = 757;
            splitContainer1.TabIndex = 0;
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = DockStyle.Fill;
            splitContainer3.Location = new Point(0, 0);
            splitContainer3.Name = "splitContainer3";
            splitContainer3.Orientation = Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(dataGridView1);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.BackColor = SystemColors.ButtonHighlight;
            splitContainer3.Panel2.Controls.Add(loadButton);
            splitContainer3.Panel2.Controls.Add(saveButton);
            splitContainer3.Size = new Size(757, 999);
            splitContainer3.SplitterDistance = 872;
            splitContainer3.TabIndex = 0;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { text, fonttype, posiX, posiY, size });
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(757, 872);
            dataGridView1.TabIndex = 1;
            // 
            // text
            // 
            text.HeaderText = "文字";
            text.MinimumWidth = 8;
            text.Name = "text";
            // 
            // fonttype
            // 
            fonttype.HeaderText = "字体";
            fonttype.MinimumWidth = 8;
            fonttype.Name = "fonttype";
            fonttype.Resizable = DataGridViewTriState.True;
            fonttype.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // posiX
            // 
            posiX.HeaderText = "位置X";
            posiX.MinimumWidth = 8;
            posiX.Name = "posiX";
            // 
            // posiY
            // 
            posiY.HeaderText = "位置Y";
            posiY.MinimumWidth = 8;
            posiY.Name = "posiY";
            // 
            // size
            // 
            size.HeaderText = "大小";
            size.MinimumWidth = 8;
            size.Name = "size";
            // 
            // loadButton
            // 
            loadButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            loadButton.Location = new Point(630, 77);
            loadButton.Name = "loadButton";
            loadButton.Size = new Size(112, 34);
            loadButton.TabIndex = 1;
            loadButton.Text = "从模板载入";
            loadButton.UseVisualStyleBackColor = true;
            loadButton.Click += loadButton_Click;
            // 
            // saveButton
            // 
            saveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            saveButton.Location = new Point(512, 77);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(112, 34);
            saveButton.TabIndex = 0;
            saveButton.Text = "保存为模板";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Margin = new Padding(16);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(label1);
            splitContainer2.Panel1.Controls.Add(pictureBox1);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(downButton);
            splitContainer2.Panel2.Controls.Add(pictureBox2);
            splitContainer2.Size = new Size(741, 999);
            splitContainer2.SplitterDistance = 499;
            splitContainer2.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Microsoft YaHei UI", 6F);
            label1.Location = new Point(0, 482);
            label1.Name = "label1";
            label1.Size = new Size(12, 17);
            label1.TabIndex = 3;
            label1.Text = " ";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.ControlDark;
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = Properties.Resources._1353834;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(741, 499);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            pictureBox1.DoubleClick += pictureBox1_DoubleClick;
            pictureBox1.MouseClick += pictureBox1_MouseClick;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            // 
            // downButton
            // 
            downButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            downButton.Location = new Point(617, 450);
            downButton.Name = "downButton";
            downButton.Size = new Size(112, 34);
            downButton.TabIndex = 2;
            downButton.Text = "导出图片";
            downButton.UseVisualStyleBackColor = true;
            downButton.Click += downButton_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Dock = DockStyle.Fill;
            pictureBox2.Location = new Point(0, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(741, 496);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1502, 999);
            Controls.Add(splitContainer1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "图片批量水印工具";
            Load += Form1_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }


        #endregion

        private SplitContainer splitContainer1;
        private PictureBox pictureBox1;
        private SplitContainer splitContainer2;
        private SplitContainer splitContainer3;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn text;
        private DataGridViewComboBoxColumn fonttype;
        private DataGridViewTextBoxColumn posiX;
        private DataGridViewTextBoxColumn posiY;
        private DataGridViewTextBoxColumn size;
        private Label label1;
        private Button loadButton;
        private Button saveButton;
        private Button downButton;
        private PictureBox pictureBox2;
    }
}
