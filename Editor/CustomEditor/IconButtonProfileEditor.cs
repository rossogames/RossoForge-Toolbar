using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Reflection;

namespace RossoForge.Toolbar.Editor
{
    [CustomEditor(typeof(IconButtonProfile))]
    public class IconDataEditor : UnityEditor.Editor
    {
        private static  List<ButtonCallbackInfo> _buttonCallbackInfo;
        private IconButtonProfile _targetData;

        private SerializedProperty _toolTipProp;
        private SerializedProperty _widthProp;
        private SerializedProperty _heightProp;
        private SerializedProperty _buttonCallbacks;

        private GUIContent _selectIconButtonContent;
        private List<bool> _foldouts = new List<bool>();

        private void OnEnable()
        {
            _targetData = (IconButtonProfile)target;

            _toolTipProp = serializedObject.FindProperty("_toolTip");
            _widthProp = serializedObject.FindProperty("_width");
            _heightProp = serializedObject.FindProperty("_height");
            _buttonCallbacks = serializedObject.FindProperty("_buttonCallbacks");

            _selectIconButtonContent = EditorGUIUtility.IconContent("Search Icon");
            _selectIconButtonContent.tooltip = "Select Icon";

            LoadCallbackInfo();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawToolTipProp();
            DrawSize();
            DrawIconSelector();
            DrawAddCallBackButton();
            DrawCallbacks();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawToolTipProp()
        {
            EditorGUILayout.PropertyField(_toolTipProp);
        }
        private void DrawSize()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Size");

            float previousLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 20;

            _widthProp.intValue = EditorGUILayout.IntField(new GUIContent("W:"), _widthProp.intValue);
            _heightProp.intValue = EditorGUILayout.IntField(new GUIContent("H:"), _heightProp.intValue);

            EditorGUIUtility.labelWidth = previousLabelWidth;
            EditorGUILayout.EndHorizontal();
        }
        private void DrawIconSelector()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Icon");

            DrawIcon();
            DrawSearchIconButton();

            EditorGUILayout.EndHorizontal();
        }
        private void DrawIcon()
        {
            if (!string.IsNullOrEmpty(_targetData.IconName))
            {
                GUIContent iconContent = EditorGUIUtility.IconContent(_targetData.IconName);
                if (iconContent != null && iconContent.image != null)
                {
                    iconContent.tooltip = string.Empty;
                    GUILayout.Label(iconContent, GUILayout.Width(32), GUILayout.Height(32));
                }
                else
                    DrawDefaultIcon();
            }
            else
                DrawDefaultIcon();
        }
        private void DrawDefaultIcon()
        {
            GUILayout.Label("UnityLogo", GUILayout.Width(32), GUILayout.Height(32));
        }
        private void DrawSearchIconButton()
        {
            if (GUILayout.Button(_selectIconButtonContent, GUILayout.Width(32), GUILayout.Height(32)))
            {
                IconSelectorWindows.ShowPopup((selectedIcon) =>
                {
                    _targetData.IconName = selectedIcon;
                    EditorUtility.SetDirty(_targetData);
                });
            }
        }
        private void DrawAddCallBackButton()
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.Space(5);

            if (GUILayout.Button("Add Callback"))
            {
                GenericMenu menu = new GenericMenu();
                foreach (var item in _buttonCallbackInfo)
                    menu.AddItem(new GUIContent(item.Description), false, () => AddCallback(item.Type));

                menu.ShowAsContext();
            }
        }
        private void DrawCallbacks()
        {
            EnsureFoldoutsListSize();

            for (int i = 0; i < _buttonCallbacks.arraySize; i++)
            {
                var element = _buttonCallbacks.GetArrayElementAtIndex(i);
                if (element.managedReferenceValue != null)
                {
                    var typeName = element.managedReferenceValue.GetType().Name;
                    EditorGUILayout.BeginHorizontal();

                    _foldouts[i] = EditorGUILayout.Foldout(_foldouts[i], $"{i} - {typeName}", true);
                    GUILayout.FlexibleSpace();

                    GUIContent deleteContent = EditorGUIUtility.IconContent("TreeEditor.Trash");
                    deleteContent.tooltip = "Delete Callback";

                    GUIStyle iconButtonStyle = new GUIStyle(GUI.skin.button);
                    iconButtonStyle.fixedWidth = 24;
                    iconButtonStyle.fixedHeight = 18;

                    if (GUILayout.Button(deleteContent, iconButtonStyle))
                    {
                        _buttonCallbacks.DeleteArrayElementAtIndex(i);
                        _foldouts.RemoveAt(i);
                        break;
                    }

                    EditorGUILayout.EndHorizontal();

                    if (_foldouts[i])
                    {
                        EditorGUILayout.BeginVertical("box");

                        SerializedProperty iterator = element.Copy();
                        SerializedProperty endProperty = iterator.GetEndProperty();

                        bool enterChildren = true;
                        while (iterator.NextVisible(enterChildren) && !SerializedProperty.EqualContents(iterator, endProperty))
                        {
                            EditorGUILayout.PropertyField(iterator, true);
                            enterChildren = false;
                        }

                        EditorGUILayout.EndVertical();
                    }
                }
            }

            EditorGUILayout.Space(10);
        }

        private void AddCallback(Type type)
        {
            var index = _buttonCallbacks.arraySize;
            _buttonCallbacks.InsertArrayElementAtIndex(index);
            var element = _buttonCallbacks.GetArrayElementAtIndex(index);
            element.managedReferenceValue = Activator.CreateInstance(type);
            serializedObject.ApplyModifiedProperties();
        }

        private void EnsureFoldoutsListSize()
        {
            while (_foldouts.Count < _buttonCallbacks.arraySize)
                _foldouts.Add(true);

            while (_foldouts.Count > _buttonCallbacks.arraySize)
                _foldouts.RemoveAt(_foldouts.Count - 1);
        }

        private void LoadCallbackInfo()
        {
            if (_buttonCallbackInfo != null)
                return;

            _buttonCallbackInfo = new List<ButtonCallbackInfo>();

            var types =
                AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsClass && !type.IsAbstract && typeof(ButtonCallback).IsAssignableFrom(type))
                .ToList();

            foreach (var type in types)
            {
                var attribute = type.GetCustomAttribute<DescriptionAttribute>();
                string description = attribute?.Description ?? string.Empty;

                _buttonCallbackInfo.Add(new ButtonCallbackInfo(description, type));
            }
        }

        internal class ButtonCallbackInfo
        {
            public ButtonCallbackInfo(string description, Type type)
            {
                Description = description;
                Type = type;
            }

            public string Description { get; private set; }
            public Type Type { get; private set; }
        }
    }
}
