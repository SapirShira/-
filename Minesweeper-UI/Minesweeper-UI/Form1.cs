using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper_UI
{
    public partial class Form1 : Form
    {
		private Button[,] btnAdd;
		int b_flag = 0, count;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
			count = 0;
			btnAdd = null;
			btnAdd = new Button[8, 8];
			for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    btnAdd[i, j] = new Button();
                    btnAdd[i, j].Width = 20;
                    btnAdd[i, j].Height = 20;
                    btnAdd[i, j].ForeColor = Color.Black;
                    btnAdd[i, j].Text = "?";
					btnAdd[i, j].Tag = 0;
					btnAdd[i, j].Location = new System.Drawing.Point(40+(i*20), 70+(j*20));
                    btnAdd[i, j].Click += new EventHandler(DynamicButton_Click);
                    this.Controls.Add(btnAdd[i, j]);
                }

            }
			int[,] arr1 = new int[2, 7];
			for (int i = 0; i < 7; i++)
			{
				arr1[0, i] = -1;
				arr1[1, i] = -1;
			}
			int a, b, flag;
			Random r = new Random();
			for (int i = 0; i < 7; i++)
			{
				flag = 0;
				a = r.Next(8);
				b = r.Next(8);
				if (i != 0)
				{
					for (int j = 0; j < i; j++)
					{
						if (Math.Abs(a - arr1[0, j]) + Math.Abs(b - arr1[1, j]) < 6)
						{
							flag = 1;
							break;
						}
					}

				}
				else
					flag = 1;
				if (flag == 0)
				{
					i--;
				}
				else
				{
					if (btnAdd[a, b].Tag.ToString() != "-1")
					{
						btnAdd[a, b].Tag = -1;
						arr1[0, i] = a;
						arr1[1, i] = b;
						if (a - 1 != -1 && b + 1 != 8 && btnAdd[a - 1, b + 1].Tag.ToString() != "-1")
							btnAdd[a - 1, b + 1].Tag = 1;
						if (a + 1 != 8 && b - 1 != -1 && btnAdd[a + 1, b - 1].Tag.ToString() != "-1")
							btnAdd[a + 1, b - 1].Tag = 2;
						if (a - 1 != -1 && b - 1 != -1 && btnAdd[a - 1, b - 1].Tag.ToString() != "-1")
							btnAdd[a - 1, b - 1].Tag = 1;
						if (a + 1 != 8 && b + 1 != 8 && btnAdd[a + 1, b + 1].Tag.ToString() != "-1")
							btnAdd[a + 1, b + 1].Tag = 2;
						if (b + 1 != 8 && btnAdd[a, b + 1].Tag.ToString() != "-1")
							btnAdd[a, b + 1].Tag = 1;
						if (b - 1 != -1 && btnAdd[a, b - 1].Tag.ToString() != "-1")
							btnAdd[a, b - 1].Tag = 2;
						if (a + 1 != 8 && btnAdd[a + 1, b].Tag.ToString() != "-1")
							btnAdd[a + 1, b].Tag = 1;
						if (a - 1 != -1 && btnAdd[a - 1, b].Tag.ToString() != "-1")
							btnAdd[a - 1, b].Tag = 2;
					}
					else
						i--;
				}

			}
		}
		public void print()
		{
			for (int i = 0; i < 8; i++)
				for (int j = 0; j < 8; j++)
					if (btnAdd[i, j].Tag.ToString().Equals("-1"))
						btnAdd[i, j].Text = "*";
					else
						btnAdd[i, j].Text = btnAdd[i, j].Tag.ToString();

		}
		private void DynamicButton_Click(object sender, EventArgs e)
        {

			label1.Text = " ";
            Button bu = (Button)sender;
			int x= (bu.Location.X-40)/20;
			int y = (bu.Location.Y-70)/20;
			int a, b;
			if (b_flag == 1)
			{
				bu.Text = "B";
				b_flag = 0;
			}
			if (b_flag == 2)
			{
				if (bu.Text.Equals("B"))
				{
					b_flag = 0;
					bu.Text = "?";
				}
				else
					label1.Text = "this position is unblock please try again";
			}


			else if (bu.Text.Equals("?") == true)
			{
				if (bu.Tag.ToString().Equals("-1"))
				{
					print();
					label1.Text = "Game Over";
					button2_Click(sender, e);

				}
				else
				{
					a = x; b = y;
					if (bu.Tag.ToString().Equals("0"))
					{
						bu.Text = " ";
						count++;
					}
					if (bu.Tag.ToString().Equals("0") || (bu.Text.Equals("B")))
					{
						if (a - 1 != -1 && b + 1 != 8)
						{
							if (btnAdd[a - 1, b + 1].Text.Equals("?"))
							{
								if (btnAdd[a - 1, b + 1].Tag.Equals("1") || (btnAdd[a - 1, b + 1].Tag.Equals("2")))
								{
									btnAdd[a - 1, b + 1].Text = btnAdd[a - 1, b + 1].Tag.ToString();
									count++;
								}
								else
									DynamicButton_Click(btnAdd[a - 1, b + 1], e);
							}
						}
						a = x; b = y;
						if (a + 1 != 8 && b - 1 != -1)
						{
							if (btnAdd[a + 1, b - 1].Text.Equals("?"))
							{
								if (btnAdd[a + 1, b - 1].Tag.Equals("1") || (btnAdd[a + 1, b - 1].Tag.Equals("2")))
								{ 
									btnAdd[a + 1, b - 1].Text = btnAdd[a + 1, b - 1].Tag.ToString();
									count++;
								}
								else
									DynamicButton_Click(btnAdd[a + 1, b - 1], e);
							}
						}
						a = x; b = y;
						if (a - 1 != -1 && b - 1 != -1)
						{
							if (btnAdd[a - 1, b - 1].Text.Equals("?"))
							{
								if (btnAdd[a - 1, b - 1].Tag.Equals("2") || btnAdd[a - 1, b - 1].Tag.Equals("1"))
								{ 
									btnAdd[a - 1, b - 1].Text = btnAdd[a - 1, b - 1].Tag.ToString();
									count++;
								}
								else
									DynamicButton_Click(btnAdd[a - 1, b - 1], e);
							}
						}
						a = x; b = y;
						if (a + 1 != 8 && b + 1 != 8)
						{
							if (btnAdd[a + 1, b + 1].Text.Equals("?"))
							{
								if (btnAdd[a + 1, b + 1].Tag.Equals("1") || btnAdd[a + 1, b + 1].Tag.Equals("2"))
								{ 
									btnAdd[a + 1, b + 1].Text = btnAdd[a + 1, b + 1].Tag.ToString();
									count++;
								}
								else
									DynamicButton_Click(btnAdd[a + 1, b + 1], e);
							}
						}
						a = x; b = y;
						if (b + 1 != 8)
						{
							if (btnAdd[a, b + 1].Text.Equals("?"))
							{
								if (btnAdd[a, b + 1].Tag.Equals("1") || btnAdd[a, b + 1].Tag.Equals("2"))
								{ 
									btnAdd[a, b + 1].Text = btnAdd[a, b + 1].Tag.ToString();
									count++;
								}
								else
									DynamicButton_Click(btnAdd[a, b + 1], e);
							}
						}
						a = x; b = y;
						if (b - 1 != -1)
						{
							if (btnAdd[a, b - 1].Text.Equals("?"))
							{
								if (btnAdd[a, b - 1].Tag.Equals("1") || btnAdd[a, b - 1].Tag.Equals("2"))
								{ 
									btnAdd[a, b - 1].Text = btnAdd[a, b - 1].Tag.ToString();
									count++;
								}
								else
									DynamicButton_Click(btnAdd[a, b - 1], e);
							}
						}
						a = x; b = y;
						if (a + 1 != 8)
						{
							if (btnAdd[a + 1, b].Text.Equals("?"))
							{
								if (btnAdd[a + 1, b].Tag.Equals("1") || btnAdd[a + 1, b].Tag.Equals("2"))
								{ 
									btnAdd[a + 1, b].Text = btnAdd[a + 1, b].Tag.ToString();
									count++;
								}
								else
									DynamicButton_Click(btnAdd[a + 1, b], e);
							}
						}
						a = x; b = y;
						if (a - 1 != -1)
						{
							if (btnAdd[a - 1, b].Text.Equals("?"))
							{
								if (btnAdd[a - 1, b].Tag.Equals("1") || btnAdd[a - 1, b].Tag.Equals("2"))
								{ 
									btnAdd[a - 1, b].Text = btnAdd[a - 1, b].Tag.ToString();
									count++;
								}
								else
									DynamicButton_Click(btnAdd[a - 1, b], e);
							}

						}
					}
					else
					{ 
						bu.Text = bu.Tag.ToString();
						count++;
					}
				}

			}
			if (count == 57)
			{
				label1.Text = "Winning";
				button2_Click(sender, e);
			}
        }

		private void button1_Click(object sender, EventArgs e)
		{
			b_flag = 1;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			b_flag = 2;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			DialogResult d;
			if (label1.Text == "Game Over" || label1.Text == "Winning")
				d = MessageBox.Show(label1.Text + "\nDo you whant to start a new game?", "new game", MessageBoxButtons.YesNo);
			else
				d = MessageBox.Show("Do you whant to start a new game?", "new game", MessageBoxButtons.YesNo);
			if (DialogResult.Yes == d)
			{
				newGame();
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			DialogResult d = MessageBox.Show("Do you whant to exit?", "Exit", MessageBoxButtons.YesNo);
			if (DialogResult.Yes == d)
			{
				this.Close();
			}
		}

		public void newGame()
		{
			count = 0;
			label1.Text = "";
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					
					btnAdd[i, j].Text = "?";
					btnAdd[i, j].Tag = 0;
					
				}

			}
			int[,] arr1 = new int[2, 7];
			for (int i = 0; i < 7; i++)
			{
				arr1[0, i] = -1;
				arr1[1, i] = -1;
			}
			int a, b, flag;
			Random r = new Random();
			for (int i = 0; i < 7; i++)
			{
				flag = 0;
				a = r.Next(8);
				b = r.Next(8);
				if (i != 0)
				{
					for (int j = 0; j < i; j++)
					{
						if (Math.Abs(a - arr1[0, j]) + Math.Abs(b - arr1[1, j]) < 6)
						{
							flag = 1;
							break;
						}
					}

				}
				else
					flag = 1;
				if (flag == 0)
				{
					i--;
				}
				else
				{
					if (btnAdd[a, b].Tag.ToString() != "-1")
					{
						btnAdd[a, b].Tag = -1;
						arr1[0, i] = a;
						arr1[1, i] = b;
						if (a - 1 != -1 && b + 1 != 8 && btnAdd[a - 1, b + 1].Tag.ToString() != "-1")
							btnAdd[a - 1, b + 1].Tag = 1;
						if (a + 1 != 8 && b - 1 != -1 && btnAdd[a + 1, b - 1].Tag.ToString() != "-1")
							btnAdd[a + 1, b - 1].Tag = 2;
						if (a - 1 != -1 && b - 1 != -1 && btnAdd[a - 1, b - 1].Tag.ToString() != "-1")
							btnAdd[a - 1, b - 1].Tag = 1;
						if (a + 1 != 8 && b + 1 != 8 && btnAdd[a + 1, b + 1].Tag.ToString() != "-1")
							btnAdd[a + 1, b + 1].Tag = 2;
						if (b + 1 != 8 && btnAdd[a, b + 1].Tag.ToString() != "-1")
							btnAdd[a, b + 1].Tag = 1;
						if (b - 1 != -1 && btnAdd[a, b - 1].Tag.ToString() != "-1")
							btnAdd[a, b - 1].Tag = 2;
						if (a + 1 != 8 && btnAdd[a + 1, b].Tag.ToString() != "-1")
							btnAdd[a + 1, b].Tag = 1;
						if (a - 1 != -1 && btnAdd[a - 1, b].Tag.ToString() != "-1")
							btnAdd[a - 1, b].Tag = 2;
					}
					else
						i--;
				}

			}
		}
	}
}
