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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnNewGame_Click(object sender, RoutedEventArgs e)
        {
            btnResumeGame.Visibility = Visibility.Collapsed;
            btnSimulateGame.Visibility = Visibility.Collapsed;
            comboBoxGameType.Visibility = Visibility.Visible;
        }

        private void BtnStartGame_Click(object sender, RoutedEventArgs e)
        {
            //Pass the user game selection to the new window.
            string gameType = comboBoxGameType.Text;            
            LaunchGameWindow(gameType);
        }

        private void ComboBoxGameType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Enable Start button when the user selects a game type
            btnStartGame.Visibility = Visibility.Visible;
            
        }

        private void LaunchGameWindow(string gameType)
        {
            //Pass the user game selection to the new window.
            GameWindow gameWindow = new GameWindow(gameType);
            gameWindow.Show();
            this.Close();

        }

        private void BtnResumeGame_Click(object sender, RoutedEventArgs e)
        {
            //Get the button text and pass it to the game window
            Button button = (Button)sender;
            LaunchGameWindow(button.Content.ToString());
        }

        private void BtnSimulateGame_Click(object sender, RoutedEventArgs e)
        {
            //Get the button text and pass it to the game window
            Button button = (Button)sender;
            LaunchGameWindow(button.Content.ToString());
        }
    }
}
