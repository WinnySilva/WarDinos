using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unidade : MonoBehaviour {

	public Transform agent;
	public GameObject dinossauro;

	private Dinossauro dino;

	void Update(){
		if(agent != null){
			Vector3 p =  new Vector3(agent.position.x,1,agent.position.z+3);
			dinossauro.GetComponent<Transform>().position = p;
		}
	}

	// Use this for initialization
	void Start () {
		dino = Dinossauro.GetComponent<Dinossauro>();
	}

	public Transform Agent {
		get {
			return agent;
		}
	}

	public GameObject Dinossauro {
		get {
			return dinossauro;
		}
	}

	double Distance(Unidade uni){
		Vector3 p1 = uni.transform.position;
		Vector3 p2 = this.Agent.position;
		return Vector3.Distance(p1,p2);
	}

	void Atacar(Unidade uni){
		GameObject dinoInimigo = uni.Dinossauro;
		if(this.Distance(uni)< this.dino.Alcance_ataque){
			
		}
	
	}
	

}
