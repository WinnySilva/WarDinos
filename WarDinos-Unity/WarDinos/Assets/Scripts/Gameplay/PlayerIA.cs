using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Windows.Forms;
using UnityEngine.UI;

public class PlayerIA : MonoBehaviour {
	public GameObject player;
	public GameObject hud;

	public GameObject[] inicioLanes;
	public GameObject[] fimLanes;
	public GameObject[] dinoPrefab;
	public LevelInfo gameLevelInfo;
	public Text levelNumber;
	public GameObject lvlNumer;

	private HUDController playerHudController;
	private Player playerScr;
	private GameObject dinoGroupPrefab;
	private float iniTime = 1f;
	private float iniTimeUp = 10f;
	private float repeatTimeDespachar= 10f;
	private float repeatTimeUp= 20f;
	private int[][] ordemLanes;
	private int[] ordemLaneAtual = { 0, 2 };
	void Awake(){
		playerHudController = hud.GetComponent<HUDController>();
		playerScr = player.GetComponent<Player>();
		dinoGroupPrefab = this.playerHudController.dinoGroupPrefab;
		int n = this.playerHudController.transform.childCount;
		for(int i =0; i<n-1; i++){
			this.playerHudController.transform.GetChild (i).gameObject.SetActive (false);
		}

		ordemLanes = new int[][] { 
			new int[]{ 0, 1, 2 }, new int[]{ 0, 2, 1 },
			new int[]{ 1, 0, 2 }, new int[]{ 1, 2, 0 },
			new int[]{ 2, 1, 0 }, new int[]{ 2, 0, 1 }
		};
		this.ordemLaneAtual [0] = Random.Range (0, ordemLanes.Length);
		this.ordemLaneAtual [1] = 0;


	}
	// Use this for initialization
	void Start () {
		this.gameLevelInfo = (GameObject.Find ("gameLevelInfo")!=null)?	GameObject.FindWithTag ("LevelInfo").GetComponent<LevelInfo>():null  ;
		if(gameLevelInfo !=null ){
			Debug.Log ("gameLevelInfo " + gameLevelInfo);
			if (gameLevelInfo.gameMode == LevelInfo.GAME_MODE.MULTI) {
				int n = this.playerHudController.transform.childCount;
				for (int i = 0; i < n - 1; i++) {
					this.playerHudController.transform.GetChild (i).gameObject.SetActive (true);
				}
				this.lvlNumer.SetActive (false);
				Destroy (this);
				return;
			} else {
				int n = this.playerHudController.transform.childCount;
				for(int i =0; i<n-1; i++){
					this.playerHudController.transform.GetChild (i).gameObject.SetActive (false);
				}
				this.lvlNumer.SetActive (true);
				this.levelNumber.text = "lvl " + this.gameLevelInfo.lvl;
			}
			repeatTimeDespachar = repeatTimeDespachar / gameLevelInfo.lvl;
			iniTime = iniTime / gameLevelInfo.lvl;
			this.playerScr.incrementarRecursos(  this.playerScr.Recursos * (gameLevelInfo.lvl-1)    );
		}


		InvokeRepeating("Jogar",iniTime,repeatTimeDespachar);
		InvokeRepeating("Upgrade",iniTimeUp,repeatTimeUp);

	}


	void Jogar(){
		
		int l = this.ordemLaneAtual [0];
		int c = this.ordemLaneAtual [1];
		jogarDinoLane((this.ordemLanes[l])[c]);

		if (this.ordemLaneAtual [1] >= 2) {
			
			this.ordemLaneAtual [0] = Random.Range (0, this.ordemLanes.Length);
			this.ordemLaneAtual [1] = 0;
		} else {
			this.ordemLaneAtual [1]++;
		}

	}

	private void jogarDinoLane(int ln){
		GroupController.DinoType e;

		GroupController.DinoType[] arg_dinos = this.gerarGrupo ();
			
		this.despacharGrupo (ln, arg_dinos);
	}

