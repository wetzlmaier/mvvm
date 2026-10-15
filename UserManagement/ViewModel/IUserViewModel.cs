using System.ComponentModel.Composition;

namespace UserManagement.ViewModel;

public interface IUserViewModel 
{
    IAddUserViewModel AddUserViewModel { get; }

    IShowUsersViewModel ShowUsersViewModel { get; }
}