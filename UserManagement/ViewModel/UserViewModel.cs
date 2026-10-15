using Mvvm;
using System.ComponentModel.Composition;

namespace UserManagement.ViewModel
{
    [Export(typeof(IUserViewModel))]
    [Export(typeof(IMainViewModel))]
    public partial class UserViewModel : MainViewModelBase, IUserViewModel
    {
        [Import]
        public IAddUserViewModel AddUserViewModel { get; set; } = null!;

        [Import]
        public IShowUsersViewModel ShowUsersViewModel { get; set; } = null!;
    }
}
