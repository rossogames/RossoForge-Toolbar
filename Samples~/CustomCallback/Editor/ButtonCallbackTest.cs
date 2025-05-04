using RossoForge.Toolbar.Editor.Callbacks;
using System;
using System.ComponentModel;
using UnityEngine;

[Serializable]
[Description("Custom callback Test")]
public class ButtonCallbackTest : ButtonCallback
{
    [SerializeField]
    private string _pintText;

    public override bool Invoke()
    {
        UnityEngine.Debug.LogWarning(_pintText);
        return true;
    }
}
