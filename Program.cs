using System;

namespace NetCoreGame
{

    class Player
    {
        public string playerName;
        public System.ConsoleColor playerColor;

        public Player (string _playerName, System.ConsoleColor _playerColor)
        {
            playerName = _playerName;
            playerColor = _playerColor;
        }
    }

    class Game
    {
        public int gameRounds;
        public int gameTurns;

        public Player[] gamePlayers;

        public Game (int _gameRounds, int _gameTurns, Player[] _gamePlayers)
        {
            gameRounds = _gameRounds;
            gameTurns = _gameTurns;
            gamePlayers = _gamePlayers;
        }
    }

    class GameTurn
    {

        public Player gamePlayer;
        public int[] gameTurns;
    
        public GameTurn (Player _gamePlayer, int _gameMaxTurns) {

            Player gamePlayer = _gamePlayer;
            int[] gameTurns = new int[_gameMaxTurns];
        }
    }

    class GameRound
    {
        public GameTurn[]  gameTurns;
        
        public GameRound (int _playerCount)
        {
            GameTurn[] gameTurn = new GameTurn[_playerCount];
        }
    }

    class Program
    {
        static int rollDice(int min, int max)
        {
            Random RandomNum = new Random();
            int roll = RandomNum.Next(min, max);

            return roll;
        }  // END method rollDice

        static string checkOddEven (int valueToCheck)
        {
            //bool checkResult = false;
            string oddEven = "unset";

            if (valueToCheck % 2 == 0)
            {
                oddEven = "Even";
            }
            else
            {
                oddEven = "Odd";
            }

            //return checkResult;
            return oddEven;
        } // END method checkOddEven

        static void Main(string[] args)
        {
            // create new game
            Console.WriteLine("Number of Rounds: ");
            int maxGameRounds = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Number of Turns per Round: ");
            int maxGameTurns = Convert.ToInt32(Console.ReadLine());

            // how many contestants
            Console.WriteLine("How many players are joining (min 2, max 4)? ");
            int playerCount = Convert.ToInt32(Console.ReadLine());

            // create array with size of players
            Player[] gameContestants = new Player[playerCount];

            // create players
            for (int i = 1; i <= playerCount; i++ ) {
                
                Console.WriteLine("Name of Player" + i + ": ");
                string cPlayerName = Console.ReadLine();
                
                Console.WriteLine("Color of Player " + i + ": ");
                System.ConsoleColor cPlayerColor = (ConsoleColor) Enum.Parse(typeof(ConsoleColor), Console.ReadLine(),true);

                gameContestants[i-1] = new Player(cPlayerName,cPlayerColor);
            }

            // initiate the game
            Game currentGame = new Game(maxGameRounds,maxGameTurns,gameContestants);

            // run turns until maxRounds is reached
            for (int i = 0; i < currentGame.gameRounds; i++)
            {
                foreach (var currentPlayer in gameContestants)
                {
                    Console.ForegroundColor = currentPlayer.playerColor;
                    Console.WriteLine(currentPlayer.playerName + " is playing...\n");

                    // create turn and run for current player
                    GameTurn currentTurn = new GameTurn(currentPlayer,currentGame.gameTurns);

                    string turnState = "continue";
                    int turnCounter = 0;

                    // run turn until turnState is stop
                    while (turnState == "continue")
                    {
                        // roll dice
                        int rolledNum = rollDice(1,6);

                        int[] currentTurns = new int[currentGame.gameTurns];

                        // check if the game can continue
                        switch (checkOddEven(rolledNum))
                        {
                            case "Even":
                                // give player chance to stop or proceed
                                Console.WriteLine("You rolled a '" + rolledNum + "' | Continue (y/n)?");
                                
                                switch (Console.ReadLine())
                                {
                                    case "y":
                                        turnState = "continue";
                                        break;
                                    case "n":
                                        turnState = "stop";
                                        break;
                                    default:
                                        turnState = "continue";
                                        break;
                                }

                                // add current value to turn array
                                currentTurns[turnCounter] = rolledNum;

                                // increase turncounter
                                turnCounter++;

                                break;
                            case "Odd":
                                // i feel bad for you... maybe not
                                Console.WriteLine("You rolled a '" + rolledNum + "' | Turn is over!");

                                // update scores
                                currentTurns[turnCounter] = rolledNum;

                                // update turnstate
                                turnState = "stop";

                                break;
                            default:
                                Console.WriteLine("I DON'T KNOW HOW I GOT HERE...");
                                break;
                        } // END switch oddEven
                    } // END turnstate

                // Restore the original console colors
                Console.ResetColor();

                } // END foreach player
            } // END for gameRounds

            // ------------------------
            // all rounds played
            // show results
            // ------------------------



        } // END method Main
    } // END class Program
}