using System;

namespace RossoForge.Toolbar.Editor
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
