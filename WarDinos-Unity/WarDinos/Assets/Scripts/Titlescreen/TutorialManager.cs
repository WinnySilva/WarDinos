using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonUp("ConfirmP1") || Input.GetButtonUp("ConfirmP2")){
			SceneManager.LoadScene (0); /*CARREGA main*/
		}

	}
	public void LoadMainMenu(){
		SceneManager.LoadScene (0); /*CARREGA main*/
	}




}
