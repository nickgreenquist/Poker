using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTournament
{
    // allows a human player to participate
    class Player8 : Player
    {
        // setup your basic human player
        public Player8(int idNum, string nm, int mny) : base(idNum, nm, mny)
        {
        }

        // handle the first round of betting
        public override PlayerAction BettingRound1(List<PlayerAction> actions, Card[] hand)
        {
            // list the hand
            ListTheHand(hand);

            //get rank manually
            Card highCard = null;
            int rank = Evaluate.RateAHand(hand, out highCard);

            // select an action
            string actionSelection = "";
            PlayerAction pa = null;
            do
            {
                Console.WriteLine("Select an action:\n1 - bet\n2 - raise\n3 - call\n4 - check\n5 - fold");

                //AI input for this round

                //fold if hand is worse than pair
                if (rank <= 1)
                {
                    actionSelection = "5";
                }

                //otherwise check 
                else
                {
                    actionSelection = "4";
                }

                // get amount if appropriate
                int amount = 0;
                if (actionSelection[0] == '1' || actionSelection[0] == '2')
                {
                    string amtText = "";
                    do
                    {
                        if (actionSelection[0] == '1') // bet
                        {
                            Console.Write("Amount to bet? ");
                            amtText = Console.ReadLine();
                        }
                        else if (actionSelection[0] == '2') // raise
                        {
                            Console.Write("Amount to raise? ");
                            amtText = Console.ReadLine();
                        }
                        // convert the string to an int
                        int tempAmt = 0;
                        int.TryParse(amtText, out tempAmt);

                        // check input
                        if (tempAmt > this.Money) //
                        {
                            Console.WriteLine("Amount bet is more than the amount you have available.");
                            amount = 0;
                        }
                        else if (tempAmt < 0)
                        {
                            Console.WriteLine("Amount bet or raised cannot be less than zero.");
                            amount = 0;
                        }
                        else
                        {
                            amount = tempAmt;
                        }
                    } while (amount <= 0);
                }

                // create the PlayerAction
                switch (actionSelection)
                {
                    case "1": pa = new PlayerAction(Name, "Bet1", "bet", amount); break;
                    case "2": pa = new PlayerAction(Name, "Bet1", "raise", amount); break;
                    case "3": pa = new PlayerAction(Name, "Bet1", "call", amount); break;
                    case "4": pa = new PlayerAction(Name, "Bet1", "check", amount); break;
                    case "5": pa = new PlayerAction(Name, "Bet1", "fold", amount); break;
                    default: Console.WriteLine("Invalid menu selection - try again"); continue;
                }
            } while (actionSelection != "1" && actionSelection != "2" &&
                    actionSelection != "3" && actionSelection != "4" &&
                    actionSelection != "5");
            // return the player action
            return pa;
        }

        // reuse the same logic for second betting round
        public override PlayerAction BettingRound2(List<PlayerAction> actions, Card[] hand)
        {
            PlayerAction pa1 = BettingRound1(actions, hand);

            // create a new PlayerAction object
            return new PlayerAction(pa1.Name, "Bet2", pa1.ActionName, pa1.Amount);
        }

        public override PlayerAction Draw(Card[] hand)
        {
            // list the hand
            ListTheHand(hand);

            //card index to deletes
            List<int> deleteIndexes = new List<int>();

            //get rank manually
            Card highCard = null;
            int rank = Evaluate.RateAHand(hand, out highCard);

            // determine how many cards to delete
            int cardsToDelete = 0;
            do
            {
                Console.Write("How many cards to delete? "); // get the count
                //string deleteStr = Console.ReadLine();
                string deleteStr;

                //use a decision tree to figure out the cards to delete
                deleteStr = "0";
                //straight, flush, full house, straight flush, or royal flush
                if(rank == 5 || rank == 6 || rank == 7 || rank == 9 || rank == 10)
                {
                    deleteStr = "0";
                }

                //4 of a kind
                if(rank == 8)
                {
                    //throw out 1 unless it's a King or Ace
                    Dictionary<int, int> handMap = new Dictionary<int, int>();
                    for(int i = 0; i < hand.Length; i++)
                    {
                        if(handMap.ContainsKey(hand[i].Value))
                        {
                            handMap[hand[i].Value]++;
                        }
                        else
                        {
                            handMap.Add(hand[i].Value, 1);
                        }
                    }

                    int v = handMap.FirstOrDefault(x => x.Value == 1).Key;
                    if(v < 14) //if less than ace
                    {
                        deleteStr = "1";
                        for (int i = 0; i < hand.Length; i++)
                        {
                            if (hand[i].Value == v)
                            {
                                deleteIndexes.Add(i);
                            }
                        }
                    }
                    else
                    {
                        deleteStr = "0";
                    }
                }

                //3 of a kind
                if(rank == 4)
                {
                    //throw out 2 that are not part of 3 of kind
                    Dictionary<int, int> handMap = new Dictionary<int, int>();
                    for (int i = 0; i < hand.Length; i++)
                    {
                        if (handMap.ContainsKey(hand[i].Value))
                        {
                            handMap[hand[i].Value]++;
                        }
                        else
                        {
                            handMap.Add(hand[i].Value, 1);
                        }
                    }

                    int v = handMap.FirstOrDefault(x => x.Value == 3).Key; //3 kind value

                    for(int i = 0; i < hand.Length; i++)
                    {
                        if(hand[i].Value != v)
                        {
                            deleteIndexes.Add(i);
                        }
                    }

                    deleteStr = "2";
                }

                //2 pair
                if(rank == 3)
                {
                    //throw out 1 card not in either pair
                    Dictionary<int, int> handMap = new Dictionary<int, int>();
                    for (int i = 0; i < hand.Length; i++)
                    {
                        if (handMap.ContainsKey(hand[i].Value))
                        {
                            handMap[hand[i].Value]++;
                        }
                        else
                        {
                            handMap.Add(hand[i].Value, 1);
                        }
                    }

                    int v = handMap.FirstOrDefault(x => x.Value == 1).Key;
                    if (v < 14) //if less than ace
                    {
                        deleteStr = "1";
                        for (int i = 0; i < hand.Length; i++)
                        {
                            if (hand[i].Value == v)
                            {
                                deleteIndexes.Add(i);
                            }
                        }
                    }
                    else
                    {
                        deleteStr = "0";
                    }
                }

                //1 pair
                if(rank == 2)
                {
                    //throw out 3 cards not in the pair
                    Dictionary<int, int> handMap = new Dictionary<int, int>();
                    for (int i = 0; i < hand.Length; i++)
                    {
                        if (handMap.ContainsKey(hand[i].Value))
                        {
                            handMap[hand[i].Value]++;
                        }
                        else
                        {
                            handMap.Add(hand[i].Value, 1);
                        }
                    }

                    int v = handMap.FirstOrDefault(x => x.Value == 3).Key;

                    for(int i = 0; i < hand.Length; i++)
                    {
                        if(hand[i].Value != v)
                        {
                            deleteIndexes.Add(i);
                        }
                    }

                    deleteStr = "3";
                }
                

                int.TryParse(deleteStr, out cardsToDelete);
            } while (cardsToDelete < 0 || cardsToDelete > 5);

            // which cards to delete if any
            PlayerAction pa = null;
            if (cardsToDelete > 0 && cardsToDelete < 5)
            {
                for (int i = 0; i < cardsToDelete; i++) // loop to delete cards
                {
                    Console.WriteLine("\nDelete card " + (i + 1) + ":");
                    for (int j = 0; j < hand.Length; j++)
                    {
                        Console.WriteLine("{0} - {1}", (j + 1), hand[j]);
                    }
                    // selete cards to delete
                    int delete = 0;
                    do
                    {

                        Console.Write("Which card to delete? (1 - 5): ");
                        //string delStr = Console.ReadLine();
                        //int.TryParse(delStr, out delete);

                        delete = deleteIndexes[i] + 1;

                        // see if the entry is valid
                        if (delete < 1 || delete > 5)
                        {
                            Console.WriteLine("Invalid entry - enter a value between 1 and 5.");
                            delete = 0;
                        }
                        else if (hand[delete - 1] == null)
                        {
                            Console.WriteLine("Entry was already deleted.");
                            delete = 0;
                        }
                        else
                        {
                            hand[delete - 1] = null; // delete entry
                            delete = 99; // flag to exit loop
                        }
                    } while (delete == 0);
                }
                // set the PlayerAction object
                pa = new PlayerAction(Name, "draw", "draw", cardsToDelete);
            }
            else if (cardsToDelete == 5)
            {
                // delete them all
                for (int i = 0; i < hand.Length; i++)
                {
                    hand[i] = null;
                }
                pa = new PlayerAction(Name, "draw", "draw", 5);
            }
            else // no cards deleted
            {
                pa = new PlayerAction(Name, "draw", "stand pat", 0);
            }

            // return the action
            return pa;
        }

        // helper method - list the hand
        private void ListTheHand(Card[] hand)
        {
            // evaluate the hand
            Card highCard = null;
            int rank = Evaluate.RateAHand(hand, out highCard);

            // list your hand
            Console.WriteLine("\nName: " + Name + " Your hand:   Rank: " + rank);
            for (int i = 0; i < hand.Length; i++)
            {
                Console.Write(hand[i].ToString() + " ");
            }
            Console.WriteLine();
        }
    }
}
