using System.Threading;

namespace Code.Runtime.Utils
{
    public static class TaskHelper 
    {
        public static CancellationToken CreateToken(ref CancellationTokenSource tokenSource)
        {
            tokenSource?.Cancel();
            tokenSource?.Dispose();
            tokenSource = new CancellationTokenSource();
            return tokenSource.Token;
        }
    }
}