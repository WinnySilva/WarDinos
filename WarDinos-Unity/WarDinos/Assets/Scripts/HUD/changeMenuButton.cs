using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeMenuButton : MonoBehaviour {
    public int menu;

    public int Menu {
        get {
            return menu;
        }
        set {
            if (value == 1 || value == 2)
                menu = value;
            else
                Debug.Log("menu precisa ser settado para 1 ou 2");
        }
    }
}
