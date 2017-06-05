using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLocker : MonoBehaviour {
    /// <summary>
    ///  This class is used to prevent multiple animations to execute at the same time in one menu
    /// </summary>

    private bool menuLocked = false;

    public void lockMenu ()
    {
        menuLocked = true;
    }

    public void unlockMenu()
    {
        menuLocked = false;
    }

    public bool isLocked ()
    {
        return menuLocked;
    }
}
