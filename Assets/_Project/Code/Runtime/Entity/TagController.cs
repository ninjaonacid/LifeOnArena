using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Modules.AbilitySystem.GameplayTags;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Runtime.Entity
{
    public class TagController : MonoBehaviour
    {
        private List<GameplayTag> _grantedTags = new List<GameplayTag>();

        public bool HasTag(GameplayTag tag)
        {
            return _grantedTags.Contains(tag);
        }

        public void AddTag(GameplayTag tag)
        {
            _grantedTags.Add(tag);
        }

        public void RemoveTag(GameplayTag tag)
        {
            if (_grantedTags.Contains(tag))
            {
                _grantedTags.Remove(tag);
            }
        }
    }
}
