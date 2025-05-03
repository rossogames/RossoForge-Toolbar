using System;
using UnityEngine;
using UnityEditor;
using System.Reflection;

using UnityEngine.UIElements;

namespace RossoForge.Toolbar.Editor
{
    public static class ToolbarCallback
    {
        private static readonly Type _toolbarType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.Toolbar");
        private static ScriptableObject _currentToolbar;

        public static Action OnToolbarGUILeft;
        public static Action OnToolbarGUIRight;

        static ToolbarCallback()
        {
            EditorApplication.update -= OnUpdate;
            EditorApplication.update += OnUpdate;
        }

        private static void OnUpdate()
        {
            if (_currentToolbar != null)
                return;

            FindToolbar();
            if (_currentToolbar == null)
                return;

            var rawRoot = GetVisualRoot();
            RegisterCallback("ToolbarZoneLeftAlign", OnToolbarGUILeft, rawRoot);
            RegisterCallback("ToolbarZoneRightAlign", OnToolbarGUIRight, rawRoot);
        }

        private static void FindToolbar()
        {
            var toolbars = Resources.FindObjectsOfTypeAll(_toolbarType);
            _currentToolbar = toolbars.Length > 0 ? (ScriptableObject)toolbars[0] : null;
        }

        private static VisualElement GetVisualRoot()
        {
            var root = _toolbarType.GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance);
            return root.GetValue(_currentToolbar) as VisualElement;
        }

        private static void RegisterCallback(string root, Action callback, VisualElement rawRoot)
        {
            var toolbarZone = rawRoot.Q(root);
            var parent = new VisualElement()
            {
                style = {
                    flexGrow = 1,
                    flexDirection = FlexDirection.Row,
                }
            };
            var container = new IMGUIContainer();
            container.style.flexGrow = 1;
            container.onGUIHandler += () =>
            {
                callback?.Invoke();
            };
            parent.Add(container);
            toolbarZone.Add(parent);
        }
    }
}
