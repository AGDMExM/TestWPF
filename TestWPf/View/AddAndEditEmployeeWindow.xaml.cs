using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestWPF.Model;
using TestWPF.ViewModel;

namespace TestWPF.View
{
    /// <summary>
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddAndEditEmployeeWindow : Window
    {
        //public ObservableCollection<Employee> Employees { get; set; }

        public Employee employee = null;
        public ObservableCollection<Division> Divisions { get; set; }

        public AddAndEditEmployeeWindow(/*ObservableCollection<Employee> Employees, */ObservableCollection<Division> Divisions)
        {
            InitializeComponent();

            //this.Employees = Employees;
            this.Divisions = Divisions;

            DataContext = new AddAndEditEmployeeViewModel(/*Employees, */Divisions);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (ThereError())
            {
                MessageBox.Show("Ошибка: \n Все поля должны быть заполнены. \n Для первого сотрудника заполнение организации не обязательно.");
                return;
            }
            employee = new Employee();

            employee.Surname = surnameBox.Text;
            employee.Name = nameBox.Text;
            employee.MiddleName = middleNameBox.Text;
            employee.DateOfBirth = (DateTime)birthDateBox.SelectedDate;
            employee.Gender = ((TextBlock)(((ComboBoxItem)genderBox.SelectedItem).Content)).Text;
            //((TextBlock)(((ComboBoxItem)genderBox.SelectedItem).Content)).Text;

            if (divisionBox.SelectedItem == null)
                employee.IdDivision = null;
            else
                employee.IdDivision = ((Division)divisionBox.SelectedItem).Id;

            //Employees.Add(newEmp);

            Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool ThereError()
        {
            if (string.IsNullOrEmpty(surnameBox.Text))
                return true;
            if (string.IsNullOrEmpty(nameBox.Text))
                return true;
            if (string.IsNullOrEmpty(middleNameBox.Text))
                return true;

            if (birthDateBox.SelectedDate == null)
                return true;

            return false;
        }
    }
}
