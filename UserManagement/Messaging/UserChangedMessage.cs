using CommunityToolkit.Mvvm.Messaging.Messages;
using UserManagement.Model;

namespace UserManagement.Messaging
{
    public class UserChangedMessage(UserChangeType changeType, UserModel value) : ValueChangedMessage<UserModel>(value)
    {
        public UserChangedMessage(UserModel value) : this(UserChangeType.Added, value)
        {

        }

        public UserChangeType UserChangeType { get; } = changeType;
    }
}