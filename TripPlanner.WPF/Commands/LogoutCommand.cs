using TripPlanner.WPF.Stores;

namespace TripPlanner.WPF.Commands
{
    public class LogoutCommand : CommandBase
    {
        private readonly UserStore _accountStore;

        public LogoutCommand(UserStore accountStore)
        {
            _accountStore = accountStore;
        }

        public override void Execute(object? parameter)
        {
            _accountStore.Logout();
        }
    }
}
