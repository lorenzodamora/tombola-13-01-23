using System;
using System.Threading.Tasks;

namespace tombolav2
{
	internal class Tombola
	{
		static void Main()
		{
			Start();

		}

		//stampa pre partita
		static void Start()
		{
			Console.BackgroundColor = ConsoleColor.Red;
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Black;
			Console.SetCursorPosition(10, 1);
			Console.Write("GIOCHIAMO A TOMBOLA!\n\n press any key to continue");
			Console.ReadKey();
			Console.BackgroundColor = ConsoleColor.White;
			Console.Clear();
			Console.BackgroundColor = ConsoleColor.Red;
			Console.Write("prima di cominciare mettere schermo intero e poi premere un tasto per continuare");
			Console.ReadKey();
			Console.BackgroundColor = ConsoleColor.White;
			Console.Clear();
		}
		static int[,] GeneraCartella()
		{
			Random R = new Random();
			//estrai 15 numeri
			int[] e;
			//cartella finale
			int[,] f = new int[0, 0];
			do
			{
				//estrae 15 volte da 1 a 90
				e = new int[15];
				for (int i = 0; i < 15; i++) e[i] = R.Next(1, 91); //91 non compreso
			} while (true); //controlli cartella

		}

	}
}
