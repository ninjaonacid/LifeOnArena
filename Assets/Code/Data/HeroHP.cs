using System;

namespace Code.Data
{
    [Serializable]
    public class HeroHP
    {
        public float CurrentHP;
        public float MaxHP;

        public void ResetHP()
        {
            CurrentHP = MaxHP;
        }
    }
}