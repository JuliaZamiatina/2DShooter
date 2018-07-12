using System.Windows.Forms;

namespace _2DShooter
{
    partial class Form1 : Form
    {
        public GameLogic gl;
        Drawing drawing = new Drawing();


        public Form1()
        {
            InitializeComponent();
            gl = new GameLogic(enemy, player, label1, label2, this);
            
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                gl.player.moveForward = true;
            }
            if (e.KeyCode == Keys.S)
            {
                gl.player.moveBackward = true;
            }
            if (e.KeyCode == Keys.A)
            {
                gl.player.rotateLeft = true;
            }
            if (e.KeyCode == Keys.D)
            {
                gl.player.rotateRight = true;
            }
            if (e.KeyCode == Keys.Space)
            {
                gl.player.Shoot(this);
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                gl.player.moveForward = false;
            }
            if (e.KeyCode == Keys.S)
            {
                gl.player.moveBackward = false;
            }
            if (e.KeyCode == Keys.A)
            {
                gl.player.rotateLeft = false;
            }
            if (e.KeyCode == Keys.D)
            {
                gl.player.rotateRight = false;
            }
        }
    }
}
