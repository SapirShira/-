using System;
using System.Collections.Generic;

namespace Minesweeper
{
    class Program
    {
		public class Game
		{
			private int[,] matToDisplay { get; set; }
			private int[,] matToremember { get; set; }
			Random r = new Random();
			public Game()
			{
				matToDisplay = new int[8, 8];
				matToremember = new int[8, 8]; 
				int[,] arr1 = new int[2,7];
				for (int i = 0; i < 7; i++)
                {
					arr1[0, i] = -1;
					arr1[1, i] = -1;
				}
				int a, b, flag;
				
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
						if (matToremember[a, b] != -1)
						{
							matToremember[a, b] = -1;
							arr1[0, i] = a;
							arr1[1, i] = b;
							if (a - 1 != -1 && b + 1 != 8 && matToremember[a - 1, b + 1] != -1)
								matToremember[a - 1, b + 1] = 1;
							if (a + 1 != 8 && b - 1 != -1 && matToremember[a + 1, b - 1] != -1)
								matToremember[a + 1, b - 1] = 2;
							if (a - 1 != -1 && b - 1 != -1 && matToremember[a - 1, b - 1] != -1)
								matToremember[a - 1, b - 1] = 1;
							if (a + 1 != 8 && b + 1 != 8 && matToremember[a + 1, b + 1] != -1)
								matToremember[a + 1, b + 1] = 2;
							if (b + 1 != 8 && matToremember[a, b + 1] != -1)
								matToremember[a, b + 1] = 1;
							if (b - 1 != -1 && matToremember[a, b - 1] != -1)
								matToremember[a, b - 1] = 2;
							if (a + 1 != 8 && matToremember[a + 1, b] != -1)
								matToremember[a + 1, b] = 1;
							if (a - 1 != -1 && matToremember[a - 1, b] != -1)
								matToremember[a - 1, b] = 2;
						}
						else
							i--;
					}
				}

			}

			public int print()
            {
				int i, j, flag = 0;
				for (i = 0; i < 8; i++)
				{
					Console.Write((i+1) + "  -");
					for (j = 0; j < 9; j++)
					{
						if (j == 8)
							Console.Write("|");
						else if (matToDisplay[i, j] == 0)
						{
							Console.Write("| ? ");
							flag = 1;
						}
						else if (matToDisplay[i, j] == 1)
						{
							if (matToremember[i, j] == 0)
								Console.Write("|   ");
							else
								Console.Write("| " + matToremember[i, j] + " ");
						}

						else if (matToDisplay[i, j] == 2)
						{
							Console.Write("| B ");
							if (matToremember[i, j] != -1)
								flag = 1;
						}

					}
					Console.WriteLine("\n");
				}
				Console.Write("      -   -   -   -   -   -   -   -\n");
				Console.Write("      1   2   3   4   5   6   7   8\n");
				return flag;
			}
			string ans;
			int a, b;
			public int fillInPosition(int lo, int la, int flag)
            {
				if (matToDisplay[lo, la] == 1)
				{
					if (flag == 0)
					{
						Console.WriteLine("you alredy opened this place, please try again");
						return 0;
					}
				}
				if (matToDisplay[lo, la] == 2 && flag == 0)
				{


					Console.WriteLine("You blocked this location Do you want to release it and select it?(Y/N)");
					while (true)
					{
						ans = Console.ReadLine();
						if (ans == "y" || ans == "Y")
						{
							matToDisplay[lo, la] = 0;
							int x = fillInPosition(lo, la, 1);
							return x;
						}
						if (ans == "n" || ans == "N")
						{
							Console.WriteLine("please choose other position");
							return 0;
						}
						Console.WriteLine("please enter a valid value");
					}
				}
				else
				{
					if (matToDisplay[lo, la] != 2)
					{
						matToDisplay[lo, la] = 1;
						if (matToremember[lo, la] == -1)
						{
							return -1;
						}
						if ((matToremember[lo, la] == 1 || matToremember[lo, la] == 2))
						{
							return 1;
						}
					}

					a = lo; b = la;
					if (a - 1 != -1 && b + 1 != 8)
					{
						if (matToDisplay[a - 1, b + 1] == 0)
						{
							if (matToremember[a - 1, b + 1] == 1 || (matToremember[a - 1, b + 1] == 2))
								matToDisplay[a - 1, b + 1] = 1;
							else
									fillInPosition(a - 1, b + 1, 1);
						}
					}
					a = lo; b = la;
					if (a + 1 != 8 && b - 1 != -1)
					{
						if (matToDisplay[a + 1, b - 1] == 0)
						{
							if (matToremember[a + 1, b - 1] == 1 || (matToremember[a + 1, b - 1] == 2))
								matToDisplay[a + 1, b - 1] = 1;
							else
								fillInPosition(a + 1, b - 1, 1);
						}
					}
					a = lo; b = la;
					if (a - 1 != -1 && b - 1 != -1)
					{
						if (matToDisplay[a - 1, b - 1] == 0)
						{
							if (matToremember[a - 1, b - 1] == 2 || matToremember[a - 1, b - 1] == 1)
								matToDisplay[a - 1, b - 1] = 1;
							else
									fillInPosition(a - 1, b - 1, 1);
						}
					}
					a = lo; b = la;
					if (a + 1 != 8 && b + 1 != 8)
					{
						if (matToDisplay[a + 1, b + 1] == 0)
						{
							if (matToremember[a + 1, b + 1] == 1 || matToremember[a + 1, b + 1] == 2)
								matToDisplay[a + 1, b + 1] = 1;
							else
								fillInPosition(a + 1, b + 1, 1);
						}
					}
					a = lo; b = la;
					if (b + 1 != 8)
					{
						if (matToDisplay[a, b + 1] == 0)
						{
							if (matToremember[a, b + 1] == 1 || matToremember[a, b + 1] == 2)
								matToDisplay[a, b + 1] = 1;
							else
								fillInPosition(a, b + 1, 1);
						}
					}
					a = lo; b = la;
					if (b - 1 != -1)
					{
						if (matToDisplay[a, b - 1] == 0)
						{
							if (matToremember[a, b - 1] == 1 || matToremember[a, b - 1] == 2)
								matToDisplay[a, b - 1] = 1;
							else
								fillInPosition(a, b - 1, 1);
						}
					}
					a = lo; b = la;
					if (a + 1 != 8)
					{
						if (matToDisplay[a + 1, b] == 0)
						{
							if (matToremember[a + 1, b] == 1 || matToremember[a + 1, b] == 2)
								matToDisplay[a + 1, b] = 1;
							else
								fillInPosition(a + 1, b, 1);
						}
					}
					a = lo; b = la;
					if (a - 1 != -1)
					{
						if (matToDisplay[a - 1, b] == 0)
						{
							if (matToremember[a - 1, b] == 1 || matToremember[a - 1, b] == 2)
								matToDisplay[a - 1, b] = 1;
							else
								fillInPosition(a - 1, b, 1);
						}

					}


				}
				return 1;
			}

