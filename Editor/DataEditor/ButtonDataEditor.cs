using Rossoforge.Toolbar.Callbacks;
using System;
using UnityEngine;

namespace Rossoforge.Toolbar.DataEditor
{
    public abstract class ButtonDataEditor : ScriptableObject
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
