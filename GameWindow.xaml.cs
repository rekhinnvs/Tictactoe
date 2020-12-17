using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        Random random;
        private bool player1;
        private bool isGameFinisihed;
        List<Button> allButtons;
        string GameType;
        XmlHandler xmlHandler;

        //File name for xml
        string xmlFileName;

        public GameWindow(string gameType)
        {
            InitializeComponent();
            GameType = gameType;
            List<string> cellContent = new List<string>();
            xmlHandler = new XmlHandler();
            

            if(gameType == "User - User")
            {
                //trim the extra characters from the user string
                xmlFileName = gameType.Replace(" ", "");
                //Get the current time in hours.
                String hourMinute = DateTime.Now.ToString("mm");
                xmlHandler.CreateXMLFile("../../xmlFiles/"+xmlFileName+hourMinute+".xml");
                NewGame();
                CanvasUserUser.Visibility = Visibility.Visible;
            }
            else if(gameType == "User - Computer")
            {
                NewGame();
                CanvasUserUser.Visibility = Visibility.Visible;
            }
            else if(gameType == "Computer - Computer")
            {
                NewGame();
                CanvasUserUser.Visibility = Visibility.Collapsed;
                CanvasComputerComputer.Visibility = Visibility.Visible;
               
            }
            else if(gameType == "Resume Game")
            {

            }
            else if(gameType == "Simulate Game")
            { 
            }
           
            
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
            allButtons = null;
            player1 = false;
            isGameFinisihed = false;
            ClearAllCells();
        }

        private void CellButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if(GameType == "User - User")
            {
                UserClicks(button);
            }
            else if(GameType == "User - Computer")
            {
                Console.WriteLine(GameType);
                //Click the user selected button first.
                UserClicks(button);
                bool status = false;
                //Get all the buttons in a list
                allButtons = gridContainer?.Children.Cast<Button>().ToList();
                while(!status)
                {
                    if (isGameFinisihed) break;
                    random = new Random();
                    int randomInt = random.Next(0, 9);
                    status = ComputerClicks((Button)allButtons[randomInt]);
                }

            }
            else
            {
                return;
            }
        }


        private void ComputerToComputer()
        {
            
            //Get all the buttons in a list
            allButtons = gridContainer?.Children.Cast<Button>().ToList();
            //Iterate 9 times
            for (int i = 0; i < 9; i++)
            {
                bool status = false;
                if (isGameFinisihed) break;
                while (!status)
                {
                    if (isGameFinisihed) break;
                    random = new Random();
                    int randomInt = random.Next(0, 9);
                    
                    status = ComputerClicks((Button)allButtons[randomInt]);
                }

            }
            
        }


        private bool ComputerClicks(Button button)
        {
            //Get the row and column number of the clicked button
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            //Find the index
            var index = column + (row * 3);

            //Return if the cell is already having a value;
            if (gameResults[index] != Mark.Blank)
            {
                return false; ;
            }

            //set the cell value based on the player.
            Thread.Sleep(2000);
            gameResults[index] = player1 ? Mark.Zero : Mark.Cross;

            //Change the button text color
            if (player1)
            {
                
                button.Foreground = Brushes.Red;

            }
            else
            {
                button.Foreground = Brushes.Black;

            }

            // change the button text
            button.Content = player1 ? "O" : "X";

            //toggle the player1 status
            player1 ^= true;
            CheckGameStatus();

            return true;
        }

        private void UserClicks(Button button)
        {          

            //Get the row and column number of the clicked button
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            //Find the index
            var index = column + (row * 3);

            //Return if the cell is already having a value;
            if (gameResults[index] != Mark.Blank)
            {
                return;
            }

            //set the cell value based on the player.
            gameResults[index] = player1 ? Mark.Zero : Mark.Cross;

            //Change the button text color
            if (player1)
            {
                button.Foreground = Brushes.Red;

            }
            else
            {
                button.Foreground = Brushes.Black;

            }

            // change the button text
            button.Content = player1 ? "O" : "X";

            //toggle the player1 status
            player1 ^= true;

            CheckGameStatus();
        }

        //Check if the game is finished or not.
        private void CheckGameStatus()
        {

            //Check if the first row matches
            if ((gameResults[0] != Mark.Blank) && (gameResults[0] & gameResults[1] & gameResults[2]) == gameResults[0])
            {
                
                //change the button background color.
                Button0_0.Foreground = Button0_1.Foreground = Button0_2.Foreground = Brushes.Green;
                isGameFinisihed = true;
                MessageBox.Show("Game completed");
                return;
            }

            //Check if second row matches
            if ((gameResults[3] != Mark.Blank) && (gameResults[3] & gameResults[4] & gameResults[5]) == gameResults[3])
            {

                //change the button background color.
                Button1_0.Foreground = Button1_1.Foreground = Button1_2.Foreground = Brushes.Green;
                isGameFinisihed = true;
                MessageBox.Show("Game completed");
                return;
            }

            //Check if third row matches
            if ((gameResults[6] != Mark.Blank) && (gameResults[6] & gameResults[7] & gameResults[8]) == gameResults[6])
            {

                //change the button background color.
                Button2_0.Foreground = Button2_1.Foreground = Button2_2.Foreground = Brushes.Green;
                isGameFinisihed = true;
                MessageBox.Show("Game completed");
                return;
            }

            //Check if first column matches
            if ((gameResults[0] != Mark.Blank) && (gameResults[0] & gameResults[3] & gameResults[6]) == gameResults[0])
            {

                //change the button background color.
                Button0_0.Foreground = Button1_0.Foreground = Button2_0.Foreground = Brushes.Green;
                isGameFinisihed = true;
                MessageBox.Show("Game completed");
                return;
            }

            //Check if second column matches
            if ((gameResults[1] != Mark.Blank) && (gameResults[1] & gameResults[4] & gameResults[7]) == gameResults[1])
            {

                //change the button background color.
                Button0_1.Foreground = Button1_1.Foreground = Button2_1.Foreground = Brushes.Green;
                isGameFinisihed = true;
                MessageBox.Show("Game completed");
                return;
            }


            //Check if third column matches
            if ((gameResults[2] != Mark.Blank) && (gameResults[2] & gameResults[5] & gameResults[8]) == gameResults[2])
            {

                //change the button background color.
                Button0_2.Foreground = Button1_2.Foreground = Button2_2.Foreground = Brushes.Green;
                isGameFinisihed = true;
                MessageBox.Show("Game completed");
                return;
            }

            //Check if first diagonal matches
            if ((gameResults[0] != Mark.Blank) && (gameResults[0] & gameResults[4] & gameResults[8]) == gameResults[0])
            {

                //change the button background color.
                Button0_0.Foreground = Button1_1.Foreground = Button2_2.Foreground = Brushes.Green;
                isGameFinisihed = true;
                MessageBox.Show("Game completed");
                return;
            }

            //Check if second diagonal matches
            if ((gameResults[2] != Mark.Blank) && (gameResults[2] & gameResults[4] & gameResults[6]) == gameResults[2])
            {

                //change the button background color.
                Button0_2.Foreground = Button1_1.Foreground = Button2_0.Foreground = Brushes.Green;
                isGameFinisihed = true;
                MessageBox.Show("Game completed");
                return;
            }

            //Check if game is finished without winning.
            if (!gameResults.Any(result => result == Mark.Blank))
            {
                isGameFinisihed = true;
                MessageBox.Show("Game draw!");
                return;
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
            

        }

        private void ClearAllCells()
        {
            //Get all the buttons inside the grid container and add it to a list and iterate through it.
            gridContainer?.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
            });
        }

        private void BtnStartGame_Click(object sender, RoutedEventArgs e)
        {
            ComputerToComputer();
        }
    }
}
