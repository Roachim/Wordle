using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Wordle
{
    public static class Main
    {
        /// <summary>
        /// Primary method. Only public method. To be run from Program.
        /// </summary>
        public static void Run()
        {
            string correctWord = GetRandomWord();   //choose word
            int attempts = 5;       //choose amount of default attempts
            ParseGuess(correctWord, attempts);      //run the game loop
        }
      
        /// <summary>
        /// Game loop. Allows player input. 
        /// Checks player input length, validity and finaly compares it to the correct word
        /// </summary>
        /// <param name="correctWord"></param>
        /// <param name="attempts"></param>
        private static void ParseGuess(string correctWord, int attempts)
        {
            Console.WriteLine("Please write a 5 letter word");
            Console.WriteLine($"You have {attempts} attempts left");
            string guess = Console.ReadLine();
            if(guess == null)
            {
                Retry("The word must be 5 letters", correctWord, attempts);
                return;
            }
            if (!GuessIs5Letters(guess))
            {
                Retry("The word must be 5 letters", correctWord, attempts);
                return;
            }
            guess = CleanGuess(guess);  //clean word after checkin for length and not null
            correctWord = CleanGuess(correctWord);  //correct word too

            if (!IsAcceptableWord(guess))
            {
                Retry($"The word {guess} is not a valid word", correctWord, attempts);
                return;
            }

            if(IsCorrectWord(correctWord, guess))
            {
                Console.WriteLine("Congratulations! You Win!");
            }
            else
            {
                PrintHintWord(guess, correctWord);
                if (!CheckAttempt(attempts))
                {
                    GameOver();
                    return;
                }
                attempts--;
                Retry("Wrong word", correctWord, attempts);
            }
        }

        /// <summary>
        /// Gets a random word from a list of words that are line seperated in a .txt file
        /// </summary>
        /// <returns>Random word from .txt file</returns>
        private static string GetRandomWord()
        {
            string textFile = "C:\\Users\\KOM\\Desktop\\Opgaver\\Wordle\\possible_words.txt";
            // Read a text file line by line.
            string[] lines = File.ReadAllLines(textFile);

            int amountOfWords = 0;

            foreach (string line in lines)
            {
                amountOfWords++;
            }

            Random rand = new Random();


            return lines[rand.Next(amountOfWords - 1)];
        }

        /// <summary>
        /// returns a word in upper case
        /// </summary>
        /// <param name="guess"></param>
        /// <returns></returns>
        private static string CleanGuess(string guess)
        {
            return guess.ToUpper();
        }

        /// <summary>
        /// Check if guess is 5 letters. Neither shorter nor longer.
        /// </summary>
        /// <param name="guess"></param>
        /// <returns>true if 5 letters exactly, otherwise false</returns>
        private static Boolean GuessIs5Letters(string guess)
        {
            if(guess.Length < 5)
            {
                return false;
            }
            if (guess.Length > 5)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Check whether or not the word can be found in the list
        /// </summary>
        /// <param name="wordList">List of words</param>
        /// <param name="guess">The guess to check for</param>
        /// <returns>bool</returns>
        private static bool IsAcceptableWord(string guess)
        {
            string textFile = "C:\\Users\\KOM\\Desktop\\Opgaver\\Wordle\\possible_words.txt";
            // Read a text file line by line.
            string[] lines = File.ReadAllLines(textFile);

            if (lines.Contains(guess.ToLower()))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// simple check against 2 words
        /// </summary>
        /// <param name="correctWord"></param>
        /// <param name="guess">User guess</param>
        /// <returns></returns>
        private static bool IsCorrectWord(string correctWord, string guess)
        {
            if (correctWord == guess)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// User has limited guesses. Check remaining amount.
        /// </summary>
        /// <param name="attempts">Current attempts</param>
        /// <returns>True if attempts are left, otherwise false if not more attempts</returns>
        private static bool CheckAttempt(int attempts)
        {
            attempts -= 1;
            if (attempts == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Prints the word back with colorcoding 
        /// </summary>
        /// <param name="guess">Guess by user</param>
        /// <param name="correctWord">The correct word</param>
        private static void PrintHintWord(string guess, string correctWord)
        {
            
            //Console.ForegroundColor = ConsoleColor.Green;   //use this before every letter to change color
            for (int i = 0; i < 5; i++)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                if (guess.Substring(i,1) == correctWord.Substring(i, 1))
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.Write(guess.Substring(i, 1));
                } else if (correctWord.Contains(guess.Substring(i, 1)))
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.Write(guess.Substring(i, 1));
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(guess.Substring(i, 1));
                }
            }
            
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;   //reset, otherwise color carries to rest of words
            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// Write reason for having to retry, then send user back to start.
        /// </summary>
        /// <param name="reasonForLoss"></param>
        /// <param name="correctWord"></param>
        /// <param name="attempts"></param>
        private static void Retry(string reasonForLoss, string correctWord, int attempts)
        {
            Console.WriteLine(reasonForLoss);
            Console.WriteLine("Please try again");
            ParseGuess(correctWord, attempts);
        }

        /// <summary>
        /// Method for when the player has 0 attempts left
        /// </summary>
        private static void GameOver()
        {
            Console.WriteLine("No more attemps!");
            Console.WriteLine("Game Over");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("");
            Console.WriteLine("");
        }

    }
}
