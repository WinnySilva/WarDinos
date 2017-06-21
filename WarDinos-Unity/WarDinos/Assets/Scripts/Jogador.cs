using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour {


	//tipos de dinossauros do jogador
	private GameObject[] dinosRef;
	private int id;
	private int hitPoint = 1000;
	private int recursos = 300;
	private Unidade[] grupoDino;

	public GameObject playerBase;
	public GameObject[] lanesStart;
	public GameObject RefDinos;
	public GameObject unidades;

	void Awake(){
		dinosRef = new GameObject[6];
		//carrega os dinos padrao
		dinosRef[0] =  (GameObject)Instantiate(Resources.Load("Prefabs/Dinos/Apatossauro"));
		dinosRef[1] =  (GameObject)Instantiate(Resources.Load("Prefabs/Dinos/estegossauro"));
		dinosRef[2] =  (GameObject)Instantiate(Resources.Load("Prefabs/Dinos/Pterodactilo"));
		dinosRef[3] =  (GameObject)Instantiate(Resources.Load("Prefabs/Dinos/raptor"));
		dinosRef[4] =  (GameObject)Instantiate(Resources.Load("Prefabs/Dinos/t_rex"));
		dinosRef[5] =  (GameObject)Instantiate(Resources.Load("Prefabs/Dinos/triceratopo"));

		foreach(GameObject go in dinosRef){
			go.name=" player ref";
			go.SetActive(false);
			go.transform.SetParent(RefDinos.transform);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	private GameObject CreateDinoRef(int indiceDino){
		GameObject novoDino = Instantiate(dinosRef[indiceDino]);
		novoDino.GetComponent<Dinossauro>().PlayerID = id;
		return novoDino;
	}

	/*RETORNA UM GAMEOBJECT COM A UNIDADE ATTACHED */
	public GameObject createUnidade(int dinoNumber, GameObject base_inimiga){
		// agente
		GameObject agente = new GameObject();
		agente.AddComponent<AgentBehaviour>();
		agente.GetComponent<AgentBehaviour>().Base_inimiga= base_inimiga;
		agente.name="agente"+dinoNumber;

		//dinossauro
		GameObject dino = this.CreateDinoRef(dinoNumber);
		dino.name="dino"+this.id+" "+dinoNumber;
		//unidade
		Unidade uni = new Unidade(agente,dino, this.id);

		//gameobject da unidade
		GameObject gb = new GameObject();
		gb.SetActive(false);
		gb.AddComponent<Unidade>();
		gb.GetComponent<Unidade>().ReplaceValues(uni);
		gb.name= "unidadePlayer"+this.id; 
		gb.transform.SetParent(this.unidades.transform);

		return gb;
	}

	public int HitPoint {
		get {
			return hitPoint;
		}
	}

	public int Recursos {
		get {
			return recursos;
		}
	}
}
