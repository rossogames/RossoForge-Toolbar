using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;

namespace Rossoforge.Toolbar.DataEditor
{
    [CreateAssetMenu(fileName = nameof(IconButtonDataEditor), menuName = "Rossoforge/Data Editor/Toolbar/Icon Button")]
    public class IconButtonDataEditor : ButtonDataEditor
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
