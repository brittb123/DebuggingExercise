using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorld
{
    public struct Item
    {
        public string dagger;
        public int daggerdmg;
        public string sword;
        public int sworddmg;
        public string bow;
        public int bowdmg;
    }
    //players struct for health, damage, armor, and name
    public struct Player
    {
        public string playername;
        public int health;
        public int armor;
        public int damage;

    }
    class Game
    {
        //Items all in a struct 
     
        //Weapon select for player 1 then player 2
        int currentWeaponDmg = ' ';
        char weaponusing1 = ' ';
        int WeaponSelect1()
        {
            while (weaponusing1 != '1' || weaponusing1 != '2' || weaponusing1 != '3')
            {

                Console.WriteLine("Now that we know who you two are" + Player1.playername + "Choose a weapon");
                Console.WriteLine("Press 1 for a dagger");
                Console.WriteLine("Press 2 for a sword");
                Console.WriteLine("Press 3 for a bow");
                weaponusing1 = Console.ReadKey().KeyChar;
                if (weaponusing1 == '1')
                {
                    Player1.damage = 25;
                    return Player1.damage;
                }
               else if (weaponusing1 == '2')
                {
                    Player1.damage = 20;
                    return Player1.damage;
                }
                else if (weaponusing1 == '3')
                {
                    Player1.damage = 20;
                    return Player1.damage;
                }
                
            }
             return Player1.damage;
        }
        int WeaponSelect()
        {
            while (weaponusing1 != '1' || weaponusing1 != '2' || weaponusing1 != '3')
            {

                Console.WriteLine("Now that the first chosen " + Player2.playername + " Choose a weapon");
                Console.WriteLine("Press 1 for a dagger");
                Console.WriteLine("Press 2 for a sword");
                Console.WriteLine("Press 3 for a bow");
                weaponusing1 = Console.ReadKey().KeyChar;
                if (weaponusing1 == '1')
                {
                    Player2.damage = 25;
                    return Player2.damage;
                }
                else if (weaponusing1 == '2')
                {
                    Player2.damage = 30;
                    return Player2.damage;
                }
                else if (weaponusing1 == '3')
                {
                    Player2.damage = 40;
                    return Player2.damage;
                }

            }
            return Player2.damage;
        }
        //weapon damage values changing this will change damagw output
       
        //players health and armor change to modify
        void PlayerValues()
        {
            Player1.health = 100;
            Player2.health = 100;
            Player1.armor = 20;
            Player2.armor = 20;

        }
        bool _gameOver = false;
        string _playerName = "Hero";
        int _playerHealth = 120;
        int _playerDamage = 20;
        int _playerDefense = 10;
        int levelScaleMax = 5;
        //Player One's stats
      Player Player1;
       Player Player2;

       string Player1Names(ref Player player1)
        {

            Console.WriteLine("Please type your name player 1!");

            Player1.playername = Console.ReadLine();
            Console.WriteLine("Player's Name: " + Player1.playername);
   
            return Player1.playername;

        }
        string Player2Name(ref Player player2)
        {

            Console.WriteLine("Please type your name player 2!");

            Player2.playername = Console.ReadLine();
            Console.WriteLine("Player's Name: " + Player2.playername);
            return Player2.playername;

        }


        
        Random random;
        //Run the game
        public void Run()
        {

            while(_gameOver == false)
            {
               
                Start();
               
                Battle();
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
        bool StartBattle()
        {
            //initialize default enemy stats
            int enemyHealth = 0;
            int enemyAttack = 0;
            int enemyDefense = 0;
            string enemyName = "";
          
        
            //Loops until the player or the enemy is dead
            while(Player1.health > 0 && Player2.health > 0)
            {
                //Displays the stats for both charactersa to the screen before the player takes their turn
                PrintStats(Player1.playername, Player1.health, Player1.damage, Player1.armor);
                PrintStats(Player2.playername, Player2.health, Player2.damage, Player2.armor);

                //Get input from the player
                char input;
                GetInput(out input, "Attack", "Defend");
                //If input is 1, the player wants to attack. By default the enemy blocks any incoming attack
                if(input == '1')
                {
                    BlockAttack(enemyHealth, _playerDamage, enemyDefense);
                    Console.WriteLine("\nYou dealt " + _playerDamage + " damage.");
                    enemyHealth -= _playerDamage;
                    Console.Write("> ");
                    Console.ReadKey();
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
                    
                    Console.Clear();
                }
                Console.Clear();
                //After the player attacks, the enemy takes its turn. Since the player decided not to defend, the block attack function is not called.
                _playerHealth -= enemyAttack;
                Console.WriteLine();
                Console.WriteLine(enemyName + " dealt " + enemyAttack + " damage.");
                Console.Write("> ");
                Console.ReadKey();
                
                
            }
            //Return whether or not our player died
            return _playerHealth != 0;

        }
        //Decrements the health of a character. The attack value is subtracted by that character's defense
        void BlockAttack(int enemyHealth, int _playerDamage, int enemyDefense)
        {
            int damage = _playerDamage - enemyDefense;
            if(damage < 0)
            {
                damage = 0;
            }
            enemyHealth -= _playerDamage;
        }

        void Battle()
        {
           while (Player1.health >= 0 && Player2.health >= 0)
            {
                char input = ' ';

                Console.Clear();
                Console.WriteLine("\nPlayer One: " + Player1.playername);
                Console.WriteLine(Player1.playername + "'s health:" + Player1.health);
                Console.WriteLine(Player1.playername + "'s armor: " + Player1.armor);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("\nPlayer Two: " + Player2.playername);
                Console.WriteLine(Player1.playername + "'s health:" + Player2.health);
                Console.WriteLine(Player2.playername + "'s armor: " + Player2.armor);
                Console.WriteLine("\nPlayer One's Turn:");
                Console.WriteLine("\nPress 1 to attack or Press 2 to defend");
                input = Console.ReadKey().KeyChar;
                if (input == '1')
                {
                    if (Player1.damage > Player2.armor)
                    {
                        Console.WriteLine(Player1.playername + " jumps and front flips whil attacking and pierced" +
                            Player2.playername + "'s armor");
                        Console.WriteLine(Player1.playername + " dealt " + Player1.damage);
                        Player2.health -= Player1.damage;
                    }
                    else
                    {
                        Console.WriteLine(Player1.playername + "didn't deal damage to health but breaks oppenents armor");
                        Player2.armor -= 5;
                    }
                }

                if (input == '2')
                {
                    Console.WriteLine(Player1.playername + "Braces for damage, boosting defense!");
                    Player1.armor += 5;
                }
                Console.WriteLine("\nPlayer One: " + Player1.playername);
                Console.WriteLine(Player1.playername + "'s health:" + Player1.health);
                Console.WriteLine(Player1.playername + "'s armor: " + Player1.armor);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("\nPlayer Two: " + Player2.playername);
                Console.WriteLine(Player1.playername + "'s health:" + Player2.health);
                Console.WriteLine(Player2.playername + "'s armor: " + Player2.armor);
                Console.WriteLine("Player Two's Turn");
                Console.WriteLine("Press 1 to attack or Press 2 to defend");
                char input2 = ' ';
                input2 = Console.ReadKey().KeyChar;
                if (input2 == '1')
                {
                    if (Player2.damage > Player1.armor)
                    {
                        Console.WriteLine();
                        Console.WriteLine(Player2.playername + "combat rolls and gets a piercing hit through" +
                            Player1.playername + "'s armor");
                        Console.WriteLine(Player2.playername + " dealt " + Player2.damage);
                        Player1.health -= Player2.damage;

                    }
                }

                if (input2 == '2')
                {
                    Console.WriteLine(Player1.playername + "Braces for damage boosting defense!");
                    Player2.armor += 5;
                }
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
            Player1Names(ref Player1);
            Player2Name(ref Player2);
            PlayerValues();
            WeaponSelect1();
            WeaponSelect();
           
            
            


        }

        //Repeated until the game ends
        // The climb and battle Sqeuqence with leveling up check
        public void Update()
        {
            Console.WriteLine("Welcome to the arena! Player 1 please put your name");
            Battle();
           
        }

        //Performed once when the game ends
        public void End()
        {
            if (Player1.health < 0)
                Console.WriteLine("Congrats Player 1 you fought and came out victorios!");
            else
            {
                Console.WriteLine("Congrats Player 2 You reigned over your oppenent!");
            }
        }
        


    }
}
