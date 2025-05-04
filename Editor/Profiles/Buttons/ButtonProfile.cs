using UnityEngine;

namespace RossoForge.Toolbar.Editor
{
    public abstract class ButtonProfile : ScriptableObject
    {
        [SerializeField]
        protected int _width;

        [SerializeField]
        protected int _height;

        [SerializeReference]
        private ButtonCallback[] _buttonCallbacks;

        public void TryDrawButton()
        {
            if (_buttonCallbacks == null)
                return;

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
