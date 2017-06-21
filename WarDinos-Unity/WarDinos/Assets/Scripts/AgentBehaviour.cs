using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentBehaviour : MonoBehaviour {
	private NavMeshAgent agent;
	public GameObject base_inimiga;


	void Awake(){
		
		agent= GetComponent<NavMeshAgent>();
		if(agent == null){
			agent = this.gameObject.AddComponent<NavMeshAgent>();
		}
		if(base_inimiga != null){
			agent.SetDestination(base_inimiga.transform.position);
		}

	}


	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject Base_inimiga {
		get {
			return base_inimiga;
		}
		set{
			base_inimiga = value;
			agent.SetDestination(base_inimiga.transform.position);
		}

	}

	void OnCollisionEnter(Collision collision)
	{
		Debug.Log(" OnCollisionEnter "+collision.gameObject.name);
	}
	void OnCollisionStay(Collision collision) {
		Debug.Log(" OnCollisionStay "+collision.gameObject.name);

	}
}
