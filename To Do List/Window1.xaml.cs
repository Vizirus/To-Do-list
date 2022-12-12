using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
namespace To_Do_List
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : Window
    {
        internal ObservableCollection<TaskClass> Tasks = new ObservableCollection<TaskClass>();
        public Window1()
        {
            InitializeComponent();
            StatusComboBox.ItemsSource = Enum.GetValues(typeof(TaskType));
            
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TaskNameTextBox.Text) && DatePicker.SelectedDate != null && StatusComboBox.SelectedItem != null)
            {
                Tasks.Add(new TaskClass(TaskNameTextBox.Text, DateTime.Now, DatePicker.SelectedDate.Value, (TaskType)StatusComboBox.SelectedItem));
               
            }
            else
            {
                MessageBox.Show("You insert something wrong!", "Please check the input, there is some mistake", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            System.GC.Collect();
        }
    }
}
