using Syncfusion.Pdf.Cryptography;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace SummerCamp.WaterStock.MobileApp.ViewModel
{
    public class User : INotifyPropertyChanged
    {
        Data.Factory _factory;
        public User(Data.Factory factory)
        {
            _factory = factory;
            LogsData = new ObservableCollection<LogItem>();
            ChartData = new ObservableCollection<LogItem>();

            LogsData.CollectionChanged += LogsData_CollectionChanged;

        }

        public User(Data.Factory factory, Data.User user)
        {
            _factory = factory;
            LogsData = new ObservableCollection<LogItem>();
            ChartData = new ObservableCollection<LogItem>();

            LogsData.CollectionChanged += LogsData_CollectionChanged;

            Load(user);
        }

        private void LogsData_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ChartData.Clear();
            var grouped = LogsData.GroupBy(l => l.Date.Date);
            foreach (var group in grouped)
            {
                double sum = group.Sum(l => l.Volume);
                ChartData.Add(new LogItem() { Date = group.Key.AddHours(12), Volume = sum });
            }

            DateTime now = DateTime.Now;
            CurrentUsage = LogsData.Where(l => l.Date.Year == now.Year && l.Date.Month == now.Month).Sum(l => l.Volume);

        }

        private ObservableCollection<Badge> _badges;

        public ObservableCollection<Badge> Badges
        {
            get { if (_badges == null) { _badges = new ObservableCollection<Badge>(); } return _badges; }
            set
            {
                _badges = value;
                OnPropertyChanged(nameof(Badges));
            }
        }

        private ObservableCollection<LogItem> _logsData;

        public ObservableCollection<LogItem> LogsData
        {
            get { return _logsData; }
            set
            {
                _logsData = value;
                OnPropertyChanged("LogsData");
            }
        }

        private ObservableCollection<LogItem> _chartData;

        public ObservableCollection<LogItem> ChartData
        {
            get { return _chartData; }
            set
            {
                _chartData = value;
                OnPropertyChanged("ChartData");
            }
        }
        

        public bool IsDisabled
        {
            get { return !_isEnabled; }
        }

        private bool _isEnabled;

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (_isEnabled.CompareTo(value) != 0)
                {
                    _isEnabled = value;
                    OnPropertyChanged(nameof(IsEnabled));
                    OnPropertyChanged(nameof(IsDisabled));
                }
            }
        }

        private int _userID;

        public int UserID
        {
            get { return _userID; }
            set
            {
                if (_userID.CompareTo(value) != 0)
                {
                    _userID = value;
                    OnPropertyChanged(nameof(UserID));
                }
            }
        }



        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName?.CompareTo(value) != 0)
                {
                    _userName = value;
                    OnPropertyChanged("UserName");
                }
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password?.CompareTo(value) != 0)
                {
                    _password = value;
                    OnPropertyChanged("Password");
                }
            }
        }


        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName?.CompareTo(value) != 0)
                {
                    _firstName = value;
                    OnPropertyChanged("FirstName");
                }
            }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName?.CompareTo(value) != 0)
                {
                    _lastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }


        private string _mail;

        public string Mail
        {
            get { return _mail; }
            set
            {
                if (_mail?.CompareTo(value) != 0)
                {
                    _mail = value;
                    OnPropertyChanged(nameof(Mail));
                    OnPropertyChanged(nameof(ImageSource));
                }
            }
        }

        private string MD5Hash(string toHash)
        {
            MD5 md5 = new MD5();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(toHash);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public string ImageSource
        {
            get
            {
                if (string.IsNullOrEmpty(Mail))
                {
                    return string.Empty;
                }
                else
                {
                    return string.Format("http://www.gravatar.com/avatar/{0}", MD5Hash(Mail));
                }
            }
        }

        private double _currentBalance;

        public double CurrentBalance
        {
            get { return _currentBalance; }
            set
            {
                if (_currentBalance.CompareTo(value) != 0)
                {
                    _currentBalance = value;
                    OnPropertyChanged("CurrentBalance");
                    OnPropertyChanged("BalanceColor");
                    OnPropertyChanged("CurentBalanceColor");
                }
            }
        }

        public Brush CurentBalanceColor
        {
            get
            {
                return new SolidColorBrush(BalanceColor);
            }
        }
        public Color BalanceColor
        {
            get
            {
                if (CurrentBalance < 5)
                {
                    return Colors.Red;
                }
                else if (CurrentBalance < 10)
                {
                    return Colors.Orange;
                }
                else
                {
                    return Colors.Green;
                }
            }
        }

        private double _currentUsage;

        public double CurrentUsage
        {
            get { return _currentUsage; }
            set
            {
                if (_currentUsage.CompareTo(value) != 0)
                {
                    _currentUsage = value;
                    OnPropertyChanged("CurrentUsage");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Load(Data.User user)
        {
            this.UserID = user.UserID;
            this.UserName = user.Login;
            this.FirstName = user.Firstname;
            this.LastName = user.Lastname;
            this.Mail = user.Email;
            this.IsEnabled = user.IsEnabled;
            this.CurrentBalance = user.CreditsLeft;
            Load(user.Badges);
        }

        public void Load(Data.Consumption[] consumptions)
        {
            LogsData.Clear();
            foreach (Data.Consumption consumption in consumptions)
            {
                LogsData.Add(new LogItem() { Adress = $"{consumption.Name} {consumption.Location}", Date = Convert.ToDateTime(consumption.RecordedDate), Volume = consumption.QuantityAdded });
            }
        }

        public void Load(Data.Badge[] badges)
        {
            Badges.Clear();
            foreach (Data.Badge badge in badges)
            {
                Badges.Add(new Badge() { BadgeID = badge.BadgeID, Reference = badge.Reference});
            }
        }
    }
}
