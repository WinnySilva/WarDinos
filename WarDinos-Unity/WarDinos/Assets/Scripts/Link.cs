using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour {
	public int limit =245, it=0,i=0;
	public float x=0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		

		while(true){
			x+=1;
			if(x>limit){
				x =0;
			}

			this.GetComponent<Transform>().position = new Vector3( x,this.GetComponent<Transform>().position.y,this.GetComponent<Transform>().position.z );

		}
	}
}
