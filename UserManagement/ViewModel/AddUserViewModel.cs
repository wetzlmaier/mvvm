using BusinessLayer;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Dto;
using Dto.Validation;
using Logging;
using Mvvm;
using System.ComponentModel.Composition;
using CommunityToolkit.Mvvm.Messaging;
using UserManagement.Messaging;
using UserManagement.Model;

namespace UserManagement.ViewModel
{
    [Export(typeof(IAddUserViewModel))]
    public partial class AddUserViewModel() : ValidatableViewModelBase<IUser>(new UserValidator()), IAddUserViewModel
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveUserCommand))]
        private string _firstName = string.Empty;

        partial void OnFirstNameChanged(string value)
        {
            /*if (_user.FirstName == value)
                return;

            _user.Name = value;*/
            ValidateProperty(this, nameof(FirstName));
        }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveUserCommand))]
        private string _lastName = string.Empty;

        partial void OnLastNameChanged(string value)
        {
            ValidateProperty(this, nameof(LastName));
        }

        [Import]
        public IUserService UserService { get; set; } = null!;

        [Import]
        public ILoggingService LoggingService { get; set; } = null!;

        [Import]
        public IMessageService MessageService { get; set; } = null!;

        // The toolkit automatically generates "SaveUserCommand" (stripping the "Async" suffix)
        [RelayCommand]
        public async Task SaveUserAsync()
        {
            try
            {
                ValidateAllProperties(this);

                if (HasErrors)
                {
                    MessageService.ShowError("User data is invalid!");
                    return;
                }

                await UserService.AddUser(new UserDto { FirstName = FirstName, LastName = LastName });
                // Notify other parts of the application about the new user
                var userModel = new UserModel { FirstName = FirstName, LastName = LastName };
                WeakReferenceMessenger.Default.Send(new UserChangedMessage(UserChangeType.Added, userModel));    
                
                // Clear the fields after saving
                FirstName = string.Empty;
                LastName = string.Empty;
            }
            catch (BusinessLayerException)
            {
                throw;
            }
        }
    }
}
