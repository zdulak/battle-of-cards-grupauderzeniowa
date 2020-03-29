﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanPokerMain
{
    public static class View
    { 
        public static void PrintMsg(string msg)
        {
            Console.WriteLine(msg);
        }
        public static void PrintStatus(int money, int jackpot)
        {
            Console.WriteLine($"Your money: {money}.  Current jackpot: {jackpot}.");
        }
        public static void DisplayBoard(Card[] dealer, Card[] player, int a, int c, int cu) 
        {
            var dealercomb = Analisis.HandAnalizer(dealer);
            var playercomb = Analisis.HandAnalizer(player);
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
            Console.Write(" -> " + dealercomb.ToString());
            Console.SetCursorPosition(2,5);
            Console.Write("--------------------------------------");
            Console.SetCursorPosition(2,6);
            Console.Write("Player Hand: ");
            for (int i = 0; i < 5; i++)
            {
                Console.Write(player[i].Icon);
            }
            Console.Write(" -> " + playercomb.ToString());
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