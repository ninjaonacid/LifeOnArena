using Code.Data;

namespace Code.Services.PersistentProgress
{
    public interface 
        IPersistentProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}