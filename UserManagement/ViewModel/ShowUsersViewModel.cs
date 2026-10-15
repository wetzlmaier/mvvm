using BusinessLayer;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Logging;
using Mvvm;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Entity.Hibernate;
using UserManagement.Messaging;
using UserManagement.Model;

namespace UserManagement.ViewModel
{
    [Export(typeof(IShowUsersViewModel))]
    public partial class ShowUsersViewModel : ViewModelBase, IShowUsersViewModel
    {
        public ShowUsersViewModel()
        {
            // Register to receive UserLoggedInMessage
            WeakReferenceMessenger.Default.Register<UserChangedMessage>(this, (recipient, message) =>
            {
                switch (message.UserChangeType)
                {
                    case UserChangeType.Added:
                        Users.Add(message.Value);
                        break;
                    case UserChangeType.Updated:
                        throw new NotImplementedException();
                    case UserChangeType.Deleted:
                        throw new NotImplementedException();
                }
            });
        }

        public ObservableCollection<UserModel> Users { get; } = [];

        [Import]
        public IUserService UserService { get; set; } = null!;

        [Import]
        public ILoggingService LoggingService { get; set; } = null!;

        [RelayCommand]
        public async Task OnLoadedAsync()
        {
            var activeUsers = await UserService.GetActiveUsers();
            foreach (var user in activeUsers.Select(u => new UserModel { FirstName = u.FirstName, LastName = u.LastName }))
            {
                Users.Add(user);
            }
        }
    }
}
