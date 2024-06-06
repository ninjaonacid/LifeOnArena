using System;

namespace Code.Runtime.Modules.Requirements
{
    [Serializable]
    public class LevelRequirement : IRequirement<int>
    {
        public bool CheckRequirement(int value)
        {
            throw new NotImplementedException();
        }
    }
}