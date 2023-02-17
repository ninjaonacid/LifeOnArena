namespace Code.Logic
{
    public interface ITimer
    {
        float Elapsed
        {
            get;
        }

        void Reset();
    }
}
