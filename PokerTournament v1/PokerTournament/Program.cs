using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTournament
{
    /*
     * This program will run a five card draw poker tournament
     * for the Game AI course. This program will use separate PlayerN
     * classes (with N being the team number) for each team whose methods 
     * will be called for decisions.
     * Kevin Bierre  Spring 2017
     */
    class Program
    {
        static void Main(string[] args)
        {
            // create two players
            Human h0 = new Human(0, "Joe", 1000);
            //Human h1 = new Human(1, "Sue", 1000);

            Player8 rex = new Player8(1, "Rex", 1000);

            // create the Game
            Game myGame = new Game(h0, rex);

            myGame.Tournament(); // run the game

            /*-----------------------------------------
             * Test the card shuffling
            Deck deck = new Deck();
            deck.NewRound();

            // test the hand values
            int handValue = 0;
            Card highCard = null;
            int[] counts = new int[11];

            while (handValue != 10)
            {
                // deal a hand
                Card[] hand = deck.Deal(5);

                // assign a value
                handValue = Evaluate.RateAHand(hand, out highCard);

                // save the count
                counts[handValue]++;
            }

            // loop through the counts
            for(int i = 1; i < counts.Length; i++)
            { 
                switch(i)
                {
                    case 1: Console.Write("High card "); break;
                    case 2: Console.Write("One pair "); break;
                    case 3: Console.Write("Two pair "); break;
                    case 4: Console.Write("Three of a kind "); break;
                    case 5: Console.Write("Straight "); break;
                    case 6: Console.Write("Flush "); break;
                    case 7: Console.Write("Full house "); break;
                    case 8: Console.Write("Four of a kind "); break;
                    case 9: Console.Write("Straight flush "); break;
                    case 10: Console.Write("Royal flush "); break;
                }
                Console.WriteLine(": " + counts[i]);
            }
            -------------------------------------------*/
        }
    }
}
