using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;
using RossoForge.Toolbar.Editor.Callbacks;

namespace RossoForge.Toolbar.Editor.Editors
{
    public abstract class ButtonProfileEditor : UnityEditor.Editor
    {
        protected static List<ButtonCallbackInfo> _buttonCallbackInfo;

        private List<bool> _foldouts = new List<bool>();
        private SerializedProperty _widthProp;
        private SerializedProperty _heightProp;
        private SerializedProperty _buttonCallbacks;

        protected virtual void OnEnable()
        {
            _widthProp = serializedObject.FindProperty("_width");
            _heightProp = serializedObject.FindProperty("_height");
            _buttonCallbacks = serializedObject.FindProperty("_buttonCallbacks");

            LoadCallbackInfo();
        }

        protected void DrawSize()
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

        protected void DrawAddCallBackButton()
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
        protected void DrawCallbacks()
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

        public class ButtonCallbackInfo
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
