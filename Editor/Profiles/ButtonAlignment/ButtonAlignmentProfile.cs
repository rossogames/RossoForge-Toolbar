using RossoForge.Toolbar.Editor.Profiles.Buttons;
using UnityEngine;

namespace RossoForge.Toolbar.Editor.Profiles.Alignments
{
    public abstract class ButtonAlignmentProfile : ScriptableObject
    {
        [field: SerializeField]
        public ButtonProfile[] Buttons { get; private set; }
    }
}
