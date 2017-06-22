using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MatchManager : MonoBehaviour {
	public Jogador[] players;

	// inicio das lanes para spawn dos dinossauro
	public GameObject[] spawnPointLanes;

	void Awake(){
		//players = new Jogador[2];
	}
	// Use this for initialization
	void Start () {
		int[] i = {1,2,3};
		DespacharGrupo(0,0,i);
		DespacharGrupo(0,1,i);
		DespacharGrupo(0,2,i);
		//--
		DespacharGrupo(1,0,i);
		DespacharGrupo(1,1,i);
		DespacharGrupo(1,2,i);
	}
	
	// Update is called once per frame
	void Update () {
		//percorreUnidadesParaAtacar();
		Unidade uniPl;
		AgentBehaviour nvA;
		foreach(GameObject pl in this.players[0].unidades ){
			uniPl = pl.GetComponent<Unidade>();
			nvA = uniPl.Agent.GetComponent<AgentBehaviour>();
			uniPl.Atacar(nvA.GetRandomUnidadeNaLane() );
		}
		foreach(GameObject pl1 in this.players[1].unidades){
			uniPl = pl1.GetComponent<Unidade>();
			nvA = uniPl.Agent.GetComponent<AgentBehaviour>();
			uniPl.Atacar(nvA.GetRandomUnidadeNaLane() );
		}

	}

	private void percorreUnidadesParaAtacar(){
		bool attack= false;
		Unidade uniPl1, uniPl2;
		foreach(GameObject pl in this.players[0].unidades ){
			uniPl1 = pl.GetComponent<Unidade>();
			foreach(GameObject pl1 in this.players[1].unidades){
				uniPl2 = pl1.GetComponent<Unidade>();
				if( uniPl2.isNearToAttack(uniPl1) ){
					uniPl2.Atacar(uniPl1);
					attack = true;
				}
				if( uniPl1.isNearToAttack(uniPl2 ) ){
					uniPl1.Atacar(uniPl2);
					attack = true;
				}
				if(attack){
					break;
				}

			}
		}
	}

	public void createUnidade(){
		GameObject go =  (GameObject)Instantiate(Resources.Load("/Prefabs/"));
	}

	public void DespacharGrupo(int numPlayer, int numLane, int[] dinoIds ){
		Debug.Log("DispatchGrupo "+numPlayer+" "+numLane);
		GameObject[] unidades = new GameObject[dinoIds.Length];
		int indexUnidades = 0;
		if(players[numPlayer]==null){
			Debug.Log("player null");
		}
		int mediaVelo =0;
		GameObject baseInimiga =this.players[ (numPlayer ==0)?1:0 ].playerBase;
		foreach(int i in dinoIds){
			unidades[indexUnidades] = this.players[numPlayer].createUnidade(i,baseInimiga);
			/* pega seta o target para a base inimiga */

			/*seta na posicao inicial da lane */
	//		unidades[indexUnidades].  = this.players[numPlayer].lanesStart[numLane].transform;
			unidades[indexUnidades].transform.SetParent(this.players[numPlayer].lanesStart[numLane].transform);
			unidades[indexUnidades].GetComponent<Unidade>().agent.GetComponent<AgentBehaviour>().base_inimiga = baseInimiga;
			unidades[indexUnidades].transform.localPosition = new Vector3(0,0,0);

			mediaVelo += unidades[indexUnidades].GetComponent<Unidade>().Dino.Velocidade_deslocamento;
			Debug.Log("DESPACHADO "+indexUnidades);
			indexUnidades++;
		}
		mediaVelo = mediaVelo / dinoIds.Length;

		foreach(GameObject d in unidades){
			d.SetActive(true);
			//Quaternion q;
			//q.eu
			d.GetComponent<Unidade>().Agent.GetComponent<NavMeshAgent>().speed = mediaVelo;
			d.GetComponent<Unidade>().dino.transform.rotation= Quaternion.Euler(new Vector3(90,0,0) ) ;
			//SE O PLAYER FOR O DIREITO FLIP
			if(numPlayer==1){
				Vector3 v = d.GetComponent<Unidade>().dino.transform.localScale;
				v.x = -v.x;
				d.GetComponent<Unidade>().dino.transform.localScale =v;
			}
		}

	}

}
