using UnityEngine;

namespace RossoForge.Toolbar.Editor.Profiles.Buttons
{
    [CreateAssetMenu(fileName = nameof(TextButtonProfile), menuName = "RossoForge/Toolbar/Buttons/Text Button")]
    public class TextButtonProfile : ButtonProfile
    {
        [SerializeField]
        private string _text;

        [SerializeField]
        private int _fontSize;

        [SerializeField]
        private Color _textColor;

        public TextButtonProfile()
        {
            _width = 80;
            _height = 20;
            _text = "Text";
            _fontSize = 12;
            _textColor = Color.white;
        }

        protected override bool DrawButton()
        {
            var _customStyle = new GUIStyle(GUI.skin.button);
            _customStyle.fontSize = _fontSize;
            _customStyle.normal.textColor = _textColor;

            return GUILayout.Button(
                _text,
                _customStyle,
                GUILayout.Width(_width),
                GUILayout.Height(_height)
            );
        }
    }
}
