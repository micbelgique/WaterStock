using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerCamp.WaterStock.MobileApp.ViewModel
{
    public class Group : INotifyPropertyChanged
    {

        private int _groupId = 0;
        private string _name;

        private Data.Factory _factory;

        public Group(Data.Factory factory, Data.Group group)
        {
            _factory = factory;

            this.GroupID = group.GroupID;
            this.Name = group.Name;
        }

        public int GroupID
        {
            get
            {
                return _groupId;
            }
            set
            {
                if (_groupId.CompareTo(value) != 0)
                {
                    _groupId = value;
                    OnPropertyChanged("GroupID");
                }
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name?.CompareTo(value) != 0)
                {
                    _name = value;
                    OnPropertyChanged("Name");
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
    }
}
