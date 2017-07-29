using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
	public GameObject casas;
	public GameObject arvores;
	public GameObject agua;
	public GameObject lanes;
	public Camera camera;

	public Sprite[] arvris;
	public Sprite[] rausis;
	void Awake(){
		SpriteRenderer srAux;
		int rnd = Random.Range(0,3);
		Object [] sprites;
		//sprites = Resources.LoadAll ("Img/Map/aderecos/");
		string dir="/Img/mapa/aderecos/";
		Color[] backColor = {new Color(1,1,1), new Color(1,0,0), new Color(0,1,0), new Color(0,0,0), new Color(1,1,1), new Color(0,0,1) };
		Debug.Log(" MAP GENERATOR");

		bool actived;
		for(int i=0; i< arvores.transform.childCount;i++){
			actived = Random.Range (0, 3) == 1 ? false : true;
			srAux = arvores.transform.GetChild(i).GetComponent<SpriteRenderer>();
			if(actived){
				srAux.sprite = arvris [Random.Range (0, arvris.Length)];
			}
			srAux.gameObject.SetActive(actived);
		}
		int auxRand;

		for(int i=0; i< casas.transform.childCount;i++){
			srAux = casas.transform.GetChild(i).GetComponent<SpriteRenderer>() ;
			actived = Random.Range (0, 4) == 1 ? false : true;
			if(actived){
				auxRand = Random.Range (0, rausis.Length);
				srAux.sprite  = rausis[auxRand   ];

				Destroy (casas.transform.GetChild (i).GetComponent<Animator> ());
				Destroy (casas.transform.GetChild (i).GetChild(0).gameObject );

				switch(auxRand){
				case 0:
					casas.transform.GetChild (i).transform.localScale = new Vector3 (1.5f, 1.5f, 0);
					break;
				case 1:
					casas.transform.GetChild (i).transform.localScale = new Vector3 (1.5f, 1.5f, 0);
					break;
				case 2: 
					casas.transform.GetChild (i).transform.localScale = new Vector3 (2f, 2f, 0);
					break;
				case 3: 
					casas.transform.GetChild (i).transform.localScale = new Vector3 (3f, 3f, 0);
					break;

				}
			}

		}
		Color aux = backColor [Random.Range (0, backColor.Length)];
		for(int i=0; i< this.lanes.transform.childCount; i++ ){
			this.lanes.transform.GetChild (i).GetComponent<SpriteRenderer> ().color = aux;
		}
		aux = backColor [Random.Range (0, backColor.Length)];
		this.agua.GetComponent<SpriteRenderer> ().color = aux;
		camera.backgroundColor = aux;

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
