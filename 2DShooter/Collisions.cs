using System.Collections.Generic;
using System.Windows.Forms;

namespace _2DShooter
{
    class Collisions
    {

        private PictureBox enemy;
        private PictureBox player;
        private List<PictureBox> walls = new List<PictureBox>();

        public Collisions(PictureBox playerPictureBox, PictureBox enemyPictureBox, List<PictureBox> wallsPictureBoxes)
        {
            enemy = enemyPictureBox;
            player = playerPictureBox;
            walls.AddRange(wallsPictureBoxes);
        }

        public bool FormCollision(Form form, int x, int y, int size)
        {
            if ((x <= 0) || (y <= 0) || (x >= form.Width - size) || (y >= form.Height - size))
                return true;
            return false;
        }

        public bool EnemyCollision(PictureBox Object)
        {
            if (Object != null)
            {
                if ((Object.Bounds.IntersectsWith(enemy.Bounds)) && (!(Object.Equals(enemy))))
                {
                    return true;
                }
            }

            return false;
        }

        public bool PlayerCollision(PictureBox Object)
        {
            if (Object != null)
            {
                if (Object != null)
                {
                    if ((Object.Bounds.IntersectsWith(player.Bounds)) && (!(Object.Equals(player))))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool WallsCollision(PictureBox Object)
        {
            if (Object != null)
            {
                foreach (PictureBox wall in walls)
                    if (Object.Bounds.IntersectsWith(wall.Bounds))
                    {
                        return true;
                    }
            }
            return false;
        }

        internal bool Collision_Bottom(PictureBox bullet)
        {

            foreach (PictureBox wall in walls)
            {
                if (bullet.Bounds.IntersectsWith(new System.Drawing.Rectangle(wall.Location.X, wall.Location.Y - 4, wall.Width, 8)))
                {
                    return true;
                }
            }
            return false;
        }

        internal bool Collision_Top(PictureBox bullet)
        {

            foreach (PictureBox wall in walls)
            {
                if (bullet.Bounds.IntersectsWith(new System.Drawing.Rectangle(wall.Location.X, wall.Location.Y + wall.Height - 4, wall.Width, 8)))
                {
                    return true;
                }
            }
            return false;
        }

        internal bool Collision_Left(PictureBox bullet)
        {

            foreach (PictureBox wall in walls)
            {
                if (bullet.Bounds.IntersectsWith(new System.Drawing.Rectangle(wall.Location.X - 4, wall.Location.Y, 8, wall.Height)))
                {
                    return true;
                }
            }
            return false;
        }

        internal bool Collision_Right(PictureBox bullet)
        {
            foreach (PictureBox wall in walls)
            {
                if (bullet.Bounds.IntersectsWith(new System.Drawing.Rectangle(wall.Location.X + wall.Width - 4, wall.Location.Y, 8, wall.Height)))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
