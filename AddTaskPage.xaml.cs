using MauiApp5.Models;

namespace MauiApp5;

public partial class AddTaskPage : ContentPage
{
    private string _selectedCategory = string.Empty;

    public AddTaskPage()
    {
        InitializeComponent();
    }

    private void Category_Clicked(object sender, EventArgs e)
    {
        if (sender is Button btn)
        {
            // usuwanie emoji, pobieranie ost słowa
            var parts = btn.Text?.Split(' ');
            _selectedCategory = parts?.LastOrDefault() ?? btn.Text ?? string.Empty;
            SelectedCategoryLabel.Text = $"Selected: {_selectedCategory}";
            SelectedCategoryLabel.TextColor = Color.FromArgb("#4A90E2");
        }
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        var name = TaskNameEntry.Text?.Trim();

        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(_selectedCategory))
        {
            await DisplayAlert("Missing info", "Please enter task name and select a category.", "OK");
            return;
        }

        // + do listy glbl
        TaskRepository.AllTasks.Add(new TaskItem
        {
            Name = name,
            Category = _selectedCategory
        });

        await DisplayAlert("Success", $"Added \"{name}\" to {_selectedCategory}.", "OK");
        await Navigation.PopAsync(); // powrót
    }
}
