using UnityEditor;
using UnityEngine;

namespace RossoForge.Toolbar.Editor
{
    [CreateAssetMenu(fileName = nameof(IconButtonProfile), menuName = "RossoForge/Toolbar/Buttons/Icon Button")]
    public class IconButtonProfile : ButtonProfile
    {
        [field: SerializeField]
        [Tooltip("EditorGUIUtility.IconContent")]
        public string IconName { get; set; }

        protected override bool DrawButton()
        {
            if (string.IsNullOrWhiteSpace(IconName))
                IconName = "UnityLogo";

            GUIContent buttonContent = EditorGUIUtility.IconContent(IconName);
            buttonContent.tooltip = _toolTip;

            return GUILayout.Button(
                buttonContent,
                GUILayout.Width(_width),
                GUILayout.Height(_height)
            );
        }
    }
}
