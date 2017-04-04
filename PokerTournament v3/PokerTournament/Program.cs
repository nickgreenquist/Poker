using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PokerTournament
{
    /*
     * This program will run a five card draw poker tournament
     * for the Game AI course. This program will use separate PlayerN
     * classes (with N being the team number) for each team whose methods 
     * will be called for decisions.
     * Kevin Bierre  Spring 2017
     * Version 3    4/4/2017:
     *  Corrected a logic error when folding
     *  Corrected a logic error in processing a call correctly
     */
    class Program
    {
        static void Main(string[] args)
        {
            // create two players
            //Human h0 = new Human(0, "Joe", 1000);
            Player8 h0 = new Player8(0, "Joe", 1000);
            //Human h1 = new Human(1, "Sue", 1000);
            Player8 h1 = new Player8(1, "Rex", 1000);

            // create the Game
            Game myGame = new Game(h1, h0);

            /*
            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;
                ostrm = new FileStream(Directory.GetCurrentDirectory() + "Redirect.txt", FileMode.OpenOrCreate, FileAccess.Write);
                writer = new StreamWriter(ostrm);
            Console.SetOut(writer);
            */

            myGame.Tournament(); // run the game

            /*
            Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();
            Console.WriteLine("Done");
            */

        }
    }
}
