using UnityEditor;
using UnityEngine;

namespace Page
{
    /// <summary>
    /// ENABLE_PAGE_DEBUG_LOG 심볼을 관리하는 에디터 스크립트.
    /// EditorWindow 기반 GUI와 메뉴 항목을 통해 제어합니다.
    /// </summary>
    internal class PageDefineSymbolManager : EditorWindow
    {
        private const string MenuPath = "Tools/Page Debug Log/";
        private bool isDebugEnabled;
        private GUIStyle headerStyle;
        private GUIStyle descriptionStyle;
        private Vector2 scrollPosition;

        [MenuItem(MenuPath + "Settings", false, 0)]
        private static void ShowWindow()
        {
            var window = GetWindow<PageDefineSymbolManager>("Page Debug Settings");
            window.minSize = new Vector2(400, 250);
            window.maxSize = new Vector2(500, 600);
        }

        private void OnEnable()
        {
            isDebugEnabled = Common.Editor.ScriptingDefineSymbolUtility.HasScriptingDefineSymbol(
                Const.LogSymbol
            );

            headerStyle = new GUIStyle();
            descriptionStyle = new GUIStyle();
        }

        private void OnGUI()
        {
            // 스타일 초기화
            if (headerStyle.normal.textColor == Color.clear)
            {
                InitializeStyles();
            }

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            // 헤더 섹션
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Page Package Debug Settings", headerStyle);
            GUILayout.Space(5);

            // 구분선
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Space(10);

            // 설명
            EditorGUILayout.LabelField(
                "디버그 로그 설정을 통해 Page 패키지의 내부 작동에 대한 상세 로그를 활성화하거나 비활성화할 수 있습니다.",
                descriptionStyle
            );
            GUILayout.Space(15);

            // 디버그 로그 토글
            bool newValue = EditorGUILayout.Toggle("디버그 로그 활성화", isDebugEnabled);
            if (newValue != isDebugEnabled)
            {
                isDebugEnabled = newValue;
                Common.Editor.ScriptingDefineSymbolUtility.SetScriptingDefineSymbol(
                    Const.LogSymbol,
                    isDebugEnabled
                );
            }

            // 현재 상태 표시
            GUILayout.Space(10);
            EditorGUILayout.LabelField(
                "현재 상태:",
                isDebugEnabled ? "활성화됨 ✓" : "비활성화됨 ✗"
            );

            // 영향 받는 디버그 기능 목록
            GUILayout.Space(20);
            EditorGUILayout.LabelField("영향 받는 기능:", EditorStyles.boldLabel);
            GUILayout.Space(5);
            EditorGUILayout.LabelField("• 페이지 전환 로깅");
            EditorGUILayout.LabelField("• 페이지 생명주기 이벤트 로깅");
            EditorGUILayout.LabelField("• 페이지 컨트롤러 작업 로깅");
            EditorGUILayout.LabelField("• 애니메이션 상태 로깅");

            // 스크립트 재컴파일 버튼
            GUILayout.Space(30);
            if (GUILayout.Button("스크립트 재컴파일"))
            {
                AssetDatabase.Refresh();
            }

            EditorGUILayout.EndScrollView();
        }

        private void InitializeStyles()
        {
            headerStyle = new GUIStyle(EditorStyles.largeLabel);
            headerStyle.fontSize = 16;
            headerStyle.fontStyle = FontStyle.Bold;
            headerStyle.alignment = TextAnchor.MiddleCenter;
            headerStyle.normal.textColor = EditorGUIUtility.isProSkin
                ? new Color(0.9f, 0.9f, 0.9f)
                : new Color(0.2f, 0.2f, 0.2f);

            descriptionStyle = new GUIStyle(EditorStyles.label);
            descriptionStyle.wordWrap = true;
            descriptionStyle.richText = true;
        }

        // 기존 메뉴 항목 유지 (빠른 접근용)
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
