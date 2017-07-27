using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
	public GameObject casas;
	public GameObject arvores;
	public GameObject agua;

	void Awake(){
		SpriteRenderer srAux;
		int rnd = Random.Range(0,3);
		Object [] sprites;
		//sprites = Resources.LoadAll ("Img/Map/aderecos/");
		string dir="/Img/mapa/aderecos/";
		string[] spritesNamesCasas = {"use46664","use46835"};
		string[] spritesNamesArvores = {"g125124" ,"use46774","use46806","use46870",
			/*resize*/ "use46872" , "XMLID_22_","XMLID_63_","XMLID_64_","g126277"};
		
		Debug.Log(" MAP GENERATOR");
		for(int i=0; i< arvores.transform.childCount;i++){
			srAux = arvores.transform.GetChild(i).GetComponent<SpriteRenderer>();
		//	srAux.sprite = Resources.Load<Sprite>("/Img/mapa/aderecos/use46806");
			srAux.gameObject.SetActive( Random.Range(0,2)==1?false:true   );
			if(srAux.sprite == null){
				Debug.Log("NULL tree :: "+arvores.transform.GetChild(i).name+" ");
			}
		}

		for(int i=0; i< casas.transform.childCount;i++){
			casas.transform.GetChild(i);			
		}


	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
