using System.ComponentModel.Composition;
using CommunityToolkit.Diagnostics;
using Dto;
using Entity.Hibernate;
using Logging;
using NHibernate.Linq;

namespace BusinessLayer
{
    [Export(typeof(IUserService))]
    public class UserService : IUserService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        [ImportingConstructor]
        public UserService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
            LoggingService = null!;
        }

        public async Task<List<UserDto>> GetActiveUsers()
        {
            using var repository = _repositoryFactory.Create();
            return await repository.GetQueryable<User>()
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                })
                .ToListAsync();
        }

        public async Task AddUser(UserDto user)
        {
            Guard.IsNotNullOrWhiteSpace(user.FirstName, "FirstName must not be null or whitespace.");
            Guard.IsNotNullOrWhiteSpace(user.LastName, "LastName must not be null or whitespace.");

            using var repository = _repositoryFactory.Create();
            repository.UnitOfWork.BeginTransaction();

            try
            {
                // use the *Internal methods within an open transaction
                await repository.Save(new User(user.FirstName, user.LastName));
                repository.UnitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                repository.UnitOfWork.RollBackTransaction();
                LoggingService.LogError(e, "Error saving user");

                // only throw a BusinessException 
                throw new BusinessLayerException("Error saving user", e);
            }
        }

        public ILoggingService LoggingService { get; set; }
    }
}
