using System;
using System.IO;
using System.Collections.Generic;

namespace Final_Project
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Random random = new Random();
            int coinsInPile;
            int coinsTaken;
            int playerTurn;
            string gameMode;
            int player1Wins = 0;
            int player2Wins = 0;
            int totalGames = 0;
            string playAgain;
            List<string> pirateMessages = new List<string>();

            // Add pirate-themed messages
            pirateMessages.Add("Argh! The gold be dwindlin' fast!");
            pirateMessages.Add("Shiver me timbers, grab those coins!");
            pirateMessages.Add("Ye better not take the last coin, matey!");
            pirateMessages.Add("Ho ho, the pile's gettin' small!");
            pirateMessages.Add("Blimey, who'll claim the victory?");

            Console.WriteLine("Welcome to Pirate's Gold!");
            Console.Write("Choose game mode (multiplayer/computer): ");
            gameMode = Console.ReadLine().ToLower();

            do
            {
                coinsInPile = random.Next(20, 31);
                playerTurn = 1;
                totalGames = totalGames + 1;

                do
                {
                    Console.WriteLine("Coins left: " + coinsInPile);

                    if (gameMode == "multiplayer" || playerTurn == 1)
                    {
                        bool validInput = false;
                        coinsTaken = 0;

                        while (!validInput)
                        {
                            Console.Write("Player " + playerTurn + ", take 1-3 coins: ");
                            try
                            {
                                coinsTaken = int.Parse(Console.ReadLine());
                                if (coinsTaken >= 1 && coinsTaken <= 3 && coinsTaken <= coinsInPile)
                                {
                                    validInput = true;
                                }
                                else
                                {
                                    Console.WriteLine("Please take 1 to 3 coins, not more than pile!");
                                }
                            }
                            catch
                            {
                                Console.WriteLine("Enter a number!");
                            }
                        }
                    }
                    else
                    {
                        coinsTaken = GetComputerMove(coinsInPile, random);
                        Console.WriteLine("Computer takes " + coinsTaken + " coins!");
                    }

                    coinsInPile = coinsInPile - coinsTaken;

                    // Show a pirate message if coins remain
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

                // Winner is the player who *didn't* take the last coin
                if (playerTurn == 1)
                {
                    player2Wins = player2Wins + 1;

                    Console.WriteLine("Computer wins!");

                }
                else
                {
                    player1Wins = player1Wins + 1;
                    Console.WriteLine("Player 1 wins!");
                }

                // Save results to a file
                try
                {
                    string result = "Game " + totalGames + ": Player 1 wins = " + player1Wins + ", Player 2/Computer wins = " + player2Wins;
                    StreamWriter sw = new StreamWriter("..\\..\\..\\GameResults.txt");
                    sw.WriteLine(result);
                    sw.Close();

                }
                catch
                {
                    Console.WriteLine("Error saving game results!");
                }

                Console.WriteLine("Do you want to play again? (yes/no): ");
                playAgain = Console.ReadLine().ToLower();

            } while (playAgain == "yes");

            // Show final results
            Console.WriteLine("Total games: " + totalGames);
            Console.WriteLine("Player 1 wins: " + player1Wins);
            if (gameMode == "computer")
            {
                Console.WriteLine("Computer wins: " + player2Wins);
            }
            else
            {
                Console.WriteLine("Player 2 wins: " + player2Wins);
            }
        }

        private static void ShowPirateMessage(List<string> messages, Random random)
        {
            int index = random.Next(0, messages.Count);
            Console.WriteLine(messages[index]);
        }

        private static int GetComputerMove(int coins, Random random)
        {
            int coinsToTake = 1;

            // Try to leave a multiple of 4 coins
            if (coins > 4)
            {
                int remainder = coins % 4;
                if (remainder >= 1 && remainder <= 3)
                {
                    coinsToTake = remainder;
                }
                else if (remainder == 0)
                {
                    coinsToTake = random.Next(1, 4);
                }
            }
            else
            {
                // If 4 or fewer coins, take as many as possible up to 3
                if (coins == 1)
                {
                    coinsToTake = coins;
                }
                else
                {
                    coinsToTake = coins -1;
                }
            }

            return coinsToTake;
        }
    } 
    }