using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unidade : MonoBehaviour {

	public GameObject agent;
	public GameObject dinossauro;

	private Dinossauro dino;
	private Transform agentTransform;

	private int playerID;

	public Unidade (GameObject ag, GameObject din, int playerId){
	//	this.agent = ag;
	//	this.dinossauro = din;
		this.playerID = playerId;
		this.Agent = ag;
		this.Dinossauro = din;
		//agent.transform.SetParent(this.transform);
		//dinossauro.transform.SetParent(this.transform);
	}

	public void ReplaceValues(Unidade un){
		this.agent = un.agent;
		this.dinossauro = un.dinossauro;
		this.dino = un.dino;
		this.agentTransform = un.agentTransform;
	}
	void Awake(){
		dino = dinossauro.GetComponent<Dinossauro>();
		agentTransform = agent.GetComponent<Transform>();
		if(dinossauro!=null){
			dinossauro.transform.SetParent(this.transform);
		}
		if(agentTransform !=null){
			agent.transform.SetParent(this.transform);
		}

	}

	void Update(){
		if(agentTransform != null){
			Vector3 p =  new Vector3(agentTransform.position.x,1,agentTransform.position.z+3);
			dinossauro.GetComponent<Transform>().position = p;
		}
	}
		


	public int PlayerID {
		get {
			return playerID;
		}
	}

	double Distance(Unidade uni){
		Vector3 p1 = uni.transform.position;
		Vector3 p2 = this.agentTransform.position;
		return Vector3.Distance(p1,p2);
	}

	void Atacar(Unidade uni){
		Dinossauro dinoInimigo = uni.dino ;
		if(this.Distance(uni)< this.dino.Alcance_ataque){
			dinoInimigo.Vida += dino.Ataque;
		}
	}



	public GameObject Agent {
		get {
			return agent;
		}
		set{
			agent = value;
			agent.transform.SetParent(this.transform);
			this.agentTransform = agent.transform;
		}
	}

	public GameObject Dinossauro {
		get {
			return dinossauro;
		}
		set{
			dinossauro = value;
			dinossauro.transform.SetParent(this.transform);
			this.dino = dinossauro.GetComponent<Dinossauro>();

		}
	}
}
