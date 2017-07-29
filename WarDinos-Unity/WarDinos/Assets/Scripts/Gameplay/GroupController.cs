using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MongoDB.Bson.Serialization.Attributes;

public class GroupController : MonoBehaviour {


	public enum DinoType {NONE=0, APATOSSAURO=1, ESTEGOSSAURO=2, PTERODACTILO=3, RAPTOR=4, TREX=5, TRICERATOPO=6}

	[BsonIgnoreAttribute] 
	private GameObject collidedFriend = null;
	[BsonIgnoreAttribute] 
	private bool collidedWithFriend = false;

    // Player who own this group
    public int player;

	[BsonIgnoreAttribute] 
    // Dinos which the group is composed of
    public DinoType[] dinos = new DinoType[4];
    // Instances of the dinosaurs in group
	[BsonIgnoreAttribute]
    private GameObject[] dinosInstances = new GameObject[4];

	[BsonIgnoreAttribute] 
	public GameObject laneBegin, laneEnd;

    // Prefabs of each dinosaur that should be instantiated
	[BsonIgnoreAttribute] public GameObject prefabVelociraptor;
	[BsonIgnoreAttribute] public GameObject prefabEstegossauro;
	[BsonIgnoreAttribute] public GameObject prefabApatossauro;
	[BsonIgnoreAttribute] public GameObject prefabPterodactilo;
	[BsonIgnoreAttribute] public GameObject prefabTriceratopo;
	[BsonIgnoreAttribute] public GameObject prefabTrex;
    // Dinosaurs with the actual upgrades. Their attributes are copied to the original prefabs when they are instantiated
	[BsonIgnoreAttribute]private GameObject actualVelociraptor;
	[BsonIgnoreAttribute]private GameObject actualEstegossauro;
	[BsonIgnoreAttribute]private GameObject actualApatossauro;
	[BsonIgnoreAttribute]private GameObject actualPterodactilo;
	[BsonIgnoreAttribute]private GameObject actualTriceratopo;
	[BsonIgnoreAttribute]private GameObject actualTrex;
    // Displacements of dinosaurs position. This is required because the prefabs of the dinosaurs are bugged, so their posistions must be compensated
	[BsonIgnoreAttribute]
	public Vector2 dispVelociraptor;
	[BsonIgnoreAttribute]
	public Vector2 dispEstegossauro;
	[BsonIgnoreAttribute]
	public Vector2 dispApatossauro;
	[BsonIgnoreAttribute]
	public Vector2 dispPterodactilo;
	[BsonIgnoreAttribute]
	public Vector2 dispTriceratopo;
	[BsonIgnoreAttribute]
	public Vector2 dispTrex;


    // You can find a dinosaur type prefab by writing "prefabList[DinoType.APATOSSAURO]"
	[BsonIgnoreAttribute] private GameObject[] prefabList = new GameObject[7];

    // You can find a displacement position by writing "dispList[DinoType.APATOSSAURO]"
	[BsonIgnoreAttribute] private Vector2[] dispList = new Vector2[7];

    // Transform of this game object, this group object
	[BsonIgnoreAttribute] private Transform trans;
    // List of Dinossauro components in dinoInstances

    private Dinossauro[] dinosDinossauro = new Dinossauro[4];
    // Depending if the group is from Player 1 or 2, there is a different tag, so these variable is used to keep the code the same
    private string myTag;
    private string enemyTag;
    private bool waitingForDispatch = true;
	private int totalVida=-5000;

    // Rigid body 2D of this group object
	[BsonIgnoreAttribute] private Rigidbody2D rb;

    // Enemy group that is being attacked by this group
	[BsonIgnoreAttribute]
	public GroupController enemyTargetGroup = null;

    // Players
	[BsonIgnoreAttribute]
    private Player playerSelf;
	[BsonIgnoreAttribute]
    private Player playerEnemy;

	[BsonIgnoreAttribute] public GameObject gameWinnerObject;
	[BsonIgnoreAttribute] public GameObject barraDeVida;

	[BsonIgnoreAttribute]
	private LoggerMongo logg;

	void Awake(){
		logg = new LoggerMongo(this.GetType() );
	}

