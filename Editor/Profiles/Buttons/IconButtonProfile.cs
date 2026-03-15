using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;

namespace Rossoforge.Toolbar.Editor.Profiles.Buttons
{
    [CreateAssetMenu(fileName = nameof(IconButtonProfile), menuName = "Rossoforge/Toolbar/Buttons/Icon Button")]
    public class IconButtonProfile : ButtonProfile
    {
        [SerializeField]
        public string _toolTip;

        [field: SerializeField]
        [Tooltip("EditorGUIUtility.IconContent")]
        public string IconName { get; set; }

        public MainToolbarElement GetToolBarButton()
        {
            var icon = EditorGUIUtility.IconContent(IconName).image as Texture2D;
            var content = new MainToolbarContent(icon, _toolTip);

            return new MainToolbarButton(content, GetCallback());
        }
    }
}
