using MauiApp5.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace MauiApp5;

public partial class TodoListPage : ContentPage
{
    private readonly string _category;
    private readonly ObservableCollection<TaskItem> _filteredTasks = new();

    public TodoListPage(string category)
    {
        InitializeComponent();
        _category = category;
        CategoryTitle.Text = $"{_category} Tasks";

        TodoList.ItemsSource = _filteredTasks;

        // lista glbl
        TaskRepository.AllTasks.CollectionChanged += AllTasks_CollectionChanged;

        RefreshList();
    }

    private void AllTasks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        // aktualziacja listy
        RefreshList();
    }

    private void RefreshList()
    {
        _filteredTasks.Clear();

        var tasks = TaskRepository.AllTasks
            .Where(t => string.Equals(t.Category, _category, StringComparison.OrdinalIgnoreCase));

        foreach (var task in tasks)
            _filteredTasks.Add(task);
    }

    private async void AddButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddTaskPage());
    }

    private async void TodoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is TaskItem selected)
        {
            bool confirm = await DisplayAlert("Delete Task", $"Remove \"{selected.Name}\"?", "Yes", "No");
            if (confirm)
            {
                TaskRepository.AllTasks.Remove(selected);
                RefreshList();
            }

            ((CollectionView)sender).SelectedItem = null;
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        // odłączanie
        TaskRepository.AllTasks.CollectionChanged -= AllTasks_CollectionChanged;
    }
}