    public void initGroup (int arg_player, GameObject arg_laneBegin, GameObject arg_laneEnd, DinoType[] arg_dinos,
        GameObject arg_prefabVelociraptor,
        GameObject arg_prefabEstegossauro,
        GameObject arg_prefabApatossauro,
        GameObject arg_prefabPterodactilo,
        GameObject arg_prefabTriceratopo,
        GameObject arg_prefabTrex)
    {
        player = arg_player;
        laneBegin = arg_laneBegin;
        laneEnd = arg_laneEnd;

        actualVelociraptor = arg_prefabVelociraptor;
        actualEstegossauro = arg_prefabEstegossauro;
        actualApatossauro = arg_prefabApatossauro;
        actualPterodactilo = arg_prefabPterodactilo;
        actualTriceratopo = arg_prefabTriceratopo;
        actualTrex = arg_prefabTrex;

        int i = 0;
        int j = 0;
        int NSlot;

        while (i < 4) {
            if (arg_dinos[j] == DinoType.APATOSSAURO) {
                NSlot = actualApatossauro.GetComponent<Dinossauro>().NSlot;
                if (4 - i - NSlot >= 0)
                    dinos[i] = arg_dinos[j];
                i += NSlot;
            }
            else if (arg_dinos[j] == DinoType.ESTEGOSSAURO) {
                NSlot = actualEstegossauro.GetComponent<Dinossauro>().NSlot;
                if (4 - i - NSlot >= 0)
                    dinos[i] = arg_dinos[j];
                i += NSlot;
            }
            else if (arg_dinos[j] == DinoType.PTERODACTILO) {
                NSlot = actualPterodactilo.GetComponent<Dinossauro>().NSlot;
                if (4 - i - NSlot >= 0)
                    dinos[i] = arg_dinos[j];
                i += NSlot;
            }
            else if (arg_dinos[j] == DinoType.RAPTOR) {
                NSlot = actualVelociraptor.GetComponent<Dinossauro>().NSlot;
                if (4 - i - NSlot >= 0)
                    dinos[i] = arg_dinos[j];
                i += NSlot;
            }
            else if (arg_dinos[j] == DinoType.TREX){
                NSlot = actualTrex.GetComponent<Dinossauro>().NSlot;
                if (4 - i - NSlot >= 0)
                    dinos[i] = arg_dinos[j];
                i += NSlot;
            }
            else if (arg_dinos[j] == DinoType.TRICERATOPO) {
                NSlot = actualTriceratopo.GetComponent<Dinossauro>().NSlot;
                if (4 - i - NSlot >= 0)
                    dinos[i] = arg_dinos[j];
                i += NSlot;
            }
            else if ((arg_dinos[j] == DinoType.NONE)) {
                i++;
            }
            j++;
        }

        gameObject.SetActive(true);
		logg.attachedObj = this;
		logg.msg ="INICIANDO GRUPO "+this.GetInstanceID();
		logg.acao = "INICIANDO GRUPO";
		logg.grupoID = this.GetInstanceID ();
		logg.writeLog ();

    }

    // Use this for initialization
    void Start () {
        playerSelf = laneBegin.GetComponent<LaneController>().player.GetComponent<Player>();
        playerEnemy = laneEnd.GetComponent<LaneController>().player.GetComponent<Player>();

        // Set the group to spawn at the beginning of its lane
        transform.position = laneBegin.transform.position;

        // Associates Prefabs with DinoTypes
        prefabList[(int)DinoType.NONE] = null;
        prefabList[(int)DinoType.APATOSSAURO] = prefabApatossauro;
        prefabList[(int)DinoType.ESTEGOSSAURO] = prefabEstegossauro;
        prefabList[(int)DinoType.RAPTOR] = prefabVelociraptor;
        prefabList[(int)DinoType.PTERODACTILO] = prefabPterodactilo;
        prefabList[(int)DinoType.TRICERATOPO] = prefabTriceratopo;
        prefabList[(int)DinoType.TREX] = prefabTrex;

        // Associates Displacement positions with dispList
        dispList[(int)DinoType.NONE] = new Vector2(0.0f, 0.0f);
        dispList[(int)DinoType.APATOSSAURO] = dispApatossauro;
        dispList[(int)DinoType.ESTEGOSSAURO] = dispEstegossauro;
        dispList[(int)DinoType.RAPTOR] = dispVelociraptor;
        dispList[(int)DinoType.PTERODACTILO] = dispPterodactilo;
        dispList[(int)DinoType.TRICERATOPO] = dispTriceratopo;
        dispList[(int)DinoType.TREX] = dispTrex;

        // Get rigid body 2D
        rb = GetComponent<Rigidbody2D>();

        // Set tags based on the Player (1 or 2) this group belongs to
        if (player == 1) {
            tag = "Player 1";
            myTag = "Player 1";
            enemyTag = "Player 2";
        }
        else {
            tag = "Player 2";
            myTag = "Player 2";
            enemyTag = "Player 1";
        }

        trans = GetComponent<Transform>();

        // Effectively instantiate the dinosaurs
        InstantiateDino(0);
        InstantiateDino(1);
        InstantiateDino(2);
        InstantiateDino(3);

        // Player 2 should have its group sprite flipped in x
        if (player == 2) {
            Vector3 v = transform.localScale;
            v.x *= -1;
            transform.localScale = v;
        }

        // Get the Dinossauro components from every instance in dinoInstances
        // TODO: Os componentes dos dinossauros devem vir atualizados com as evolucoes das lojas
        int i = 0;
        foreach (GameObject go in dinosInstances)
        {
            if (go != null) {
                dinosDinossauro[i] = go.GetComponent<Dinossauro>();
                ++i;
            }
        }

        //StartWalking();

		this.totalVida = 0;
		foreach( Dinossauro d in this.dinosDinossauro ){
			if(d!=null)
			totalVida += d.Vida;
		}


		logg.msg ="DESPACHANDO GRUPO "+this.GetInstanceID();
		logg.grupoID = this.GetInstanceID ();
		logg.acao ="DESPACHANDO GRUPO ";
		//logg.attachedObj = this;
		logg.writeLog ();
    }

