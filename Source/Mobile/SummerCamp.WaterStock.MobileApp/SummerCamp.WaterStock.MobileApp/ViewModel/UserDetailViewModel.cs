using SummerCamp.WaterStock.MobileApp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerCamp.WaterStock.MobileApp.ViewModel
{
    public class UserDetailViewModel : INotifyPropertyChanged
    {
        Factory _factory;

        public UserDetailViewModel()
        {
            _factory = new Factory();
        }

        public async void Load(int userID)
        {
            var user = await _factory.GetUser(userID);
            CurrentUser.Load(user);
        }
        
        private User _user;

        public User CurrentUser
        {
            get
            {
                if (_user == null)
                {
                    _user = new User(_factory);
                }
                return _user;
            }
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
