using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Windows.Forms;

public class PlayerIA : MonoBehaviour {
	public GameObject player;
	public GameObject hud;

	public GameObject[] inicioLanes;
	public GameObject[] fimLanes;
	public GameObject[] dinoPrefab;

	private HUDController playerHudController;
	private Player playerScr;
	private GameObject dinoGroupPrefab;
	private float iniTime = 5f;
	private float repeatTime= 5f;
	void Awake(){
		playerHudController = hud.GetComponent<HUDController>();
		playerScr = player.GetComponent<Player>();
		dinoGroupPrefab = this.playerHudController.dinoGroupPrefab;

		int n = this.playerHudController.transform.childCount;
		for(int i =0; i<n-1; i++){
			this.playerHudController.transform.GetChild (i).gameObject.SetActive (false);
		}

	}
	// Use this for initialization
	void Start () {
		InvokeRepeating("Jogar",iniTime,repeatTime);
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

		Debug.Log(" "+KeyCode.RightArrow.ToString() );

		Event e = Event.KeyboardEvent( KeyCode.RightArrow.ToString() );

		e.Use(); */
		jogarDinoLane ();
	
	}

	private void jogarDinoLane(){


		int ln = Random.Range (0, 3);
		GroupController.DinoType e;

		GroupController.DinoType[] arg_dinos = {
			randomDino(),randomDino(),randomDino(),randomDino()
		};
			
		this.despacharGrupo (ln, arg_dinos);


	}

	private void despacharGrupo(int lane,GroupController.DinoType[] arg_dinos){
		GameObject gb = new GameObject ();
		//GroupController gp = gb.AddComponent<GroupController> ();
		GroupController gp = Instantiate(dinoGroupPrefab).GetComponent<GroupController>();

		//gb.SetActive (false);
		gp.initGroup(
			playerScr.playerID,
			this.inicioLanes[lane],
			this.fimLanes[lane],
			arg_dinos,
			this.dinoPrefab[0],
			this.dinoPrefab[1],
			this.dinoPrefab[2],
			this.dinoPrefab[3],
			this.dinoPrefab[4],
			this.dinoPrefab[5]
		);

	}

	private GroupController.DinoType randomDino(){
		int rnd = Random.Range (0, 7);
		switch (rnd) {
		case 0: 
			return GroupController.DinoType.NONE;
			break;
		case 1:
			return GroupController.DinoType.APATOSSAURO;
			break;
		case 2:
			return GroupController.DinoType.ESTEGOSSAURO;
			break;
		case 3:
			return GroupController.DinoType.PTERODACTILO;
			break;
		case 4:
			return GroupController.DinoType.RAPTOR;
			break;
		case 5:
			return GroupController.DinoType.TREX ;
			break;
		case 6:
			return GroupController.DinoType.TRICERATOPO ;
			break;
		default:
			return GroupController.DinoType.NONE;
			break;
		}
		return GroupController.DinoType.NONE;
	}

}
