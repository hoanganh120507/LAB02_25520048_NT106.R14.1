using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LAB02_25520048_NT106._1
{
    public partial class Bai1 : Form
    {
        private Button btnRead;
        private Button btnWrite;
        private RichTextBox rtbContent;

        public Bai1()
        {
            //InitializeComponent();
            TaoGiaoDienTuDong();
        }

        private void TaoGiaoDienTuDong()
        {
            this.Text = "Bài 01 - Ghi và Đọc file";
            this.Size = new Size(600, 450);
            this.StartPosition = FormStartPosition.CenterScreen;

 
            btnRead = new Button();
            btnRead.Text = "ĐỌC FILE";
            btnRead.Size = new Size(150, 45);
            btnRead.Location = new Point(25, 30);
            btnRead.Font = new Font("Arial", 10, FontStyle.Bold);
            btnRead.Click += BtnRead_Click;
            this.Controls.Add(btnRead);

          
            btnWrite = new Button();
            btnWrite.Text = "GHI FILE";
            btnWrite.Size = new Size(150, 45);
            btnWrite.Location = new Point(25, 95);
            btnWrite.Font = new Font("Arial", 10, FontStyle.Bold);
            btnWrite.Click += BtnWrite_Click;
            this.Controls.Add(btnWrite);

         
            rtbContent = new RichTextBox();
            rtbContent.Location = new Point(200, 30);
            rtbContent.Size = new Size(365, 350);
            rtbContent.Font = new Font("Segoe UI", 10);
            this.Controls.Add(rtbContent);
        }

     
        private void BtnRead_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                using (StreamReader sr = new StreamReader(fs))
                {
                    rtbContent.Text = sr.ReadToEnd(); 
                }
            }
        }

      
        private void BtnWrite_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            sfd.FileName = "output1.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
              
                string contentUpper = rtbContent.Text.ToUpper();

                using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(contentUpper);
                }

                MessageBox.Show("Đã ghi file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Bai1_Load(object sender, EventArgs e)
        {

        }
    }
}