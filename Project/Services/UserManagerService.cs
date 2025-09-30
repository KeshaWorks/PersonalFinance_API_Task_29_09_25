using Project.Interfaces;

namespace Project.Services
{
    public class UserManagerService : IUserManagerService
    {
        private ILimitManagerService _limitManagerService;
        private IRecommendationManagerService _recommendManagerService;

        private IUserManagerService _userManagerService;

        public UserManagerService
            (
            ILimitManagerService limitManagerService, 
            IRecommendationManagerService recommendManagerService, 
            IUserManagerService userManagerService
            )
        {
            _limitManagerService = limitManagerService;
            _recommendManagerService = recommendManagerService;
            _userManagerService = userManagerService;
        }

    }
}