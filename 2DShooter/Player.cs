using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace _2DShooter
{
    class Player
    { 
        public bool moveForward;
        public bool moveBackward;

        public bool rotateLeft;
        public bool rotateRight;

        public float currentAngle;

        public Vector direction = new Vector(-1,0);

        public Drawing dr = new Drawing();

        public Collisions playerCollisions;

        private static Point startLocation = new Point(742, 205);
        public PictureBox playerPB;

        public List<Bullet> allBullets= new List<Bullet>();

        public Player() { }
        public Player(PictureBox playerPictureBox, Collisions collisions)
        {
            playerPB = playerPictureBox;
            playerCollisions = collisions;
        }

        public void PlayerMove(Form1 parentForm)
        {
            if (moveForward)
            {
                MoveForward(parentForm);
            }
            if (moveBackward)
            {
                MoveBackward(parentForm);
            }
            if (rotateLeft)
            {
                RotateLeft();
            }
            if (rotateRight)
            {
                RotateRight();
            }
        }

        public void GoToStart()
        {
            playerPB.Location = startLocation;
            currentAngle = 0;
            dr.DrawThePlayer(playerPB, currentAngle);
            direction.X = -1;
            direction.Y = 0;
        }

        public void MoveForward(Form1 form)
        {
            if (playerCollisions.FormCollision(form, playerPB.Left + (int)Math.Round(direction.X * 5), playerPB.Top + (int)Math.Round(direction.Y * 5),40))
                return;
            if (playerCollisions.WallsCollision(playerPB))
            {
                GoToStart();
                return;
            }
            playerPB.Left += (int)Math.Round(direction.X * 5);
            playerPB.Top += (int)Math.Round(direction.Y * 5);
        }

        public void MoveBackward(Form1 form)
        {
            if (playerCollisions.FormCollision(form, playerPB.Left - (int)Math.Round(direction.X * 5), playerPB.Top - (int)Math.Round(direction.X * 5), 40))
                return;
            if ((playerCollisions.WallsCollision(playerPB)) || (playerCollisions.EnemyCollision(playerPB)))
            {
                GoToStart();
                return;
            }

            playerPB.Left -= (int)Math.Round(direction.X * 5);
            playerPB.Top -= (int)Math.Round(direction.Y * 5);
        }

        public void RotateLeft()
        {
            currentAngle-=3;
            if (currentAngle == -360) currentAngle = 0;
            direction.Rotate(-3*Math.PI/180);
            dr.DrawThePlayer(playerPB, currentAngle);
        }

        public void RotateRight()
        {
            currentAngle+=3;
            if (currentAngle == 360) currentAngle = 0;
            direction.Rotate(3*Math.PI / 180);
            dr.DrawThePlayer(playerPB, currentAngle);
        }

        public void Shoot(Form1 form)
        {
            Bullet shot = new Bullet(form, playerPB.Left, playerPB.Top, playerCollisions, direction);
            allBullets.Add(shot);
            shot.FlyingBullet(this);
        }
    }
}
