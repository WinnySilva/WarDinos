using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UnitButton : MonoBehaviour {
    /// <summary>
    ///  This class is used to differentiate unit buttons
    /// </summary>

    public Sprite spriteInFrame;
    public string dinosaur;
    public GameObject quantityText;

    private int quantityOnGroup = 0;


    public string getDinosaur()
    {
        return dinosaur;
    }

    public Sprite getSpriteInFrame()
    {
        return spriteInFrame;
    }

    public int getQuantityOnGroup()
    {
        return quantityOnGroup;
    }
    public void incQuantityOnGroup()
    {
        quantityOnGroup++;
        quantityText.GetComponent<Text>().text = quantityOnGroup.ToString();
    }
    public void decQuantityOnGroup()
    {
        if (quantityOnGroup > 0) {
            quantityOnGroup--;
            quantityText.GetComponent<Text>().text = quantityOnGroup.ToString();
        }
    }
    public void resetQuantityOnGroup()
    {
        quantityOnGroup = 0;
        quantityText.GetComponent<Text>().text = quantityOnGroup.ToString();
    }
}
