using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class PlayGround : Form
    {
        int cols = 50;                  // 列
        int rows = 25;                  // 行
        int score = 0;                  // スコア
        int dx = 0;                     // X ポジション
        int dy = 0;                     // Y ポジション
        int front = 0;                  // ヘビの前
        int back = 0;                   // ヘビの後

        SnakeBody[] s_body = new SnakeBody[1250];
        List<int> nPosition = new List<int>();
        bool[,] isAvailable;

        Random rand = new Random();

        Timer timer = new Timer();

        /// <summary>
        /// 初期表示‘
        /// </summary>
        public PlayGround()
        {
            InitializeComponent();
            startGame();
            launchTimer();
        }

        /// <summary>
        /// キー押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Snake_KeyDown(object sender, KeyEventArgs e)
        {
            dx = dy = 0;
            switch (e.KeyCode)
            {
                case Keys.Right:
                    dx = 20;
                    break;
                case Keys.Left:
                    dx = -20;
                    break;
                case Keys.Up:
                    dy = -20;
                    break;
                case Keys.Down:
                    dy = 20;
                    break;
            }
        }

        #region メソッド
        /// <summary>
        /// タイマーを設定する
        /// </summary>
        private void launchTimer()
        {
            timer.Interval = 100;
            timer.Tick += moveNext;
            timer.Start();
        }

        /// <summary>
        /// ゲームを起動する
        /// </summary>
        private void startGame()
        {
            isAvailable = new bool[rows, cols];
            // ヘビの初期ピースを作成
            SnakeBody head
                = new SnakeBody((rand.Next() % cols) * 20, (rand.Next() % rows) * 20);
            // 食物のポジションを設定
            lblFood.Location
                = new Point((rand.Next() % cols) * 20, (rand.Next() % rows) * 20);
            // ヘビが動けない場所を設定
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    isAvailable[i, j] = false;
                    nPosition.Add(i * cols + j);
                }
            // ヘビが動けるところを設定
            isAvailable[head.Location.Y / 20, head.Location.X / 20] = true;
            nPosition.Remove(head.Location.Y / 20 * cols + head.Location.X / 20);
            Controls.Add(head);
            s_body[front] = head;
        }

        /// <summary>
        /// ヘビを起動する。毎１００millisecondsに再起動する。※interval = 100
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveNext(object sender, EventArgs e)
        {
            int x = s_body[front].Location.X;  // X Position
            int y = s_body[front].Location.Y;  // Y Position
            if (dx == 0 && dy == 0) return;
            if (game_over(x + dx, y + dy))
            {
                timer.Stop();
                MessageBox.Show("Congratulation! Your score is " + score,
                    "Game Over", MessageBoxButtons.OK);
                this.Close();
                return;
            }
            // 食物を食べるかどうかチェック
            if (collisionFood(x + dx, y + dy))
            {
                score += 1;
                lblScore.Text = "Score: " + score.ToString();
                if (hits((y + dy) / 20, (x + dx) / 20)) return;
                SnakeBody head = new SnakeBody(x + dx, y + dy);
                front = (front - 1 + 1250) % 1250;
                s_body[front] = head;
                isAvailable[head.Location.Y / 20, head.Location.X / 20] = true;
                Controls.Add(head);
                randomFood();
            }
            else
            {
                // ヘビの体を打つかどうかチェック
                if (hits((y + dy) / 20, (x + dx) / 20)) return;
                isAvailable[s_body[back].Location.Y / 20, s_body[back].Location.X / 20] = false;
                front = (front - 1 + 1250) % 1250;
                s_body[front] = s_body[back];
                s_body[front].Location = new Point(x + dx, y + dy);
                back = (back - 1 + 1250) % 1250;
                isAvailable[(y + dy) / 20, (x + dx) / 20] = true;
            }
        }

        /// <summary>
        /// 新しい食物を作成する
        /// </summary>
        private void randomFood()
        {
            nPosition.Clear();
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    if (!isAvailable[i, j]) nPosition.Add(i * cols + j);
            int idx = rand.Next(nPosition.Count) % nPosition.Count;
            lblFood.Left = (nPosition[idx] * 20) % Width;
            lblFood.Top = (nPosition[idx] * 20) / Width * 20;
        }

        /// <summary>
        /// ヘビの体を打つかどうかチェック
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool hits(int x, int y)
        {   
            if (isAvailable[x, y])
            {
                timer.Stop();
                MessageBox.Show("You Lose. Snake Hit his Body.", "Exit Game", MessageBoxButtons.OK);
                this.Close();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 食物を食べったかどうかチェック
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool collisionFood(int x, int y)
        {
            return x == lblFood.Location.X && y == lblFood.Location.Y;
        }

        /// <summary>
        /// 画面のサイズにチェックして、ゲームを判定する
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool game_over(int x, int y)
        {
            return x < 0 || y < 0 || x > 980 || y > 480;
        }
       
        #endregion
    }
}