			public void play ()
            {
				int x;
				int flag = 0;
				print();
				Console.WriteLine("Please enter longitude and latitude (each in a separate line)");
				Console.WriteLine("if you whant to block a position please enter -1 twice (each in a separate line)");
				while (true)
				{
					try
					{
						int a, o;
						string lo = Console.ReadLine();
						string la = Console.ReadLine();
						try
						{
							o = Convert.ToInt32(lo);
							a = Convert.ToInt32(la);
						}
                        catch
                        {
							Console.WriteLine("Please enter a valid values (each in a separate line)");
							continue;
						}
						if ((o == -1 && a == -1) || flag == 1)
						{
							if (flag != 1)
							{
								flag = 1;
								Console.WriteLine("Please enter longitude and latitude to block (each in a separate line)");
								lo = Console.ReadLine();
								la = Console.ReadLine();
								try
								{
									o = Convert.ToInt32(lo);
									a = Convert.ToInt32(la);
									if (matToDisplay[o-1, a-1] == 1)
										throw new Exception("you alredy opened this place, please try again");
								}
								catch
								{
									if (matToDisplay[o-1, a-1] == 1)
										Console.WriteLine("you alredy opened this place, please try again");
									else
										Console.WriteLine("Please enter a valid values (each in a separate line)");
									continue;
								}
							}
							matToDisplay[o-1, a-1] = 2;
							Console.WriteLine("Location marked as blocked");
							print();
							Console.WriteLine("please enter position again");
							flag = 0;
							continue;
						}
						if (o > 8 || a > 8 || o < 1 || a < 1)
							throw new Exception("at list 1 number is not valid please try again");
						x = fillInPosition(o - 1, a - 1, 0);
						if (x == 0)
							continue;
						else if (x == -1)
						{
							Console.WriteLine("You hit a bomb the game is over");
							break;
						}
						else if (x == 1)
						{
							if(print() == 0)
                            {
								Console.WriteLine("win");
								break;
                            }
							else
                            {
								Console.WriteLine("Please enter longitude and latitude (each in a separate line)");
								Console.WriteLine("if you whant to block a position please enter -1 twice (each in a separate line)");
							}
						}

					}
					catch (Exception e)
					{
						Console.WriteLine(e);

					}
				}
			}
			
		}
        static void Main(string[] args)
        {

			Game game = new Game();
			game.play();
			
		}
    }
}
