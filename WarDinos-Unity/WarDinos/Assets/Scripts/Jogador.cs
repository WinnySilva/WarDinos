using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour {


	//tipos de dinossauros do jogador
	private GameObject[] dinosRef;
	private int id;
	private int hitPoint = 1000;
	private Unidade[] grupoDino;

	void Awake(){
		dinosRef = new GameObject[6];
		//carrega os dinos padrao
		dinosRef[0] =  (GameObject)Instantiate(Resources.Load("/Prefabs/Dinos/Apatossauro"));	
		dinosRef[1] =  (GameObject)Instantiate(Resources.Load("/Prefabs/Dinos/estegossauro"));
		dinosRef[2] =  (GameObject)Instantiate(Resources.Load("/Prefabs/Dinos/Pterodactilo"));
		dinosRef[3] =  (GameObject)Instantiate(Resources.Load("/Prefabs/Dinos/raptor"));
		dinosRef[4] =  (GameObject)Instantiate(Resources.Load("/Prefabs/Dinos/t_rex"));
		dinosRef[5] =  (GameObject)Instantiate(Resources.Load("/Prefabs/Dinos/triceratopo"));
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public GameObject CreateDinoRef(int indiceDino){
		GameObject novoDino = Instantiate(dinosRef[indiceDino]);
		novoDino.GetComponent<Dinossauro>().PlayerID = id;
		return novoDino;
	}

	public int HitPoint {
		get {
			return hitPoint;
		}
	}
}
