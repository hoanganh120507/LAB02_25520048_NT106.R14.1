using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LAB02_25520048_NT106._1
{
    public partial class Bai7 : Form
    {
        private Button btnSelectFolder;
        private TreeView tvExplorer;
        private RichTextBox rtbText;
        private PictureBox pbImage;

        public Bai7()
        {
            TaoGiaoDienTuDong();
        }

        private void TaoGiaoDienTuDong()
        {
            this.Text = "Bài 07 - Quản lý & Xem trước File";
            this.Size = new Size(850, 520);
            this.StartPosition = FormStartPosition.CenterScreen;

            btnSelectFolder = new Button();
            btnSelectFolder.Text = "CHỌN THƯ MỤC";
            btnSelectFolder.Size = new Size(180, 35);
            btnSelectFolder.Location = new Point(20, 15);
            btnSelectFolder.Font = new Font("Arial", 9, FontStyle.Bold);
            btnSelectFolder.Click += BtnSelectFolder_Click;
            this.Controls.Add(btnSelectFolder);

            tvExplorer = new TreeView();
            tvExplorer.Location = new Point(20, 60);
            tvExplorer.Size = new Size(280, 400);
            tvExplorer.NodeMouseClick += TvExplorer_NodeMouseClick;
            this.Controls.Add(tvExplorer);

            rtbText = new RichTextBox();
            rtbText.Location = new Point(315, 60);
            rtbText.Size = new Size(495, 400);
            rtbText.Font = new Font("Segoe UI", 10);
            rtbText.ReadOnly = true;
            this.Controls.Add(rtbText);

            pbImage = new PictureBox();
            pbImage.Location = new Point(315, 60);
            pbImage.Size = new Size(495, 400);
            pbImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbImage.BorderStyle = BorderStyle.FixedSingle;
            pbImage.Visible = false;
            this.Controls.Add(pbImage);
        }

        private void BtnSelectFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    tvExplorer.Nodes.Clear();
                    DirectoryInfo rootDir = new DirectoryInfo(fbd.SelectedPath);
                    TreeNode rootNode = new TreeNode(rootDir.Name) { Tag = rootDir.FullName };
                    tvExplorer.Nodes.Add(rootNode);
                    LoadSubNodes(rootDir, rootNode);
                    rootNode.Expand();
                }
            }
        }

        private void LoadSubNodes(DirectoryInfo dir, TreeNode parentNode)
        {
            try
            {
                foreach (DirectoryInfo subDir in dir.GetDirectories())
                {
                    TreeNode subNode = new TreeNode(subDir.Name) { Tag = subDir.FullName };
                    parentNode.Nodes.Add(subNode);
                    LoadSubNodes(subDir, subNode);
                }

                foreach (FileInfo file in dir.GetFiles())
                {
                    TreeNode fileNode = new TreeNode(file.Name) { Tag = file.FullName };
                    parentNode.Nodes.Add(fileNode);
                }
            }
            catch { }
        }

        private void TvExplorer_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string path = e.Node.Tag as string;
            if (string.IsNullOrEmpty(path) || !File.Exists(path)) return;

            string ext = Path.GetExtension(path).ToLower();

            if (ext == ".jpg" || ext == ".png" || ext == ".jpeg" || ext == ".bmp" || ext == ".gif")
            {
                try
                {
                    rtbText.Visible = false;
                    if (pbImage.Image != null)
                    {
                        pbImage.Image.Dispose();
                    }
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        pbImage.Image = Image.FromStream(fs);
                    }
                    pbImage.Visible = true;
                }
                catch (Exception ex)
                {
                    pbImage.Visible = false;
                    rtbText.Visible = true;
                    rtbText.Text = "Lỗi đọc file ảnh: " + ex.Message;
                }
            }
            else
            {
                pbImage.Visible = false;
                rtbText.Visible = true;
                try
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        rtbText.Text = sr.ReadToEnd();
                    }
                }
                catch
                {
                    FileInfo fi = new FileInfo(path);
                    rtbText.Text = $"Tên file: {fi.Name}\nĐường dẫn: {fi.FullName}\nKích thước: {fi.Length} bytes\nNgày tạo: {fi.CreationTime}";
                }
            }
        }
    }
}