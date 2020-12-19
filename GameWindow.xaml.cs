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
using System.Windows.Threading;
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

        //Object for all the xml attributes
        XmlAttributes xmlElements;

        //File name for xml
        string xmlFileName;

        //Counter for steps
        int counter;

        public GameWindow(string gameType)
        {
            InitializeComponent();

           
            GameType = gameType;
            List<string> cellContent = new List<string>();
            xmlHandler = new XmlHandler();
            

            if(gameType == "User - User")
            {
                //Initialize the xml attribute
                xmlElements = new XmlAttributes();

                //trim the extra characters from the user string
                xmlFileName = gameType.Replace(" ", "");
                
                NewGame();
                CanvasUserComputer.Visibility = Visibility.Collapsed;
                CanvasUserUser.Visibility = Visibility.Visible;
                CanvasComputerComputer.Visibility = Visibility.Collapsed;
                CanvasSimulateGame.Visibility = Visibility.Collapsed;
            }
            else if(gameType == "User - Computer")
            {
                //Initialize the xml attribute
                xmlElements = new XmlAttributes();

                //trim the extra characters from the user string
                xmlFileName = gameType.Replace(" ", "");

                NewGame();
                CanvasUserComputer.Visibility = Visibility.Collapsed;
                CanvasUserUser.Visibility = Visibility.Visible;
                CanvasComputerComputer.Visibility = Visibility.Collapsed;
                CanvasSimulateGame.Visibility = Visibility.Collapsed;
            }
            else if(gameType == "Computer - Computer")
            {
                //Initialize the xml attribute
                xmlElements = new XmlAttributes();

                //trim the extra characters from the user string
                xmlFileName = gameType.Replace(" ", "");

                NewGame();

                CanvasUserComputer.Visibility = Visibility.Collapsed;
                CanvasUserUser.Visibility = Visibility.Collapsed;
                CanvasComputerComputer.Visibility = Visibility.Visible;
                CanvasSimulateGame.Visibility = Visibility.Collapsed;

                ComputerToComputer();
            }
            else if(gameType == "Resume Game")
            {

            }
            else if(gameType == "Simulate Game")
            {
                //Initialize the xml attribute
                xmlElements = new XmlAttributes();

                //trim the extra characters from the user string
                xmlFileName = gameType.Replace(" ", "");

                CanvasUserComputer.Visibility = Visibility.Collapsed;
                CanvasUserUser.Visibility = Visibility.Collapsed;
                CanvasComputerComputer.Visibility = Visibility.Collapsed;
                CanvasSimulateGame.Visibility = Visibility.Visible;

                //This will create a new file even this is a simulation, need to work on this.
                NewGame();

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

            //Get the current time in hours.
            String hourMinute = DateTime.Now.ToString("mm");
            //Do not create a file if its a simulation
            if(!(xmlFileName == "SimulateGame"))
            {
                //Add the file name to xmlattributes class
                xmlElements.FileName = "../../xmlFiles/" + xmlFileName + hourMinute + ".xml";
                //MessageBox.Show(xmlElements.FileName);
                xmlHandler.CreateXMLFile(xmlElements);
            }

            //New array to store all the results.
            gameResults = new Mark[9];
            allButtons = null;
            player1 = false;
            isGameFinisihed = false;
            ClearAllCells();
            counter = 0;

            //Set the time as 2 seconds by default for the xml node
            xmlElements.StepTime = 2.ToString();
        }

        private void CellButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if(GameType == "User - User")
            {
                //Set the player type for xml element
                xmlElements.PlayerType = "user";
                UserClicks(button);
            }
            else if(GameType == "User - Computer")
            {
                xmlElements.PlayerType = "computer";

                //Click the user selected button first.
                UserClicks(button);
                bool status = false;

                //Get all the buttons in a list
                allButtons = gridContainer?.Children.Cast<Button>().ToList();
                while (!status)
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
                Delay(2);

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

        private void SimulateGame()
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

                    string counter = (i + 1).ToString();

                    //Get the coordinates from xml file.
                    string coordinates = xmlHandler.ReadXml(xmlFileName,counter);
                    int column = Convert.ToInt32(Char.GetNumericValue(coordinates[0]));
                    int row = Convert.ToInt32(Char.GetNumericValue(coordinates[1]));

                    status = SimulateClick(GetChildren(column,row),column,row);
                }
                //create a two second delay
                Delay(2);
            }

        }

        private bool SimulateClick(Button button, int column, int row)
        {

            //Find the index
            var index = column + (row * 3);
            

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

        private bool ComputerClicks(Button button)
        {
            //Get the row and column number of the clicked button
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            //Add the row and column to the xml attribute class
            xmlElements.PlayLocation = column.ToString() + row.ToString();

            //Find the index
            var index = column + (row * 3);

            //Return if the cell is already having a value;
            if (gameResults[index] != Mark.Blank)
            {
                return false; ;
            }

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

            //Increment the step id
            counter++;

            //Add a two second delay
            Delay(2);

            // change the button text
            button.Content = player1 ? "O" : "X";

            //Set the play sign for the xml element
            xmlElements.PlaySign = player1 ? "O" : "X";

            //Add the player details to xml element class
            xmlElements.PlayerID = player1 ? "Player1" : "Computer";

            //toggle the player1 status
            player1 ^= true;

            xmlElements.StepID = counter;
            xmlHandler.AppendToRoot(xmlElements);
            CheckGameStatus();

            return true;
        }

        private void UserClicks(Button button)
        {          

            //Get the row and column number of the clicked button
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            //Add the row and column to the xml attribute class
            xmlElements.PlayLocation = column.ToString() + row.ToString();
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

            //Increment the step id
            counter++;

            // change the button text
            button.Content = player1 ? "O" : "X";

            //Update the Button UI
            UpdateUI();

            //Set the play sign for the xml element
            xmlElements.PlaySign = player1 ? "O" : "X";

            //Add the player details to xml element class
            xmlElements.PlayerID = player1 ? "Player2" : "Player1";

            //toggle the player1 status
            player1 ^= true;

            xmlElements.StepID = counter;
            xmlHandler.AppendToRoot(xmlElements);
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

        private void Delay(int seconds)
        { 
            DateTime end = DateTime.Now.AddSeconds(seconds);
            int x = 0;
            while (DateTime.Now <= end)
            {
                x++;
                continue;
            }

            //Update the buttons
            UpdateUI();

        }

        //Update the UI as soon as the button content changes.
        public void UpdateUI()
        {
            Dispatcher.Invoke(new Action(() => { }), DispatcherPriority.ContextIdle, null);
        }

        private void BtnLoadGame_Click(object sender, RoutedEventArgs e)
        {
            xmlFileName = xmlHandler.LoadFile();
            //MessageBox.Show(xmlFileName);
        }

        private void BtnSimulateStartGame_Click(object sender, RoutedEventArgs e)
        {
            if(xmlFileName == null)
            {
                MessageBox.Show("File not loaded");
                return;
            }
            SimulateGame();
        }

        //Get the button from from the grid coordinates
        private Button GetChildren( int column, int row)
        {
            Button button = new Button();
            foreach (UIElement child in gridContainer.Children)
            {
                if (Grid.GetRow(child) == row && Grid.GetColumn(child) == column)
                {
                    return (Button)child;
                }
            }
            return button;
        }
    }
}
