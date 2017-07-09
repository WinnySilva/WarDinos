using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelDinoAttributes : MonoBehaviour {
    public GameObject pgo;
    public GameObject hud;

    private Player pgoPlayer;
    private HUDController hudController;

	// Use this for initialization
	void Start () {
        pgoPlayer = pgo.GetComponent<Player>();
        hudController = hud.GetComponent<HUDController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
