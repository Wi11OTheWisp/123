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
using System.Windows.Threading;

namespace Game
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        bool goLeft, goRight, goUp, goDown;
        int playerSpeed = 10;
        int enemiseSpead = 8;
        string lastClick = "up";
        Rect playerHitBox;

        DispatcherTimer gameTimer = new DispatcherTimer();

        Random rand = new Random();

        public MainWindow()
        {
            InitializeComponent();

            MyCanvas.Focus();

            gameTimer.Tick += GameTimerEvent;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();

            //ImageBrush playerImage = new ImageBrush();
            //playerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/player.png"));
            //player.Fill = playerImage;
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            if (goLeft == true && Canvas.GetLeft(player) > 5)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - playerSpeed);
            }
            if (goRight == true && Canvas.GetLeft(player) + (player.Width + 20) < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + playerSpeed);
            }
            //if (goUp == true && Canvas.GetTop(player) > 35)
            //{
            //   Canvas.SetTop(player, Canvas.GetTop(player) - playerSpeed);
            //}
            //if (goDown == true && Canvas.GetTop(player) + (player.Height + 43) < Application.Current.MainWindow.Height)
            //{
            //   Canvas.SetTop(player, Canvas.GetTop(player) + playerSpeed);
            //}

            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);

            foreach (var x in MyCanvas.Children.OfType<Rectangle>())
            {
                if (x is Rectangle && (string)x.Tag == "bullet")
                {

                    Canvas.SetTop(x, Canvas.GetTop(x) - 20);
                    //Canvas.SetTop(x, Canvas.GetTop(x) + 20);
                    //Canvas.SetLeft(x, Canvas.GetLeft(x) - 20);
                    //Canvas.SetLeft(x, Canvas.GetLeft(x) + 20);



                }
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                goLeft = true;
            }
            if (e.Key == Key.Right)
            {
                goRight = true;
            }
            //if (e.Key == Key.Up)
            //{
            //    goUp = true;
            //}
            //if (e.Key == Key.Down)
            //{
            //   goDown = true;
            //}
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                goLeft = false;
            }
            if (e.Key == Key.Right)
            {
                goRight = false;
            }
            //if (e.Key == Key.Up)
            //{
            //    goUp = false;
            //}
            //if (e.Key == Key.Down)
            //{
            //    goDown = false;
            //}

            if (e.Key == Key.Space)
            {
                Rectangle newBullet = new Rectangle
                {
                    Tag = "bullet",
                    Height = 5,
                    Width = 5,
                    Fill = Brushes.Black,
                };

                Canvas.SetLeft(newBullet, Canvas.GetLeft(player) + 8);
                Canvas.SetTop(newBullet, Canvas.GetTop(player) +8);

                MyCanvas.Children.Add(newBullet);
            }
        }




        private void MakeEnemise()
        {
            Rectangle newEnemy = new Rectangle
            {
                Tag = "enemy",
                Height = 20,
                Width = 20,
                Fill = Brushes.Red
            };

            Canvas.SetTop(newEnemy, rand.Next(50, 750));
            Canvas.SetLeft(newEnemy, rand.Next(50, 750));

            MyCanvas.Children.Add(newEnemy);
        }

    }
}
