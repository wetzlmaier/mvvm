using CommunityToolkit.Mvvm.ComponentModel;

namespace UserManagement.Model
{
    public partial class UserModel : ObservableObject
    {
        [ObservableProperty]
        private string _firstName = string.Empty;

        [ObservableProperty]
        private string _lastName = string.Empty;
    }
}
