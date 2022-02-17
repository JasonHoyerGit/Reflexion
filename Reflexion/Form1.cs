using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Reflexion
{
    public partial class Form1 : Form
    {
        Rectangle spieler;
        int anteilRechteckDimension = 10;
        List<Rectangle> alleZiele = new List<Rectangle>();
        Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            ResizeRedraw = true;

            InitialisiereRechteck();

            timerGame.Interval = 100;
            timerGame.Start();
        }

        private void InitialisiereRechteck()
        {
            spieler = new Rectangle();
            spieler.Width = ClientSize.Width / anteilRechteckDimension;
            spieler.Height = ClientSize.Height / anteilRechteckDimension;
            spieler.X = ClientSize.Width / 2 - spieler.Width / 2;
            spieler.Y = ClientSize.Height / 2 - spieler.Height / 2;
        }

        private void FrmReflexion_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            Brush brush = new SolidBrush(Color.Orange);
            Pen pen = new Pen(Color.Black);

            graphics.FillRectangle(brush, spieler);
            graphics.DrawRectangle(pen, spieler);

            foreach (Rectangle rectangle in alleZiele)
            {
                graphics.FillRectangle(new SolidBrush(Color.Red), rectangle);
            }

        }

        private void timerGame_Tick(object sender, System.EventArgs e)
        {
            spawnTargets();
        }

        int spawnRate = 14;
        int spawnZaehler = 0;
        private void spawnTargets()
        {
            if (spawnZaehler >= spawnRate)
            {
                Rectangle rectangle = new Rectangle();
                do
                {
                    rectangle.X = random.Next(0, ClientSize.Width);
                    rectangle.Y = random.Next(0, ClientSize.Height);
                } while (rectangle.IntersectsWith(spieler));

                rectangle.Width = ClientSize.Width / 15;
                rectangle.Height = rectangle.Width;

                alleZiele.Add(rectangle);
                spawnZaehler = 0;
            }
            else
            {
                spawnZaehler++;
            }
        }

        int schrittweite = 10;
        private void FrmReflexion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                spieler.Y -= schrittweite;
            }
            if (e.KeyCode == Keys.Down)
            {
                spieler.Y += schrittweite;
            }
            if (e.KeyCode == Keys.Left)
            {
                spieler.X -= schrittweite;
            }
            if (e.KeyCode == Keys.Right)
            {
                spieler.X += schrittweite;
            }

            Refresh();
        }
    }
}
