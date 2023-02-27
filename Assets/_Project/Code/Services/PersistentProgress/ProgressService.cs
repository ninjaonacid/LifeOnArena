using Code.Data;

namespace Code.Services.PersistentProgress
{
    public class ProgressService : IProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}