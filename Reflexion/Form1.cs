using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Reflexion.Properties;

namespace Reflexion
{
    public partial class Form1 : Form
    {
        private int ZieleBreite;
        Rectangle spieler;
        int anteilRechteckDimension = 10;
        List<Rectangle> alleZiele = new List<Rectangle>();
        List<Rectangle> alleFeinde = new List<Rectangle>();
        Random random = new Random();
        bool moveLeft;
        bool moveRight;
        bool moveUp;
        bool moveDown;
        int movingSpeed = 10;
        Bitmap obst;
        int anzahlObstKonsumiert = 0;

        public Form1()
        {
            InitializeComponent();
            ResizeRedraw = true;
            DoubleBuffered = true;

            ZieleBreite = ClientSize.Width / 15;

            InitialisiereRechteck();

            obst = Resources.Obst;

            while (alleZiele.Count < 3)
            {
                spawnTarget();
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
                graphics.DrawImage(obst, alleZiele[i]);
            }

            graphics.DrawString("Obst Konsumiert: " + anzahlObstKonsumiert, new Font("Arial", 12), new SolidBrush(Color.Black), 0,0);
        }

        private void spawnTarget()
        {
            Rectangle rectangle = zufaelligesRechteck();

            alleZiele.Add(rectangle);
        }

        private Rectangle zufaelligesRechteck()
        {
            Rectangle rectangle = new Rectangle();
            do
            {
                rectangle.X = random.Next(0, ClientSize.Width - ZieleBreite);
                rectangle.Y = random.Next(0, ClientSize.Height - ZieleBreite);
            } while (rectangle.IntersectsWith(spieler));

            rectangle.Width = ZieleBreite;
            rectangle.Height = ZieleBreite;
            return rectangle;
        }
        int spawnRate = 14;
        int spawnZaehler = 0;
        private void spawnEnemies()
        {
            if (spawnZaehler >= spawnRate)
            {
                alleFeinde.Add(zufaelligesRechteck());
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
                if (spieler.Y + spieler.Height > ClientSize.Height)
                {
                    spieler.Y = ClientSize.Height - spieler.Height;
                }
            }
            if (moveUp)
            {
                spieler.Y -= movingSpeed;
                if (spieler.Y < 0)
                {
                    spieler.Y = 0;
                }
            }
            if (moveRight)
            {
                spieler.X += movingSpeed;
                if (spieler.X + spieler.Width > ClientSize.Width)
                {
                    spieler.X = ClientSize.Width - spieler.Width;
                }
            }
            if (moveLeft)
            {
                spieler.X -= movingSpeed;
                if (spieler.X < 0)
                {
                    spieler.X = 0;
                }
            }
        }

        private void timerGame_Tick(object sender, EventArgs e)
        {
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
                    spawnTarget();
                    anzahlObstKonsumiert++;
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
