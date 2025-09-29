using MauiApp5.Models;

namespace MauiApp5;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        AllTasksList.ItemsSource = TaskRepository.AllTasks;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        AllTasksList.ItemsSource = TaskRepository.AllTasks;
    }

    private async void AddTask_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddTaskPage());
    }

    private async void Category_Clicked(object sender, EventArgs e)
    {
        if (sender is Button btn)
        {
            var category = btn.Text.Split(' ').LastOrDefault() ?? btn.Text;
            await Navigation.PushAsync(new TodoListPage(category));
        }
    }

    private async void Task_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is TaskItem selected)
        {
            bool confirm = await DisplayAlert("Usuń zadanie", $"Usunąć \"{selected.Name}\"?", "Tak", "Nie");
            if (confirm)
                TaskRepository.AllTasks.Remove(selected);

            ((CollectionView)sender).SelectedItem = null;
        }
    }
}
