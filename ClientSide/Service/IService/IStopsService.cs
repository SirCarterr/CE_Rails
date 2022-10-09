using Models;

namespace ClientSide.Service.IService
{
    public interface IStopsService
    {
        public Task<IEnumerable<StopDTO>> GetAll();
        public Task<IEnumerable<StopDTO>> Get(int trainId);
    }
}
