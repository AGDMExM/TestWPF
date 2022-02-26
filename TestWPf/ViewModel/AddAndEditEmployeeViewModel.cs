using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWPF.Model;

namespace TestWPF.ViewModel
{
    public class AddAndEditEmployeeViewModel
    {
        public ObservableCollection<Division> Divisions { get; set; }
        //public ObservableCollection<Employee> Employees { get; set; }

        public AddAndEditEmployeeViewModel(/*ObservableCollection<Employee> Employees, */ObservableCollection<Division> Divisions)
        {
            //this.Employees = Employees;
            this.Divisions = Divisions;
        }


    }
}
