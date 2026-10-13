using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LAB02_25520048_NT106._1
{
    public partial class Bai6 : Form
    {
        private TreeView tvDirectory;
        private RichTextBox rtbContent;
        private Button btnSelectFolder;

        public Bai6()
        {
            TaoGiaoDienTuDong();
        }

        private void TaoGiaoDienTuDong()
        {
            this.Text = "Bài 06 - Duyệt Cây Thư Mục & Đọc File";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            btnSelectFolder = new Button();
            btnSelectFolder.Text = "CHỌN THƯ MỤC";
            btnSelectFolder.Size = new Size(180, 35);
            btnSelectFolder.Location = new Point(20, 15);
            btnSelectFolder.Font = new Font("Arial", 9, FontStyle.Bold);
            btnSelectFolder.Click += BtnSelectFolder_Click;
            this.Controls.Add(btnSelectFolder);

            tvDirectory = new TreeView();
            tvDirectory.Location = new Point(20, 60);
            tvDirectory.Size = new Size(280, 380);
            tvDirectory.NodeMouseClick += TvDirectory_NodeMouseClick;
            this.Controls.Add(tvDirectory);

            rtbContent = new RichTextBox();
            rtbContent.Location = new Point(315, 60);
            rtbContent.Size = new Size(445, 380);
            rtbContent.Font = new Font("Segoe UI", 10);
            rtbContent.ReadOnly = true;
            this.Controls.Add(rtbContent);
        }

        private void BtnSelectFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    tvDirectory.Nodes.Clear();
                    DirectoryInfo rootDir = new DirectoryInfo(fbd.SelectedPath);
                    TreeNode rootNode = new TreeNode(rootDir.Name) { Tag = rootDir.FullName };
                    tvDirectory.Nodes.Add(rootNode);
                    LoadSubDirectoriesAndFiles(rootDir, rootNode);
                    rootNode.Expand();
                }
            }
        }

        private void LoadSubDirectoriesAndFiles(DirectoryInfo dir, TreeNode parentNode)
        {
            try
            {
                foreach (DirectoryInfo subDir in dir.GetDirectories())
                {
                    TreeNode subNode = new TreeNode(subDir.Name) { Tag = subDir.FullName };
                    parentNode.Nodes.Add(subNode);
                    LoadSubDirectoriesAndFiles(subDir, subNode);
                }

                foreach (FileInfo file in dir.GetFiles())
                {
                    TreeNode fileNode = new TreeNode(file.Name) { Tag = file.FullName };
                    parentNode.Nodes.Add(fileNode);
                }
            }
            catch { }
        }

        private void TvDirectory_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string path = e.Node.Tag as string;
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                try
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        rtbContent.Text = sr.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    rtbContent.Text = "Không thể đọc file: " + ex.Message;
                }
            }
        }
    }
}