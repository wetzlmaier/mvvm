using Dto;
using Logging;

namespace BusinessLayer
{
    public interface IUserService
    {
        Task<List<UserDto>> GetActiveUsers();

        Task AddUser(UserDto user);

        ILoggingService LoggingService { get; set; }
    }
}