	/* gera um grupo com 4 dinotype*/
	private GroupController.DinoType[] gerarGrupo(){
		GroupController.DinoType[] arg_dinos;
		int recursos = this.playerScr.Recursos;
		int indexDino = dinoPrefab [0].GetComponent<Dinossauro> ().Custo;
		if (recursos < indexDino) {
			GroupController.DinoType[] arg = {
				GroupController.DinoType.NONE,
				GroupController.DinoType.NONE,
				GroupController.DinoType.NONE,
				GroupController.DinoType.NONE
			};
			return arg;
		}
		arg_dinos = new GroupController.DinoType[4];
		int custoTotal = 0;
		int slotsTotal = 4;
		Dinossauro dinoAux, dinoAuxEscolhido;
		GroupController.DinoType aux;
		arg_dinos[0] = randomDino (false);

		//SE O RANDOM NAO FOR NONE
		slotsTotal -=  (int)(arg_dinos[0])!=0? this.dinoPrefab[ (int)(arg_dinos[0]) -1 ].GetComponent<Dinossauro> ().NSlot: 0 ;
		custoTotal +=  (int)(arg_dinos[0])!=0? this.dinoPrefab[ (int)(arg_dinos[0])-1  ].GetComponent<Dinossauro> ().Custo: 0;
		int skip = 0;//(int)(arg_dinos[0])!=0? this.dinoPrefab[ (int)(arg_dinos[0]) -1 ].GetComponent<Dinossauro> ().NSlot-1: -1;

		string debugMSG = ("  0 : "+arg_dinos[0]+" "+custoTotal+"/"+recursos +" "+(4-slotsTotal)+"/"+4+" skip: "+skip+ " ; ");
		for(int i=1; i<4; i++ ) {
			dinoAuxEscolhido = null;
	//		Debug.Log ("skip: " + skip);
			if (skip--<1 ) {
			//	Debug.Log ("2:: skip: " + skip);
				for (int j = 0; (j < 6); j++) {
					dinoAux = this.dinoPrefab [j].GetComponent<Dinossauro> ();
					if ((dinoAux.NSlot <= slotsTotal) && ((custoTotal + dinoAux.Custo) <= recursos)) {
						arg_dinos [i] = dinoAux.DinoType;
						dinoAuxEscolhido = dinoAux;
					}
				}
			}
			if (dinoAuxEscolhido == null) {
				arg_dinos [i] = GroupController.DinoType.NONE;
				debugMSG += ("  "+i+": x "+arg_dinos [i]+" "+0+"/ "+recursos+"  "+0+"/"+4)+" skip: "+skip+" total slots:"+slotsTotal+ ";";

			} else {
				custoTotal += dinoAuxEscolhido.Custo;
				slotsTotal -= dinoAuxEscolhido.NSlot;
				skip = 0;//dinoAuxEscolhido.NSlot - 1;
				debugMSG += ("  "+i+": x "+dinoAuxEscolhido.DinoType+" "+dinoAuxEscolhido.Custo+"/ "+recursos+"  "+dinoAuxEscolhido.NSlot+"/"+4)+" skip: "+skip+" total slots:"+slotsTotal+ ";";
			}
		}

	//msg += ("=== GRUPO::: " + custoTotal + " slots: " + slotsTotal+" skip: "+skip)+"\n";

		debugMSG +="\n"+arg_dinos [0]+" ; "+arg_dinos [1]+"; "+arg_dinos [2]+"; "+arg_dinos [3]+"; \n";
	//	Debug.LogError(msg+ "=== GRUPO: custo:" + custoTotal+"/"+recursos + " slots: " + slotsTotal);

		this.playerScr.reduzirRecursos (custoTotal);
		return arg_dinos;
	}

	private void despacharGrupo(int lane,GroupController.DinoType[] arg_dinos){
		GameObject gb = new GameObject ();
		//GroupController gp = gb.AddComponent<GroupController> ();
		GroupController gp = Instantiate(dinoGroupPrefab).GetComponent<GroupController>();
		//gb.SetActive (false);
		gp.initGroup(
			playerScr.playerID,
			this.inicioLanes[lane],
			this.fimLanes[lane],
			arg_dinos,
			this.dinoPrefab[3], /*RAPTOR*/
			this.dinoPrefab[1], /*ESTEGOSSAURO*/
			this.dinoPrefab[0], /*APATO*/
			this.dinoPrefab[2], /*PTERO*/
			this.dinoPrefab[5], /*TRICERATOPO*/
			this.dinoPrefab[4] /*T REX*/
		);
			}

	private GroupController.DinoType randomDino(bool noneInclude){
		int rnd = Random.Range ( noneInclude?0:1  , 7);

		switch (rnd) {
		case 0: 
			return GroupController.DinoType.NONE;
			break;
		case 1:
			return GroupController.DinoType.APATOSSAURO;
			break;
		case 2:
			return GroupController.DinoType.ESTEGOSSAURO;
			break;
		case 3:
			return GroupController.DinoType.PTERODACTILO;
			break;
		case 4:
			return GroupController.DinoType.RAPTOR;
			break;
		case 5:
			return GroupController.DinoType.TREX ;
			break;
		case 6:
			return GroupController.DinoType.TRICERATOPO ;
			break;
		default:
			return GroupController.DinoType.NONE;
			break;
		}
		return GroupController.DinoType.NONE;
	}

	void Upgrades(){
		Attributes a = (Attributes) Random.Range(0,5);
		this.playerScr.Upgrade( this.randomDino(false), a );
	}

}
