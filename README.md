# Rosso Games

<table>
  <tr>
    <td><img src="https://github.com/rossogames/RossoForge-Toolbar/blob/master/logo.png?raw=true" alt="Rossoforge" width="64"/></td>
    <td><h2>Rossoforge - Toolbar</h2></td>
  </tr>
</table>

**Rossoforge-Toolbar** is a package for Unity that allows you to extend the editor toolbar by adding custom buttons in a simple and flexible way.

> [!IMPORTANT]
> **Compatibility:** This version requires **Unity 6.3 or superior**. 
> Due to significant API changes in Unity 6 that made previous implementations obsolete, this version has been simplified to focus on stability and native integration.

---

### What's new in this version?

- **Simplified Icon Buttons:** Following the API shift, we now focus on Icon Buttons. The legacy Text Button feature has been deprecated to ensure full compatibility with the new Unity 6 Toolbar system.
- **Native Customization:** You can now position your buttons exactly where you want. Use **Ctrl + Click & Drag** to move your custom buttons to any section of the toolbar (Left, Middle, or Right).

### Key Features:

- **Quick Integration:** Add custom buttons to your editor in seconds.
- **Chainable Actions:** Define multiple actions (callbacks) for a single button using easily extendable scripts (e.g., Open Scene + Enter Play Mode).
- **Workflow Optimization:** Save time and improve productivity with reusable button profiles.

---

### Implementation Example

Since buttons are no longer auto-mapped, you need to create a simple drawer script to load your profile and register the element:

```csharp
public static class CustomToolbarDrawer
{
    // Load your IconButtonProfile from a Resources folder
    public static IconButtonProfile _bootAndPlayButton = Resources.Load<IconButtonProfile>("Boot&Play");

    [MainToolbarElement("Custom Toolbar/Boot & Play", defaultDockPosition = MainToolbarDockPosition.Middle)]
    static MainToolbarElement LoadBootAndPlayButtonToolbar()
    {
        // Return the toolbar element using the profile data
        return _bootAndPlayButton.GetToolBarButton();
    }
}
```

---
### Old version

- Version: 1.3.0
- Watch tutorial on: [https://www.youtube.com/watch?v=A7qKsfk7OhU](https://www.youtube.com/watch?v=A7qKsfk7OhU)
- https://github.com/rossogames/Rossoforge-Toolbar/tree/release/v1.3.0

---
This package is part of the **Rossoforge** suite, designed to streamline and enhance Unity development workflows.

**Developed by Agustin Rosso** [LinkedIn Profile](https://www.linkedin.com/in/rossoagustin/)
