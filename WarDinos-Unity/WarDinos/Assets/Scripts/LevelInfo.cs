using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MongoDB.Bson.Serialization.Attributes;
using UnityEngine.UI;

public class LevelInfo : MonoBehaviour {
	public enum GAME_MODE {SINGLE=0, MULTI=1 };

	public GAME_MODE gameMode;
	public int lvl=1;
	public int id;
	[BsonIgnoreAttribute]
	private LoggerMongo logg;

	private string tempoJogo;

	void Awake(){
		
		DontDestroyOnLoad (this);
		Debug.LogError ("LevelInfo  "+this.GetInstanceID() );
		if( FindObjectsOfType(this.GetType() ).Length >1 ){
			Destroy (gameObject);
		}
		id = this.GetInstanceID ();
		logg = new LoggerMongo (this.GetType() );
		logg.classId = this.GetInstanceID();
		logg.msg = "INICIANDO LVL INFO";
		logg.acao = "INICIANDO";
		logg.writeLog ();
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
		SceneManager.sceneUnloaded += OnSceneUnload;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (SceneManager.GetActiveScene ().buildIndex == 3) {
			this.tempoJogo = GameObject.Find ("TextTempo").GetComponent<Text> ().text;
		}
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		int scenaN = scene.buildIndex;
		Debug.Log ("cena: "+scenaN);

		logg.msg = "MUDANDO DE CENA";
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

	void OnSceneUnload(Scene scene){

		Debug.LogError ("SAINDO DA CENA " + scene.buildIndex);
		int sceneID = scene.buildIndex;
		logg.acao = "MUDANDO DE CENA";
		logg.msg = "SAINDO DA CENA "+sceneID;
		switch (sceneID) {
		case 3:
			logg.classId = this.GetInstanceID ();
			logg.tempoDePartida = this.tempoJogo;
			logg.modoJogo = this.gameMode.ToString ();
			logg.LevelJogo = this.lvl;
			logg.writeLog ();
			Debug.LogError (":: SAINDO DA CENA " + scene.buildIndex);
			break;

		}


	}
}
