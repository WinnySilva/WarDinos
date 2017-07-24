using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinnerController : MonoBehaviour {
    private int player;
    private int fixedPlayer;
    public AudioSource victorySound;
    public AudioSource victorySound2;
	private LoggerMongo logg;

	void Awake(){
		logg = new LoggerMongo (this.GetType() );
	}

    // Use this for initialization
    void Start () {

        fixedPlayer = player;
        if (!GlobalParameters.someoneIsWinning) {
            GlobalParameters.someoneIsWinning = true;
            StartCoroutine(routine: WinGame());
        }
		logg.acao= "START";
		logg.msg = "START WIN GAME" + this.GetType ().Name;
		logg.playerID = player;
		logg.writeLog ();


    }
	
    public int Player { set{ player = value; } }

    IEnumerator WinGame()
    {
        // Setta qual jogador venceu para aparecer na tela de remake
        GlobalParameters.playerWinner = fixedPlayer;

        Debug.Log("PLAYER " + fixedPlayer + " VENCEU!");

        victorySound.Play();
        yield return new WaitForSeconds(0.75f);
        victorySound2.Play();
        yield return new WaitForSeconds(3.75f);

        // Vai para cena de remake
        SceneManager.LoadScene("game_remake");

        yield return null;
    }
}
