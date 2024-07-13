using UnityEngine;

namespace Code.Runtime.Modules.Utils
{
    public static class WebApplication
    {
        public static bool IsWebApp
        {
            get
            {
                #if UNITY_WEBGL
                return true;
                #endif
                return false;
            }
        }
    }
}
