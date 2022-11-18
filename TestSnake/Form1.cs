using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TestSnake
{
    struct Pos
    {
        public int x;
        public int y;
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Graphics g;

        Random rand = new Random();

        List<Pos> Elements = new List<Pos>();

        private int ColVoX;
        private int ColVoY;

        private readonly int sz = 10;

        private int xMove = 0;
        private int yMove = 0;

        private int rt = 1;

        private Pos food;

        DialogResult t;
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(Width, Height);

            g = Graphics.FromImage(pictureBox1.Image);

            ColVoX = Width / sz;
            ColVoY = Height / sz;

            Elements.Add(new Pos() { x = rand.Next(5, ColVoX - 5), y = rand.Next(5, ColVoY - 5) });

            food = new Pos() { x = rand.Next(0, ColVoX), y = rand.Next(0, ColVoY) };


            for (int i = 0; i < rt; i++)
            {
                g.FillRectangle(Brushes.Green, Elements[i].x * sz, Elements[i].y * sz, sz, sz);
            }

            g.FillRectangle(Brushes.Red, food.x * sz, food.y * sz, sz, sz);

            t = MessageBox.Show("Автоматически или нет? (В данном режиме не быдет проверки столкновения змейка с собой)", "Вопрос",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            g.FillRectangle(Brushes.Red, food.x * sz, food.y * sz, sz, sz);

            foreach (var iten in Elements)
            {
                g.FillRectangle(Brushes.Green, iten.x * sz, iten.y * sz, sz, sz);
            }

            g.FillRectangle(Brushes.Yellow, Elements[0].x * sz, Elements[0].y * sz, sz, sz);



            if (food.x == Elements[0].x && food.y == Elements[0].y)
            {
                rt += 1;

                Elements.Add(new Pos() { x = 0, y = 0 });

                MoveArrayRight();

                Elements[0] = new Pos() { x = food.x, y = food.y };

                food = new Pos() { x = rand.Next(0, ColVoX), y = rand.Next(0, ColVoY) };
                g.FillRectangle(Brushes.Red, food.x * sz, food.y * sz, sz, sz);
            }

            MoveArrayRight();

            Elements[0] = new Pos() { x = Elements[0].x + xMove, y = Elements[0].y + yMove };

            for (int i = 0; i < rt; i++)
                Elements[i] = ProvPos(Elements[i]);


            if (t == DialogResult.No)
            {
                for (int i = 2; i < rt; i++)
                {
                    if (Elements[0].x == Elements[i].x && Elements[0].y == Elements[i].y)
                    {
                        DialogResult t = MessageBox.Show("Выйти?", "You're DIE ", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                        if (t == DialogResult.Yes)
                            Close();
                        else
                            Application.Restart();
                    }
                }
            }
            else if (t == DialogResult.Yes)
            {
                AvtoMove();
            }
            pictureBox1.Invalidate();
        }

        private void MoveArrayRight()
        {
            Pos prev = Elements[0];
            Pos next;
            for (int i = 0; i < rt - 1; ++i)
            {
                next = Elements[i + 1];
                Elements[i + 1] = prev;
                prev = next;
            }
        }

        private void AvtoMove()
        {
            if (Elements[0].x > food.x)
            {
                xMove = -1;
                yMove = 0;
            }
            else if (Elements[0].x < food.x)
            {
                xMove = 1;
                yMove = 0;
            }
            else if (Elements[0].x == food.x)
            {
                if (Elements[0].y > food.y)
                {
                    xMove = 0;
                    yMove = -1;
                }
                else if (Elements[0].y < food.y)
                {
                    xMove = 0;
                    yMove = 1;
                }
            }
        }

        private Pos ProvPos(Pos pos)
        {
            if (pos.x == ColVoX)
                pos.x = 0;

            else if (pos.x < 0)
                pos.x = ColVoX - 1;

            if (pos.y == ColVoY)
                pos.y = 0;

            else if (pos.y < 0)
                pos.y = ColVoY - 1;

            return pos;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {


            if (e.KeyChar == 'a' || e.KeyChar == 'A' || e.KeyChar == 'ф' || e.KeyChar == 'Ф')
            {
                xMove = -1;
                yMove = 0;
                timer1.Start();
            }
            else if (e.KeyChar == 'w' || e.KeyChar == 'W' || e.KeyChar == 'ц' || e.KeyChar == 'Ц')
            {
                xMove = 0;
                yMove = -1;
                timer1.Start();
            }
            else if (e.KeyChar == 's' || e.KeyChar == 'S' || e.KeyChar == 'ы' || e.KeyChar == 'Ы')
            {
                xMove = 0;
                yMove = 1;
                timer1.Start();
            }
            else if (e.KeyChar == 'd' || e.KeyChar == 'D' || e.KeyChar == 'в' || e.KeyChar == 'В')
            {
                xMove = 1;
                yMove = 0;
                timer1.Start();
            }
        }
    }
}
