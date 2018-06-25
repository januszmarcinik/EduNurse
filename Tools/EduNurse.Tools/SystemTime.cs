using System;

namespace EduNurse.Tools
{
    public static class SystemTime
    {
        private static Func<DateTime> _setDateFunc;

        public static DateTime Now => _setDateFunc?.Invoke() ?? DateTime.Now;

        public static void SetDate(DateTime dateTime)
        {
            _setDateFunc = () => dateTime;
        }
    }
}
