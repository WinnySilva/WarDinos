using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WDHudUpgradeButton : MonoBehaviour {
    /// <summary>
    ///  This class is used to differentiate upgrade buttons
    /// </summary>

    public string Dinosaur;
    public GroupController.DinoType DinoType;
    public Sprite SpriteHabilidade;

    public bool[] attributesInMaxLevel;

    void Start ()
    {
        attributesInMaxLevel = new bool[5] { false, false, false, false, false };
    }

    public string getDinosaur() {
        return Dinosaur;
    }
}
