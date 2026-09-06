using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;

namespace Rossoforge.Toolbar.DataTool
{
    [CreateAssetMenu(fileName = nameof(IconButtonDataTool), menuName = "Rossoforge/Data Tools/Toolbar/Icon Button")]
    public class IconButtonDataTool : ButtonDataTool
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
