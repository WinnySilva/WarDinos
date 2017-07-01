using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WDHudAttributeButton : MonoBehaviour {
    /// <summary>
    ///  This class is used to differentiate attribute buttons
    /// </summary>

    public string Attribute;
    public Attributes AttrType;

    public string getAttribute ()
    {
        return Attribute;
    }
}
