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

    // Use this for initialization
    void Start () {
        GlobalParameters.someoneIsWinning = false;
        playAgainButton.onClick.AddListener(TaskOnClickPlayAgainButton);
        titleScreenButton.onClick.AddListener(TaskOnClickTitleScreenButton);
        if (GlobalParameters.playerWinner == 1)
            textPlayerWins.GetComponent<Text>().text = "Player 1 wins!";
        else
            textPlayerWins.GetComponent<Text>().text = "Player 2 wins!";
    }

    void TaskOnClickPlayAgainButton()
    {
        Debug.Log("Clicou no botao Jogar Novamente");
        if (GlobalParameters.gameMode == 1) 
            SceneManager.LoadScene(cenaJogarNovamente);
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
