using UnityEngine;

namespace RossoForge.Toolbar.Editor
{
    public abstract class ButtonAlignmentProfile : ScriptableObject
    {
        [field: SerializeField]
        public ButtonProfile[] Buttons { get; private set; }
    }
}
