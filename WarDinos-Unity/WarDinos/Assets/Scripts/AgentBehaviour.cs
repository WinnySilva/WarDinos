using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentBehaviour : MonoBehaviour {
	private NavMeshAgent agent;
	public GameObject base_inimiga;
	void Start(){
		agent= GetComponent<NavMeshAgent>();
		agent.SetDestination(base_inimiga.transform.position);
	}


	
	// Update is called once per frame
	void Update () {
		
	}
}
