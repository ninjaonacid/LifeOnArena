namespace Code.Runtime.Logic.Timer
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
