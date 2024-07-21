using UnityEngine;

namespace Code.Runtime.Services.PauseService
{
    public class PauseService
    {
        public void PauseGame()
        {
            Time.timeScale = 0;
        }

        public void UnpauseGame()
        {
            Time.timeScale = 1;
        }
    }
}
