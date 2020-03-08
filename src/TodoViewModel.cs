using Caliburn.Micro;

namespace Todos
{
    public class TodoViewModel : PropertyChangedBase
    {
        private string name;
        private bool isCompleted;

        public TodoViewModel(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public bool IsCompleted
        {
            get => isCompleted;
            set
            {
                isCompleted = value;
                NotifyOfPropertyChange(() => IsCompleted);
            }
        }
    }
}
