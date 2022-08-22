using System;

namespace CodeBase.Data
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