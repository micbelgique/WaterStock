using SummerCamp.WaterStock.MobileApp.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerCamp.WaterStock.MobileApp.ViewModel
{
    public class LambdaViewModel : INotifyPropertyChanged
    {
        Factory _factory;

        public LambdaViewModel()
        {
            Load();
        }

        public async void Load()
        {

            _factory = new Factory();
            var users = await _factory.GetUsers();
            var user = users.First(u => u.Login.CompareTo("admin") != 0);
            CurrentUser.Load(user);

            var consumption = await _factory.GetConsumption(user.UserID, 30);
            CurrentUser.Load(consumption);
        }

        private User _user;

        public User CurrentUser
        {
            get {
                if(_user == null)
                {
                    _user = new User(_factory);
                }
                return _user; }
            set
            {
                _user = value;
                OnPropertyChanged("CurrentUser");
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
    }
}
