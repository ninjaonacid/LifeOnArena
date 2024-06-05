using System;
using UnityEngine;

namespace Code.Runtime.ConfigData.Identifiers
{
    public class Identifier : ScriptableObject, IComparable
    {
        [ScriptableObjectId]
        public string Name;

        [ScriptableObjectId]
        public int Id;
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Identifier otherIdentifier = obj as Identifier;
            if (otherIdentifier != null)
            {
                int nameComparison = string.Compare(Name, otherIdentifier.Name, StringComparison.Ordinal);
                if (nameComparison != 0)
                {
                    return nameComparison;
                }
                else
                {
                    return Id.CompareTo(otherIdentifier.Id);
                }
            }
            else
            {
                throw new ArgumentException("Object is not an Identifier");
            }
        }
    }
}

