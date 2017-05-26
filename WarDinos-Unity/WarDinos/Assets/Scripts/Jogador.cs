using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour {
	//tipos de dinossauros do jogador
	private GameObject[] dinosRef;

	// inicio das lanes para spawn dos dinossauro
	public GameObject[] spawnPointLanes; 

	void Awake(){
		dinosRef = new GameObject[6];
		//carrega os dinos padrao
		dinosRef[0] =  (GameObject)Instantiate(Resources.Load("/Prefabs/Apatossauro"));	
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
