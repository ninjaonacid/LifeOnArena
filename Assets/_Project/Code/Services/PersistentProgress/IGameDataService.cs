using Code.Data;

namespace Code.Services.PersistentProgress
{
    public interface 
        IProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}