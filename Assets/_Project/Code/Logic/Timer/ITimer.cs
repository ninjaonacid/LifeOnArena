namespace Code.Logic.Timer
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
