namespace Code.Runtime.Utils
{
    public static class IdentifierGenerator
    {
        public static int GenerateIntId(string name)
        {
            int hash = 0;
            for (int i = 0; i < name.Length; i++)
            {
                hash = (hash << 5) - hash + name[i];
            }

            return hash;
        }
    }
}
