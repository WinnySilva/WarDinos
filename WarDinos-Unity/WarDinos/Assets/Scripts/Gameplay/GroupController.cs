using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupController : MonoBehaviour {
    public enum DinoType {NONE=0, APATOSSAURO=1, ESTEGOSSAURO=2, PTERODACTILO=3, RAPTOR=4, TREX=5, TRICERATOPO=6}

    // Player who own this group
    public int player;

    // Dinos which the group is composed of
    public DinoType[] dinos = new DinoType[4];
    // Instances of the dinosaurs in group
    private GameObject[] dinosInstances = new GameObject[4];

    public GameObject laneBegin, laneEnd;

    // Prefabs of each dinosaur
    public GameObject prefabVelociraptor;
    public GameObject prefabEstegossauro;
    public GameObject prefabApatossauro;
    public GameObject prefabPterodactilo;
    public GameObject prefabTriceratopo;
    public GameObject prefabTrex;
    // Displacements of dinosaurs position. This is required because the prefabs of the dinosaurs are bugged, so their posistions must be compensated
    public Vector2 dispVelociraptor;
    public Vector2 dispEstegossauro;
    public Vector2 dispApatossauro;
    public Vector2 dispPterodactilo;
    public Vector2 dispTriceratopo;
    public Vector2 dispTrex;

    // You can find a dinosaur type prefab by writing "prefabList[DinoType.APATOSSAURO]"
    private GameObject[] prefabList = new GameObject[7];

    // You can find a displacement position by writing "dispList[DinoType.APATOSSAURO]"
    private Vector2[] dispList = new Vector2[7];

    // Transform of this game object, this group object
    private Transform trans;
    // List of Dinossauro components in dinoInstances
    private Dinossauro[] dinosDinossauro = new Dinossauro[4];
    // Depending if the group is from Player 1 or 2, there is a different tag, so these variable is used to keep the code the same
    private string myTag;
    private string enemyTag;
    private bool waitingForDispatch = true;

    // Rigid body 2D of this group object
    private Rigidbody2D rb;

    // Enemy group that is being attacked by this group
    GroupController enemyTargetGroup = null;

    public void initGroup (int arg_player, GameObject arg_laneBegin, GameObject arg_laneEnd, DinoType[] arg_dinos) {
        player = arg_player;
        laneBegin = arg_laneBegin;
        laneEnd = arg_laneEnd;
        for (int i = 0; i < 4; i++)
            dinos[i] = arg_dinos[i];

        gameObject.SetActive(true);
    }

    void Awake () {
        
    }

    // Use this for initialization
    void Start () {
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
    }
	
	void FixedUpdate () {
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
            EnteredEnemyBase();
        }
        // If collided with opponent group
        else if (other.CompareTag(enemyTag)) {
            StopWalking();
            AttackGroup(other.GetComponent<GroupController>());
        }
        // If collided with friend group
        else if (other.CompareTag(myTag)) {
            // The group behind stop walking
            if (player == 1) {
                if (transform.position.x <= other.transform.position.x)
                    StopWalking();
            }
            else {
                if (transform.position.x >= other.transform.position.x)
                    StopWalking();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // If is not colliding with enemy group anymore
        if (other.CompareTag(enemyTag))
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

        // TODO: AQUI VIRAO AS MUDANCAS DE ATRIBUTOS REFERENTES AS EVOLUCOES DOS DINOSSAUROS
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

    void EnteredEnemyBase () {
        //TODO: DIMINUIR VIDA DO OPONENTE
        Destroy(gameObject);
    }

    void AttackGroup (GroupController enemy) {
        //enemyTargetGroup = enemy;
        //yield return new WaitForSeconds(waitTime);
        foreach (Dinossauro dd in dinosDinossauro) {
            StartCoroutine(routine: AttackWithDinosaur(dd, enemy));
        }
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