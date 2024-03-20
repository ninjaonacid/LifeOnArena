using Code.Runtime.Logic.Timer;

namespace Code.Runtime.UI.Model
{
    public class MessageWindowModelWithTimer : IScreenModel
    {

        public string Message;
        public int Seconds;
        public Timer Timer;

        public MessageWindowModelWithTimer(IScreenModelDto modelDto)
        {
            
        }
        public void Initialize()
        {
            throw new System.NotImplementedException();
        }
    }
}