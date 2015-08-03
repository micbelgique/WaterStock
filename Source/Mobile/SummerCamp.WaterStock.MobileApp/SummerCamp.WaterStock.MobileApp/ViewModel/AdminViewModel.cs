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
    public class AdminViewModel : INotifyPropertyChanged
    {
        public AdminViewModel()
        {
            Load();
        }

        public async void Load()
        {
            Users = new ObservableCollection<User>();
            Groups = new ObservableCollection<Group>();
            StorePoints = new ObservableCollection<StorePoint>();

            Factory factory = new Factory();

            var users = await factory.GetUsers();
            var realUsers = users.Where(u => u.Login.CompareTo("admin") != 0);
            foreach(var user in realUsers)
            {
                Users.Add(new User(factory, user));
            }

            var groups = await factory.GetGroups();
            foreach(var group in groups)
            {
                Groups.Add(new Group(factory, group));
            }

            var storepoints = await factory.GetStorePoints();
            foreach(var storepoint in storepoints)
            {
                StorePoints.Add(new StorePoint(storepoint));
            }
        }

        private ObservableCollection<User> _users;

        public ObservableCollection<User> Users
        {
            get { return _users; }
            set { _users = value; }
        }

        private ObservableCollection<Group> _groups;

        public ObservableCollection<Group> Groups
        {
            get { return _groups; }
            set { _groups = value; }
        }

        private ObservableCollection<StorePoint> _storePoints;

        public ObservableCollection<StorePoint> StorePoints
        {
            get { return _storePoints; }
            set { _storePoints = value; }
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
