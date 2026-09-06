using System;

namespace Rossoforge.Toolbar.Callbacks
{
    [Serializable]
    public abstract class ButtonCallback
    {
        public abstract bool Invoke();
    }
}
