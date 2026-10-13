using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LAB02_25520048_NT106._1
{
    public partial class Bai4 : Form
    {
        public class SinhVien
        {
            public string MSSV { get; set; }
            public string HoTen { get; set; }
            public string SDT { get; set; }
            public float Diem1 { get; set; }
            public float Diem2 { get; set; }
            public float DTB => (Diem1 + Diem2) / 2;
        }

        private List<SinhVien> dsSinhVien = new List<SinhVien>();
        private List<SinhVien> dsHienThi = new List<SinhVien>();
        private int currentIndex = 0;

        private TextBox txtMSSV, txtHoTen, txtSDT, txtDiem1, txtDiem2, txtDTB;
        private Button btnAdd, btnWrite, btnRead, btnBack, btnNext;
        private Label lblIndex;

        public Bai4()
        {
            TaoGiaoDienTuDong();
        }

        private void TaoGiaoDienTuDong()
        {
            this.Text = "Bài 04 - Quản lý Sinh viên";
            this.Size = new Size(450, 380);
            this.StartPosition = FormStartPosition.CenterScreen;

            string[] labels = { "MSSV", "Họ Tên", "Điện thoại", "Điểm Môn 1", "Điểm Môn 2", "Điểm TB" };
            TextBox[] textBoxes = new TextBox[6];

            for (int i = 0; i < labels.Length; i++)
            {
                Label lbl = new Label();
                lbl.Text = labels[i];
                lbl.Location = new Point(25, 25 + i * 38);
                lbl.Size = new Size(90, 25);
                this.Controls.Add(lbl);

                TextBox txt = new TextBox();
                txt.Location = new Point(120, 22 + i * 38);
                txt.Size = new Size(160, 25);
                if (i == 5) txt.ReadOnly = true;
                this.Controls.Add(txt);

                textBoxes[i] = txt;
            }

            txtMSSV = textBoxes[0];
            txtHoTen = textBoxes[1];
            txtSDT = textBoxes[2];
            txtDiem1 = textBoxes[3];
            txtDiem2 = textBoxes[4];
            txtDTB = textBoxes[5];

            btnAdd = new Button();
            btnAdd.Text = "Thêm SV";
            btnAdd.Location = new Point(300, 20);
            btnAdd.Size = new Size(110, 30);
            btnAdd.Click += BtnAdd_Click;
            this.Controls.Add(btnAdd);

            btnWrite = new Button();
            btnWrite.Text = "Ghi File";
            btnWrite.Location = new Point(300, 60);
            btnWrite.Size = new Size(110, 30);
            btnWrite.Click += BtnWrite_Click;
            this.Controls.Add(btnWrite);

            btnRead = new Button();
            btnRead.Text = "Đọc File";
            btnRead.Location = new Point(300, 100);
            btnRead.Size = new Size(110, 30);
            btnRead.Click += BtnRead_Click;
            this.Controls.Add(btnRead);

            btnBack = new Button();
            btnBack.Text = "Back";
            btnBack.Location = new Point(50, 275);
            btnBack.Size = new Size(70, 30);
            btnBack.Click += BtnBack_Click;
            this.Controls.Add(btnBack);

            lblIndex = new Label();
            lblIndex.Text = "0/0";
            lblIndex.Location = new Point(160, 280);
            lblIndex.Size = new Size(80, 25);
            lblIndex.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblIndex);

            btnNext = new Button();
            btnNext.Text = "Next";
            btnNext.Location = new Point(270, 275);
            btnNext.Size = new Size(70, 30);
            btnNext.Click += BtnNext_Click;
            this.Controls.Add(btnNext);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                SinhVien sv = new SinhVien
                {
                    MSSV = txtMSSV.Text,
                    HoTen = txtHoTen.Text,
                    SDT = txtSDT.Text,
                    Diem1 = float.Parse(txtDiem1.Text),
                    Diem2 = float.Parse(txtDiem2.Text)
                };

                dsSinhVien.Add(sv);
                txtDTB.Text = sv.DTB.ToString("0.00");
                MessageBox.Show("Đã thêm sinh viên vào danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng điểm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnWrite_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            sfd.FileName = "input4.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    foreach (var sv in dsSinhVien)
                    {
                        sw.WriteLine($"{sv.MSSV};{sv.HoTen};{sv.SDT};{sv.Diem1};{sv.Diem2}");
                    }
                }
                MessageBox.Show("Đã ghi file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnRead_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                dsHienThi.Clear();
                using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                using (StreamReader sr = new StreamReader(fs))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;
                        string[] parts = line.Split(';');
                        if (parts.Length >= 5)
                        {
                            SinhVien sv = new SinhVien
                            {
                                MSSV = parts[0],
                                HoTen = parts[1],
                                SDT = parts[2],
                                Diem1 = float.Parse(parts[3]),
                                Diem2 = float.Parse(parts[4])
                            };
                            dsHienThi.Add(sv);
                        }
                    }
                }

                if (dsHienThi.Count > 0)
                {
                    currentIndex = 0;
                    HienThiSinhVien(currentIndex);
                }
            }
        }

        private void HienThiSinhVien(int index)
        {
            if (index >= 0 && index < dsHienThi.Count)
            {
                var sv = dsHienThi[index];
                txtMSSV.Text = sv.MSSV;
                txtHoTen.Text = sv.HoTen;
                txtSDT.Text = sv.SDT;
                txtDiem1.Text = sv.Diem1.ToString();
                txtDiem2.Text = sv.Diem2.ToString();
                txtDTB.Text = sv.DTB.ToString("0.00");
                lblIndex.Text = $"{index + 1}/{dsHienThi.Count}";
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (dsHienThi.Count > 0 && currentIndex > 0)
            {
                currentIndex--;
                HienThiSinhVien(currentIndex);
            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (dsHienThi.Count > 0 && currentIndex < dsHienThi.Count - 1)
            {
                currentIndex++;
                HienThiSinhVien(currentIndex);
            }
        }

        private void ClearInputs()
        {
            txtMSSV.Clear();
            txtHoTen.Clear();
            txtSDT.Clear();
            txtDiem1.Clear();
            txtDiem2.Clear();
        }
    }
}