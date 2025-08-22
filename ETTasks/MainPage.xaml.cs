using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using muxc=Microsoft.UI.Xaml.Controls;

// Boş Sayfa öğe şablonu https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x41f adresinde açıklanmaktadır

namespace ETTasks
{
    /// <summary>
    /// Kendi başına kullanılabilecek ya da bir Çerçeve içine taşınabilecek boş bir sayfa.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public class TaskItem : INotifyPropertyChanged
        {
            private string _title;
            private string _description;
            private bool _isCompleted;

            public string Title
            {
                get => _title;
                set { _title = value; OnPropertyChanged(nameof(Title)); }
            }

            public string Description
            {
                get => _description;
                set { _description = value; OnPropertyChanged(nameof(Description)); }
            }

            public bool IsCompleted
            {
                get => _isCompleted;
                set { _isCompleted = value; OnPropertyChanged(nameof(IsCompleted)); }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string propertyName) =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class BoolToTextDecorationConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, string language)
            {
                bool isCompleted = value is bool b && b;
                if (!isCompleted)
                {
                    return null;
                }
                else
                {
                    return TextDecorations.Strikethrough;
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, string language)
            {
                // Not needed for one-way binding
                throw new NotImplementedException();
            }
        }

        private ObservableCollection<TaskItem> TaskList = new ObservableCollection<TaskItem>();
        public MainPage()
        {
            this.InitializeComponent();
            TaskListView.ItemsSource = TaskList;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(TaskInput.Text))
            {
                TaskList.Add(new TaskItem { Title = TaskInput.Text, IsCompleted = false });
                TaskInput.Text = string.Empty;
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button button && button.DataContext is TaskItem task)
            {
                TaskList.Remove(task);
            }
        }
    }
}
