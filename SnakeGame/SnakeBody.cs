using System.Drawing;

namespace SnakeGame
{
    class SnakeBody : System.Windows.Forms.Label
    {
        public SnakeBody(int x, int y)
        {
            Location = new Point(x, y);
            Size = new Size(20, 20);
            BackColor = Color.Black;
            Enabled = false;
        }
    }
}
