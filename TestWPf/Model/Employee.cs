using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestWPF.Model
{
    public partial class Employee : INotifyPropertyChanged
    {
        private int _id;
        private string _surname = null!;
        private string _name = null!;
        private string _middleName = null!;
        private DateTime _dateOfBirth;
        private string _gender = null!;
        private int? _idDivision;

        public Employee()
        {
            Divisions = new HashSet<Division>();
            Orders = new HashSet<Order>();
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

        public string Surname
        {
            get => _surname;
            set
            {
                if (value == _surname)
                    return;

                _surname = value;
                OnPropertyChanged("Surname");
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

        public string MiddleName
        {
            get => _middleName;
            set
            {
                if (value == _middleName)
                    return;

                _middleName = value;
                OnPropertyChanged("MiddleName");
            }
        }

        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                if (value == _dateOfBirth)
                    return;

                _dateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
            }
        }

        public string Gender
        {
            get => _gender;
            set
            {
                if (value == _gender)
                    return;

                _gender = value;
                OnPropertyChanged("Gender");
            }
        }

        public int? IdDivision
        {
            get => _idDivision;
            set
            {
                if (value == _idDivision)
                    return;

                _idDivision = value;
                OnPropertyChanged("IdDivision");
            }
        }

        public virtual Division? IdDivisionNavigation { get; set; }
        public virtual ICollection<Division> Divisions { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
