using Project.Models.TakedFromBody;

namespace Project.Interfaces
{
    public interface ILimitManagerService
    {
        public void AddLimit(AddLimitRequest addLimitRequest);
    }
}