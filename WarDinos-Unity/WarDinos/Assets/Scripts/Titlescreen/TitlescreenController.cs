using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitlescreenController : MonoBehaviour {
    public Button singleplayerButton;
    public Button multiplayerButton;
    public Button crebitosButton;
    public Button sairButton;
    public string singleplayerScene;
    public string multiplayerScene;
    public GameObject gameObjectTitlescreen;
    public GameObject gameObjectCrebitos;
	public LevelInfo gameLevelInfo;


    private MenuLocker titlescreenLocker;
    private MenuLocker crebitosLocker;

    // Use this for initialization
    void Start () {
		
        singleplayerButton.onClick.AddListener(TaskOnClickSingleplayer);
        multiplayerButton.onClick.AddListener(TaskOnClickMultiplayer);
        crebitosButton.onClick.AddListener(TaskOnClickCrebitos);
        sairButton.onClick.AddListener(TaskOnClickSair);

        titlescreenLocker = GetComponent<MenuLocker>();
        crebitosLocker = gameObjectCrebitos.GetComponent<MenuLocker>();
    }

    void TaskOnClickSingleplayer ()
    {
        Debug.Log("Clicou no botao Singleplayer");
		gameLevelInfo.gameMode = LevelInfo.GAME_MODE.SINGLE;
		gameLevelInfo.lvl = 1;
		SceneManager.LoadScene(multiplayerScene);
    }

    void TaskOnClickMultiplayer()
    {
        Debug.Log("Clicou no botao Multiplayer");
		gameLevelInfo.gameMode = LevelInfo.GAME_MODE.MULTI;
		gameLevelInfo.lvl = 0;
        SceneManager.LoadScene(multiplayerScene);
    }

    void TaskOnClickCrebitos()
    {
        if (!titlescreenLocker.isLocked() && !crebitosLocker.isLocked()) {
            Debug.Log("Clicou no botao Crebitos");
            
            titlescreenLocker.lockMenu();
            StartCoroutine(routine: fadeOutGameObject(gameObjectTitlescreen));
            crebitosLocker.lockMenu();
            StartCoroutine(routine: fadeInGameObject(gameObjectCrebitos));
        }
    }

    void TaskOnClickSair()
    {
        Debug.Log("Clicou no botao Sair");
        Application.Quit();
    }

    IEnumerator fadeOutGameObject(GameObject go)
    {
        // While Crebitos screen is on, titlescreen can not be used
        go.GetComponent<CanvasGroup>().interactable = false;

        CanvasGroup cg = go.GetComponent<CanvasGroup>();
        float time = 0.5f;
        while (cg.alpha > 0.0f)
        {
            cg.alpha -= Time.deltaTime / time;
            yield return null;
        }
        cg.alpha = 0.0f;
        titlescreenLocker.unlockMenu();
    }

    IEnumerator fadeInGameObject(GameObject go)
    {
        CanvasGroup cg = go.GetComponent<CanvasGroup>();
        float time = 0.5f;
        while (cg.alpha < 1.0f)
        {
            cg.alpha += Time.deltaTime / time;
            yield return null;
        }
        cg.alpha = 1.0f;
        crebitosLocker.unlockMenu();
    }
}
