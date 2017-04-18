/*
 * App Name: Slot Game
 * Author's Name: Ming Ying
 * Student#: 200258201
 * Creation Date: 17-April-2017
 * Description: A game for betting money.
 * 
 * */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlotMachine
{
    public partial class SlotMachineForm : Form
    {
        private int playerMoney = 1000;
        private int winnings = 0;
        private int jackpot = 5000;
        private float turn = 0.0f;
        private int playerBet = 0;
        private float winNumber = 0.0f;
        private float lossNumber = 0.0f;
        private string[] spinResult;
        private string fruits = "";
        private float winRatio = 0.0f;
        private float lossRatio = 0.0f;
        private int grapes = 0;
        private int bananas = 0;
        private int oranges = 0;
        private int cherries = 0;
        private int bars = 0;
        private int bells = 0;
        private int sevens = 0;
        private int blanks = 0;

        private Random random = new Random();

        public SlotMachineForm()
        {
            InitializeComponent();
        }

        /* Utility function to show Player Stats */
        private void showPlayerStats()
        {
            winRatio = winNumber / turn;
            lossRatio = lossNumber / turn;
            string stats = "";
            stats += ("Jackpot: " + jackpot + "\n");
            stats += ("Player Money: " + playerMoney + "\n");
            stats += ("Turn: " + turn + "\n");
            stats += ("Wins: " + winNumber + "\n");
            stats += ("Losses: " + lossNumber + "\n");
            stats += ("Win Ratio: " + (winRatio * 100) + "%\n");
            stats += ("Loss Ratio: " + (lossRatio * 100) + "%\n");
            MessageBox.Show(stats, "Player Stats");
            TotalLabel.Text = playerMoney.ToString();
            BetLabel.Text = playerBet.ToString();
        }

        /* Utility function to reset all fruit tallies*/
        private void resetFruitTally()
        {
            grapes = 0;
            bananas = 0;
            oranges = 0;
            cherries = 0;
            bars = 0;
            bells = 0;
            sevens = 0;
            blanks = 0;
        }

        /* Utility function to reset the player stats */
        private void resetAll()
        {
            playerMoney = 1000;
            winnings = 0;
            jackpot = 5000;
            turn = 0;
            playerBet = 0;
            winNumber = 0;
            lossNumber = 0;
            winRatio = 0.0f;

            JackpotLabel.Text = jackpot.ToString();
            BetLabel.Text = playerBet.ToString();
            TotalLabel.Text = playerMoney.ToString();
            WinnerLabel.Text = winnings.ToString();
            
        }

        /* Check to see if the player won the jackpot */
        private void checkJackPot()
        {
            /* compare two random values */
            var jackPotTry = this.random.Next(21) + 1;
            var jackPotWin = this.random.Next(21) + 1;
            if (jackPotTry == jackPotWin)
            {
                MessageBox.Show("You Won the $" + jackpot + " Jackpot!!","Jackpot!!");
                playerMoney += jackpot;
                TotalLabel.Text = playerMoney.ToString();
                jackpot = 1000;
                JackpotLabel.Text = jackpot.ToString();
            }
        }

        /* Utility function to show a win message and increase player money */
        private void showWinMessage()
        {
            playerMoney += winnings;
            WinnerLabel.Text = winnings.ToString();
            TotalLabel.Text = playerMoney.ToString();
            //MessageBox.Show("You Won: $" + winnings, "Winner!");
            resetFruitTally();
            checkJackPot();
        }

        /* Utility function to show a loss message and reduce player money */
        private void showLossMessage()
        {
            playerMoney -= playerBet;
            WinnerLabel.Text = (playerBet*-1).ToString();
            TotalLabel.Text = playerMoney.ToString();
            //MessageBox.Show("You Lost!", "Loss!");
            resetFruitTally();
            checkBetTotal();
        }

        /* Utility function to check if a value falls within a range of bounds */
        private bool checkRange(int value, int lowerBounds, int upperBounds)
        {
            return (value >= lowerBounds && value <= upperBounds) ? true : false;
            
        }

        /* When this function is called it determines the betLine results.
    e.g. Bar - Orange - Banana */
        private string[] Reels()
        {
            string[] betLine = { " ", " ", " " };
            int[] outCome = { 0, 0, 0 };

            for (var spin = 0; spin < 3; spin++)
            {
                outCome[spin] = this.random.Next(65) + 1;

               if (checkRange(outCome[spin], 1, 27)) {  // 41.5% probability
                    betLine[spin] = "blank";
                    blanks++;
                    }
                else if (checkRange(outCome[spin], 28, 37)){ // 15.4% probability
                    betLine[spin] = "Grapes";
                    grapes++;
                }
                else if (checkRange(outCome[spin], 38, 46)){ // 13.8% probability
                    betLine[spin] = "Banana";
                    bananas++;
                }
                else if (checkRange(outCome[spin], 47, 54)){ // 12.3% probability
                    betLine[spin] = "Orange";
                    oranges++;
                }
                else if (checkRange(outCome[spin], 55, 59)){ //  7.7% probability
                    betLine[spin] = "Cherry";
                    cherries++;
                }
                else if (checkRange(outCome[spin], 60, 62)){ //  4.6% probability
                    betLine[spin] = "Bar";
                    bars++;
                }
                else if (checkRange(outCome[spin], 63, 64)){ //  3.1% probability
                    betLine[spin] = "Bell";
                    bells++;
                }
                else if (checkRange(outCome[spin], 65, 65)){ //  1.5% probability
                    betLine[spin] = "Seven";
                    sevens++;
                }

            }
            return betLine;
        }

        /* This function calculates the player's winnings, if any */
        private void determineWinnings()
        {
            if (blanks == 0)
            {
                if (grapes == 3)
                {
                    winnings = playerBet * 10;
                }
                else if (bananas == 3)
                {
                    winnings = playerBet * 20;
                }
                else if (oranges == 3)
                {
                    winnings = playerBet * 30;
                }
                else if (cherries == 3)
                {
                    winnings = playerBet * 40;
                }
                else if (bars == 3)
                {
                    winnings = playerBet * 50;
                }
                else if (bells == 3)
                {
                    winnings = playerBet * 75;
                }
                else if (sevens == 3)
                {
                    winnings = playerBet * 100;
                }
                else if (grapes == 2)
                {
                    winnings = playerBet * 2;
                }
                else if (bananas == 2)
                {
                    winnings = playerBet * 2;
                }
                else if (oranges == 2)
                {
                    winnings = playerBet * 3;
                }
                else if (cherries == 2)
                {
                    winnings = playerBet * 4;
                }
                else if (bars == 2)
                {
                    winnings = playerBet * 5;
                }
                else if (bells == 2)
                {
                    winnings = playerBet * 10;
                }
                else if (sevens == 2)
                {
                    winnings = playerBet * 20;
                }
                else if (sevens == 1)
                {
                    winnings = playerBet * 5;
                }
                else
                {
                    winnings = playerBet * 1;
                }
                winNumber++;
                showWinMessage();
            }
            else
            {
                lossNumber++;
                showLossMessage();
            }

        }

        // When click the spin button
        private void SpinPictureBox_Click(object sender, EventArgs e)
        {
            //playerBet = 10; // default bet amount
            if(playerBet!=0)
            {
                if (playerMoney == 0)
                {
                    if (MessageBox.Show("You ran out of Money! \nDo you want to play again?","Out of Money!",MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        resetAll();
                        showPlayerStats();
                        checkBetTotal();
                    }
                }
                else if (playerBet > playerMoney)
                {
                    MessageBox.Show("You don't have enough Money to place that bet.", "Insufficient Funds");
                }
                else if (playerBet < 0)
                {
                    MessageBox.Show("All bets must be a positive $ amount.", "Incorrect Bet");
                }
                else if (playerBet <= playerMoney)
                {
                    spinResult = Reels();
                    fruits = spinResult[0] + " - " + spinResult[1] + " - " + spinResult[2];
                    Reel1PictureBox.Image = SlotMachine.Properties.Resources.banana;
                    setImage(spinResult[0],Reel1PictureBox);
                    setImage(spinResult[1], Reel2PictureBox);
                    setImage(spinResult[2], Reel3PictureBox);

                    MessageBox.Show(fruits);
                    determineWinnings();
                    turn++;
                    showPlayerStats();
                }
                else
                {
                    MessageBox.Show("Please enter a valid bet amount");
                }
            }
            else
            {
                MessageBox.Show("You need to choose a bet!");
                checkBetTotal();
            }
        }

        // When the game start, initialize everything
        private void SlotMachineForm_Load(object sender, EventArgs e)
        {
            Reel1PictureBox.Image = SlotMachine.Properties.Resources.blank;
            Reel1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            Reel2PictureBox.Image = SlotMachine.Properties.Resources.blank;
            Reel2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            Reel3PictureBox.Image = SlotMachine.Properties.Resources.blank;
            Reel3PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            JackpotLabel.Text = jackpot.ToString();
            TotalLabel.Text = playerMoney.ToString();
            BetLabel.Text = playerBet.ToString();
        }

        // set the picture box image by the input string
        private void setImage(String input, PictureBox pictureBox)
        {
            if(input == "Banana"){
                pictureBox.Image = SlotMachine.Properties.Resources.banana;
            }
            else if(input == "Bar"){
                pictureBox.Image = SlotMachine.Properties.Resources.bar;
            }
            else if (input == "Bell")
            {
                pictureBox.Image = SlotMachine.Properties.Resources.bell;
            }
            else if (input == "Seven")
            {
                pictureBox.Image = SlotMachine.Properties.Resources.seven;
            }
            else if (input == "Cherry")
            {
                pictureBox.Image = SlotMachine.Properties.Resources.cherry;
            }
            else if (input == "Orange")
            {
                pictureBox.Image = SlotMachine.Properties.Resources.orange;
            }
            else if (input == "Grapes")
            {
                pictureBox.Image = SlotMachine.Properties.Resources.grapes;
            }
            else
            {
                pictureBox.Image = SlotMachine.Properties.Resources.blank;
            }
        }

        // when click the reset button, reset everything
        private void ResetPictureBox_Click(object sender, EventArgs e)
        {
            resetAll();
            resetFruitTally();
            Reel1PictureBox.Image = SlotMachine.Properties.Resources.blank;
            Reel1PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            Reel2PictureBox.Image = SlotMachine.Properties.Resources.blank;
            Reel2PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            Reel3PictureBox.Image = SlotMachine.Properties.Resources.blank;
            Reel3PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            showPlayerStats();
        }

        // when click the quit button, exit this application
        private void QuitPictureBox_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // set the bet to 1
        private void Bet1PictureBox_Click(object sender, EventArgs e)
        {
            playerBet = 1;
            BetLabel.Text = playerBet.ToString();
            checkBetTotal();
        }

        // set the bet to 2
        private void Bet2PictureBox_Click(object sender, EventArgs e)
        {
            playerBet = 2;
            BetLabel.Text = playerBet.ToString();
            checkBetTotal();
        }

        // set the bet to 5
        private void Bet5PictureBox_Click(object sender, EventArgs e)
        {
            playerBet = 5;
            BetLabel.Text = playerBet.ToString();
            checkBetTotal();
        }

        // set the bet to 1
        private void Bet10PictureBox_Click(object sender, EventArgs e)
        {
            playerBet = 10;
            BetLabel.Text = playerBet.ToString();
            checkBetTotal();
        }

        // set the bet to 25
        private void Bet25PictureBox_Click(object sender, EventArgs e)
        {
            playerBet = 25;
            BetLabel.Text = playerBet.ToString();
            checkBetTotal();
        }

        // set the bet to 50
        private void Bet50PictureBox_Click(object sender, EventArgs e)
        {
            playerBet = 50;
            BetLabel.Text = playerBet.ToString();
            checkBetTotal();
        }

        // set the bet to 100
        private void Bet100PictureBox_Click(object sender, EventArgs e)
        {
            playerBet = 100;
            BetLabel.Text = playerBet.ToString();
            checkBetTotal();
        }

        // set the bet to 500
        private void Bet500PictureBox_Click(object sender, EventArgs e)
        {
            playerBet = 500;
            BetLabel.Text = playerBet.ToString();
            checkBetTotal();
        }

        // check if the total player money is less then the bet money
        public void checkBetTotal()
        {
            if (playerMoney < playerBet)
            {
                SpinPictureBox.Image = SlotMachine.Properties.Resources.spin_pressed;
            }
            else
            {
                SpinPictureBox.Image = SlotMachine.Properties.Resources.spin;
            }
        }



        
    }

}
