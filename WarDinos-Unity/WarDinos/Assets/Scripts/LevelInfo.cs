﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInfo : MonoBehaviour {
	public enum GAME_MODE {SINGLE=0, MULTI=1 };

	public GAME_MODE gameMode;
	public int lvl=1;
	public int id;

	void Awake(){
		DontDestroyOnLoad (this);
		Debug.LogError ("LevelInfo  "+this.GetInstanceID() );
		if( FindObjectsOfType(this.GetType() ).Length >1 ){
			Destroy (gameObject);
		}
		id = this.GetInstanceID ();
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		int scenaN = scene.buildIndex;
		Debug.Log ("cena: "+scenaN);
		switch (scenaN) {
		case 0: 
			this.lvl = 1;
			break;

		case 3:
			/*se for tela de jogo */
			GameObject playerIA;
			playerIA = GameObject.Find("IA") ; //GameObject.FindGameObjectWithTag ("PlayerIA");
			Debug.Log ("player: "+playerIA);
			if( gameMode == GAME_MODE.SINGLE){
				if (playerIA != null) {
					playerIA.SetActive (true);
				}
			}
			else if (gameMode == GAME_MODE.MULTI) {
				if (playerIA != null) {
				//	playerIA.SetActive (false);
			}
			}
			break;

		case 4:
			/*se for tela de remake */
			if( gameMode == GAME_MODE.SINGLE){
				this.lvl++;
			}
			break;

		}

	}
}
