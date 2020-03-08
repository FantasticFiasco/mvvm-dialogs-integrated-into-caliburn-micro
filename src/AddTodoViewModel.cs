using System.Threading.Tasks;
using Caliburn.Micro;

namespace Todos
{
    public class AddTodoViewModel : Screen
    {
        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                NotifyOfPropertyChange(() => Name);
                NotifyOfPropertyChange(() => CanOk);
            }
        }

        public async Task Ok()
        {
            if (!string.IsNullOrEmpty(Name))
            {
                await TryCloseAsync(true);
            }
        }

        public bool CanOk => !string.IsNullOrWhiteSpace(Name);
    }
}
