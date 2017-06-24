using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupController : MonoBehaviour {
    public enum DinoType {NONE=0, APATOSSAURO=1, ESTEGOSSAURO=2, PTERODACTILO=3, RAPTOR=4, TREX=5, TRICERATOPO=6}

    //-------------------------
    // DEBUG VARIABLES
    //-------------------------

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
    string myTag;
    string enemyTag;

    // Rigid body 2D of this group object
    Rigidbody2D rb;

    void Awake () {
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
    }

    // Use this for initialization
    void Start () {
        // Get rigid body 2D
        rb = GetComponent<Rigidbody2D>();

        // Set tags based on the Player (1 or 2) this group belongs to
        if (player == 1) {
            myTag = "Player 1";
            enemyTag = "Player 2";
        }
        else {
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

        StartWalking();
    }
	
	/*void FixedUpdate () {
	}*/

    void OnTriggerEnter2D(Collider2D other)
    {
        // If collided with opponent lane
        if (other.CompareTag(laneEnd.tag)) {
            Debug.Log(GetInstanceID() + "Colidiu com " + other.GetInstanceID());
            EnteredEnemyBase();
        }
        else if (other.CompareTag(enemyTag)) {
            StopWalking();
            Debug.Log("Group from Player " + player + ", with ID="+gameObject.GetInstanceID()+" collided with enemy group with ID="+other.gameObject.GetInstanceID());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // If is not colliding with enemy group anymore
        if (other.CompareTag(enemyTag))
        {
            StartWalking();
            Debug.Log("Group from Player " + player + ", with ID=" + gameObject.GetInstanceID() + " stop colliding with enemy group with ID=" + other.gameObject.GetInstanceID());
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
        mediaVels /= dinoQuant;
        if (player == 2)
            mediaVels *= -1;
        Debug.Log(mediaVels);
        return mediaVels;
    }

    void StartWalking() {
        // Apply movement to the dinos
        Vector2 vel = new Vector2(VelocidadeMedia(), 0.0f);
        rb.velocity = vel;
    }
    
    void StopWalking() {
        rb.velocity = new Vector2(0.0f, 0.0f);
    }

    void EnteredEnemyBase () {
        //TODO: DIMINUIR VIDA DO OPONENTE
        Destroy(gameObject);
    }
}