using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestWPF.Model
{
    public partial class Division : INotifyPropertyChanged
    {
        private int _id;
        private string _name = null!;
        private int _idDirector;

        public Division()
        {
            Employees = new HashSet<Employee>();
        }

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

        public int IdDirector
        {
            get => _idDirector;
            set
            {
                if (value == _idDirector)
                    return;

                _idDirector = value;
                OnPropertyChanged("IdDirector");
            }
        }

        public virtual Employee IdDirectorNavigation { get; set; } = null!;
        public virtual ICollection<Employee> Employees { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
