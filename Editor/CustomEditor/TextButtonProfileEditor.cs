using UnityEditor;

namespace RossoForge.Toolbar.Editor
{
    [CustomEditor(typeof(TextButtonProfile))]
    public class TextButtonProfileEditor : ButtonProfileEditor
    {
        private SerializedProperty _textProp;
        private SerializedProperty _fontSizeProp;
        private SerializedProperty _textColorProp;

        protected override void OnEnable()
        {
            base.OnEnable();

            _textProp = serializedObject.FindProperty("_text");
            _fontSizeProp = serializedObject.FindProperty("_fontSize");
            _textColorProp = serializedObject.FindProperty("_textColor");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawTextProp();
            DrawFontSizeProp();
            DrawTextColorProp();
            base.DrawSize();
            base.DrawAddCallBackButton();
            base.DrawCallbacks();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawTextProp()
        {
            EditorGUILayout.PropertyField(_textProp);
        }
        private void DrawFontSizeProp()
        {
            EditorGUILayout.PropertyField(_fontSizeProp);
        }
        private void DrawTextColorProp()
        {
            EditorGUILayout.PropertyField(_textColorProp);
        }
    }
}
