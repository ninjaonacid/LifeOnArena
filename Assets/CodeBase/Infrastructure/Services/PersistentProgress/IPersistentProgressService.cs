using CodeBase.Infrastructure.Services;

namespace CodeBase
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}