using LAB01_25520048_NT106.R14._1;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LAB02_25520048_NT106._1
{
    public partial class Mainform : Form
    {
        public Mainform()
        {
            InitializeComponent();
            TaoNutTuDong();
        }

        private void TaoNutTuDong()
        {
            this.Text = "Lab 2 - Menu Chính";
            this.Size = new Size(350, 480);
            this.StartPosition = FormStartPosition.CenterScreen;

            for (int i = 1; i <= 7; i++)
            {
                Button btn = new Button();
                btn.Text = $"Bài 0{i}";
                btn.Size = new Size(200, 40);
                btn.Location = new Point(60, 25 + (i - 1) * 55);

                int baiIndex = i;

                btn.Click += (sender, e) =>
                {
                    Form formToOpen = null;
                    switch (baiIndex)
                    {
                        case 1: formToOpen = new Bai1(); break;
                        case 2: formToOpen = new Bai2(); break;
                        case 3: formToOpen = new Bai3(); break;
                        case 4: formToOpen = new Bai4(); break;
                        case 5: formToOpen = new Bai5(); break;
                        case 6: formToOpen = new Bai6(); break;
                        case 7: formToOpen = new Bai7(); break;
                    }
                    formToOpen?.ShowDialog();
                };

                this.Controls.Add(btn);
            }
        }
    }
}