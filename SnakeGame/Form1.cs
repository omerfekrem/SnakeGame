using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Yilan yilanimiz;
        Yon yonumuz;
        PictureBox[] pb_YilanParcalari;
        bool yemVarmi = false;
        Random r = new Random();
        PictureBox pb_Yem = new PictureBox();
        int skor = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            YeniOyun();
        }
        private void YeniOyun()
        {
            yilanimiz = new Yilan();
            yonumuz = new Yon(-10, 0);
            pb_YilanParcalari = new PictureBox[0];
            for (int i = 0; i < 3; i++)
            {
                Array.Resize(ref pb_YilanParcalari, pb_YilanParcalari.Length + 1);
                pb_YilanParcalari[i] = pb_Ekle();
            }
            timer1.Start();
            btnRestart.Enabled = false;
        }
        private PictureBox pb_Ekle()
        {
            PictureBox pb = new PictureBox();
            pb.Size = new Size(10, 10);
            pb.BackColor = Color.White;
            pb.Location = yilanimiz.GetPos(pb_YilanParcalari.Length-1);
            snakeGamePanel.Controls.Add(pb);
            return pb;
        }
        private void pb_Guncelle()
        {
            for (int i = 0; i < pb_YilanParcalari.Length; i++)
            {
                pb_YilanParcalari[i].Location = yilanimiz.GetPos(i);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                if (yonumuz._y != 10)
                {
                    yonumuz = new Yon(0, -10);
                }
            }

            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                if (yonumuz._y != -10)
                {
                    yonumuz = new Yon(0, 10);
                }
            }

            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                if (yonumuz._x != 10)
                {
                    yonumuz = new Yon(-10, 0);
                }

            }

            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                if (yonumuz._x != -10)
                {
                    yonumuz = new Yon(10, 0);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblPuan.Text = "Puan : " + skor.ToString();
            yilanimiz.Ilerle(yonumuz);
            pb_Guncelle();
            YemOlustur();
            YemYedimi();
            YilanKendineCarpti();
            DuvarlaraCarpti();
        }
        public void YemOlustur()
        {
            if (!yemVarmi)
            {
                PictureBox pb = new PictureBox();
                pb.BackColor = Color.Red;
                pb.Size = new Size(10, 10);
                pb.Location = new Point(r.Next(snakeGamePanel.Width / 10) * 10, r.Next(snakeGamePanel.Height / 10) * 10);
                pb_Yem = pb;
                yemVarmi = true;
                snakeGamePanel.Controls.Add(pb);
            }
        }

        public void YemYedimi()
        {
            if(yilanimiz.GetPos(0) == pb_Yem.Location)
            {
                skor += 10;
                yilanimiz.Buyu();
                Array.Resize(ref pb_YilanParcalari, pb_YilanParcalari.Length + 1);
                pb_YilanParcalari[pb_YilanParcalari.Length - 1] = pb_Ekle();
                yemVarmi = false;
                snakeGamePanel.Controls.Remove(pb_Yem);
            }
        }

        public void YilanKendineCarpti()
        {
            for (int i = 1; i < yilanimiz.YilanBuyuklugu; i++)
            {
                if (yilanimiz.GetPos(0) == yilanimiz.GetPos(i))
                {
                    Yenildi();
                }
            }
        }

        public void DuvarlaraCarpti()
        {
            Point p = yilanimiz.GetPos(0);
            if (p.X<0 || p.X>snakeGamePanel.Width-10||p.Y<0||p.Y>snakeGamePanel.Height-10)
            {
                Yenildi();
            }
        }

        private void Yenildi()
        {
            timer1.Stop();
            MessageBox.Show("Oyun Bitti Kaybettiniz. \n\n Puanınız : " + skor.ToString(), "Oyun Bitti", MessageBoxButtons.OK, MessageBoxIcon.Information);
            skor = 0;
            btnRestart.Enabled = true;
            yemVarmi = false;
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            lblPuan.Text = "Puan : 0";
            snakeGamePanel.Controls.Clear();
            YeniOyun();
        }
    }
}
