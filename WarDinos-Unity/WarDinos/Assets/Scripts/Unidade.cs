using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unidade : MonoBehaviour {

	public GameObject agent;
	public GameObject dinossauro;

	public Dinossauro dino;
	private Transform agentTransform;


	public  int playerID;

	public void setUnidade (GameObject ag, GameObject din, int playerId){
	//	this.agent = ag;
	//	this.dinossauro = din;
		this.playerID = playerId;
		this.Agent = ag;
		this.Dinossauro = din;
		agent.transform.SetParent(this.transform);
		dinossauro.transform.SetParent(this.transform);
		agent.transform.localPosition= new Vector3(0,0,0);
		dinossauro.transform.localPosition = new Vector3(0,0,0);
	

	}

	public void ReplaceValues(Unidade un){
		this.agent = un.agent;
		this.dinossauro = un.dinossauro;
		this.dino = un.dino;
		this.agentTransform = un.agentTransform;
	}
	void Start(){
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

			Vector3 p =  new Vector3(agentTransform.position.x, 3,agentTransform.position.z+ this.dino.NSlot);
			dinossauro.transform.position = p;
		}
		NavMeshAgent ag = agentTransform.GetComponent<NavMeshAgent>();
		if( (dino!=null)&&(dino.Vida <= 0)  ){
			Destroy(this);
		}

	//	Debug.Log(" (ag.pathStatus ): "+(ag.pathStatus ));
	}
		


	public int PlayerID {
		get {
			return playerID;
		}
	}

	public double Distance(Unidade uni){
		Vector3 p1 = uni.transform.position;
		Vector3 p2 = this.agentTransform.position;
		return Vector3.Distance(p1,p2);
	}
	public bool isNearToAttack(Unidade uni){

		return this.Distance(uni)<= this.dino.Alcance_ataque;

	}

	public Dinossauro Dino {
		get {
			return dino;
		}
	}

	public bool Atacar(Unidade uni){
		if(uni == null) return false;
		Dinossauro dinoInimigo = uni.dino ;
		if(this.Distance(uni)< this.dino.Alcance_ataque){
			dinoInimigo.Vida -= dino.Ataque;
		}
		return true;
	}



	public GameObject Agent {
		get {
			return agent;
		}
		set{
			agent = value;
			agent.transform.SetParent(this.transform);
			this.agentTransform = agent.transform;
			agent.name = "uniade"+this.playerID;
		}
	}

	public GameObject Dinossauro {
		get {
			return dinossauro;
		}
		set{
			dinossauro = value;
		//	dinossauro.transform.SetParent(this.transform);
			this.dino = dinossauro.GetComponent<Dinossauro>();

		}
	}
}
