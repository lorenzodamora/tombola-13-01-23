using System;
using System.Threading.Tasks;

namespace tombola
{
	internal class Program
	{
		static void Main()
		{
			//spiegazione nomi delle variabili in .txt a parte

			Start();
			int[,] c1 = GeneraCartella(); //crea cartella 1
			Task.Delay(1).Wait(); //necessario o mi fa i numeri uguali, Thread.Sleep(1) non serve a nulla
			int[,] c2 = GeneraCartella(); //crea cartella 2

			//stampa targhette
			Console.SetCursorPosition(2, 0);
			Console.Write("Cartella 1");
			Console.SetCursorPosition(52, 0);
			Console.Write("Cartella 2");
			Console.SetCursorPosition(2, 10);
			Console.Write("TABELLONE");

			//sfondo cartelle
			Console.SetCursorPosition(2, 2);
			Console.BackgroundColor = ConsoleColor.Green;
			for (int i = 2; i < 9; i++)
			{
				for (int j = 3; j < 42; j++)
				{
					Console.SetCursorPosition(j, i);
					Console.Write(" ");
				}
				for (int j = 3; j < 42; j++)
				{
					Console.SetCursorPosition(j + 50, i);
					Console.Write(" ");
				}
			}

			//stampa cartelle
			StampCart(c1, 5);
			StampCart(c2, 55);

			//suddivisione decine visibile
			Console.BackgroundColor = ConsoleColor.DarkRed;
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.SetCursorPosition(5, 1);
			Console.Write("00--10--20--30--40--50--60--70--80---");
			Console.SetCursorPosition(55, 1);
			Console.Write("00--10--20--30--40--50--60--70--80---");

			//tabellone
			Console.SetCursorPosition(5, 11);
			Console.Write(" 1-- 2-- 3-- 4-- 5-- 6-- 7-- 8-- 9--10            51--52--53--54--55--56--57--58--59--60");

			//sfondo tabellone
			Console.BackgroundColor = ConsoleColor.Cyan;
			Console.ForegroundColor = ConsoleColor.Black;
			for (int i = 12; i < 23; i++)
			{
				for (int j = 3; j < 95; j++)
				{
					Console.SetCursorPosition(j, i);
					Console.Write(" ");
				}
			}

			//crea e stampa tabellone
			int[,] t = new int[5, 20];
			int s = 0;
			for (int j = 0; j < 5; j++) for (int i = 0; i < 10; i++)
				{
					s++;
					t[j, i] = s;
					Console.SetCursorPosition(5 + i * 4, 13 + j * 2);
					Console.Write(s);
				}
			for (int j = 0; j < 4; j++) for (int i = 10; i < 20; i++)
				{
					s++;
					t[j, i] = s;
					Console.SetCursorPosition(55 + (i - 10) * 4, 13 + j * 2);
					Console.Write(s);
				}

			//inizio game
			Console.ReadKey();
			bool w12 = true; //vince c1 o c2? //se pareggio vince 1
			bool win; //finisci gioco

			//estrazione
			int max = 90;
			//array da dove estrae
			int[] e = new int[90];
			for (int i = 1; i < 91; i++) e[i - 1] = i;
			Random R = new Random();
			do
			{
				int ir = R.Next(0, max); //indice random
				int ne = e[ir]; //numero estratto
				max--;
				for (int i = 0; i < max; i++) if (i >= ir) e[i] = e[i + 1];
				//in questo modo si cancella il numero trovato e ogni numero retrocede di una posizione,
				//in fondo rimarrà sporco ma non ci si arriverà perchè il massimo è decrementato,
				//la prima volta il 90 è incluso per il +1, il massimo è decrementato quindi non uscirà dall'array

				//controllo e segnare
				c1 = Csc(c1, ne, 5);
				c2 = Csc(c2, ne, 55);
				t = Cst(t, ne);

				//scrivi il numero appena estratto
				Console.BackgroundColor = ConsoleColor.Black;
				Console.ForegroundColor = ConsoleColor.White;
				Console.SetCursorPosition(46, 6);
				Console.Write(ne);
				Console.ForegroundColor = ConsoleColor.Black;

				//win, finire il ciclo e dire chi ha vinto
				win = true; //estrai di nuovo
				s = 0; int s2 = 0; // somma cartelle
				foreach (var v in c1) s += v;
				foreach (var v in c2) s2 += v;
				if (s2 == 0)
				{
					win = false;
					w12 = false;
				}
				if (s == 0)
				{
					win = false;
					w12 = true;
				}
				Console.ReadKey();
			} while (win); //somma c1 o c2 = 0 

			//fine //non ho capito perchè creda non ci possa arrivare
			Task.Delay(2500).Wait();
			Console.BackgroundColor = ConsoleColor.Green;
			Console.Clear();
			Console.WriteLine("TOMBOLA!!");
			if (w12 == true) Console.Write("VINCE LA CARTELLA 1!!");
			else Console.Write("VINCE LA CARTELLA 2!!");
			Console.SetCursorPosition(0, 27);
			Console.BackgroundColor = ConsoleColor.DarkBlue;
		}
		//stampa pre partita
		static void Start()
		{
			Console.BackgroundColor = ConsoleColor.Red;//setta il colore di sfondo
			Console.Clear(); //colora tutto
			Console.ForegroundColor = ConsoleColor.Black;//setta il colore del carattere
			Console.SetCursorPosition(10, 1);//setta la posizione del cursore;
			Console.Write("GIOCHIAMO A TOMBOLA!\n\n press any key to continue");
			Console.ReadKey();//press any key to continue
			Console.BackgroundColor = ConsoleColor.White;
			Console.Clear();
			//
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
			int[] e = new int[0];
			//cartella finale
			int[,] f = new int[0, 0];
			do
			{//
				//estrae 15 volte da 1 a 90
				e = new int[15];
				for (int i = 0; i < 15; i++) e[i] = R.Next(1, 91);
			} while (Cc(e)); //controlli cartella

			//ordina cartella finale
			int[] cinque = new int[0]; //5 numeri per riga
			do
			{
				f = new int[3, 9]; //resetta cartella
				cinque = new int[3]; //resetta contenggio righe
				for (int j = 0; j < 9; j++)//per ogni decina esclusa 90
				{
					int cd = 0; int pd = 0; //cd conta decine //pd prima decina
					for (int i = 0; i < 15; i++) //ogni numero
					{
						//questo if è troppo da spiegare, in breve conta quante decine nella stessa colonna
						if (e[i].ToString().Length == 2 && e[i].ToString().StartsWith(j.ToString())) //se non è 00 e se è uguale alla decina del ciclo
						{
							cd++; //conta quante decine
							if (cd == 1)
							{
								pd = i; //segna la posizione nell'array della prima decina
								if (j == 8 && e[14] == 90) cd++; //se c'è il 90 stampa insieme agli 80
							}
						}
						else if (j == 0 && e[i] < 10) cd++; //da 00 a 09
					}

					//se sono 3 2 o 1 decina, se 3 riempi, se 2 o 1 fa a caso
					//cr controllo ripetizione
					int cr = 3;
					if (cd == 3) for (int i = 0; i < 3; i++)
						{
							cinque[i]++;
							f[i, j] = e[pd + i];
						}
					//ciclo infinito logico per l'ultima cifra lungo da spiegare, il counter rompe il for per far resettare tutto, per fortuna
					int counter = 20;
					if (cd != 3) for (int i = 0; i < cd; i++)
						{
							int g = -1;
							Task.Delay(1).Wait(); //necessario o mi fa i numeri uguali, Thread.Sleep(1) non serve a nulla
							//4 if per evitare troppi cicli a vuoto
							if (cinque[0] == 5 && cinque[1] == 5) g = 2;
							if (cinque[1] == 5 && cinque[2] == 5) g = 0;
							if (cinque[0] == 5 && cinque[2] == 5) g = 1;
							if (g == -1)
							{
								g = R.Next(0, 3);
								if (cinque[0] == 5) g = R.Next(1, 3);
								if (cinque[1] == 5) g = R.Next(0, 2) * 2; //se da 0 risulta 0, se da 1 risulta 2;
								if (cinque[2] == 5) g = R.Next(0, 2);
							}
							//per non cancellare numeri appena messi
							if (g != cr)
							{
								cr = g; //controllo ripetizione
								cinque[g]++; //numeri nella riga
								f[g, j] = e[pd + i]; //copia numero nella cartella
							}
							else //se non può far nulla
							{
								counter--; //conta ciclo
								if (counter == 0) //dopo 20 cicli
								{
									cinque[0] = 6; //controlli per ricominciare tutto
									break; //segna 6 così ripete e rompe il ciclo infinito
								}
								i--; //ripete if cd = 2 e cd = 1
							}
						}
				}

			} while (!(cinque[0] == 5 && cinque[1] == 5)); //rifai fino a quando ogni riga ha 5 numeri
			return f; //cartella finita
		}
		//controlli cartella
		static bool Cc(int[] e)
		{
			//ordina l'array cosicchè se ce ne sono due uguali sono uno davanti all'altro ripete
			Array.Sort(e); //.Sort ordina anche l'array della funzione GeneraCartella, comodo
			//controlli stesso numero
            for (int i = 0; i < 14; i++) if (e[i] == e[i + 1]) return true;

			//controlla se ci sono 4 decine uguali
			for (int i = 0; i < 15; i++)
			{
				int d = 0; //conta decine
				for (int j = 1; j < 15; j++)
				{
					//confronto decine tranne 90 che lo segna dopo
					if ((e[i].ToString().Length == 2 && e[j].ToString().Length == 2 && e[i].ToString().Substring(0, 1) == e[j].ToString().Substring(0, 1)) || (e[i].ToString().Length == 1 && e[j].ToString().Length == 1)) d++;
					//il 90 conta nelle 80ine:
					//solo nella posizione p14 c'è il 90, e nella p13 c'è sempre un 80ina se nella p14 c'è il 90, quando j controlla l'ultima 80ina conta anche il 90 se c'è
					if (e[14] == 90 && j == 13) d++;
					if (d == 4) return true; //deve rifare con 4 decine uguali
				}
			}

			//controlla se c'è una decina in ogni colonna
			if (e[0].ToString().Length != 1) return true; //nel primo spazio c'è sempre un numero a una cifra.
			for (int j = 1; j < 9; j++) //per ogni decina escluso il 90 e 00
			{
				bool cc = true;
				//ogni decina //se c'è una decina uguale al ciclo allora va bene
				for (int i = 0; i < 15; i++) if (e[i].ToString().Length == 2 && e[i].ToString().Substring(0, 1) == j.ToString()) cc = false;
				if (cc == true) return true; // return cc senza if non va bene perchè finisce la funzione
			}
			return false;
		}
		//stampa cartelle
		static void StampCart(int[,] c, int x)
		{
			for (int j = 0; j < 3; j++) for (int i = 0; i < 9; i++)
				{
					//calcolo posizione comodo per segnare dopo
					Console.SetCursorPosition(x + i * 4, 3 + j * 2);
					if (c[j, i] != 0) Console.Write(c[j, i]);
				}
		}
		//controllo e segnare cartella
		static int[,] Csc(int[,] c, int ne, int x)
		{
			Console.BackgroundColor = ConsoleColor.DarkRed;
			for (int j = 0; j < 3; j++) for (int i = 0; i < 9; i++) if (c[j, i] == ne)
					{
						//calcolo posizione di prima
						Console.SetCursorPosition(x - 1 + i * 4, 3 + j * 2);
						Console.Write($" {ne} ");
						c[j, i] = 0; //cancella il numero uscito per il calcolo vittoria
					}
			return c;
		}
		//controllo e segnare tabellone
		static int[,] Cst(int[,] t, int ne)
		{
			for (int j = 0; j < 5; j++) for (int i = 0; i < 10; i++) if (t[j, i] == ne)
					{
						Console.SetCursorPosition(4 + i * 4, 13 + j * 2);
						Console.Write($" {ne} ");
						t[j, i] = 0;

					}
			for (int j = 0; j < 4; j++) for (int i = 10; i < 20; i++) if (t[j, i] == ne)
					{
						Console.SetCursorPosition(54 + (i - 10) * 4, 13 + j * 2);
						Console.Write($" {ne} ");
						t[j, i] = 0;
					}
			return t;
		}
	}
}