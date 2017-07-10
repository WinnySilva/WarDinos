using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Windows.Forms;

public class PlayerIA : MonoBehaviour {
	public GameObject player;
	public GameObject hud;

	private HUDController playerHudController;
	private Player playerScr;


	void Awake(){
		playerHudController = hud.GetComponent<HUDController>();
		playerScr = player.GetComponent<Player>();
	}
	// Use this for initialization
	void Start () {
		Invoke("Jogar",1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Jogar(){
		int mv = Random.Range(0,6);
		Debug.Log(" IA JOGAR");
	/*	Debug.Log(this.playerHudController.keyUp );
		Debug.Log(this.playerHudController.keyLeft);
		Debug.Log(this.playerHudController.keyRight);
		Debug.Log(this.playerHudController.keyDown );
		Debug.Log(this.playerHudController.keyConfirm);
*/

		Event e = Event.KeyboardEvent(""+this.playerHudController.keyRight +"");
		e.Use();
	}

}
