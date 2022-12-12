using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data.SQLite;
using System.Windows.Documents;
/*using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;*/

namespace To_Do_List
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal enum TaskType
    {
        NotImportant,
        Usuall,
        Important,
        VeryImportant,
        TimeDepended,
        Finished
    }
    internal enum SelectExecution
    {
        SelectItems,
        LoadToTaskList
    }
    
    public partial class MainWindow : Window
    {
        private List<TaskClass> SelectItemsFromDB(SelectExecution selectExecution)
        {
            using (var connectToDB = new SQLiteConnection("Data Source=DataBase.db"))
            {
                connectToDB.Open();
                var command = connectToDB.CreateCommand();
                command.CommandText = @"
                        SELECT TaskName, StartDate, FinishDate, TypeOfTask
                        FROM ListDataBase
                    ";
                command.Connection = connectToDB;
                List<TaskClass> list = new List<TaskClass>();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (selectExecution == SelectExecution.LoadToTaskList)
                        {
                            string result = reader.GetString(0);
                            DateTime stDt = reader.GetDateTime(1);
                            DateTime fnDt = reader.GetDateTime(2);
                            string taskTp = reader.GetString(3);
                            TaskType rslt = (TaskType)Enum.Parse(typeof(TaskType), taskTp, false);
                            window.Tasks.Add(new TaskClass(result, stDt, fnDt, rslt));
                        }     
                        else if (selectExecution == SelectExecution.SelectItems)
                        {
                            TaskType taskType = (TaskType)Enum.Parse(typeof(TaskType), reader.GetString(3), false);
                            list.Add(new TaskClass(reader.GetString(0), reader.GetDateTime(1), reader.GetDateTime(2), taskType));
                        }
                    }
                }
                connectToDB.Close();
                return list;
            }
        }
        private void AddItemsToDB(string taskName, DateTime startDate, DateTime finishDate, TaskType taskType)
        {
            using (var connectToDB = new SQLiteConnection("Data Source=DataBase.db"))
            {
                connectToDB.Open();
                var command = connectToDB.CreateCommand();
                string com = @" INSERT INTO ListDataBase (TaskName, StartDate, FinishDate, TypeOfTask) VALUES ($Task_Name, $Start_Date, $Finish_Date, $Type_Of_Task)";
                command.CommandText = com;
                command.Connection = connectToDB;

                command.Parameters.Add("$Task_Name", DbType.String).Value = taskName;
                command.Parameters.Add("$Start_Date", DbType.DateTime).Value = Convert.ToDateTime(startDate);
                command.Parameters.Add("$Finish_Date", DbType.DateTime).Value = Convert.ToDateTime(finishDate);
                command.Parameters.Add("$Type_Of_Task", DbType.String).Value = taskType.ToString();
                command.ExecuteNonQuery();


                connectToDB.Close();
               
            }
        }
        Window1 window;
        public MainWindow()
        {
            InitializeComponent();
            window = new Window1();
            TaskList.ItemsSource = window.Tasks;
            SelectItemsFromDB(SelectExecution.LoadToTaskList);
            StatusComboBox.ItemsSource = Enum.GetValues(typeof(TaskType));
            
            System.GC.Collect();

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {    
            window.Show();     
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            var itemsRemover = TaskList.SelectedItem as TaskClass;
            using (var connect = new SQLiteConnection("Data Source=DataBase.db"))
            {
                connect.Open();
                var command = connect.CreateCommand();
                string commandText = @"DELETE FROM ListDataBase WHERE TaskName=$TaskName";
                command.CommandText = commandText;
                command.Parameters.Add("$TaskName", DbType.String).Value = itemsRemover.Task;
                command.ExecuteNonQuery();
            }
            window.Tasks.Remove(itemsRemover);
        }

        private void ClearButton_click(object sender, RoutedEventArgs e)
        {
            using (var connect = new SQLiteConnection("Data Source=DataBase.db"))
            {
                connect.Open();
                var command = connect.CreateCommand();
                string commandText = @"DELETE FROM ListDataBase";
                command.CommandText = commandText;
                command.ExecuteNonQuery();
            }
            var itemsSourceCleaner = TaskList.ItemsSource as IList<TaskClass>;
            itemsSourceCleaner.Clear();
        }

        private void ChangeStatusComboBox(object sender, RoutedEventArgs e)
        {
            
            if (TaskList.SelectedItem != null && StatusComboBox.SelectedItem != null)
            {
                var itemToChangeStatus = TaskList.SelectedItem as TaskClass;
                
                using(var connection = new SQLiteConnection("Data Source=DataBase.db"))
                {
                    connection.Open();
                    SQLiteCommand command = connection.CreateCommand();
                    string commandText = @"UPDATE ListDataBase SET TaskName=$Task_Name, FinishDate=$Finish_Date, TypeOfTask=$Task_Type WHERE TaskName=$TaskName;";
                                        
                    command.CommandText = commandText;
                    command.Connection = connection;
                    if (ChangeText.Text == "")
                    {                       
                        command.Parameters.Add("$Task_Name", DbType.String).Value = itemToChangeStatus.Task;
                    }
                    else
                    {
                        command.Parameters.Add("$Task_Name", DbType.String).Value = ChangeText.Text;
                        itemToChangeStatus.Task = ChangeText.Text;
                    }
                    var newDate = DatePicker.SelectedDate;
                    if (newDate != null)
                    {
                        command.Parameters.Add("$Finish_Date", DbType.DateTime).Value = newDate;
                        itemToChangeStatus.FinishDate = (DateTime)DatePicker.SelectedDate;
                    }
                    else
                    {
                        command.Parameters.Add("$Finish_Date", DbType.DateTime).Value = itemToChangeStatus.FinishDate;
                    }
                    command.Parameters.Add("$Task_Type", DbType.String).Value = StatusComboBox.SelectedItem.ToString();
                    itemToChangeStatus.TypeOfTask = (TaskType)StatusComboBox.SelectedItem;
                    
                    command.Parameters.Add("$TaskName", DbType.String).Value = itemToChangeStatus.Task;
                    command.ExecuteNonQuery();                    
                    
                    TaskList.Items.Refresh();
                    connection.Close();
                    System.GC.Collect();
                }
                TaskList.Items.Refresh();
            }
            else
            {
                MessageBox.Show("You didnt select item or didnt put date!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if(window.Tasks.Count > 0)
            {
                List<TaskClass> InDB = SelectItemsFromDB(SelectExecution.SelectItems);
                foreach (TaskClass item in window.Tasks)
                {
                    if (InDB.Contains(item) == false)
                    {
                        AddItemsToDB(item.Task, item.StartDate, item.FinishDate, item.TypeOfTask);
                    }
                }
            }
            else
            {
                MessageBox.Show("The tasklist shoudn`t be empty !", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            System.GC.Collect();
        }

        private void OpenBtton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            window.Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
    