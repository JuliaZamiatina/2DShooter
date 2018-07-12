using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace _2DShooter
{
    class GameLogic
    {
        private Timer tm = new Timer();
        public Point playerStartPosition = new Point(714, 212);
        public static Point enemyStartPosition = new Point(12, 212);
        Label playerScoreL, enemyScoreL;
        private int enemyScore = 0, playerScore = 0;


        Form1 parentForm;
        public Player player;
        public Enemy enemy;
        Collisions allCollisions;


        public GameLogic(PictureBox enemy, PictureBox player, Label playerScore, Label enemyScore, Form1 form)
        {
            playerScoreL = playerScore;
            enemyScoreL = enemyScore;
            parentForm = form;

            List<PictureBox> walls = new List<PictureBox>();
            foreach (Control cont in form.Controls)
                if ((cont is PictureBox) && (cont.Tag.ToString().Equals("wall")))
                    walls.Add((PictureBox)cont);
            allCollisions = new Collisions(player, enemy, walls);

            this.player = new Player(player, allCollisions);
            this.player.dr.DrawThePlayer(player, 0);

            this.enemy = new Enemy(enemy, player, allCollisions, parentForm);
            this.player.dr.DrawTheEnemy(enemy, 0);

            tm.Interval = 20;
            tm.Tick += new EventHandler(Tm_Tick);
            tm.Start();
        }

        public void Tm_Tick(object sender, EventArgs e)
        {
            if (allCollisions.EnemyCollision(player.playerPB))
            {
                player.GoToStart();
                enemy.GoToStart();
                return;
            }
            player.PlayerMove(parentForm);
            enemy.EnemyMove(parentForm);
        }

        public void BulletInTarget(string target)
        {
            if (target.Equals("enemy"))
                playerScoreL.Text = (++playerScore).ToString();
            if (target.Equals("player"))
                enemyScoreL.Text = (++enemyScore).ToString();

            foreach (Bullet bul in player.allBullets)
                if (bul.bullet!=null)
                    bul.BulletDispose();
            player.allBullets.Clear();

            foreach (Bullet bul in enemy.allBullets)
                if (bul.bullet != null)
                    bul.BulletDispose();
            enemy.allBullets.Clear();

            player.GoToStart();
            enemy.GoToStart();
        }
    }
}
