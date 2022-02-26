using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TestWPF.Model;

namespace TestWPF.ViewModel
{
    

    public class AddAndEditDivisionViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Employee> Employees { get; set; }

        private Division _selectedDivision;

        public AddAndEditDivisionViewModel(ObservableCollection<Employee> Employees)
        {
            this.Employees = Employees;
        }

        /*public Division SelectedDivision
        {
            get => _selectedDivision;
            set
            {
                _selectedDivision = value;
                // Problem...
                if (SelectedDivision != null)
                    ((MainWindow)System.Windows.Application.Current.MainWindow).selectedNameDivision.Text = GetNameDivisionById(SelectedEmployee.IdDivision);
                //
                OnPropertyChanged("SelectedEmployee");
            }
        }*/

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
