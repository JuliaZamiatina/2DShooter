using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

namespace _2DShooter
{
    class Bullet
    {
        // private string direction;
        private Vector direction;
        private int speed = 4;

        public PictureBox bullet;
        private Timer tm;
        private Collisions bulletCollisions;
        private Form1 parentForm;

        public Bullet(Form1 form, int xCoord, int yCoord, Collisions collisions, Vector bulletDirection)
        {
            bullet = new PictureBox();
            parentForm = form;
            bullet.Left = xCoord;
            bullet.Top = yCoord;
            bulletCollisions = collisions;
            direction = new Vector(bulletDirection.X,bulletDirection.Y);
            tm = new Timer();
        }

        public void FlyingBullet(Player player)
        {
            bullet.Left = player.playerPB.Left + player.playerPB.Width/2;
            bullet.Top = player.playerPB.Top + player.playerPB.Height / 2;
            bullet.BackColor = Color.Black;
            bullet.Size = new Size(2, 2);
            bullet.Tag = "playerBullet";
            bullet.BringToFront();
            parentForm.Controls.Add(bullet);
            parentForm.Refresh();

            tm.Interval = 20;
            tm.Tick += new EventHandler(Tm_Tick);
            tm.Start();
        }

        public void FlyingBullet(Enemy enemy)
        {
            bullet.Left = enemy.enemyPB.Left + enemy.enemyPB.Width / 2;
            bullet.Top = enemy.enemyPB.Top + enemy.enemyPB.Height / 2;
            bullet.BackColor = Color.Black;
            bullet.Size = new Size(2, 2);
            bullet.Tag = "enemyBullet";
            bullet.BringToFront();
            parentForm.Controls.Add(bullet);
            parentForm.Refresh();

            tm.Interval = 20;
            tm.Tick += new EventHandler(Tm_Tick);
            tm.Start();
        }

        public void BulletMove()
        {
            bullet.Left += (int)Math.Round(direction.X * speed);
            bullet.Top += (int)Math.Round(direction.Y * speed);
        }

        public void BulletDispose()
        {
            tm.Stop();
            tm.Dispose();
            tm = null;
            bullet.Dispose();
            bullet = null;
        }

        private void Tm_Tick(object sender, EventArgs e)
        {
            if (bulletCollisions.WallsCollision(bullet))
            {
                Vector norm = direction;
                if (bulletCollisions.Collision_Bottom(bullet))
                {
                    norm = new Vector(0, -1);
                }
                if (bulletCollisions.Collision_Top(bullet))
                {
                    norm = new Vector(0, 1);
                }
                if (bulletCollisions.Collision_Left(bullet))
                {
                    norm = new Vector(1, 0);
                }
                if (bulletCollisions.Collision_Right(bullet))
                {
                    norm = new Vector(-1, 0);
                }
                direction = direction - norm*(norm*direction)*2.0;
                direction.Normalize();
            }

            BulletMove();
            if ((bulletCollisions.EnemyCollision(bullet))&&(bullet.Tag.ToString().Equals("playerBullet")))
            {
                parentForm.gl.BulletInTarget("enemy");
                return;
            }
            if ((bulletCollisions.PlayerCollision(bullet))&& (bullet.Tag.ToString().Equals("enemyBullet")))
            {
                //tm.Stop();
                //tm.Dispose();
                //tm = null;
                parentForm.gl.BulletInTarget("player");
                return;
            }
            if (bulletCollisions.FormCollision(parentForm, bullet.Location.X, bullet.Location.Y, 2))
            {
                //tm.Stop();
                //tm.Dispose();
                //tm = null;
                BulletDispose();
            }
        }
    }
}
