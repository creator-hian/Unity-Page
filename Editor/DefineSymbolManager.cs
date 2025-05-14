#if !ENABLE_CREATOR_HIAN_LOGGING
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

namespace Page.Editor
{
    /// <summary>
    /// ENABLE_PAGE_DEBUG_LOG 심볼을 직접 관리하는 에디터 스크립트.
    /// Logging 패키지 없이도 독립적으로 작동합니다.
    /// Tools/Page Debug Log 메뉴를 통해 제어합니다.
    /// </summary>
    internal static class DefineSymbolManager
    {
        private const string MenuPath = "Tools/Page Debug Log/";
        private const string LogSymbol = "ENABLE_PAGE_DEBUG_LOG";

        [MenuItem(MenuPath + "Enable", false, 100)]
        private static void EnableLogSymbol()
        {
            SetScriptingDefineSymbol(LogSymbol, true);
        }

        [MenuItem(MenuPath + "Enable", true, 100)]
        private static bool ValidateEnableLogSymbol()
        {
            return !HasScriptingDefineSymbol(LogSymbol);
        }

        [MenuItem(MenuPath + "Disable", false, 101)]
        private static void DisableLogSymbol()
        {
            SetScriptingDefineSymbol(LogSymbol, false);
        }

        [MenuItem(MenuPath + "Disable", true, 101)]
        private static bool ValidateDisableLogSymbol()
        {
            return HasScriptingDefineSymbol(LogSymbol);
        }

        /// <summary>
        /// 현재 빌드 타겟에 지정된 스크립팅 심볼이 존재하는지 확인합니다.
        /// </summary>
        /// <param name="symbol">확인할 심볼</param>
        /// <returns>심볼이 존재하면 true, 아니면 false</returns>
        private static bool HasScriptingDefineSymbol(string symbol)
        {
            try
            {
                NamedBuildTarget namedBuildTarget = GetCurrentNamedBuildTarget();
                string definesString = PlayerSettings.GetScriptingDefineSymbols(namedBuildTarget);
                return definesString.Split(';').Select(s => s.Trim()).Contains(symbol);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"HasScriptingDefineSymbol 오류: {e.Message}");
                return false;
            }
        }

        /// <summary>
        /// 현재 빌드 타겟에 스크립팅 심볼을 추가하거나 제거합니다.
        /// </summary>
        /// <param name="symbol">추가/제거할 심볼</param>
        /// <param name="enable">true이면 추가, false이면 제거</param>
        private static void SetScriptingDefineSymbol(string symbol, bool enable)
        {
            try
            {
                NamedBuildTarget namedBuildTarget = GetCurrentNamedBuildTarget();

                string definesString = PlayerSettings.GetScriptingDefineSymbols(namedBuildTarget);
                var defines = definesString
                    .Split(';')
                    .Select(d => d.Trim())
                    .Where(d => !string.IsNullOrEmpty(d))
                    .ToList();

                bool hasSymbol = defines.Contains(symbol);

                if (enable && !hasSymbol)
                {
                    // 심볼 추가
                    defines.Add(symbol);
                    PlayerSettings.SetScriptingDefineSymbols(
                        namedBuildTarget,
                        string.Join(";", defines)
                    );

                    string message =
                        $"'{symbol}' 심볼이 추가되었습니다. (타겟: {namedBuildTarget.TargetName})";
                    Debug.Log(message);
                    EditorUtility.DisplayDialog("심볼 추가", message, "확인");
                }
                else if (!enable && hasSymbol)
                {
                    // 심볼 제거
                    defines.Remove(symbol);
                    PlayerSettings.SetScriptingDefineSymbols(
                        namedBuildTarget,
                        string.Join(";", defines)
                    );

                    string message =
                        $"'{symbol}' 심볼이 제거되었습니다. (타겟: {namedBuildTarget.TargetName})";
                    Debug.Log(message);
                    EditorUtility.DisplayDialog("심볼 제거", message, "확인");
                }
            }
            catch (System.Exception e)
            {
                string errorMessage = $"심볼 관리 오류: {e.Message}";
                Debug.LogError(errorMessage);
                EditorUtility.DisplayDialog("심볼 관리 오류", errorMessage, "확인");
            }
        }

        /// <summary>
        /// 현재 선택된 빌드 타겟에 해당하는 NamedBuildTarget을 반환합니다.
        /// 유효한 타겟을 찾을 수 없는 경우 예외를 발생시킵니다.
        /// </summary>
        /// <returns>현재 선택된 NamedBuildTarget</returns>
        private static NamedBuildTarget GetCurrentNamedBuildTarget()
        {
            BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;

            // Unknown인 경우 활성 빌드 타겟에서 그룹 유추
            if (buildTargetGroup == BuildTargetGroup.Unknown)
            {
                buildTargetGroup = BuildPipeline.GetBuildTargetGroup(
                    EditorUserBuildSettings.activeBuildTarget
                );

                // 여전히 Unknown이면 기본값 사용
                if (buildTargetGroup == BuildTargetGroup.Unknown)
                {
                    Debug.LogWarning(
                        "BuildTargetGroup을 결정할 수 없습니다. Standalone으로 기본 설정합니다."
                    );
                    buildTargetGroup = BuildTargetGroup.Standalone;
                }
            }

            return NamedBuildTarget.FromBuildTargetGroup(buildTargetGroup);
        }
    }
}
#endif
