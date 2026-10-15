using UserManagement.Model;
using UserManagement.ViewModel;

namespace UserManagement.Design
{
    public class DesignShowUsersViewModel : ShowUsersViewModel
    {
        public DesignShowUsersViewModel()
        {
            // Populate demo data for the XAML Designer
            Users.Add(new UserModel { FirstName = "Jane", LastName = "Doe" });
            Users.Add(new UserModel { FirstName = "John", LastName = "Smith" });
            Users.Add(new UserModel { FirstName = "Alice", LastName = "Johnson" });
        }
    }
}
