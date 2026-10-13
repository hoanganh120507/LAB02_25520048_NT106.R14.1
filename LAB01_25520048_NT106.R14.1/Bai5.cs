using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LAB02_25520048_NT106._1
{
    public partial class Bai5 : Form
    {
        private ComboBox cbPhim;
        private ComboBox cbPhongChieu;
        private TextBox txtHoTen;
        private Label lblTongTien;
        private Button btnDatVe;
        private Button btnXuatFile;
        private List<CheckBox> listGhe = new List<CheckBox>();
        private Dictionary<string, int> giaGhe = new Dictionary<string, int>();

        public Bai5()
        {
            TaoGiaoDienTuDong();
        }

        private void TaoGiaoDienTuDong()
        {
            this.Text = "Bài 05 - Đặt Vé Xem Phim";
            this.Size = new Size(650, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblPhim = new Label { Text = "Chọn Phim:", Location = new Point(30, 20), AutoSize = true };
            this.Controls.Add(lblPhim);

            cbPhim = new ComboBox { Location = new Point(120, 17), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            cbPhim.Items.AddRange(new string[] { "Đào, Phở và Piano", "Mai", "Gặp Lại Chị Bầu" });
            cbPhim.SelectedIndex = 0;
            this.Controls.Add(cbPhim);

            Label lblPhong = new Label { Text = "Phòng Chiếu:", Location = new Point(350, 20), AutoSize = true };
            this.Controls.Add(lblPhong);

            cbPhongChieu = new ComboBox { Location = new Point(440, 17), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            cbPhongChieu.Items.AddRange(new string[] { "Phòng 1", "Phòng 2", "Phòng 3" });
            cbPhongChieu.SelectedIndex = 0;
            this.Controls.Add(cbPhongChieu);

            Label lblTen = new Label { Text = "Họ Tên Khách:", Location = new Point(30, 60), AutoSize = true };
            this.Controls.Add(lblTen);

            txtHoTen = new TextBox { Location = new Point(120, 57), Width = 200 };
            this.Controls.Add(txtHoTen);

            GroupBox gbGhe = new GroupBox { Text = "Sơ Đồ Ghế Ngồi", Location = new Point(30, 100), Size = new Size(560, 220) };
            this.Controls.Add(gbGhe);

            string[] hang = { "A", "B", "C" };
            for (int i = 0; i < 3; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    string tenGhe = $"{hang[i]}{j}";
                    CheckBox chk = new CheckBox
                    {
                        Text = tenGhe,
                        Location = new Point(30 + (j - 1) * 100, 30 + i * 50),
                        Size = new Size(80, 30),
                        Appearance = Appearance.Button,
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    int gia = 50000;
                    if (hang[i] == "B" && (j >= 2 && j <= 4)) gia = 100000;
                    else if (hang[i] == "A" || hang[i] == "C") gia = 25000;

                    giaGhe[tenGhe] = gia;
                    chk.CheckedChanged += Chk_CheckedChanged;
                    listGhe.Add(chk);
                    gbGhe.Controls.Add(chk);
                }
            }

            Label lblTT = new Label { Text = "Tổng Tiền:", Location = new Point(30, 340), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            this.Controls.Add(lblTT);

            lblTongTien = new Label { Text = "0 VNĐ", Location = new Point(120, 340), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.Red };
            this.Controls.Add(lblTongTien);

            btnDatVe = new Button { Text = "ĐẶT VÉ", Location = new Point(30, 380), Size = new Size(260, 45), Font = new Font("Arial", 10, FontStyle.Bold) };
            btnDatVe.Click += BtnDatVe_Click;
            this.Controls.Add(btnDatVe);

            btnXuatFile = new Button { Text = "XUẤT BÁO CÁO", Location = new Point(330, 380), Size = new Size(260, 45), Font = new Font("Arial", 10, FontStyle.Bold) };
            btnXuatFile.Click += BtnXuatFile_Click;
            this.Controls.Add(btnXuatFile);
        }

        private void Chk_CheckedChanged(object sender, EventArgs e)
        {
            TinhTongTien();
        }

        private int TinhTongTien()
        {
            int tong = 0;
            foreach (var chk in listGhe)
            {
                if (chk.Checked && chk.Enabled)
                {
                    tong += giaGhe[chk.Text];
                }
            }
            lblTongTien.Text = $"{tong:N0} VNĐ";
            return tong;
        }

        private void BtnDatVe_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var gheDaChon = listGhe.Where(g => g.Checked && g.Enabled).ToList();
            if (gheDaChon.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một ghế!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int tongTien = TinhTongTien();
            string danhSachGhe = string.Join(", ", gheDaChon.Select(g => g.Text));

            MessageBox.Show($"Đặt vé thành công!\nKhách hàng: {txtHoTen.Text}\nPhim: {cbPhim.SelectedItem}\nPhòng: {cbPhongChieu.SelectedItem}\nGhế: {danhSachGhe}\nTổng tiền: {tongTien:N0} VNĐ", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

            foreach (var chk in gheDaChon)
            {
                chk.Enabled = false;
                chk.BackColor = Color.LightGray;
            }

            txtHoTen.Clear();
            lblTongTien.Text = "0 VNĐ";
        }

        private void BtnXuatFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            sfd.FileName = "output5.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                int soGheDaDat = listGhe.Count(g => !g.Enabled);
                int soGheConLai = listGhe.Count - soGheDaDat;
                double tiLe = (double)soGheDaDat / listGhe.Count * 100;

                using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine($"Phim: {cbPhim.SelectedItem}");
                    sw.WriteLine($"Phòng chiếu: {cbPhongChieu.SelectedItem}");
                    sw.WriteLine($"Số ghế đã đặt: {soGheDaDat}");
                    sw.WriteLine($"Số ghế còn trống: {soGheConLai}");
                    sw.WriteLine($"Tỷ lệ lấp đầy: {tiLe:F2}%");
                }

                MessageBox.Show("Đã xuất báo cáo ra file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}