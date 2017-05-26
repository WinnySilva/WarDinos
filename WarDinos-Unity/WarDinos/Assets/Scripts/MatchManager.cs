using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour {
	public GameObject unidade;
	Jogador[] players;

	void Awake(){
		
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
