﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    class Game
    {
        bool _gameOver = false;
        string _playerName = "Hero";
        int _playerHealth = 120;
        int _playerDamage = 20;
        int _playerDefense = 10;
        int levelScaleMax = 5;
        Random random;
        //Run the game
        public void Run()
        {

            while(_gameOver == false)
            {
               
                Start();
                Update();
                End();
                
            }

        }

        //After all battles checks health 
        void HealthCheck()
        {
            if (_playerHealth <= 0)
            {
                End();
            }
        }
        //This function handles the battles for our ladder. roomNum is used to update the our opponent to be the enemy in the current room. 
        //turnCount is used to keep track of how many turns it took the player to beat the enemy
        bool StartBattle(int roomNum, ref int turnCount)
        {
            //initialize default enemy stats
            int enemyHealth = 0;
            int enemyAttack = 0;
            int enemyDefense = 0;
            string enemyName = "";
            //Changes the enemy's default stats based on our current room number. 
            //This is how we make it seem as if the player is fighting different enemies
            switch (roomNum)
            {
                case 0:
                    {
                        enemyHealth = 100;
                        enemyAttack = 20;
                        enemyDefense = 5;
                        enemyName = "Wizard";
                        break;
                    }
                case 1:
                    {
                        enemyHealth = 80;
                        enemyAttack = 30;
                        enemyDefense = 13;
                        enemyName = "Troll";
                        break;
                    }
                case 2:
                    {
                        
                        enemyHealth = 200;
                        enemyAttack = 40;
                        enemyDefense = 16;
                        enemyName = "Giant";
                        break;
                    }
            }

        
            //Loops until the player or the enemy is dead
            while(_playerHealth > 0 && enemyHealth > 0)
            {
                //Displays the stats for both charactersa to the screen before the player takes their turn
                PrintStats(_playerName, _playerHealth, _playerDamage, _playerDefense);
                PrintStats(enemyName, enemyHealth, enemyAttack, enemyDefense);

                //Get input from the player
                char input;
                GetInput(out input, "Attack", "Defend");
                //If input is 1, the player wants to attack. By default the enemy blocks any incoming attack
                if(input == '1')
                {
                    BlockAttack(enemyHealth, _playerDamage, enemyDefense);
                    random = new Random();
                    int chanceToHit = random.Next(1, 20);

                   
                    if (chanceToHit < enemyDefense)
                    {
                        Console.WriteLine("You swing your best but could not land a strike!");
                        _playerDamage = 0;
                        Console.ReadKey();


                    }
                    else if (chanceToHit > enemyDefense)
                    {
                        Console.WriteLine("You swing with dead aim and strike the target");
                        enemyHealth -= _playerDamage;
                        Console.ReadKey();
                    }

                    
                }
                //If the player decides to defend the enemy just takes their turn. However this time the block attack function is
                //called instead of simply decrementing the health by the enemy's attack value.
                else
                {
                    BlockAttack(_playerHealth, enemyAttack, _playerDefense);
                    Console.WriteLine();
                    Console.WriteLine(enemyName + " dealt " + enemyAttack + " damage.");
                    Console.Write("> ");
                    Console.ReadKey();
                    turnCount++;
                    Console.Clear();
                }
                Console.Clear();
                //After the player attacks, the enemy takes its turn. Since the player decided not to defend, the block attack function is not called.
                _playerHealth -= enemyAttack;
                Console.WriteLine();
                Console.WriteLine(enemyName + " dealt " + enemyAttack + " damage.");
                Console.Write("> ");
                Console.ReadKey();
                turnCount++;
                
            }
            //Return whether or not our player died
            return _playerHealth != 0;

        }
        //Decrements the health of a character. The attack value is subtracted by that character's defense
        void BlockAttack(int enemyHealth, int _playerDamage, int enemyDefense)

        { 
      

            int damage = _playerDamage;
            if(damage < 0)
            {
                damage = 0;
            }
            
        }
        //Scales up the player's stats based on the amount of turns it took in the last battle
        void UpgradeStats(int turnCount)
        {
            if (_playerHealth <= 0)
            {

                End();
                
            }
            //Subtract the amount of turns from our maximum level scale to get our current level scale
            int scale = levelScaleMax - turnCount;
            if(scale <= 0)
            {
                scale = 1;
            }
            _playerHealth += 10 * scale;
            _playerDamage *= scale;
            _playerDefense *= scale;
            if (_playerHealth > 1)
            {

                if (turnCount < levelScaleMax)
                {
                    Console.WriteLine("\nYour speed and skill has leveled up! Also you are healed for the next fight!");
                    _playerHealth += 25;
                }
            }
           
        }

        char upgradepoint = ' ';
        void UpgradeStats(int turncount, string query)
        {
            if (turncount < levelScaleMax && _playerHealth > 0)
            {
                Console.WriteLine("Well look at this you have potetional to upgrade your skills!");
                Console.WriteLine("There is many you can upgrade damage, health, defense!");
                Console.WriteLine("Press 1 to upgrade Health");
                Console.WriteLine("Press 2 to upgrade Defense");
                Console.WriteLine("Press 3 to upgrade Damage");
               char upgradepoint = Console.ReadKey().KeyChar;
                while (upgradepoint != '1' || upgradepoint != 2 || upgradepoint != 3)
                {


                    if (upgradepoint == '1')
                    {
                        _playerHealth += 10;
                        Console.WriteLine("The fuel of your skills drive your health higher!");
                        Console.ReadKey();
                        Console.Clear();
                        _playerHealth = 110;
                        break;
                    }

                    else if (upgradepoint == '2')
                    {
                        _playerDefense += 15;
                        Console.WriteLine("The blood of battle stirs more in you increasing defense");
                        Console.ReadKey();
                        Console.Clear();
                        
                        break;
                    }
                    else if (upgradepoint == '3')
                    {
                        _playerDamage += 20;
                        
                        Console.WriteLine("\nThe rush of battle runs like a river in you, you gain more damage output");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }

                }
            } 
        }

        //Directions and input to go eitherWay
        
        //Gets input from the player
        //Out's the char variable given. This variables stores the player's input choice.
        //The parameters option1 and option 2 displays the players current chpices to the screen
        void GetInput(out char input,string option1, string option2)
        {
            //Initialize input
            input = ' ';
            //Loop until the player enters a valid input
            while (input != '1' && input != '2')
            {


                Console.WriteLine("\n1." + option1);
                Console.WriteLine("\n2." + option2);
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
            }
        }

        //Prints the stats given in the parameter list to the console
        void PrintStats(string name, int health, int damage, int defense)
        {
            Console.WriteLine("\n" + name);
            Console.WriteLine("Health: " + health);
            Console.WriteLine("Damage: " + damage);
            Console.WriteLine("Defense: " + defense);
        }

        //This is used to progress through our game. A recursive function meant to switch the rooms and start the battles inside them.
        void ClimbLadder(int roomNum)
        {
            //Displays context based on which room the player is in
            switch (roomNum)
            {
                case 0:
                    {
                        Console.WriteLine("\nA wizard blocks your path");
                        break;
                    }
                case 1:
                    {
                        Console.WriteLine("\nA troll stands before you");
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("\nA giant has appeared!");
                        break;
                    }
                default:
                    {
                        _gameOver = true;
                        return;
                    }
            }
            int turnCount = 0;
            //Starts a battle. If the player survived the battle, level them up and then proceed to the next room.
             void BattleStarting()
            {
               

                if (StartBattle(roomNum, ref turnCount))
                {
                    UpgradeStats(turnCount, "What do you want to upgrade");
                    
                    ClimbLadder(roomNum++);
                }
                _gameOver = true;
            }
        }

        //Displays the character selection menu. 
        void SelectCharacter()
        {
            char input = ' ';
            //Loops until a valid option is choosen
            while(input != '1' && input != '2' && input != '3')
            {
                //Prints options
                Console.WriteLine("Welcome! Please select a character.");
                Console.WriteLine("1.Sir Kibble");
                Console.WriteLine("2.Gnojoel");
                Console.WriteLine("3.Joedazz");
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
                //Sets the players default stats based on which character was picked
                switch (input)
                {
                    case '1':
                        {
                            _playerName = "Sir Kibble";
                            _playerHealth = 120;
                            _playerDefense = 10;
                            _playerDamage = 40;
                            break;
                        }
                    case '2':
                        {
                            _playerName = "Gnojoel";
                            _playerHealth = 40;
                            _playerDefense = 2;
                            _playerDamage = 70;
                            break;
                        }
                    case '3':
                        {
                            _playerName = "Joedazz";
                            _playerHealth = 200;
                            _playerDefense = 5;
                            _playerDamage = 25;
                            break;
                        }
                        //If an invalid input is selected display and input message and input over again.
                    default:
                        {
                            Console.WriteLine("Invalid input. Press any key to continue.");
                            Console.Write("> ");
                            Console.ReadKey();
                            break;
                        }
                }
                Console.Clear();
            }
            //Prints the stats of the choosen character to the screen before the game begins to give the player visual feedback
            PrintStats(_playerName,_playerHealth,_playerDamage,_playerDefense);
            Console.WriteLine("Press any key to continue.");
            Console.Write("> ");
            Console.ReadKey();
            Console.Clear();
        }
        //Performed once when the game begins
        public void Start()
        {
            SelectCharacter();
        }

        //Repeated until the game ends
        // The climb and battle Sqeuqence with leveling up check
        public void Update()
        {
            int turnCount = 0;
            ClimbLadder(0);
            StartBattle(0, ref turnCount);
            if (_playerHealth <= 0)
            {
                return;
            }
            UpgradeStats(turnCount, "What do you want to upgrade");
            turnCount = 0;
            ClimbLadder(1);
            StartBattle(1, ref turnCount);
            if (_playerHealth <= 0)
            {
                return;
            }
            UpgradeStats(turnCount, "What do you want to upgrade");
            turnCount = 0;
            ClimbLadder(2);
            StartBattle(2, ref turnCount);
            if (_playerHealth <= 0)
            {
                return;
            }
            turnCount = 0;
            UpgradeStats(turnCount, "What do you want to upgrade");
            ClimbLadder(3);

        }

        //Performed once when the game ends
        public void End()
        {
            //If the player died print death message
            if(_playerHealth <= 0)
            {
                Console.Clear();
                Console.WriteLine("\nYour knees hit the ground as you perish. Game Over");
                return;
            }
            //Print game over message
            Console.WriteLine("\nYour skill shows huge prmoise take care traveler you done well");
        }
    }
}
