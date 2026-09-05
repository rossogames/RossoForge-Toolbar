#if UNITY_EDITOR
using Rossoforge.Toolbar.DataTool;
using UnityEditor.Toolbars;
using UnityEngine;

public static class CustomToolbarDrawer
{
    public static IconButtonDataTool _bootAndPlayButton = Resources.Load<IconButtonDataTool>("Boot&Play");

    [MainToolbarElement("Custom Toolbar/Boot & Play", defaultDockPosition = MainToolbarDockPosition.Middle)]
    static MainToolbarElement LoadBootAndPlayButtonToolbar()
    {
        return _bootAndPlayButton.GetToolBarButton();
    }
}
#endif