using GameMultiplayer.Client.Connections;
using GameMultiplayer.Client.Domain;
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

namespace GameMultiplayer.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Player _player;

        private Rectangle _rectangle;

        private Connection _connection;

        public Connection Connection { get => _connection; set => _connection = value; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = true;
        }

        private void GameRenderer(Game game)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (_player is null)
                    return;

                Players.ItemsSource = game.Players.Select(x => new 
                {
                    Id = x.Id,
                    Name = x.Name,
                    Points = x.Points,
                    Color = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)x.Color.R, (byte)x.Color.G, (byte)x.Color.B))
                })
                .OrderByDescending(x => x.Points);

                NumberOfPlayers.Text = $"{game.Players.Count()} Players";
                Platform.Children.Clear();

                foreach (Player player in game.Players)
                {
                    Rectangle rectangle = CreatePlayer(player);
                    Platform.Children.Add(rectangle);

                    if (player.Id == _player.Id)
                    {
                        PlatformPosition.Text = player.Position.ToString();
                        _rectangle = rectangle;
                    }
                     
                }

                PlatformGoals.Text = $"Alvos: {game.Goals.Count()}";
                foreach (Goal goal in game.Goals)
                {
                    Rectangle rectangle = CreateGoal(goal);
                    Platform.Children.Add(rectangle);
                }

            });

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private async void Close_Click(object sender, RoutedEventArgs e)
        {
            if(Connection.IsOpen)
                await Connection.Desconect(_player);

            this.Close();
        }

        private async void Start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(MyName.Text))
                    return;

                await Connection.Open(GameRenderer);
                _player = await Connection.RegisterPlayer(MyName.Text);
                await Connection.Update();
                DialogHost.IsOpen = false;
                this.KeyUp += Window_KeyUp;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private Rectangle CreatePlayer(Player player)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.ToolTip = player.Name;
            rectangle.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb((byte)player.Color.R, (byte)player.Color.G, (byte)player.Color.B));
            Grid.SetColumn(rectangle, player.Position.Column);
            Grid.SetRow(rectangle, player.Position.Row);
            return rectangle;
        }

        private Rectangle CreateGoal(Goal goal)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 255, 0));
            Grid.SetColumn(rectangle, goal.Position.Column);
            Grid.SetRow(rectangle, goal.Position.Row);
            return rectangle;
        }

        private async void Window_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (_player is null)
                    return;
                
                string direction = e.Key.ToString();
                _player.Position.Move(direction);
                PlatformPosition.Text = _player.Position.ToString();

                Grid.SetColumn(_rectangle, _player.Position.Column);
                Grid.SetRow(_rectangle, _player.Position.Row);
                await Connection.MovePlayer(_player, direction);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

    }

}
