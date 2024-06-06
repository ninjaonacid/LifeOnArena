using System;
using Code.Runtime.Data.PlayerData;

namespace Code.Runtime.Modules.Requirements
{
    public interface IRequirement<T>
    {
        public bool CheckRequirement(T value);
    }
    
}
