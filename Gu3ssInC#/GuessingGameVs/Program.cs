using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace GuessingGameVs
{
    class Program
    {
        //displays the tutorial of the game
        public static void tutorial()
        {
            Console.Write("\n");
            Console.WriteLine("Rules:  \n1)You have to guess the three numbers in order from 0-9 \n2)The first number can't start at 0 " +
                              "\n3)There will be no repeating numbers i.e 7,4,4 \n4)Please input your number in this format #,#,#" +
                              "\n5)If you get a number correct regardless of its position, you will get 1B." +
                              "\n6)If you get a number correct and its correct position, you will get 1A." +
                              "\n7)If you get none of the numbers correctly, you will get X" +
                              "\n8)If you get 2 correct numbers but in the wrong position, you will get 2B." +
                              "\n9)Goal is to get 3A, which is guessing all the numbers in the correct order." +
                              "\nNote: you might get 3B which is all the correct numbers but in different order");
            Console.WriteLine("Enjoy!");

            Console.Write("\n");

            Console.WriteLine("Would you like to see more examples? y for yes, n for no");
            string YorN = Console.ReadLine();

            //keeps asking for a proper input
            bool isInputValid = true;
            while (isInputValid)
            {
                if (YorN == "y") examples();
                else if (YorN == "n")gameMode();
                //keeps asking if input is invalid
                else isValidExample();
            }
        }

        //prints out additonal examples
        public static void examples()
        {
            Console.Write("\n");
            Console.WriteLine("For example, if the Numbers to guess are 4,7,9 and you input 1,2,3 then you wil get an X" +
                "\nIf you input 1,7,2 You will get 1A because 7 is in the correct position" +
                "\nIf you input 9,7,1 You will get 1A 1B because 7 is in the correct position and 9 is in a wrong position" +
                "\nIf you input 9,7,4 You will get 1A 2B");

            Console.Write("\n");
            Console.WriteLine("We will now continue to the game!");
            gameMode();
        }

        //ask user if they want to guess 3 or 4 numbers
        public static void gameMode()
        {
            Console.WriteLine("\nWhat mode do you want to play?\n1)Three Numbers \n2)Four Numbers");
            string difficulty = Console.ReadLine();
            if (difficulty == "1") ThreeNumberGuess();
            else if (difficulty == "2") fourNumberGuess();
            else
            {
                isValidGameMode();
            }
        }

        //asks for a valid input in the example question
        public static void isValidExample()
        {
            Console.WriteLine("Invalid input");
            Console.WriteLine("Would you like to see more examples? y for yes, n for no");
            string YorN = Console.ReadLine();
            if (YorN == "y") examples();
            else if (YorN == "n")
            {
                gameMode();
            }
        }

        //asks for a valid input in the tutorial question
        public static void isValidTutorial()
        {
            Console.WriteLine("Invalid input");
            Console.WriteLine("Would you like to read the tutorial? y for yes, n for no");
            string YorN = Console.ReadLine();
            if (YorN == "y") tutorial();
            else if (YorN == "n")
            {
                gameMode();
            }
        }

        //asks for a valid input for the game mode question
        public static void isValidGameMode()
        {
            Console.WriteLine("Invalid input");
            Console.WriteLine("\nWhat mode do you want to play?\n1)Three Numbers \n2)Four Numbers \n");
            string YorN = Console.ReadLine();
            if (YorN == "1") ThreeNumberGuess();
            else if (YorN == "2") fourNumberGuess();
            else
;           {
                isValidGameMode();
            }
        }

        //this is the three numbers game mode
        public static void ThreeNumberGuess()
        {
            Console.Write("\n");
            Console.WriteLine("Please input your first three numbers");
            Random random = new Random();

            //first number cannot start at 0
            int firstNumber = random.Next(1, 9);
            int secondNumber = random.Next(0, 9);
            int thirdNumber = random.Next(0, 9);

            //makes sure that computer doesnt generate same numbers
            while (firstNumber == secondNumber || firstNumber == thirdNumber)
            {
                firstNumber = random.Next(1, 9);
            }
            while (secondNumber == thirdNumber)
            {
                secondNumber = random.Next(0, 9);
            }

            //the three nubmers that will be guessed are stored in the numbersToGuess array
            int[] numbersToGuess = new int[3];
            numbersToGuess[0] = firstNumber;
            numbersToGuess[1] = secondNumber;
            numbersToGuess[2] = thirdNumber;

            bool Guess = false;
            int numberOfGuesses = 0;

            while (!Guess)
            {
                string playerInput = Console.ReadLine();
                bool isOK = Regex.IsMatch(playerInput, @"[1-9]\,[0-9]\,[0-9]");

                //keeps asking the user to input in the #,#,# format
                while(isOK == false)
                {
                    Console.WriteLine("Please enter the input in this format: #,#,#");
                    playerInput = Console.ReadLine();
                    isOK = Regex.IsMatch(playerInput, @"^[1-9]\,[0-9]\,[0-9]$");
                    if (isOK == true) continue;
                }

                //the play inputs the digits into #,#,# format and stores it in this playerDigits array
                int[] playerDigits = playerInput.Split(',').Select(n => Convert.ToInt32(n)).ToArray();

                numberOfGuesses++;

                //keeps track of numbers in the right position
                int counterA = 0;
                //keepts track of the numbers that are guessed correctly
                int counterB = 0;

                //inputs A if number is in correct position
                for (int i = 0; i < numbersToGuess.Length; i++)
                {


                    if (numbersToGuess[i] == playerDigits[i])
                    {
                        counterA++;
                    }
                }

                //finishes the game if player gets every number correctly
                if (counterA == 3)
                {
                    Console.WriteLine("Congratulations!");
                    break;
                }

                //inputs B if number is there but in a different position
                if (numbersToGuess.Contains(playerDigits[0]) && numbersToGuess[0] != playerDigits[0]) counterB++;
                if (numbersToGuess.Contains(playerDigits[1]) && numbersToGuess[1] != playerDigits[1]) counterB++;
                if (numbersToGuess.Contains(playerDigits[2]) && numbersToGuess[2] != playerDigits[2]) counterB++;

                if (counterA == 0 && counterB == 0)
                {
                    Console.WriteLine("X");
                }
                else
                {
                    Console.WriteLine(counterA + " A");
                    Console.WriteLine(counterB + " B");
                }

                Console.WriteLine("Current number of guesses " + numberOfGuesses);

                //keeps asking after 5 guesses
                if (numberOfGuesses > 5)
                {
                    Console.WriteLine("Would you like to give up? y for yes n for no");
                    string YesOrNo = Console.ReadLine();
                    if (YesOrNo == "y")
                    {
                        Guess = true;
                        break;
                    }
                    else if (YesOrNo == "n")
                    {
                        Console.WriteLine("Please enter your guess");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Game will go on. Please enter your guess");   
                    }
                }
            }
            Console.WriteLine("The numbers to guess are: ");
            Console.WriteLine(string.Join(",", numbersToGuess));
            System.Environment.Exit(1);
        }

        //this is the four numbers game mode
        public static void fourNumberGuess()
        {
            Console.Write("\n");
            Console.WriteLine("Please input your first four numbers");
            Random random = new Random();

            //first number cannot start at 0
            int firstNumber = random.Next(1, 9);
            int secondNumber = random.Next(0, 9);
            int thirdNumber = random.Next(0, 9);
            int fourthNumber = random.Next(0, 9);

            //makes sure that computer doesnt generate same numbers
            while (firstNumber == secondNumber || firstNumber == thirdNumber || firstNumber == fourthNumber)
            {
                firstNumber = random.Next(1, 9);
            }
            while (secondNumber == thirdNumber || secondNumber == firstNumber || secondNumber == fourthNumber)
            {
                secondNumber = random.Next(0, 9);
            }
            while (thirdNumber == fourthNumber || thirdNumber == firstNumber || thirdNumber == secondNumber)
            {
                thirdNumber = random.Next(0, 9);
            }

            //the four nubmers that will be guessed are stored in the numbersToGuess array
            int[] numbersToGuess = new int[4];
            numbersToGuess[0] = firstNumber;
            numbersToGuess[1] = secondNumber;
            numbersToGuess[2] = thirdNumber;
            numbersToGuess[3] = fourthNumber;

            bool Guess = false;
            int numberOfGuesses = 0;

            while (!Guess)
            {
                string playerInput = Console.ReadLine();
                bool isOK = Regex.IsMatch(playerInput, @"[1-9]\,[0-9]\,[0-9]\,[0-9]");

                //keeps asking the user to input in the #,#,#,# format
                while (isOK == false)
                {
                    Console.WriteLine("Please enter the input in this format: #,#,#,#");
                    playerInput = Console.ReadLine();
                    isOK = Regex.IsMatch(playerInput, @"[1-9]\,[0-9]\,[0-9]\,[0-9]");
                    if (isOK == true) continue;
                }

                //the play inputs the digits into #,#,#,# format and stores it in this playerDigits array
                int[] playerDigits = playerInput.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                numberOfGuesses++;

                //keeps track of numbers in the right position
                int counterA = 0;
                //keepts track of the numbers that are guessed correctly
                int counterB = 0;

                //inputs A if number is in correct position
                for (int i = 0; i < numbersToGuess.Length; i++)
                {
                    if (numbersToGuess[i] == playerDigits[i])
                    {
                        counterA++;
                    }
                }

                //finishes the game if player gets every number correctly
                if (counterA == 4)
                {
                    Console.WriteLine("Congratulations!");
                    break;
                }

                //inputs B if number is there but in a different position
                if (numbersToGuess.Contains(playerDigits[0]) && numbersToGuess[0] != playerDigits[0]) counterB++;
                if (numbersToGuess.Contains(playerDigits[1]) && numbersToGuess[1] != playerDigits[1]) counterB++;
                if (numbersToGuess.Contains(playerDigits[2]) && numbersToGuess[2] != playerDigits[2]) counterB++;
                if (numbersToGuess.Contains(playerDigits[3]) && numbersToGuess[3] != playerDigits[3]) counterB++;

                if (counterA == 0 && counterB == 0)
                {
                    Console.WriteLine("X");
                }
                else
                {
                    Console.WriteLine(counterA + " A");
                    Console.WriteLine(counterB + " B");
                }

                Console.WriteLine("Current number of guesses " + numberOfGuesses);


                //keeps asking after 7 guesses
                if (numberOfGuesses > 7)
                {
                    Console.WriteLine("Would you like to give up? y for yes n for no");
                    string YesOrNo = Console.ReadLine();
                    if (YesOrNo == "y")
                    {
                        Guess = true;
                        break;
                    }
                    else if (YesOrNo == "n")
                    {
                        Console.WriteLine("Please enter your guess");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Game will go on. Please enter your guess");
                    }
                }
            }
            Console.WriteLine("The numbers to guess are: ");
            Console.WriteLine(string.Join(",", numbersToGuess));
            System.Environment.Exit(1);
        }

        static void Main()
        {
            Console.WriteLine("Welcome to the NumberGuess game!\n");
            Console.WriteLine("Would you like to read the tutorial? y for yes, n for no");

            string YorN = Console.ReadLine();
            bool isValidInput = true;

            //keeps asking for a valid input
            while (isValidInput)
            {
                if (YorN == "y") tutorial();
                else if (YorN == "n") gameMode();
                else isValidTutorial();
            }
        }
    }
}