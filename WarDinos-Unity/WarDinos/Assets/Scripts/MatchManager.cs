using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour {
	public GameObject unidade;
	public Jogador[] players;

	// inicio das lanes para spawn dos dinossauro
	public GameObject[] spawnPointLanes; 

	void Awake(){
		players = new Jogador[2];
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void createUnidade(){
		GameObject go =  (GameObject)Instantiate(Resources.Load("/Prefabs/"));
	}

}
