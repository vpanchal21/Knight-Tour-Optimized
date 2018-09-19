using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        const int BOARD_SIZE = 8;
        const int WIDTH = 30;
        const int HEIGHT = 30;
        int intCounter = 1;
        bool TERMINATE = false;
        Button[,] buttons = new Button[BOARD_SIZE,BOARD_SIZE];
        int[,] Matrix = new int[BOARD_SIZE, BOARD_SIZE];

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int x = 0; x < BOARD_SIZE; x++)
                for (int y = 0; y < BOARD_SIZE; y++)
                    Matrix[x, y]=0;

            DrawBoard();
        }
        private void DrawBoard()
        {
            for (int x = 0; x < BOARD_SIZE; x++)
            {
                for (int y = 0; y < BOARD_SIZE; y++)
                {
                    buttons[x, y] = new Button();
                    buttons[x,y].Visible = true;
                    buttons[x,y].Size = new System.Drawing.Size(30, 30);
                    buttons[x,y].Location = new System.Drawing.Point(WIDTH + x * 30, HEIGHT + y * 30);
                    buttons[x, y].Text = "";
                    if ((y % 2==0 && x % 2 == 0) || (y % 2 == 1 && x % 2 == 1))
                        buttons[x, y].BackColor = Color.Yellow;
                    else
                        buttons[x, y].BackColor = Color.White;
                    this.Controls.Add(buttons[x,y]);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < BOARD_SIZE; x++)            
                for (int y=0; y < BOARD_SIZE; y++)
                    StepMe(x, y);
        }
        private void StepMe(int x,int y)
        {
            if (TERMINATE == true) return;
            if (x > BOARD_SIZE - 1) return;
            if (y > BOARD_SIZE - 1) return;
            if (x < 0) return;
            if (y < 0) return;
            if (Matrix[x, y]!=0) return;

            Matrix[x, y] = intCounter;
            
            //checking deal move for one of the corner. this can be checked for all 4 corners
            if (Matrix[5,1] != 0 && Matrix[6,2]  != 0 && Matrix[7,0]  == 0 && intCounter!=63)
            {
                Matrix[x, y]=0;
                return;
            }
            if (intCounter == BOARD_SIZE * BOARD_SIZE)
            {

                for (int i = 0; i < BOARD_SIZE; i++)
                    for (int j = 0; j < BOARD_SIZE; j++)
                        buttons[i,j].Text = Matrix[i,j].ToString();

                if (MessageBox.Show("Solution found. Do you want to continue to find next solution?", "Knight Tour", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    TERMINATE = true;
                    button1.Visible = false;
                }
            }
            intCounter++;
            StepMe(x + 1, y + 2);
            StepMe(x + 2, y + 1);
            StepMe(x + 2, y - 1);
            StepMe(x + 1, y - 2);
            StepMe(x - 1, y - 2);
            StepMe(x - 2, y - 1);
            StepMe(x - 2, y + 1);
            StepMe(x - 1, y + 2);
            Matrix[x, y]=0;
            intCounter--;
        }
    }
}
