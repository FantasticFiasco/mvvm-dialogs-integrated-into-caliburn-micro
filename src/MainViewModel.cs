using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using MvvmDialogs;

namespace Todos
{
    public class MainViewModel : Screen
    {
        // Lets use IWindowManager from Caliburn.Micro to open modal dialogs and non-modal windows
        private readonly IWindowManager windowManager;

        // Lets use IDialogService from MVVM Dialogs to open native dialogs, like the save file
        // dialog or the message box
        private readonly IDialogService nativeDialogService;
        
        public MainViewModel(IWindowManager windowManager, IDialogService nativeDialogService)
        {
            this.windowManager = windowManager;
            this.nativeDialogService = nativeDialogService;

            Todos.CollectionChanged += OnTodoCollectionChanged;
        }

        public BindableCollection<TodoViewModel> Todos { get; } = new BindableCollection<TodoViewModel>();

        public async Task Add()
        {
            var dialogViewModel = new AddTodoViewModel();

            var success = await windowManager.ShowDialogAsync(dialogViewModel);
            if (success == true)
            {
                Todos.Add(new TodoViewModel(dialogViewModel.Name));
            }
        }

        public void ClearCompleted()
        {
            var result = nativeDialogService.ShowMessageBox(this, "Are you sure?", "Clear Completed", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                Todos.RemoveRange(Todos.Where(todo => todo.IsCompleted).ToArray());
            }
        }

        public bool CanClearCompleted => Todos.Any(todo => todo.IsCompleted);

        private void OnTodoCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (TodoViewModel todo in e.NewItems)
                    {
                        todo.PropertyChanged += OnTodoPropertyChanged;
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (TodoViewModel todo in e.OldItems)
                    {
                        todo.PropertyChanged -= OnTodoPropertyChanged;
                    }
                    break;
            }
        }

        private void OnTodoPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TodoViewModel.IsCompleted))
            {
                NotifyOfPropertyChange(() => CanClearCompleted);
            }
        }
    }
}
