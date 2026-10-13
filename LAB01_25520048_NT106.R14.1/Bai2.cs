using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LAB02_25520048_NT106._1
{
    public partial class Bai2 : Form
    {
        private Button btnRead;
        private TextBox txtFileName;
        private TextBox txtSize;
        private TextBox txtURL;
        private TextBox txtLineCount;
        private TextBox txtWordCount;
        private TextBox txtCharCount;
        private RichTextBox rtbContent;

        public Bai2()
        {
            TaoGiaoDienTuDong();
        }

        private void TaoGiaoDienTuDong()
        {
            this.Text = "Bài 02 - Thông tin File";
            this.Size = new Size(720, 480);
            this.StartPosition = FormStartPosition.CenterScreen;

            btnRead = new Button();
            btnRead.Text = "ĐỌC FILE";
            btnRead.Size = new Size(240, 40);
            btnRead.Location = new Point(20, 20);
            btnRead.Font = new Font("Arial", 10, FontStyle.Bold);
            btnRead.Click += BtnRead_Click;
            this.Controls.Add(btnRead);

            string[] labels = { "Tên file", "Kích thước", "Đường dẫn", "Số dòng", "Số từ", "Số ký tự" };
            TextBox[] textBoxes = new TextBox[6];

            for (int i = 0; i < labels.Length; i++)
            {
                Label lbl = new Label();
                lbl.Text = labels[i];
                lbl.Location = new Point(20, 80 + i * 40);
                lbl.Size = new Size(80, 25);
                lbl.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                this.Controls.Add(lbl);

                TextBox txt = new TextBox();
                txt.Location = new Point(105, 77 + i * 40);
                txt.Size = new Size(155, 25);
                txt.ReadOnly = true;
                this.Controls.Add(txt);

                textBoxes[i] = txt;
            }

            txtFileName = textBoxes[0];
            txtSize = textBoxes[1];
            txtURL = textBoxes[2];
            txtLineCount = textBoxes[3];
            txtWordCount = textBoxes[4];
            txtCharCount = textBoxes[5];

            rtbContent = new RichTextBox();
            rtbContent.Location = new Point(280, 20);
            rtbContent.Size = new Size(400, 400);
            rtbContent.Font = new Font("Segoe UI", 10);
            rtbContent.ReadOnly = true;
            this.Controls.Add(rtbContent);
        }

        private void BtnRead_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileInfo fileInfo = new FileInfo(ofd.FileName);
                txtFileName.Text = fileInfo.Name;
                txtSize.Text = $"{fileInfo.Length} Bytes";
                txtURL.Text = fileInfo.FullName;

                using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                using (StreamReader sr = new StreamReader(fs))
                {
                    string content = sr.ReadToEnd();
                    rtbContent.Text = content;

                    int charCount = content.Length;

                    string[] lines = content.Split('\n');
                    int lineCount = lines.Length;

                    string[] words = content.Split(new char[] { ' ', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    int wordCount = words.Length;

                    txtLineCount.Text = lineCount.ToString();
                    txtWordCount.Text = wordCount.ToString();
                    txtCharCount.Text = charCount.ToString();
                }
            }
        }
    }
}