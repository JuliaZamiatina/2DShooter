using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace _2DShooter
{
    class Enemy
    {
        public double currentAngle = Math.PI;
        public Vector direction = new Vector(1, 0);
        private Timer tm = new Timer();

        public Drawing dr = new Drawing();
        public Collisions enemyCollisions;
        private static Point startLocation = new Point(12, 205);
        public PictureBox enemyPB;
        private PictureBox playerPB;
        public List<Bullet> allBullets = new List<Bullet>();

        Vector toPlayer;

        private Form1 form;

        public Enemy(PictureBox enemyPictureBox, PictureBox playerPictureBox, Collisions collisions, Form1 parentForm)
        {
            enemyPB = enemyPictureBox;
            enemyCollisions = collisions;
            playerPB = playerPictureBox;
            form = parentForm;

            tm.Interval = 1500;
            tm.Tick += new EventHandler(Tm_Tick);
            tm.Start();
        }

        private void Tm_Tick(object sender, EventArgs e)
        {
            if (PlayerIsAhead())
                Shoot(form);
        }

        public void EnemyMove(Form1 parentForm)
        {
            if (!PlayerIsAhead())
            {
                RotateToPlayer();
            }
            MoveToPlayer();
        }

        public void GoToStart()
        {
            enemyPB.Location = startLocation;
            currentAngle = 0;
            dr.DrawTheEnemy(enemyPB, currentAngle);
            direction.X = 1;
            direction.Y = 0;
        }

        private bool PlayerIsAhead()
        {
            toPlayer = DirectionToPlayer();
            if ((Math.Round(toPlayer.X,1) == Math.Round(direction.X,1)) && (Math.Round(toPlayer.Y,1) == Math.Round(direction.Y,1)))
                return true;
            return false;
        }

        public void RotateToPlayer()
        {            
            double sin = direction.X * toPlayer.Y - toPlayer.X * direction.Y;
            if (sin > 0)
                RotateRight();
            else
                RotateLeft();
        }

        public void RotateLeft()
        {
            currentAngle--;
            if (currentAngle == -360) currentAngle = 0;
            direction.Rotate(-Math.PI / 180);
            dr.DrawTheEnemy(enemyPB, currentAngle);
        }

        public void RotateRight()
        {
            currentAngle++;
            if (currentAngle == 360) currentAngle = 0;
            direction.Rotate(Math.PI/180);
            dr.DrawTheEnemy(enemyPB, currentAngle);
        }

        public void MoveToPlayer()
        {
            if (enemyCollisions.WallsCollision(enemyPB))
            {
                GoToStart();
                return;
            }
            enemyPB.Left += (int)Math.Round(direction.X * 2);
            enemyPB.Top += (int)Math.Round(direction.Y * 2);
        }
        public Vector DirectionToPlayer()
        {
            double X1 = enemyPB.Location.X + (enemyPB.Width / 2);
            double X2 = playerPB.Location.X + (playerPB.Width / 2);
            double Y1 = enemyPB.Location.Y + (enemyPB.Height / 2);
            double Y2 = playerPB.Location.Y + (playerPB.Height / 2);
            Vector temp = new Vector(X2 - X1, Y2 - Y1);
            temp.Normalize();
            return temp;
        }
        public void Shoot(Form1 form)
        {
            Bullet shot = new Bullet(form, enemyPB.Left, enemyPB.Top, enemyCollisions, direction);
            allBullets.Add(shot);
            shot.FlyingBullet(this);
        }
    }
}

