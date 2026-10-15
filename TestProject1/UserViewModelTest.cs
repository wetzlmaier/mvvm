using BusinessLayer;
using CommunityToolkit.Mvvm.Messaging;
using Dto;
using Moq;
using Mvvm;
using UserManagement.ViewModel;

namespace TestProject1
{
    [TestClass]
    [DoNotParallelize]
    public sealed class UserViewModelTest
    {
        private Mock<IUserService> _mockUserService = null!;
        private Mock<IMessageService> _mockMessageService = null!;
        private UserViewModel _viewModel = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockUserService = new Mock<IUserService>();
            _mockMessageService = new Mock<IMessageService>();

            _viewModel = new UserViewModel
            {
                ShowUsersViewModel = new ShowUsersViewModel()
                {
                    UserService = _mockUserService.Object
                },
                AddUserViewModel = new AddUserViewModel()
                {
                    UserService = _mockUserService.Object,
                    MessageService = _mockMessageService.Object
                }
            };
        }

        [TestCleanup]
        public void Teardown()
        {
            WeakReferenceMessenger.Default.UnregisterAll(_viewModel.ShowUsersViewModel);
            WeakReferenceMessenger.Default.UnregisterAll(_viewModel.AddUserViewModel);
        }

        [TestMethod]
        [DataRow("", "Doe")]      // Missing First Name
        [DataRow("John", "")]     // Missing Last Name
        [DataRow("   ", "Doe")]   // Whitespace First Name
        [DataRow("John", "   ")]  // Whitespace Last Name
        public async Task CanSaveUser_ValidatesInputsCorrectly(string firstName, string lastName)
        {
            // Act
            _viewModel.AddUserViewModel.FirstName = firstName;
            _viewModel.AddUserViewModel.LastName = lastName;

            _mockUserService
                .Setup(service => service.AddUser(It.IsAny<UserDto>()))
                .Returns(Task.FromResult(1));

            _mockMessageService.Setup(service => service.ShowError("User data is invalid!"));

            await _viewModel.AddUserViewModel.SaveUserAsync();

            _mockMessageService.Verify(service => service.ShowError("User data is invalid!"), Times.Once);
            _mockUserService.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task SaveUserAsync_WhenSuccessful_AddsUserToCollectionAndClearsFields()
        {
            // Arrange
            _viewModel.AddUserViewModel.FirstName = "Jane";
            _viewModel.AddUserViewModel.LastName = "Smith";

            _mockUserService
                .Setup(service => service.AddUser(It.IsAny<UserDto>()))
                .Returns(Task.FromResult(1));

            // Act
            //await _viewModel.SaveUserCommand.ExecuteAsync(null);
            await _viewModel.AddUserViewModel.SaveUserAsync();

            // Assert
            // 1. Verify the service was called with the correct data
            _mockUserService.Verify(service => service.AddUser(It.Is<UserDto>(u =>
                u.FirstName == "Jane" && u.LastName == "Smith")), Times.Once);

            // 2. Verify the user was added to the local ObservableCollection
            Assert.AreEqual(1, _viewModel.ShowUsersViewModel.Users.Count);
            Assert.AreEqual("Jane", _viewModel.ShowUsersViewModel.Users[0].FirstName);
            Assert.AreEqual("Smith", _viewModel.ShowUsersViewModel.Users[0].LastName);

            // 3. Verify the fields were cleared
            Assert.AreEqual(string.Empty, _viewModel.AddUserViewModel.FirstName);
            Assert.AreEqual(string.Empty, _viewModel.AddUserViewModel.LastName);
        }

        [TestMethod]
        public async Task SaveUserAsync_WhenServiceThrows_RethrowsExceptionAndDoesNotAddUser()
        {
            // Arrange
            _viewModel.AddUserViewModel.FirstName = "Error";
            _viewModel.AddUserViewModel.LastName = "Prone";
            var expectedException = new BusinessLayerException("Database connection failed", new Exception());

            _mockUserService
                .Setup(service => service.AddUser(It.IsAny<UserDto>()))
                .ThrowsAsync(expectedException);

            // Act & Assert
            var exception = await Assert.ThrowsExactlyAsync<BusinessLayerException>(() => _viewModel.AddUserViewModel.SaveUserCommand.ExecuteAsync(null));

            Assert.AreEqual("Database connection failed", exception.Message);
            Assert.AreEqual(0, _viewModel.ShowUsersViewModel.Users.Count); // Ensure the UI collection wasn't updated
        }

        [TestMethod]
        public async Task OnLoadedAsync_FetchesActiveUsers_AndPopulatesCollection()
        {
            // Arrange
            var activeUsersFromService = new List<UserDto>
            {
                new() { FirstName = "Alice", LastName = "Wonderland" },
                new() { FirstName = "Bob", LastName = "Builder" }
            };

            _mockUserService
                .Setup(service => service.GetActiveUsers())
                .ReturnsAsync(activeUsersFromService);

            // Act
            await _viewModel.ShowUsersViewModel.LoadedCommand.ExecuteAsync(null);

            // Assert
            _mockUserService.Verify(service => service.GetActiveUsers(), Times.Once);

            Assert.AreEqual(2, _viewModel.ShowUsersViewModel.Users.Count);

            Assert.AreEqual("Alice", _viewModel.ShowUsersViewModel.Users[0].FirstName);
            Assert.AreEqual("Wonderland", _viewModel.ShowUsersViewModel.Users[0].LastName);

            Assert.AreEqual("Bob", _viewModel.ShowUsersViewModel.Users[1].FirstName);
            Assert.AreEqual("Builder", _viewModel.ShowUsersViewModel.Users[1].LastName);
        }
    }
}
