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
		int[] i = {1,2,3};
		DespacharGrupo(1,0,i);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void createUnidade(){
		GameObject go =  (GameObject)Instantiate(Resources.Load("/Prefabs/"));
	}

	public void DespacharGrupo(int numPlayer, int numLane, int[] dinoIds ){
		Debug.Log("DispatchGrupo "+numPlayer+" "+numLane);
		GameObject[] unidades = new GameObject[dinoIds.Length];
		int indexUnidades = 0;
		foreach(int i in dinoIds){
			unidades[indexUnidades] = this.players[numPlayer].createUnidade(i);
			/* pega seta o target para a base inimiga */
			unidades[indexUnidades].GetComponent<Unidade>().agent.GetComponent<AgentBehaviour>().base_inimiga = this.players[ (numPlayer ==0)?1:0].playerBase;

			/*seta na posicao inicial da lane */
	//		unidades[indexUnidades].  = this.players[numPlayer].lanesStart[numLane].transform;


			unidade.SetActive(true);
			indexUnidades++;
		}
	}

}
