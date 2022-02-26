using System;
using System.Collections.Generic;
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

namespace TestWPF.View
{
    /// <summary>
    /// Логика взаимодействия для AddAndEditTagWindow.xaml
    /// </summary>
    public partial class AddAndEditTagWindow : Window
    {
        public Tag tag = null;
        public AddAndEditTagWindow()
        {
            InitializeComponent();


        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(nameTagBox.Text))
            {
                MessageBox.Show("Ошибка: \n Название должно быть заполнено");
                return;
            }
            tag = new Tag();

            tag.Name = nameTagBox.Text;
            tag.Description = descriptionTagBox.Text;

            Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
