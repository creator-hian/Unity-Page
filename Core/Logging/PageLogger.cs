using Common.Logging;
using UnityEngine;

namespace Page
{
    /// <summary>
    /// Page 시스템 전용 중앙 집중식 로거.
    /// ENABLE_PAGE_DEBUG_LOG 심볼이 정의된 경우에만 디버그 로그가 출력됩니다.
    /// </summary>
    public static class PageLogger
    {
        private const string Category = "Page";

        [System.Diagnostics.Conditional(Const.DEVELOPMENT_BUILD)]
        [System.Diagnostics.Conditional(Const.LogSymbol)]
        public static void Log(string message)
        {
#if ENABLE_PAGE_DEBUG_LOG || DEVELOPMENT_BUILD
            Debug.Log(LoggingUtils.FormatMessage(Category, message));
#endif
        }

        [System.Diagnostics.Conditional(Const.DEVELOPMENT_BUILD)]
        [System.Diagnostics.Conditional(Const.LogSymbol)]
        public static void LogFormat(string format, params object[] args)
        {
#if ENABLE_PAGE_DEBUG_LOG || DEVELOPMENT_BUILD
            Debug.Log(LoggingUtils.FormatMessage(Category, format, args));
#endif
        }

        [System.Diagnostics.Conditional(Const.DEVELOPMENT_BUILD)]
        [System.Diagnostics.Conditional(Const.LogSymbol)]
        public static void LogWarning(string message)
        {
#if ENABLE_PAGE_DEBUG_LOG || DEVELOPMENT_BUILD
            Debug.LogWarning(LoggingUtils.FormatMessage(Category, message));
#endif
        }

        [System.Diagnostics.Conditional(Const.DEVELOPMENT_BUILD)]
        [System.Diagnostics.Conditional(Const.LogSymbol)]
        public static void LogWarningFormat(string format, params object[] args)
        {
#if ENABLE_PAGE_DEBUG_LOG || DEVELOPMENT_BUILD
            Debug.LogWarning(LoggingUtils.FormatMessage(Category, format, args));
#endif
        }

        public static void LogError(string message)
        {
            Debug.LogError(LoggingUtils.FormatMessage(Category, message));
        }

        public static void LogErrorFormat(string format, params object[] args)
        {
            Debug.LogError(LoggingUtils.FormatMessage(Category, format, args));
        }
    }
}
