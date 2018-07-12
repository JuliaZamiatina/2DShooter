using System.Drawing;
using System.Windows.Forms;

namespace _2DShooter
{
    class Drawing
    {
        public void DrawThePlayer(PictureBox player, double angle)
        {
            Image img = Properties.Resources.PlayerW;
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            Graphics gfx = Graphics.FromImage(bmp);

            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            gfx.RotateTransform((float)angle);

            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            gfx.Clear(Color.White);

            gfx.DrawImage(img, new Point(0, 0));

            gfx.Dispose();
            player.Image = bmp;
        }

        public void DrawTheEnemy(PictureBox player, double angle)
        {
            Image img = Properties.Resources.EnemyE;
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            Graphics gfx = Graphics.FromImage(bmp);

            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            gfx.RotateTransform((float)angle);

            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            gfx.Clear(Color.White);

            gfx.DrawImage(img, new Point(0, 0));

            gfx.Dispose();
            player.Image = bmp;
        }
    }
}
