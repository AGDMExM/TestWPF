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
    /// Логика взаимодействия для AddAndEditDivisionWindow.xaml
    /// </summary>
    public partial class AddAndEditDivisionWindow : Window
    {
        public Division division = null;

        public AddAndEditDivisionWindow(ObservableCollection<Employee> employees)
        {
            InitializeComponent();

            DataContext = new AddAndEditDivisionViewModel(employees);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (ThereError())
            {
                MessageBox.Show("Ошибка: \n Все поля должны быть заполнены.");
                return;
            }
            division = new Division();

            division.Name = nameDivisionBox.Text;
            division.IdDirector = ((Employee)employeeBox.SelectedItem).Id;

            Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool ThereError()
        {
            if (string.IsNullOrEmpty(nameDivisionBox.Text))
                return true;
            if (employeeBox.SelectedItem == null)
                return true;

            return false;
        }
    }
}
