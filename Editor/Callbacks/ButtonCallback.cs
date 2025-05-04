using System;

namespace RossoForge.Toolbar.Editor.Callbacks
{
    [Serializable]
    public abstract class ButtonCallback
    {
        public virtual bool Enabled 
        {
            get => true;
        }
     
        public abstract bool Invoke();
    }
}
