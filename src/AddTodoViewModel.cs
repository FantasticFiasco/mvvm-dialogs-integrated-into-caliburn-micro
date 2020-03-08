using Caliburn.Micro;
using MvvmDialogs;

namespace Todos
{
    public class AddTodoViewModel : Screen, IModalDialogViewModel
    {
        private string name;
        private bool? dialogResult;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public bool? DialogResult
        {
            get => dialogResult;
            private set
            {
                dialogResult = value;
                NotifyOfPropertyChange(() => DialogResult);
            }
        }

        public void Ok()
        {
            if (!string.IsNullOrEmpty(Name))
            {
                DialogResult = true;
            }
        }

        public bool CanOk => !string.IsNullOrWhiteSpace(Name);
    }
}
