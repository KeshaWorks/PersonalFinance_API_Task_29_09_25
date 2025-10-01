using Project.Models;

namespace Project.Interfaces
{
    public interface ILimitManagerService
    {
        public void AddLimit(AddLimitRequest addLimitRequest);
    }
}