	void Update(){
		if(totalVida>=-5000){
//			Debug.Log(" UPDATE VIDA>500");
			int atualVida = 0;
			foreach( Dinossauro d in this.dinosDinossauro ){
				if(d!=null)
					atualVida += d.Vida;

			}
            float scalevida = 0.0f;
            if (totalVida > 0)
			    scalevida = (float)atualVida/(float)this.totalVida;
			Vector3 v = new Vector3(scalevida,1f,1f);
			this.barraDeVida.transform.localScale = v;
		}

	}

	void FixedUpdate () {
        if (collidedFriend == null && collidedWithFriend == true && !waitingForDispatch) {
            collidedWithFriend = false;
            StartWalking();
        }

        // If there are no more dinosaurs on the group, it is destroyed
        bool thereAreDinosInGroup = false;
        foreach (GameObject d in dinosInstances) {
            if (d != null) {
                thereAreDinosInGroup = true;
            }
        }
        if (!thereAreDinosInGroup) {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        // If collided with opponent lane
        if (other.CompareTag(laneEnd.tag)) {
            // Reduce life from the enemy player equal to the sum of the dinosaurs lifes in the group
            // Also increase resources of the player by that same amount
            int reducedLife = 0;
            foreach (Dinossauro d in dinosDinossauro) {
                if (d != null)
                    reducedLife += d.Vida;
            }
            playerEnemy.reduzirVida(reducedLife);
            playerSelf.incrementarRecursos(reducedLife);

		//	logg.attachedObj = this;
			logg.grupoID = this.GetInstanceID ();
			logg.acao ="ENTROU NA BASE";
			logg.msg ="ENTROU NA BASE GRUPO "+this.GetInstanceID();
			logg.writeLog ();

            // Finishes the game if the player reachs zero life
            if (other.GetComponent<LaneController>().player.GetComponent<Player>().Vida <= 0) {
                GameObject gwo = Instantiate(
                    gameWinnerObject,
                    transform.position,
                    transform.rotation
                );
                gwo.GetComponent<GameWinnerController>().Player = player;
                gwo.SetActive(true);

		//		logg.attachedObj = this;
				logg.grupoID = this.GetInstanceID();
				logg.acao ="FIM JOGO";
				logg.jogadorVencedor = this.player;
				logg.writeLog ();


            }

            Destroy(gameObject);
        }
        // If collided with opponent group
        else if (other.CompareTag(enemyTag) && !waitingForDispatch) {
            StopWalking();
            AttackGroup(other.GetComponent<GroupController>());

        }
        // If collided with friend group
        else if (other.CompareTag(myTag) && !waitingForDispatch) {
            // The group behind stop walking
            if (player == 1) {
                if (transform.position.x <= other.transform.position.x) {
                    StopWalking();
                    if (collidedWithFriend == false) {
                        collidedWithFriend = true;
                        collidedFriend = other.gameObject;
                    }
                }
            }
            else {
                if (transform.position.x >= other.transform.position.x) {
                    StopWalking();
                    if (collidedWithFriend == false) {
                        collidedWithFriend = true;
                        collidedFriend = other.gameObject;
                    }
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // If is not colliding with enemy group anymore
        if (other.CompareTag(enemyTag) && !waitingForDispatch)
        {
            StartWalking();
        }
        // If collided with friend group
        else if (other.CompareTag(myTag) && !waitingForDispatch) {
            // The group behind stop walking
            if (player == 1) {
                if (transform.position.x < other.transform.position.x)
                    StartWalking();
            }
            else {
                if (transform.position.x > other.transform.position.x)
                    StartWalking();
            }
        }
    }

    // Instantiate a Dinosaur corresponding to the Dinosaur in this group selected position
    void InstantiateDino(int groupPosition) {
        Vector3 position = laneBegin.transform.position - (Vector3)dispList[(int)dinos[groupPosition]];
        position.y += (groupPosition - 2) * 1.0f;

        if (dinos[groupPosition] != DinoType.NONE) {
            dinosInstances[groupPosition] = Instantiate(
                prefabList[(int)dinos[groupPosition]],
                position,
                laneBegin.transform.rotation,
                trans
            );
        }
        
        // Update the attributes of the dinosaurs by the upgrades of the player
        if (dinos[groupPosition] == DinoType.APATOSSAURO)
            dinosInstances[groupPosition].GetComponent<Dinossauro>().CopyAttr(actualApatossauro.GetComponent<Dinossauro>());
        else if (dinos[groupPosition] == DinoType.ESTEGOSSAURO)
            dinosInstances[groupPosition].GetComponent<Dinossauro>().CopyAttr(actualEstegossauro.GetComponent<Dinossauro>());
        else if (dinos[groupPosition] == DinoType.PTERODACTILO)
            dinosInstances[groupPosition].GetComponent<Dinossauro>().CopyAttr(actualPterodactilo.GetComponent<Dinossauro>());
        else if (dinos[groupPosition] == DinoType.RAPTOR)
            dinosInstances[groupPosition].GetComponent<Dinossauro>().CopyAttr(actualVelociraptor.GetComponent<Dinossauro>());
        else if (dinos[groupPosition] == DinoType.TREX)
            dinosInstances[groupPosition].GetComponent<Dinossauro>().CopyAttr(actualTrex.GetComponent<Dinossauro>());
        else if (dinos[groupPosition] == DinoType.TRICERATOPO)
            dinosInstances[groupPosition].GetComponent<Dinossauro>().CopyAttr(actualTriceratopo.GetComponent<Dinossauro>());

        if (dinosInstances[groupPosition] != null) {
            dinosInstances[groupPosition].GetComponent<Dinossauro>().PlayerSelf = playerSelf;
            dinosInstances[groupPosition].GetComponent<Dinossauro>().PlayerEnemy = playerEnemy;
            dinosInstances[groupPosition].GetComponent<Dinossauro>().Gc = this;
        }
    }

    float VelocidadeMedia () {
        float mediaVels = 0.0f;
        int dinoQuant = 0;
        foreach (Dinossauro d in dinosDinossauro)
        {
            if (d != null) {
                mediaVels += d.Velocidade_deslocamento;
                ++dinoQuant;
            }
        }
        if (dinoQuant != 0) {
            mediaVels /= dinoQuant;
            if (player == 2)
                mediaVels *= -1;
            return mediaVels;
        }
        else
            return 0.0f;
    }

    public void StartWalking() {
        // Apply movement to the dinos
        Vector2 vel = new Vector2(VelocidadeMedia(), 0.0f);
        rb.velocity = vel;

        // Animate the dinosaurs
        foreach (GameObject d in dinosInstances) {
            if (d != null) {
                d.GetComponent<Animator>().SetInteger("transicao", 1);
            }
        }
    }
    
    public void StopWalking() {
        rb.velocity = new Vector2(0.0f, 0.0f);
        
        // Animate the dinosaurs
        foreach (GameObject d in dinosInstances) {
            if (d != null) {
                d.GetComponent<Animator>().SetInteger("transicao", 0);
            }
        }
    }

    void AttackGroup (GroupController enemy) {
        enemyTargetGroup = enemy;
        //yield return new WaitForSeconds(waitTime);
        foreach (Dinossauro dd in dinosDinossauro) {
            StartCoroutine(routine: AttackWithDinosaur(dd, enemy));
        }

	/*	logg.grupoID = this.GetInstanceID ();
		logg.enemyGrupoID = enemy.GetInstanceID ();
		logg.acao ="ATACAR";
		logg.msg =" GRUPO "+this.GetInstanceID()+" ATACAR "+enemy.GetInstanceID();
		//logg.attachedObj = this;
		logg.writeLog ();*/
    }

    IEnumerator AttackWithDinosaur (Dinossauro dino, GroupController gp) {
        bool thereAreTargets = true;
        while (thereAreTargets && dino != null && gp != null) {
            dino.gameObject.GetComponent<Animator>().SetTrigger("ataque");
            yield return new WaitForSeconds((float)dino.VelocidadeAtaque);
            thereAreTargets = dino.Atacar(gp);
        }

        yield return null;
    }

    public Dinossauro[] DinosDinossauro {
        get { return dinosDinossauro; }
    }

    public bool WaitingForDispatch {
        get { return waitingForDispatch; }
        set { waitingForDispatch = value; }
    }
}
