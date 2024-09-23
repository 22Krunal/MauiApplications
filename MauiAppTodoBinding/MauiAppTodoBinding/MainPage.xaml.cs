using MauiAppTodoBinding.Models;
using MauiAppTodoBinding.Data;
using System.Collections.ObjectModel;

namespace MauiAppTodoBinding
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<TodoItem> Todos { get; set; } = new();

        string _todoListData = string.Empty;
        readonly Database _database;

        public MainPage()
        {
            InitializeComponent();
            _database = new Database();
            _ = Initialize();

        }

        private async Task Initialize()
        {
            var todos = await _database.GetTodos();

            foreach (var todo in todos)
            {
                _todoListData += $"{todo.Title} - {todo.Due:f}{Environment.NewLine}";
                //todos.Add(todo);
                Todos.Add(todo);
            }
            //TodosLabel.Text = _todoListData;
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            var todo = new TodoItem
            {
                Due = DueDatepicker.Date,
                Title = TodotitleEntry.Text
            };

            var inserted = await _database.AddTodo(todo);

            if (inserted != 0)
            {
                _todoListData += $"{todo.Title} - {todo.Due:f}{Environment.NewLine}";
                Todos.Add(todo);

                //TodosLabel.Text = _todoListData;

                TodotitleEntry.Text = String.Empty;
                DueDatepicker.Date = DateTime.Now;
            }
        }
    }

}
