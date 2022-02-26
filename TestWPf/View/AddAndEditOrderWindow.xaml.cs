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
    /// Логика взаимодействия для AddAndEditOrderWindow.xaml
    /// </summary>
    public partial class AddAndEditOrderWindow : Window
    {
        public Order order = null;
        private ObservableCollection<Tag> tags;
        public AddAndEditOrderWindow(ObservableCollection<Employee> employees, ObservableCollection<Tag> tags)
        {
            InitializeComponent();

            this.tags = tags;

            DataContext = new AddAndEditOrderViewModel(employees);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (ThereError())
            {
                MessageBox.Show("Ошибка: \n Поля - номер, название и сотрудник должны быть заполнены.");
                return;
            }
            order = new Order();

            order.Number = Convert.ToInt32(numberBox.Text);
            order.Name = nameBox.Text;
            order.IdTags = GetCollectionTagsFromNames(tagsBox.Text.Replace(",", ""));
            order.IdEmployee = ((Employee)employeeBox.SelectedItem).Id;

            Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool ThereError()
        {
            if (string.IsNullOrEmpty(numberBox.Text))
                return true;

            if (!int.TryParse(numberBox.Text, out _))
                return true;

            if (string.IsNullOrEmpty(nameBox.Text))
                return true;
            if (employeeBox.SelectedItem == null)
                return true;

            return false;
        }

        private ICollection<Tag> GetCollectionTagsFromNames(string str)
        {
            List<Tag> result = new List<Tag>();
            List<string> names = str.Split().ToList();

            foreach (Tag tag in tags)
            {
                if (names.Contains(tag.Name))
                    result.Add(tag);
            }

            return result;
        }
    }
}
