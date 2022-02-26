using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWPF.Model;

namespace TestWPF.ViewModel
{
    public class AddAndEditOrderViewModel
    {
        public ObservableCollection<Employee> Employees { get; set; }

        public AddAndEditOrderViewModel(ObservableCollection<Employee> Employees)
        {
            this.Employees = Employees;
        }



    }
}
