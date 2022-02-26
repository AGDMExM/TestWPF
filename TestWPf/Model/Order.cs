using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestWPF.Model
{
    public partial class Order
    {
        public Order()
        {
            IdTags = new HashSet<Tag>();
        }

        private int _id { get; set; }
        private int _number { get; set; }
        private string _name { get; set; } = null!;
        private int _idEmployee { get; set; }

        public virtual Employee IdEmployeeNavigation { get; set; } = null!;

        public virtual ICollection<Tag> IdTags { get; set; }

        public int Id
        {
            get => _id;
            set
            {
                if (value == _id)
                    return;

                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public int Number
        {
            get => _number;
            set
            {
                if (value == _number)
                    return;

                _number = value;
                OnPropertyChanged("Number");
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (value == _name)
                    return;

                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public int IdEmployee
        {
            get => _idEmployee;
            set
            {
                if (value == _idEmployee)
                    return;

                _idEmployee = value;
                OnPropertyChanged("IdEmployee");
            }
        }

        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
