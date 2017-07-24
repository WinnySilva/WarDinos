using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDPlayerAttributes : MonoBehaviour {
    public GameObject hudVida;
    public GameObject hudRecursos;
    public GameObject player;

    private Text hudVida_text;
    private Text hudRecursos_text;
    private Player player_player;

    static private int INCOME_VALUE = 10;
    static private float INCOME_INTERVAL = 5.0f;
    private float tElapsed;
    private float tStart;
	private LoggerMongo logg ;

	void Awake(){
		logg = new LoggerMongo (this.GetType ());
	}

    // Use this for initialization
    void Start () {
        hudVida_text = hudVida.GetComponent<Text>();
        hudRecursos_text = hudRecursos.GetComponent<Text>();
        player_player = player.GetComponent<Player>();

        tStart = Time.realtimeSinceStartup;
        tElapsed = Time.realtimeSinceStartup;

		logg.acao= "HUD START";
		logg.msg ="INICIANDO HUDPlayerAttributes";
		logg.writeLog ();
    }
	
	// Update is called once per frame
	void Update () {
        hudVida_text.text = player_player.Vida.ToString();
        hudRecursos_text.text = player_player.Recursos.ToString();

        //Debug.Log("tElapsed: "+tElapsed);
        //Debug.Log("tStart: "+tStart);
        while (tElapsed - tStart >= INCOME_INTERVAL) {
            tStart += INCOME_INTERVAL;
            player_player.incrementarRecursos(INCOME_VALUE);
        }
        tElapsed = Time.realtimeSinceStartup;
    }
}
