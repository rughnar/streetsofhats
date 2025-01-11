using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenToggler : MonoBehaviour
{
    public void Fullscreen()
    {
        Screen.fullScreen = true;
    }

    public void Windowed()
    {
        Screen.fullScreen = false;
    }

    public void Change()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
