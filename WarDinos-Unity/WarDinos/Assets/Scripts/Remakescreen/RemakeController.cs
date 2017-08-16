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
    public AudioSource vsAIAnnouncerDefeat;
    public AudioSource vsAIAnnouncerVictory;
    public AudioSource pvpAnnouncer;
    public AudioSource victoryMusic;
    public AudioSource defeatMusic;

    // Use this for initialization
    void Start () {
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
                victoryMusic.Play();
                vsAIAnnouncerVictory.Play();
            }
            else {
                playAgain.text = "Repetir Nível";
                defeatMusic.Play();
                vsAIAnnouncerDefeat.Play();
            }
        }
        else {
            playAgain.text = "Jogar novamente";
            victoryMusic.Play();
            pvpAnnouncer.Play();
        }
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
