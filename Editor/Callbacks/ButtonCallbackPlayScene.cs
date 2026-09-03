using System;
using System.ComponentModel;
using UnityEditor;

namespace Rossoforge.Toolbar.Callbacks
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