using System;
using UnityEngine;

namespace Code.Runtime.Modules.Requirements
{
    public abstract class Requirement<T> : IRequirement where T : IComparable
    {
        [SerializeField] protected  T _requiredValue;

        protected abstract bool CheckRequirement(T value);
        
        public bool CheckRequirement(object value)
        {
            if (value is T typedValue)
            {
                return CheckRequirement(typedValue);
            }

            throw new ArgumentException($"Invalid value type. Expected {typeof(T)}, but got {value.GetType()}");
        }
    }
}