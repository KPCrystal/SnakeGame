using System;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class Main : Form
    {
        /// <summary>
        /// 初期表示
        /// </summary>
        public Main()
        {
            InitializeComponent();
        }

        #region イベント
        /// <summary>
        /// Aboutメニュー押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form aboutBox = new AboutBox1();
            aboutBox.ShowDialog();
        }

        /// <summary>
        /// Exitメニュー押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Start Gameボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            Form frm = new PlayGround();
            frm.Show();
        }

        /// <summary>
        /// Exitボタン押下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion
    }
}
