using System;
using System.Collections.Generic;
using System.Text;

namespace battle_of_cards_grupauderzeniowa
{
    public class DisplayTable
    { 
        public static void DisplayBoard(List<Card> dealer, List<Card> player, int a, int c, int cu, 
        string re1, string re2)
        {
            Console.Clear();
            Console.WriteLine("           ===  CASINO ROYAL ===");
            Console.WriteLine(".===========================================.");
            for (int i = 0; i <10; i++)
            {
                Console.WriteLine("|                                           |");
            }
            Console.WriteLine("*===========================================*");
            Console.SetCursorPosition(11,2);
            Console.Write("Caribbean Stud Poker");
            Console.SetCursorPosition(2,4);
            Console.Write("Dealer Hand: ");
            for (int i = 0; i < 5; i++)
            {
                Console.Write(dealer[i].Icon);
            }
            Console.Write(" -> " + re1);
            Console.SetCursorPosition(2,5);
            Console.Write("--------------------------------------");
            Console.SetCursorPosition(2,6);
            Console.Write("Player Hand: ");
            for (int i = 0; i < 5; i++)
            {
                Console.Write(player[i].Icon);
            }
            Console.Write(" -> " + re2);
            Console.SetCursorPosition(2,8);
            Console.WriteLine("       ANTE: " + a.ToString() + "$       CALL: " + c.ToString() + "$");
            Console.SetCursorPosition(2,10);
            Console.Write("            YOUR MONEY: " + cu.ToString()+"$");
            Console.SetCursorPosition(0,13);
            Console.WriteLine("Would You like to play another game <Y/N>");
            Console.ReadKey();
        }
    }
}