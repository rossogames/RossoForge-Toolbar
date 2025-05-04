using RossoForge.Toolbar.Editor.Profiles.Alignments;
using RossoForge.Toolbar.Editor.Profiles.Buttons;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RossoForge.Toolbar.Editor
{
    [InitializeOnLoad]
    public static class ToolbarDrawer
    {
        private static ButtonAlignmentProfile[] _alignmentProfile;

        private static readonly List<Action> LeftToolbarGUI = new List<Action>();
        private static readonly List<Action> RightToolbarGUI = new List<Action>();

        static ToolbarDrawer()
        {
            _alignmentProfile = Resources.LoadAll<ButtonAlignmentProfile>(string.Empty);
            LoadLeftToolbar();
            LoadCenterLeftToolbar();
            LoadCenterRightToolbar();
            LoadRightToolbar();

            ToolbarCallback.OnToolbarGUILeft = GUILeft;
            ToolbarCallback.OnToolbarGUIRight = GUIRight;
        }

        private static void LoadLeftToolbar()
        {
            var buttonLeftAlignmentProfile = GetAlignmentProfile<ButtonLeftAlignmentProfile>();
            if (buttonLeftAlignmentProfile == null)
                return;

            LeftToolbarGUI.Add(() =>
            {
                foreach (ButtonProfile profile in buttonLeftAlignmentProfile.Buttons)
                    profile.TryDrawButton();
            });
        }
        private static void LoadCenterLeftToolbar()
        {
            var buttonCenterLeftAlignmentProfile = GetAlignmentProfile<ButtonCenterLeftAlignmentProfile>();
            if (buttonCenterLeftAlignmentProfile == null)
                return;

            LeftToolbarGUI.Add(() =>
            {
                GUILayout.FlexibleSpace();
                foreach (ButtonProfile profile in buttonCenterLeftAlignmentProfile.Buttons)
                    profile.TryDrawButton();
            });
        }
        private static void LoadCenterRightToolbar()
        {
            var buttonCenterRightAlignmentProfile = GetAlignmentProfile<ButtonCenterRightAlignmentProfile>();
            if (buttonCenterRightAlignmentProfile == null)
                return;

            RightToolbarGUI.Add(() =>
            {
                foreach (ButtonProfile profile in buttonCenterRightAlignmentProfile.Buttons)
                    profile.TryDrawButton();
            });
        }
        private static void LoadRightToolbar()
        {
            var buttonRightAlignmentProfile = GetAlignmentProfile<ButtonRightAlignmentProfile>();
            if (buttonRightAlignmentProfile == null)
                return;

            RightToolbarGUI.Add(() =>
            {
                GUILayout.FlexibleSpace();
                foreach (ButtonProfile profile in buttonRightAlignmentProfile.Buttons)
                    profile.TryDrawButton();
            });
        }
        private static T GetAlignmentProfile<T>() where T : ButtonAlignmentProfile
        {
            if (_alignmentProfile == null)
                return default;

            foreach (ButtonAlignmentProfile profile in _alignmentProfile)
            {
                if (profile is T tProfile)
                    return tProfile;
            }

            return default;
        }
        private static void GUILeft()
        {
            GUILayout.BeginHorizontal();
            foreach (var handler in LeftToolbarGUI)
            {
                handler();
            }
            GUILayout.EndHorizontal();
        }
        private static void GUIRight()
        {
            GUILayout.BeginHorizontal();
            foreach (var handler in RightToolbarGUI)
            {
                handler();
            }
            GUILayout.EndHorizontal();
        }
    }
}
