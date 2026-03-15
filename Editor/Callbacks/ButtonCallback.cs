using System;

namespace Rossoforge.Toolbar.Editor.Callbacks
{
    [Serializable]
    public abstract class ButtonCallback
    {
        public abstract bool Invoke();
    }
}
