using System;
using System.ComponentModel;
using UnityEditor;

namespace RossoForge.Toolbar.Editor.Callbacks
{
    [Serializable]
    [Description("Play Current Scene")]
    public class ButtonCallbackPlayScene : ButtonCallback
    {
        public override bool Invoke()
        {
            EditorApplication.isPlaying = true;
            return true;
        }
    }
}