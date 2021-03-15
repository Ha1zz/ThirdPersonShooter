using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppEvents 
{
    // Start is called before the first frame update
    public delegate void MouseCursorEnable(bool enabled);

    public static event MouseCursorEnable MouseCursorEnabled;

    public static void Invoke_OnMouseCursorEnble(bool enabled)
    {
        MouseCursorEnabled?.Invoke(enabled);
    }
}
