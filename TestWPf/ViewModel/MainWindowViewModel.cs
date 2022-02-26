using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestWPF.Model;
using TestWPF.View;

namespace TestWPF.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        //public WaterCarrierContext Context { get; }

        private Employee _selectedEmployee;
        private Division _selectedDivision;
        private Order _selectedOrder;
        private Tag _selectedTag;

        public string SelectedNameDivisionFromEmployee { get; set; }

        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
        public ObservableCollection<Division> Divisions { get; set; } = new ObservableCollection<Division>();
        public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();
        public ObservableCollection<Tag> Tags { get; set; } = new ObservableCollection<Tag>();

        public MainWindowViewModel()
        {
            using (WaterCarrierContext Context = new WaterCarrierContext())
            {
                Employees = new ObservableCollection<Employee>(Context.Employees.ToList());
                Divisions = new ObservableCollection<Division>(Context.Divisions.ToList());
                Orders = new ObservableCollection<Order>(Context.Orders.ToList());
                Tags = new ObservableCollection<Tag>(Context.Tags.ToList());

                /*foreach (Order ord in Orders)
                {
                    try
                    {
                        List<int> tagsForOrder = Context.Tags.FromSqlRaw($"select tags_for_orders.idTag from tags_for_orders where tags_for_orders.idOrder = {ord.Id};").ToList();

                        foreach (Tag tag in tagsForOrder)
                        {
                            ord.IdTags.Add(tag);
                        }
                    }
                    catch
                    {

                    }          
                    
                }*/
            }
            //Employees.CollectionChanged += ItemsOnCollectionChanged;
        }

        private RelayCommand removeEmployeeCommand;
        public RelayCommand RemoveEmployeeCommand
        {
            get
            {
                return removeEmployeeCommand ??
                    (removeEmployeeCommand = new RelayCommand(obj =>
                    {
                        Employee emp = obj as Employee;
                        if (emp != null)
                        {
                            using (WaterCarrierContext context = new WaterCarrierContext())
                            {
                                context.Employees.Remove(emp);
                                context.SaveChanges();
                                Employees.Clear();
                                foreach (Employee currEmp in context.Employees.ToList())
                                {
                                    Employees.Add(currEmp);
                                }
                                //Employees = new ObservableCollection<Employee>(context.Employees.ToList());
                            }
                            /*Employees.Remove(emp);
                            Context.Employees.Remove(emp);
                            Context.SaveChanges();*/
                        }
                    },
                    (obj) => Employees.Count > 0));
            }
        }

        private RelayCommand removeDivisionCommand;
        public RelayCommand RemoveDivisionCommand
        {
            get
            {
                return removeDivisionCommand ??
                    (removeDivisionCommand = new RelayCommand(obj =>
                    {
                        Division div = obj as Division;
                        if (div != null)
                        {
                            using (WaterCarrierContext context = new WaterCarrierContext())
                            {
                                context.Divisions.Remove(div);
                                context.SaveChanges();
                                Divisions.Clear();
                                foreach (Division currDiv in context.Divisions.ToList())
                                {
                                    Divisions.Add(currDiv);
                                }
                                //Employees = new ObservableCollection<Employee>(context.Employees.ToList());
                            }
                            /*Employees.Remove(emp);
                            Context.Employees.Remove(emp);
                            Context.SaveChanges();*/
                        }
                    },
                    (obj) => Divisions.Count > 0));
            }
        }

        private RelayCommand removeTagCommand;
        public RelayCommand RemoveTagCommand
        {
            get
            {
                return removeTagCommand ??
                    (removeTagCommand = new RelayCommand(obj =>
                    {
                        Tag tag = obj as Tag;
                        if (tag != null)
                        {
                            using (WaterCarrierContext context = new WaterCarrierContext())
                            {
                                context.Tags.Remove(tag);
                                context.SaveChanges();
                                Tags.Clear();
                                foreach (Tag currTag in context.Tags.ToList())
                                {
                                    Tags.Add(currTag);
                                }
                            }
                        }
                    },
                    (obj) => Tags.Count > 0));
            }
        }

        private RelayCommand removeOrderCommand;
        public RelayCommand RemoveOrderCommand
        {
            get
            {
                return removeOrderCommand ??
                    (removeOrderCommand = new RelayCommand(obj =>
                    {
                        Order ord = obj as Order;
                        if (ord != null)
                        {
                            using (WaterCarrierContext context = new WaterCarrierContext())
                            {
                                context.Orders.Remove(ord);
                                context.SaveChanges();
                                Orders.Clear();
                                foreach (Order currOrd in context.Orders.ToList())
                                {
                                    Orders.Add(currOrd);
                                }
                            }
                        }
                    },
                    (obj) => Orders.Count > 0));
            }
        }

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                // Problem...
                if (SelectedEmployee != null)
                    ((MainWindow)System.Windows.Application.Current.MainWindow).selectedNameDivision.Text = GetNameDivisionById(SelectedEmployee.IdDivision);
                //
                OnPropertyChanged("SelectedEmployee");
            }
        }

        public Division SelectedDivision
        {
            get => _selectedDivision;
            set
            {
                _selectedDivision = value;
                // Problem...
                if (SelectedDivision != null)
                    ((MainWindow)System.Windows.Application.Current.MainWindow).selectedNameDirector.Text = GetFioById(SelectedDivision.IdDirector);
                //
                OnPropertyChanged("SelectedDivision");
            }
        }

        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                // Problem...
                if (SelectedOrder != null)
                {
                    using (WaterCarrierContext context = new WaterCarrierContext())
                    {

                    }
                    ((MainWindow)System.Windows.Application.Current.MainWindow).tagsBox.Text = GetStrTags(SelectedOrder.IdTags);
                    ((MainWindow)System.Windows.Application.Current.MainWindow).workerOnSelectedOrder.Text = GetFioById(SelectedOrder.IdEmployee);
                }

                //
                OnPropertyChanged("SelectedOrder");
            }
        }

        public Tag SelectedTag
        {
            get => _selectedTag;
            set
            {
                _selectedTag = value;
                OnPropertyChanged("SelectedTag");
            }
        }

        public string GetFioById(int id)
        {
            foreach (Employee emp in Employees)
            {
                if (emp.Id == id)
                    return emp.Surname + " " + emp.Name[0] + ". " + emp.MiddleName[0] + ".";
            }

            return null;
        }

        private string GetNameDivisionById(int? id)
        {
            foreach (Division div in Divisions)
                if (div.Id == id)
                    return div.Name;

            return "";
        }

        public string GetStrTags(ICollection<Tag> tagsList)
        {
            string res = "";
            List<Tag> tags = new List<Tag>(tagsList);
            foreach (Tag tag in tags)
            {
                if (tags.IndexOf(tag) != tags.Count() - 1)
                    res += tag.Name + ", ";
                else
                    res += tag.Name;
            }

            return res;
        }

        /*private void ItemsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("Employees");

            *//*if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                    item.PropertyChanged -= UpdatePrice;
            }
            if (e.NewItems != null)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                    item.PropertyChanged += UpdatePrice;
            }*//*
        }*/

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
