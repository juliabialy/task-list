using System.Collections.ObjectModel;

namespace MauiApp5.Models
{
    public static class TaskRepository
    {
        public static ObservableCollection<TaskItem> AllTasks { get; } = new ObservableCollection<TaskItem>();
    }
}
