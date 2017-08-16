using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RemakeController : MonoBehaviour {
    public Button playAgainButton;
    public Button titleScreenButton;
    public string cenaJogarNovamente;
    public string cenaTelaInicial;
    public GameObject textPlayerWins;
    public Text playAgain;
	public LevelInfo gameLevelInfo;

    // Use this for initialization
    void Start () {
		this.gameLevelInfo = (GameObject.Find ("gameLevelInfo")!=null)?	GameObject.FindWithTag ("LevelInfo").GetComponent<LevelInfo>():null  ;
        GlobalParameters.someoneIsWinning = false;
        playAgainButton.onClick.AddListener(TaskOnClickPlayAgainButton);
        titleScreenButton.onClick.AddListener(TaskOnClickTitleScreenButton);
        if (GlobalParameters.playerWinner == 1)
            textPlayerWins.GetComponent<Text>().text = "Jogador 1 venceu!";
        else
            textPlayerWins.GetComponent<Text>().text = "Jogador 2 venceu!";

        if (GlobalParameters.gameMode == 1) {
			if (GlobalParameters.playerWinner == 1) {
				playAgain.text = "Próximo Nível";
				this.gameLevelInfo.lvl++;
			}
            else
                playAgain.text = "Repetir Nível";
        }
        else {
            playAgain.text = "Jogar novamente";
        }
    }

    void TaskOnClickPlayAgainButton()
    {
        Debug.Log("Clicou no botao Jogar Novamente");
		if (GlobalParameters.gameMode == 1) {
			
			SceneManager.LoadScene (cenaJogarNovamente);
		}
        else
            // TODO: Quando o modo vs IA estiver pronto, devera ir para essa tela
            SceneManager.LoadScene(cenaJogarNovamente);
    }

    void TaskOnClickTitleScreenButton()
    {
        Debug.Log("Clicou no botao Voltar para Tela Inicial");
        SceneManager.LoadScene(cenaTelaInicial);
    }
}
