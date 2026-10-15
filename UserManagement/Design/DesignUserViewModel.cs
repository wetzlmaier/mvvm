using UserManagement.ViewModel;

namespace UserManagement.Design
{
    public class DesignUserViewModel : UserViewModel
    {
        public DesignUserViewModel()
        {
            ShowUsersViewModel=new DesignShowUsersViewModel();
            AddUserViewModel = new DesignAddUserViewModel();
        }
    }
}
