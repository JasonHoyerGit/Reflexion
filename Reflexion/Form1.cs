using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Reflexion
{
    public partial class Form1 : Form
    {
        private int ZieleBreite;
        Rectangle spieler;
        int anteilRechteckDimension = 10;
        List<Rectangle> alleZiele = new List<Rectangle>();
        Random random = new Random();
        bool moveLeft;
        bool moveRight;
        bool moveUp;
        bool moveDown;
        int movingSpeed = 10;

        public Form1()
        {
            InitializeComponent();
            ResizeRedraw = true;
            DoubleBuffered = true;

            ZieleBreite = ClientSize.Width / 15;

            InitialisiereRechteck();

            while (alleZiele.Count <= 3)
            {
                spawnTargets();
            }

            timerGame.Interval = 10;
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

            for (int i = 0; i < alleZiele.Count; i++)
            {
                graphics.FillRectangle(new SolidBrush(Color.Red), alleZiele[i]);
            }
                
        }

        int spawnRate = 100;
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

                rectangle.Width = ZieleBreite;
                rectangle.Height = ZieleBreite;

                alleZiele.Add(rectangle);
                spawnZaehler = 0;
            }
            else
            {
                spawnZaehler++;
            }
        }
        private void FrmReflexion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
                moveDown = true;
            }
            else if (e.KeyCode == Keys.W)
            {
                moveUp = true;
            }
            else if (e.KeyCode == Keys.D)
            {
                moveRight = true;
            }
            else if (e.KeyCode == Keys.A)
            {
                moveLeft = true;
            }
        }
        private void playerMovement()
        {
            if (moveDown)
            {
                spieler.Y += movingSpeed;
            }
            if (moveUp)
            {
                spieler.Y -= movingSpeed;
            }
            if (moveRight)
            {
                spieler.X += movingSpeed;
            }
            if (moveLeft)
            {
                spieler.X -= movingSpeed;
            }
        }

        private void timerGame_Tick(object sender, EventArgs e)
        {
            spawnTargets();
            playerMovement();
            kollisionMitSpieler();
            Refresh();
        }

        private void kollisionMitSpieler()
        {
            foreach (Rectangle rectangle in alleZiele.ToList())
            {
                if (spieler.IntersectsWith(rectangle))
                {
                    alleZiele.Remove(rectangle);
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
                moveDown = false;
            }
            if (e.KeyCode == Keys.W)
            {
                moveUp = false;
            }
            if (e.KeyCode == Keys.D)
            {
                moveRight = false;
            }
            if (e.KeyCode == Keys.A)
            {
                moveLeft = false;
            }
        }
    }
}
