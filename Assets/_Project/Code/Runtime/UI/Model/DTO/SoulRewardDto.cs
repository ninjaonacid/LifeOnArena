using UnityEngine;

namespace Code.Runtime.UI.Model.DTO
{
    public class SoulRewardDto : IScreenModelDto
    {
        public int Value;
        public Sprite Icon;
        public SoulRewardDto(int value, Sprite icon)
        {
            Value = value;
            Icon = icon;
        }
    }
}