using System;
using System.IO;
using System.Collections.Generic;

namespace Final_Project
{
    internal class Program
    {

        /*
         * Pirate's Gold: A console-based C# game where players take 1-3 coins from a pile of 20-30 coins,
         * aiming to avoid taking the last coin. Supports multiplayer (Player 1 vs. Player 2) and computer
         * modes, with pirate-themed messages and game results saved to GameResults.txt.
         */
        private static void Main(string[] args)
        {
            // Initialize variables
            Random random = new Random();
            int coinsInPile;            // Number of coins in the pile
            int coinsTaken = 0;             // Coins taken each turn
            int playerTurn;            // Tracks whose turn it is (1 or 2)
            string gameMode;         // Multiplayer or computer mode
            int player2Wins = 0;         // Player 2 count
            int computerWins = 0;         //computer win count
            int totalGamesPvP = 0;        // Total games played between players
            int totalGamesPvC = 0;         // Total games played between player and computer
            string playAgain;            // For replaying the game
            List<string> pirateMessages = new List<string>();           // List for pirate-themed messages

            // Populate pirate messages
            pirateMessages.Add("Argh! The gold be dwindlin' fast!");
            pirateMessages.Add("Shiver me timbers, grab those coins!");
            pirateMessages.Add("Ye better not take the last coin, matey!");
            pirateMessages.Add("Ho ho, the pile's gettin' small!");
            pirateMessages.Add("Blimey, who'll claim the victory?");

            // Welcome message
            Console.WriteLine("Welcome to Pirate's Gold!");
            Console.Write("Choose game mode (multiplayer/computer): ");
            gameMode = Console.ReadLine().ToLower();

            // Main game loop
            do
            {
                // Set up new game
                coinsInPile = random.Next(20, 31);            // Random pile between 20-30 coins
                playerTurn = 1;                              // Start with Player 1
                
                if (gameMode == "multiplayer")
                    totalGamesPvP ++;
                else
                    totalGamesPvC ++;

                    // Play until no coins remain
                    do
                    {
                        Console.WriteLine("Coins left: " + coinsInPile);

                        // Handle players turn
                        if (gameMode == "multiplayer" || playerTurn == 1)
                        {

                            // Get valid player input
                            do
                            {
                                Console.Write("Player " + playerTurn + ", take 1-3 coins: ");
                                coinsTaken = int.Parse(Console.ReadLine());
                          

                            } while (coinsTaken < 1 || coinsTaken > 3 || coinsTaken > coinsInPile);
                             
                             
                        }
                        else
                        {
                            // Computer's turn
                            coinsTaken = GetComputerMove(coinsInPile);
                            Console.WriteLine("Computer takes " + coinsTaken + " coins!");
                        }

                        // Update pile
                        coinsInPile = coinsInPile - coinsTaken;

                        // Show message and switch turns if game continues
                        if (coinsInPile > 0)
                        {
                            ShowPirateMessage(pirateMessages, random);
                            if (playerTurn == 1)
                            {
                               
                                playerTurn = 2;
                                
                            }
                            else
                            {
                                playerTurn = 1;

                            }
                        }
                    } while (coinsInPile > 0);

                // Determine winner (player who didn't take last coin wins)
                if (playerTurn == 1)
                {
                    
                    if (gameMode == "multiplayer")
                    {
                        player2Wins = player2Wins + 1;
                        Console.WriteLine("Player 2 wins!");
                    }
                    else
                    {
                        computerWins = computerWins + 1;
                        Console.WriteLine("Computer wins!");
                    }
                }
                else
                {
                   
                    Console.WriteLine("Player 1 wins!");
                }

                // Save game results to file
                try
                {

                    StreamWriter writer = new StreamWriter("..\\..\\..\\GameResults.txt");     // Append mode
                    writer.WriteLine("Multiplayer");
                    writer.WriteLine("Total Games " + totalGamesPvP);
                    writer.WriteLine("Player 1 wins = " + (totalGamesPvP - player2Wins) + ", Player 2 wins = " + player2Wins);
                    double player1Percent = Math.Round(((totalGamesPvP - player2Wins) * 100.0) / totalGamesPvP, 2);
                    double player2Percent = Math.Round((player2Wins * 100.0) / totalGamesPvP, 2);
                    writer.WriteLine("Player 1 win percentage: " + player1Percent + "%");
                    writer.WriteLine("Player 2 win percentage: " + player2Percent + "%");

                    writer.WriteLine();

                    writer.WriteLine("Player vs Computer");
                    writer.WriteLine("Total Games " + totalGamesPvC);
                    writer.WriteLine("Player 1 wins = " + (totalGamesPvC - computerWins) + ", Computer wins = " + computerWins);
                    double playerPercent = Math.Round(((totalGamesPvC - computerWins) * 100.0) / totalGamesPvC, 2);
                    double computerPercent = Math.Round((computerWins * 100.0) / totalGamesPvC, 2);
                    writer.WriteLine("Player 1 win percentage: " + playerPercent + "%");
                    writer.WriteLine("Player 2 win percentage: " + computerPercent + "%");

                    writer.Close(); // Close file
                }
                catch
                {
                    Console.WriteLine("Error saving game results!");
                }


                // Ask to play again
                Console.WriteLine("Do you want to play again? (yes/no): ");
                playAgain = Console.ReadLine().ToLower();

            } while (playAgain == "yes");

            
           
        }

        // Method to display a random pirate message
        private static void ShowPirateMessage(List<string> messages, Random random)
        {
            int index = random.Next(0, messages.Count); // Pick random index
            Console.WriteLine(messages[index]);
        }

        // Method to calculate computer's move
        private static int GetComputerMove(int coins)
        {
            int coinsToTake = 1;            // Default move
            Random random1 = new Random();
            // Try to leave multiple of 4 coins
            if (coins > 4)
            {
                int remainder = coins % 4;           // Use modulo operator
                if (remainder >= 1 && remainder <= 3)
                {
                    coinsToTake = remainder;              // Take remainder to leave multiple of 4
                }
                else
                {
                    coinsToTake = random1.Next(1, 4);         // Random move if no ideal choice
                }
            }
            else
            {
                // Take all coins if 1, else take coins-1
                if (coins <= 4 && coins > 1)
                {
                    
                    coinsToTake = coins - 1;
                }
                else
                {
                    coinsToTake = 1;
                }
            }

            return coinsToTake;
        }
    }
}
