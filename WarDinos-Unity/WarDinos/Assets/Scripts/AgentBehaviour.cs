using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentBehaviour : MonoBehaviour {
	private NavMeshAgent agent;
	public GameObject base_inimiga;
	void Start(){
		
		agent= GetComponent<NavMeshAgent>();
		if(agent == null){
			agent = this.gameObject.AddComponent<NavMeshAgent>();
		}
		agent.SetDestination(base_inimiga.transform.position);
	}


	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision)
	{
		Debug.Log(" COLISAO "+collision.gameObject.name);
	}

}
