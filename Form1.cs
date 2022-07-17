using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace Guess_A_Number
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static int roundswon;
        private static int roundslost;
        private static int guesses;
        private static int hints;
        private static int minutes;
        private static int seconds;
        private static int difficulty;
        private static int number;
        private static int guessednumber;
        private static bool gamerunning;

        private void StartGame()
        {
            if (gamerunning)
            {
                return;
            }
            Random rnd = new Random();
            gamerunning = true;
            switch (difficulty)
            {
                case 1: label6.Text = "4"; label8.Text = "3"; label10.Text = "00"; label7.Text = "3"; MessageBox.Show("Number Is Between 1 and 15. You have 4 Guesses and 3 minutes!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information); number = rnd.Next(1, 15); break;
                case 2: label6.Text = "3"; label8.Text = "2"; label10.Text = "00"; label7.Text = "2"; MessageBox.Show("Number Is Between 1 and 30. You have 3 Guesses and 2 minutes!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information); number = rnd.Next(1, 30); break;
                case 3: label6.Text = "2"; label8.Text = "1"; label10.Text = "00"; label7.Text = "1"; MessageBox.Show("Number Is Between 1 and 50. You have 2 Guesses and 1 minutes!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information); number = rnd.Next(1, 50); break;
                case 4: label6.Text = "1"; label8.Text = "0"; label10.Text = "30"; label7.Text = "0"; MessageBox.Show("Number Is Between 1 and 100. You have 1 Guess and 30 Seconds!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information); number = rnd.Next(1, 100); break;
            }
            timer1.Start();

        }
        private void Parse()
        {
            int.TryParse(textBox1.Text, out guessednumber);
            int.TryParse(label6.Text, out guesses);
            int.TryParse(label12.Text, out roundswon);
            int.TryParse(label13.Text, out roundslost);
            int.TryParse(label8.Text, out minutes);
            int.TryParse(label10.Text, out seconds);
            int.TryParse(label7.Text, out hints);
        }
        public static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            easyToolStripMenuItem.Checked = true;
            mediumToolStripMenuItem.Checked = false;
            hardToolStripMenuItem.Checked = false;
            extremeToolStripMenuItem.Checked = false;
            difficulty = 1;
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            easyToolStripMenuItem.Checked = false;
            mediumToolStripMenuItem.Checked = true;
            hardToolStripMenuItem.Checked = false;
            extremeToolStripMenuItem.Checked = false;
            difficulty = 2;
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            easyToolStripMenuItem.Checked = false;
            mediumToolStripMenuItem.Checked = false;
            hardToolStripMenuItem.Checked = true;
            extremeToolStripMenuItem.Checked = false;
            difficulty = 3;
        }

        private void extremeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            easyToolStripMenuItem.Checked = false;
            mediumToolStripMenuItem.Checked = false;
            hardToolStripMenuItem.Checked = false;
            extremeToolStripMenuItem.Checked = true;
            difficulty = 4;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!gamerunning)
            {
                return;
            }
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please Enter A Numer!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Parse();
            if (!Regex.IsMatch(textBox1.Text, "^[1-9]+$"))
            {
                MessageBox.Show("Please Enter Only Numerical Values!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (guessednumber == 0)
            {
                MessageBox.Show("Please Enter A Value Greater Than 0!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            switch (difficulty)
            {
                case 1:
                    if (guessednumber > 15)
                    {
                        MessageBox.Show("Please Enter A Value Less Than 15!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                case 2:
                    if (guessednumber > 30)
                    {
                        MessageBox.Show("Please Enter A Value Less Than 30!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                case 3:
                    if (guessednumber > 50)
                    {
                        MessageBox.Show("Please Enter A Value Less Than 50!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
                case 4:
                    if (guessednumber > 100)
                    {
                        MessageBox.Show("Please Enter A Value Less Than 100!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;
            }


            if (guessednumber != number)
            {
                guesses = guesses - 1;
                label6.Text = guesses.ToString();
                if (guesses == 0)
                {
                    timer1.Stop();
                    gamerunning = false;
                    MessageBox.Show("You've Ran Out Of Guesses!" + " " + number.ToString() + " " + "Was The Number!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    roundslost = roundslost + 1;
                    label13.Text = roundslost.ToString();
                    DialogResult result = MessageBox.Show("Would You Like To Play Again?", "Play Again?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        StartGame();
                    }
                    return;
                }
                MessageBox.Show("Try Again!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (guessednumber == number)
            {
                gamerunning = false;
                timer1.Stop();
                MessageBox.Show("Correct!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                roundswon = roundswon + 1;
                label12.Text = roundswon.ToString();
                DialogResult result = MessageBox.Show("Would You Like To Play Again?", "Play Again?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    StartGame();
                }
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.ResetText();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Parse();
            if (minutes == 0 & seconds == 0)
            {
                timer1.Stop();
                MessageBox.Show("Time Is Up!" + " " + number.ToString() + " " + "Was The Number!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gamerunning = false;
                int.TryParse(label13.Text, out roundslost);
                roundslost = roundslost + 1;
                label13.Text = roundslost.ToString();
                DialogResult result = MessageBox.Show("Would You Like To Play Again?", "Play Again?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    StartGame();
                }
                return;
            }
            if (seconds == 0)
            {
                label10.Text = "60";
                Parse();
                if (minutes != 0)
                {
                    minutes = minutes - 1;
                    label8.Text = minutes.ToString();
                }
            }
            seconds = seconds - 1;
            if (seconds < 10)
            {
                label10.Text = "0" + seconds.ToString();
                return;
            }
            label10.Text = seconds.ToString();
        }
        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gamerunning)
            {
                gamerunning = false;
                timer1.Stop();
                StartGame();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            easyToolStripMenuItem.Checked = true;
            difficulty = 1;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Parse();
            if (!gamerunning)
            {
                return;
            }
            if (hints != 0)
            {
                switch (hints)
                {
                    case 3:
                        hints = hints - 1;
                        label7.Text = hints.ToString();
                        if (number % 2 == 0)
                        {
                            MessageBox.Show("Number Is An Even Number.", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (number % 2 != 0)
                        {
                            MessageBox.Show("Number Is An Odd Number.", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case 2:
                        hints = hints - 1;
                        label7.Text = hints.ToString();
                        if (number >= 10)
                        {
                            MessageBox.Show("Number Is Two Digits.", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (number < 10)
                        {
                            MessageBox.Show("Number Is One Digit.", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        break;
                    case 1:
                        hints = hints - 1;
                        label7.Text = hints.ToString();
                        if (IsPrime(number))
                        {
                            MessageBox.Show("Number Is A Prime Number.", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (!IsPrime(number))
                        {
                            MessageBox.Show("Number Is A Composite Number.", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                }
            }
            else if (hints == 0)
            {
                MessageBox.Show("You Don't Have Any Hints Left!", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
