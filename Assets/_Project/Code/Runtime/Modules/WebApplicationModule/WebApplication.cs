using GamePush;

namespace Code.Runtime.Modules.WebApplicationModule
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
#pragma warning disable CS0162
                return false;
#pragma warning restore CS0162
            }
        }

        public static bool IsMobile => GP_Device.IsMobile();
        public static bool IsDesktop => GP_Device.IsDesktop();
    }
}
