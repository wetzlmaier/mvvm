using Common.Composition;
using UserManagement.ViewModel;

namespace ConsoleApp1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using (var container = CompositionHelper.CreateCompositionContainer(["ConsoleApp1.exe"])) // the ConsoleApp1.exe is not a valid .net assembly
            {
                var userViewModel = container.GetExportedValues<IUserViewModel>().SingleOrDefault();

                if (userViewModel == null)
                    throw new Exception("IUserViewModel not found.");

                for (int i = 0; i < 100; i++)
                {
                    userViewModel.AddUserViewModel.FirstName = "Firstname";
                    userViewModel.AddUserViewModel.LastName = "Lastname";
                    await userViewModel.AddUserViewModel.SaveUserAsync();
                }

                await userViewModel.ShowUsersViewModel.OnLoadedAsync();

                foreach (var user in userViewModel.ShowUsersViewModel.Users)
                {
                    Console.WriteLine($"Name: {user.FirstName} {user.LastName}");
                }
            }
        }
    }
}