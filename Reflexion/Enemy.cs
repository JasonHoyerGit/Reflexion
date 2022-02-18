using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Reflexion
{
    class Enemy
    {
        Random rnd = new Random();
        bool moveRight;
        bool moveLeft;
        bool moveUp;
        bool moveDown;
        public Enemy(int movingSpeed, Rectangle recEnemy, Size ClientSize)
        {
            MovingSpeed = movingSpeed;
            RecEnemy = recEnemy;

            int i = rnd.Next(1, 4);
            if (i==1)
            {
                moveRight = true;
                RecEnemy.X = 0 - RecEnemy.Width;
            }
            else if (i ==2)
            {
                moveLeft = true;
                RecEnemy.X = ClientSize.Width + RecEnemy.Width;
            }
            else if (i == 3)
            {
                moveUp = true;
                RecEnemy.Y = ClientSize.Height + RecEnemy.Height;
            }
            else if (i == 4)
            {
                moveDown = true;
                RecEnemy.Y = 0 - RecEnemy.Height;
            }
        }
        public int MovingSpeed;
        public Rectangle RecEnemy;
        public void Move()
        {
            if (moveRight)
            {
                RecEnemy.X += MovingSpeed;
            }
            if (moveLeft)
            {
                RecEnemy.X -= MovingSpeed;
            }
            if (moveUp)
            {
                RecEnemy.Y -= MovingSpeed;
            }
            if (moveDown)
            {
                RecEnemy.Y += MovingSpeed;
            }
        }
    }
}
