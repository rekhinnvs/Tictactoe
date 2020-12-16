using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TicTacToe.Classes;
namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        //holds the result
        private Mark[] gameResults;
        private bool player1;
        private bool isGameFinisihed;

        public GameWindow(string gameType)
        {
            //string gameType
            InitializeComponent();
           // lbltest.Content = gameType;
        }

        public GameWindow()
        {
            //string gameType
            InitializeComponent();
            // lbltest.Content = gameType;
            NewGame();
        }

        private void NewGame()
        {
            //New array to store all the results.
            gameResults = new Mark[9];

            for (int i = 0; i < gameResults.Length; i++)
            {
                gameResults[i] = Mark.Blank;
            }

            //Get all the buttons inside the grid container and add it to a list and iterate through it.
            gridContainer?.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
            });

        }

        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;

            //Get the row and column number of the clicked button
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            //Find the index
            var index = column + (row * 3);

            //Return if the cell is already having a value;
            if(gameResults[index] != Mark.Blank)
            {
                return;
            }

            //set the cell value based on the player.
            gameResults[index] = player1 ? Mark.Cross : Mark.Zero;

            //Change the button text color
            if(player1)
            {
                button.Foreground = Brushes.Red;
            }
            else
            {
                button.Foreground = Brushes.Black;
            }

            // change the button text
            button.Content = player1 ? "X" : "O";

            //toggle the player1 status
            player1 ^= true;


            CheckGameStatus();
        }

        //Check if the game is finished or not.
        private void CheckGameStatus()
        {
           // MessageBox.Show(gameResults[0].ToString() + gameResults[1].ToString() + gameResults[2].ToString());
           // if ((gameResults[0] & gameResults[1] & gameResults[2]) == gameResults[0]) MessageBox.Show("success");
            //Check if the first row matches
            if ((gameResults[0] != Mark.Blank) && (gameResults[0] & gameResults[1] & gameResults[2]) == gameResults[0])
            {
                isGameFinisihed = true;
                MessageBox.Show("Success");
                //change the button background color.
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Yellow;
            }
        }
    }
}
