using System;

namespace CodeBase.Data
{
    [Serializable]
    public class State
    {
        public float CurrentHP;
        public float MaxHP;

        public void ResetHP()
        {
            CurrentHP = MaxHP;
        }
    }
}