using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using MvvmDialogs;

namespace Todos
{
    public class MainViewModel : Screen
    {
        private readonly IDialogService dialogService;
        
        public MainViewModel(IDialogService dialogService)
        {
            this.dialogService = dialogService;

            //Todos.CollectionChanged += OnTodoCollectionChanged;
        }

        public ObservableCollection<TodoViewModel> Todos { get; } = new ObservableCollection<TodoViewModel>();

        public void Add()
        {
            var dialogViewModel = new AddTodoViewModel();

            var success = dialogService.ShowDialog(this, dialogViewModel);
            if (success == true)
            {
                Todos.Add(new TodoViewModel(dialogViewModel.Name));
            }
        }

        public void ClearCompleted()
        {
            var result = dialogService.ShowMessageBox(this, "Are you sure?", "Clear Completed", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                foreach (var completed in Todos.Where(todo => todo.IsCompleted).ToArray())
                {
                    Todos.Remove(completed);
                }
            }
        }

        public bool CanClearCompleted => Todos.Any(todo => todo.IsCompleted);

        //private void OnTodoCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    switch (e.Action)
        //    {
        //        case NotifyCollectionChangedAction.Add:
        //            foreach (TodoViewModel todo in e.NewItems)
        //            {
        //                todo.PropertyChanged += OnTodoPropertyChanged;
        //            }
        //            break;

        //        case NotifyCollectionChangedAction.Remove:
        //            foreach (TodoViewModel todo in e.OldItems)
        //            {
        //                todo.PropertyChanged -= OnTodoPropertyChanged;
        //            }
        //            break;
        //    }
        //}

        //private void OnTodoPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == nameof(TodoViewModel.IsCompleted))
        //    {
        //        clearCompletedCommand.RaiseCanExecuteChanged();
        //    }
        //}
    }
}
