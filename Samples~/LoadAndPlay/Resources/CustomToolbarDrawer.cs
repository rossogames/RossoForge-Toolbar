using Rossoforge.Toolbar.Editor.Profiles.Buttons;
using UnityEditor.Toolbars;
using UnityEngine;

public static class CustomToolbarDrawer
{
    public static IconButtonProfile _bootAndPlayButton = Resources.Load<IconButtonProfile>("Boot&Play");

    [MainToolbarElement("Custom Toolbar/Boot & Play", defaultDockPosition = MainToolbarDockPosition.Middle)]
    static MainToolbarElement LoadBootAndPlayButtonToolbar()
    {
        return _bootAndPlayButton.GetToolBarButton();
    }
}
