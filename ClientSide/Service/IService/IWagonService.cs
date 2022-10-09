using Models;

namespace ClientSide.Service.IService
{
    public interface IWagonService
    {
        public Task<IEnumerable<WagonDTO>> GetAll();
        public Task<IEnumerable<WagonDTO>> Get(int trainId);
    }
}
