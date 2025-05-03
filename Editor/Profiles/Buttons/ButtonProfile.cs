using UnityEngine;

namespace RossoForge.Toolbar.Editor
{
    public abstract class ButtonProfile : ScriptableObject
    {
        [SerializeField]
        protected string _toolTip;

        [SerializeField]
        protected int _width = 35;

        [SerializeField]
        protected int _height = 20;

        [SerializeReference]
        private ButtonCallback[] _buttonCallbacks;

        public void TryDrawButton()
        {
            foreach (var callback in _buttonCallbacks)
            {
                if (!callback.Enabled)
                    return;
            }

            if (!DrawButton())
                return;

            foreach (var callback in _buttonCallbacks)
            {
                if (!callback.Invoke())
                    break;
            }
        }

        protected virtual bool DrawButton()
        {
            return false;
        }
    }
}
