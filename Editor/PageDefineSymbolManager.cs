using UnityEditor;

namespace Page
{
    /// <summary>
    /// ENABLE_PAGE_DEBUG_LOG 심볼을 수동으로 관리하는 에디터 스크립트.
    /// Tools/Page Debug Log 메뉴를 통해 제어합니다.
    /// </summary>
    internal static class PageDefineSymbolManager
    {
        private const string MenuPath = "Tools/Page Debug Log/";

        [MenuItem(MenuPath + "Enable", false, 1)]
        private static void EnableLogSymbol()
        {
            Common.Editor.ScriptingDefineSymbolUtility.SetScriptingDefineSymbol(
                Const.LogSymbol,
                true
            );
        }

        [MenuItem(MenuPath + "Enable", true, 1)]
        private static bool ValidateEnableLogSymbol()
        {
            return !Common.Editor.ScriptingDefineSymbolUtility.HasScriptingDefineSymbol(
                Const.LogSymbol
            );
        }

        [MenuItem(MenuPath + "Disable", false, 2)]
        private static void DisableLogSymbol()
        {
            Common.Editor.ScriptingDefineSymbolUtility.SetScriptingDefineSymbol(
                Const.LogSymbol,
                false
            );
        }

        [MenuItem(MenuPath + "Disable", true, 2)]
        private static bool ValidateDisableLogSymbol()
        {
            return Common.Editor.ScriptingDefineSymbolUtility.HasScriptingDefineSymbol(
                Const.LogSymbol
            );
        }
    }
}
