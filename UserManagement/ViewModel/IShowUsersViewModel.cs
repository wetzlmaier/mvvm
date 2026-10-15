using System.Collections.ObjectModel;
using BusinessLayer;
using CommunityToolkit.Mvvm.Input;
using Logging;
using UserManagement.Model;

namespace UserManagement.ViewModel;

public interface IShowUsersViewModel 
{
    ObservableCollection<UserModel> Users { get; }
    IUserService UserService { get; set; }
    ILoggingService LoggingService { get; set; }
    
    IAsyncRelayCommand LoadedCommand { get; }

    Task OnLoadedAsync();
}