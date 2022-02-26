using Microsoft.EntityFrameworkCore;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestWPF.Model;
using TestWPF.View;
using TestWPF.ViewModel;

namespace TestWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel mainWindowViewModel;
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowViewModel();
            mainWindowViewModel = (MainWindowViewModel)DataContext;
        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            AddAndEditEmployeeWindow addEmployee = new AddAndEditEmployeeWindow(mainWindowViewModel.Divisions);

            addEmployee.Owner = this;
            addEmployee.ShowDialog();
            if (addEmployee.employee != null)
            {
                using (WaterCarrierContext context = new WaterCarrierContext())
                {
                    context.Employees.Add(addEmployee.employee);
                    //mainWindowViewModel.Employees.Add(addEmployee.employee);
                    context.SaveChanges();

                    //mainWindowViewModel.Employees = new ObservableCollection<Employee>(context.Employees.ToList());
                    mainWindowViewModel.Employees.Clear();
                    foreach (Employee currEmp in context.Employees.ToList())
                    {
                        mainWindowViewModel.Employees.Add(currEmp);
                    }
                }
                
            }
            
        }

        private void EditEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindowViewModel.SelectedEmployee == null)
            {
                MessageBox.Show("Работник не выбран");
                return;
            }    

            AddAndEditEmployeeWindow editEmployee = new AddAndEditEmployeeWindow(mainWindowViewModel.Divisions);

            editEmployee.Owner = this;

            editEmployee.surnameBox.Text = mainWindowViewModel.SelectedEmployee.Surname;
            editEmployee.nameBox.Text = mainWindowViewModel.SelectedEmployee.Name;
            editEmployee.middleNameBox.Text = mainWindowViewModel.SelectedEmployee.MiddleName;
            editEmployee.birthDateBox.SelectedDate = mainWindowViewModel.SelectedEmployee.DateOfBirth;

            if (mainWindowViewModel.SelectedEmployee.Gender == "Мужской")
            {
                ((TextBlock)(((ComboBoxItem)editEmployee.genderBox.SelectedItem).Content)).Text = "Мужской";
                   ((TextBlock)(((ComboBoxItem)editEmployee.genderBox.Items[1]).Content)).Text = "Женский";
            }
            else
            {
                ((TextBlock)(((ComboBoxItem)editEmployee.genderBox.SelectedItem).Content)).Text = "Женский";
                ((TextBlock)(((ComboBoxItem)editEmployee.genderBox.Items[1]).Content)).Text = "Мужской";
            }

            ((TextBlock)(((ComboBoxItem)editEmployee.genderBox.SelectedItem).Content)).Text = mainWindowViewModel.SelectedEmployee.Gender;

            if (mainWindowViewModel.SelectedEmployee.IdDivision != null)
                editEmployee.divisionBox.SelectedItem = GetDivisionById((int)mainWindowViewModel.SelectedEmployee.IdDivision);

            editEmployee.ShowDialog();

            using (WaterCarrierContext context = new WaterCarrierContext())
            {
                Employee emp = context.Employees.Where(c => c.Id == mainWindowViewModel.SelectedEmployee.Id).FirstOrDefault();

                emp.Surname = editEmployee.surnameBox.Text;
                emp.Name = editEmployee.nameBox.Text;
                emp.MiddleName = editEmployee.middleNameBox.Text;
                emp.DateOfBirth = (DateTime)editEmployee.birthDateBox.SelectedDate;
                emp.Gender = ((TextBlock)(((ComboBoxItem)editEmployee.genderBox.SelectedItem).Content)).Text;

                if (editEmployee.divisionBox.SelectedItem == null)
                    emp.IdDivision = null;
                else
                    emp.IdDivision = ((Division)editEmployee.divisionBox.SelectedItem).Id;

                context.SaveChanges();

                mainWindowViewModel.Employees.Clear();
                foreach (Employee currEmp in context.Employees.ToList())
                {
                    mainWindowViewModel.Employees.Add(currEmp);
                }
            }
        }

        private void AddDivisionButton_Click(object sender, RoutedEventArgs e)
        {
            AddAndEditDivisionWindow addDivisionWindow = new AddAndEditDivisionWindow(mainWindowViewModel.Employees);

            addDivisionWindow.Owner = this;
            addDivisionWindow.ShowDialog();
            if (addDivisionWindow.division != null)
            {
                using (WaterCarrierContext context = new WaterCarrierContext())
                {
                    context.Divisions.Add(addDivisionWindow.division);
                    //mainWindowViewModel.Employees.Add(addEmployee.employee);
                    context.SaveChanges();

                    //mainWindowViewModel.Employees = new ObservableCollection<Employee>(context.Employees.ToList());
                    mainWindowViewModel.Divisions.Clear();
                    foreach (Division currDiv in context.Divisions.ToList())
                    {
                        mainWindowViewModel.Divisions.Add(currDiv);
                    }
                }

            }

        }

        private void EditDivisionButton_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindowViewModel.SelectedDivision == null)
            {
                MessageBox.Show("Организация не выбрана");
                return;
            }

            
            AddAndEditDivisionWindow addDivisionWindow = new AddAndEditDivisionWindow(mainWindowViewModel.Employees);

            addDivisionWindow.Owner = this;

            addDivisionWindow.nameDivisionBox.Text = mainWindowViewModel.SelectedDivision.Name;
            /*foreach (Employee emp in mainWindowViewModel.Employees)
            {
                addDivisionWindow.employeeBox.Items.Add(emp);
            }*/
            addDivisionWindow.employeeBox.SelectedItem = mainWindowViewModel.SelectedEmployee;

            addDivisionWindow.ShowDialog();

            if (string.IsNullOrEmpty(addDivisionWindow.division.Name) || addDivisionWindow.division.IdDirector == null)
            {

                return;
            }

            using (WaterCarrierContext context = new WaterCarrierContext())
            {
                Division div = context.Divisions.Where(c => c.Id == mainWindowViewModel.SelectedDivision.Id).FirstOrDefault();

                div.Name = addDivisionWindow.nameDivisionBox.Text;
                div.IdDirector = ((Employee)addDivisionWindow.employeeBox.SelectedItem).Id;

                context.SaveChanges();

                mainWindowViewModel.Divisions.Clear();
                foreach (Division currDiv in context.Divisions.ToList())
                {
                    mainWindowViewModel.Divisions.Add(currDiv);
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        private void AddTagButton_Click(object sender, RoutedEventArgs e)
        {
            AddAndEditTagWindow addTagWindow = new AddAndEditTagWindow();

            addTagWindow.Owner = this;
            addTagWindow.ShowDialog();
            if (addTagWindow.tag != null)
            {
                using (WaterCarrierContext context = new WaterCarrierContext())
                {
                    context.Tags.Add(addTagWindow.tag);
                    //mainWindowViewModel.Employees.Add(addEmployee.employee);
                    context.SaveChanges();

                    //mainWindowViewModel.Employees = new ObservableCollection<Employee>(context.Employees.ToList());
                    mainWindowViewModel.Tags.Clear();
                    foreach (Tag currTag in context.Tags.ToList())
                    {
                        mainWindowViewModel.Tags.Add(currTag);
                    }
                }

            }

        }

        private void EditTagButton_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindowViewModel.SelectedTag == null)
            {
                MessageBox.Show("Тег не выбран");
                return;
            }


            AddAndEditTagWindow addTagWindow = new AddAndEditTagWindow();

            addTagWindow.Owner = this;

            addTagWindow.nameTagBox.Text = mainWindowViewModel.SelectedTag.Name;

            addTagWindow.descriptionTagBox.Text = mainWindowViewModel.SelectedTag.Description;

            addTagWindow.ShowDialog();

            if (string.IsNullOrEmpty(addTagWindow.nameTagBox.Text) || string.IsNullOrEmpty(addTagWindow.descriptionTagBox.Text))
                return;

            using (WaterCarrierContext context = new WaterCarrierContext())
            {
                Tag tag = context.Tags.Where(c => c.Id == mainWindowViewModel.SelectedTag.Id).FirstOrDefault();

                tag.Name = addTagWindow.nameTagBox.Text;
                tag.Description = addTagWindow.descriptionTagBox.Text;

                context.SaveChanges();

                mainWindowViewModel.Tags.Clear();
                foreach (Tag currTag in context.Tags.ToList())
                {
                    mainWindowViewModel.Tags.Add(currTag);
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            AddAndEditOrderWindow addOrderWindow = new AddAndEditOrderWindow(mainWindowViewModel.Employees, mainWindowViewModel.Tags);

            addOrderWindow.Owner = this;
            addOrderWindow.tipsForTag.ToolTip = mainWindowViewModel.GetStrTags(mainWindowViewModel.Tags);
            addOrderWindow.ShowDialog();
            if (addOrderWindow.order != null)
            {
                using (WaterCarrierContext context = new WaterCarrierContext())
                {
                    //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT tags ON;");
                    //context.Orders.Add(addOrderWindow.order);

                    context.Database.ExecuteSqlRaw($"insert into orders values ({addOrderWindow.order.Number}, " +
                        $"'{addOrderWindow.order.Name}', {addOrderWindow.order.IdEmployee});");

                    foreach (Tag tag in addOrderWindow.order.IdTags)
                    {
                        foreach (Order ord in tag.IdOrders)
                        {
                            context.Database.ExecuteSqlRaw($"insert into tags_for_orders values ({tag.Id}, {ord.Id});");
                        }
                        
                    }
                    context.SaveChanges();
                    //context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT tags OFF;");

                    mainWindowViewModel.Orders.Clear();
                    foreach (Order currOrd in context.Orders.ToList())
                    {
                        mainWindowViewModel.Orders.Add(currOrd);
                    }
                }

            }

        }

        private void EditOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindowViewModel.SelectedOrder == null)
            {
                MessageBox.Show("Заказ не выбран");
                return;
            }


            AddAndEditOrderWindow addOrderWindow = new AddAndEditOrderWindow(mainWindowViewModel.Employees, mainWindowViewModel.Tags);

            addOrderWindow.Owner = this;

            addOrderWindow.numberBox.Text = mainWindowViewModel.SelectedOrder.Number.ToString();
            addOrderWindow.nameBox.Text = mainWindowViewModel.SelectedOrder.Name;
            addOrderWindow.tagsBox.Text = mainWindowViewModel.GetStrTags(mainWindowViewModel.SelectedOrder.IdTags);
            addOrderWindow.employeeBox.SelectedItem = GetEmployeeById(mainWindowViewModel.SelectedOrder.IdEmployee);
            addOrderWindow.tipsForTag.ToolTip = mainWindowViewModel.GetStrTags(mainWindowViewModel.Tags);

            addOrderWindow.ShowDialog();

            if (addOrderWindow.order == null)
                return;

            if (string.IsNullOrEmpty(addOrderWindow.numberBox.Text) ||
                string.IsNullOrEmpty(addOrderWindow.nameBox.Text) ||
                string.IsNullOrEmpty(addOrderWindow.tagsBox.Text) ||
                addOrderWindow.employeeBox.SelectedItem == null)
                return;

            if (!int.TryParse(addOrderWindow.numberBox.Text, out _))
                return;

            using (WaterCarrierContext context = new WaterCarrierContext())
            {
                Order ord = context.Orders.Where(c => c.Id == mainWindowViewModel.SelectedOrder.Id).FirstOrDefault();

                ord.Number = Convert.ToInt32(addOrderWindow.numberBox.Text);
                ord.Name = addOrderWindow.nameBox.Text;
                //ord.IdTags = GetCollectionTagsFromNames(addOrderWindow.tagsBox.Text);
                ord.IdEmployee = ((Employee)addOrderWindow.employeeBox.SelectedItem).Id;

                foreach (Tag tag in addOrderWindow.order.IdTags)
                {
                    foreach (Order currOrd in tag.IdOrders)
                    {
                        context.Database.ExecuteSqlRaw($"insert into tags_for_orders values ({tag.Id}, {ord.Id});");
                    }

                }

                context.SaveChanges();

                mainWindowViewModel.Orders.Clear();
                foreach (Order currOrd in context.Orders.ToList())
                {
                    mainWindowViewModel.Orders.Add(currOrd);
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////

        private ICollection<Tag> GetCollectionTagsFromNames(string str)
        {
            List<Tag> result = new List<Tag>();
            List<string> names = str.Split().ToList();

            foreach (Tag tag in mainWindowViewModel.Tags)
            {
                if (names.Contains(tag.Name))
                    result.Add(tag);
            }

            return result;
        }

        private Division GetDivisionById(int id)
        {
            foreach (Division div in mainWindowViewModel.Divisions)
            {
                if (div.Id == id)
                    return div;
            }
            
            return null;
        }
        
        private Employee GetEmployeeById(int id)
        {
            foreach (Employee emp in mainWindowViewModel.Employees)
            {
                if (emp.Id == id)
                    return emp;
            }

            return null;
        }
    }
}