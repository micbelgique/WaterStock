using SummerCamp.WaterStock.MockupApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
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

namespace SummerCamp.WaterStock.MockupApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int STOREPOINT_1 = 1;
        private const int STOREPOINT_2 = 2;

        private User[] _allUsers = null;
        private StorePoint store1 = null;
        private StorePoint store2 = null;
        private string StorePointReference2 = string.Empty;
        private System.Timers.Timer _timer = new System.Timers.Timer(5000);
        private System.Timers.Timer _timerFillWater1 = new System.Timers.Timer(24);
        private System.Timers.Timer _timerFillWater2 = new System.Timers.Timer(24);

        private MediaPlayer mediaPlayer = new MediaPlayer();

        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();            

            _timer.Elapsed += async (sender, e) =>
            {
                await this.Refresh();
            };
            _timer.Start();

            _timerFillWater1.Elapsed += TimerFillWater_Elapsed;
            _timerFillWater2.Elapsed += TimerFillWater_Elapsed;

            mediaPlayer.Open(new Uri(Environment.CurrentDirectory+"\\Sounds\\wc.mp3"));
            
            this.Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TimerFillWater_Elapsed(object sender, ElapsedEventArgs e)
        {
            await Dispatcher.BeginInvoke(DispatcherPriority.Normal, new ThreadStart(() =>
            {
                if (sender == _timerFillWater1)
                {
                    if (progress1.Value >= 100)
                    {                        
                        Refresh();
                        _timerFillWater1.Stop();
                        return;
                    }
                    progress1.Value += 1;
                }

                else if (sender == _timerFillWater2)
                {
                    if (progress2.Value >= 100)
                    {                        
                        Refresh();
                        _timerFillWater2.Stop();
                        return;
                    }
                    progress2.Value += 1;
                }

            }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_allUsers == null)
                _allUsers = await DataService.Instance.GetAllUsers();

            if (sender == image1)
            {
                progress1.Value = 0;
                _timerFillWater1.Start();
                User user = _allUsers.FirstOrDefault(u => u.Badges.Any(b => b.Reference == txtUserRef1.Text));
                if(!CheckUser(user))
                {
                    ErrorWindow error = new ErrorWindow();
                    error.ShowDialog();
                    return;
                }

                mediaPlayer.Play();
                await DataService.Instance.AddConsumption(user.UserID, store1.Reference, 200);
            }
            else if (sender == image2)
            {
                progress2.Value = 0;
                _timerFillWater2.Start();
                User user = _allUsers.FirstOrDefault(u => u.Badges.Any(b => b.Reference == txtUserRef2.Text));
                if (!CheckUser(user))
                {
                    ErrorWindow error = new ErrorWindow();
                    error.ShowDialog();
                    return;
                }

                mediaPlayer.Play();
                await DataService.Instance.AddConsumption(user.UserID, store2.Reference, 400);
            }
        }

        /// <summary>
        /// Check the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool CheckUser(User user)
        {
            if(user == null)
            {
                return false;
            }

            if (user.UserID == default(int))
            {
                return false;
            }

            return true;
        }

        private async Task Refresh()
        {
            StorePoint[] storepoints = await DataService.Instance.GetStorePoints();

            store1 = storepoints.FirstOrDefault(s => s.StorePointID == STOREPOINT_1);
            store2 = storepoints.FirstOrDefault(s => s.StorePointID == STOREPOINT_2);

            // Display 
            await Dispatcher.BeginInvoke(DispatcherPriority.Normal, new ThreadStart(() =>
            {
                if (store1 != null)
                    txtStorePoint1.Text = $"{store1.Name} = {store1.QuantityLeft.ToString("0.0")}/{store1.Quantity.ToString("0.0")} ({Convert.ToInt32(store1.QuantityLeft/store1.Quantity*100)}%)";
                else
                    txtStorePoint1.Text = "Not found";

                if (store2 != null)
                    txtStorePoint2.Text = $"{store2.Name} = {store2.QuantityLeft.ToString("0.0")}/{store2.Quantity.ToString("0.0")} ({Convert.ToInt32(store2.QuantityLeft / store2.Quantity * 100)}%)";
                else
                    txtStorePoint2.Text = "Not found";
            }));

        }

        private async Task GetWater()
        {

        }

    }
}
