using System.ComponentModel;
using BusinessLayer;
using CommunityToolkit.Mvvm.Input;
using Dto;
using Logging;

namespace UserManagement.ViewModel;

public interface IAddUserViewModel : INotifyDataErrorInfo, IUser
{
    IUserService UserService { get; set; }
    ILoggingService LoggingService { get; set; }
    IAsyncRelayCommand SaveUserCommand { get; }

    Task SaveUserAsync();
}