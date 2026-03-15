using Rossoforge.Toolbar.Editor.Callbacks;
using System;
using UnityEngine;

namespace Rossoforge.Toolbar.Editor.Profiles.Buttons
{
    public abstract class ButtonProfile : ScriptableObject
    {
        [SerializeReference]
        private ButtonCallback[] _buttonCallbacks;

        public Action GetCallback()
        {
            return () =>
            {
                if (_buttonCallbacks == null)
                    return;

                foreach (var callback in _buttonCallbacks)
                {
                    if (!callback.Invoke())
                        break;
                }
            };
        }
    }
}
