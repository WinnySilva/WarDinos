using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentBehaviour : MonoBehaviour {
	private NavMeshAgent agent;
	public GameObject base_inimiga;
	public List<Unidade> enemies;
	public Unidade minhaUnidade;

	void Awake(){
		agent= GetComponent<NavMeshAgent>();
		if(agent == null){
			agent = this.gameObject.AddComponent<NavMeshAgent>();
		}
		if(base_inimiga != null ){
			agent.SetDestination(base_inimiga.transform.position);
		}
		enemies = new List<Unidade>();
	}

	void Start(){
		minhaUnidade = this.transform.parent.gameObject.GetComponent<Unidade>();
	}

	
	// Update is called once per frame
	void Update () {
/*		if(Vector3.Distance(agent.destination, Base_inimiga.transform.localPosition )<=0.01 ){
			Debug.Log(" agent.pathStatus "+agent.destination+" "+Base_inimiga.transform.position );
			Destroy(this.transform.parent.gameObject) ;
		}
*/
	//	Debug.Log(minhaUnidade.PlayerID+" enemies.Count "+enemies.Count);
		if(enemies.Count != 0){
			foreach(Unidade u in enemies){
				if( (u==null) || (u.dino.Vida<=0)){
					enemies.Remove(u);
				}
			}
			//agent.acceleration=0;
		}/*else{
			agent.acceleration = minhaUnidade.Dino.Velocidade_deslocamento;
		}*/

	}

	public Unidade GetRandomUnidadeNaLane(){
		if(enemies.Count==0 ) return null;
		Unidade en = enemies[Random.Range(0,enemies.Count)];
		return en;
	}

	public GameObject Base_inimiga {
		get {
			return base_inimiga;
		}
		set{
			base_inimiga = value;
			if( (base_inimiga != null)&&(agent !=null) )
				agent.SetDestination(base_inimiga.transform.position);

		}

	}

	void OnCollisionEnter(Collision collision)
	{
		GameObject col = collision.gameObject;
		if( (col.tag.Contains("agente"))){
			Unidade un = col.transform.parent.gameObject.GetComponent<Unidade>();
			Debug.Log(" AGENTE "+un.PlayerID);
			if((un!= null)&&(minhaUnidade!=null)&&(un.PlayerID != minhaUnidade.PlayerID)){
				Debug.Log(" AGENTE add inimigo "+un.PlayerID);
				enemies.Add(col.GetComponent<Unidade>());
			}
		}

	}
	/*
	void OnCollisionStay(Collision collision) {
		GameObject col = collision.gameObject;
		if(col.tag.Contains("agente")){
			Debug.Log(" OnCollisionStay COM AGENTE ");
		}

	}
	void OnCollisionExit(Collision collision) {
		GameObject col = collision.gameObject;
		if(col.tag.Contains("agente")){
			Debug.Log(" OnCollisionExit COM AGENTE ");
		}
	}
	void OnTriggerEnter(Collider other) {
		Debug.Log(" OnTriggerEnter ");
	}*/
}
