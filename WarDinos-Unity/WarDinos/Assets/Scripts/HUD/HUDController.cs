using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public int player;

    public GameObject dinoGroupPrefab;
    public GameObject lane1P1;
    public GameObject lane1P2;
    public GameObject lane2P1;
    public GameObject lane2P2;
    public GameObject lane3P1;
    public GameObject lane3P2;
   
    private int laneToSpawn;

    public KeyCode keyUp;
    public KeyCode keyDown;
    public KeyCode keyLeft;
    public KeyCode keyRight;
    public KeyCode keyConfirm;

    public GameObject panelUnits;
    public GameObject panelUpgrades;
    public float PanelUpgradesXTranslation;

    public GameObject imageSelectedDino;
    public Button buttonUnitVelociraptor;
    public Button buttonUnitEstegossauro;
    public Button buttonUnitTriceratopo;
    public Button buttonUnitPterodactilo;
    public Button buttonUnitApatossauro;
    public Button buttonUnitTiranossauro;
    public Button buttonLane1;
    public Button buttonLane2;
    public Button buttonLane3;

    public Button buttonUpgradeVelociraptor;
    public Button buttonUpgradeEstegossauro;
    public Button buttonUpgradeTriceratopo;
    public Button buttonUpgradePterodactilo;
    public Button buttonUpgradeApatossauro;
    public Button buttonUpgradeTiranossauro;
    public Button buttonUpgradeVida;
    public Button buttonUpgradePAtaque;
    public Button buttonUpgradeVelAtaque;
    public Button buttonUpgradeVelDeslocamento;
    public Button buttonUpgradeHabilidade;

    public Button changeToPesquisas;
    public Button changeToUnidades;

    public MatchManager matchManager;

    private Button selectedButton;
    private Button lastSelectedUnitButton;
    private Button lastSelectedLaneButton;
    private Button lastSelectedUpgradeUnitButton;
    private Button lastSelectedUpgradeAttributeButton;

    private float panelUpgradesClosedX;
    private float panelUpgradesOpenX;

    private bool doingFadeIn = false;
    private bool doingFadeOut = false;
    private bool doingTranslateIn = false;
    private bool doingTranslateOut = false;

    // Use this for initialization
    void Start()
    {
        selectedButton = buttonLane1;
        lastSelectedUnitButton = buttonUnitVelociraptor;
        lastSelectedLaneButton = buttonLane1;
        lastSelectedUpgradeUnitButton = changeToUnidades;
        lastSelectedUpgradeAttributeButton = buttonUpgradeVida;
        changeButton(selectedButton, false);

        panelUpgradesClosedX = panelUpgrades.transform.localPosition.x;
        panelUpgradesOpenX = panelUpgrades.transform.localPosition.x + PanelUpgradesXTranslation;
    }


    // Update is called once per frame
    void Update()
    {
        // ------ KEY DOWN --------------------------------------------------------------------
        if (Input.GetKeyDown(keyDown))
        {
            if (selectedButton.FindSelectableOnDown())
            {
                // Every button
                changeButton((Button)selectedButton.FindSelectableOnDown(), false);
                // If button is
                // Unit button
                if (selectedButton.GetComponent<UnitButton>())
                {
                    // Unpress add button
                    Selectable addB = selectedButton.FindSelectableOnRight();
                    addB.targetGraphic.CrossFadeColor(addB.colors.normalColor, addB.colors.fadeDuration, true, true);
                    // Unpress sub button
                    Selectable subB = selectedButton.FindSelectableOnLeft();
                    subB.targetGraphic.CrossFadeColor(subB.colors.normalColor, subB.colors.fadeDuration, true, true);

                    UnitButton ub = selectedButton.GetComponent<UnitButton>();

                    // Change the dinosaur displayed in HUD if button
                    // changed to is a unit button
                    imageSelectedDino.GetComponent<Image>().sprite = ub.getSpriteInFrame();
                }
                // Attribute Upgrade button
                else if (selectedButton.GetComponent<WDHudAttributeButton>()) {
                    // Unpress add button
                    Selectable addB = selectedButton.FindSelectableOnLeft();
                    addB.targetGraphic.CrossFadeColor(addB.colors.normalColor, addB.colors.fadeDuration, true, true);
                }
            }
        }
        // ------------------------------------------------------------------------------------


        // ------- KEY UP ---------------------------------------------------------------------
        if (Input.GetKeyDown(keyUp))
        {
            if (selectedButton.FindSelectableOnUp())
            {
                // Every button
                changeButton((Button)selectedButton.FindSelectableOnUp(), false);
                // If button is
                // Unit button
                if (selectedButton.GetComponent<UnitButton>())
                {
                    // Unpress add button
                    Selectable addB = selectedButton.FindSelectableOnRight();
                    addB.targetGraphic.CrossFadeColor(addB.colors.normalColor, addB.colors.fadeDuration, true, true);
                    // Unpress sub button
                    Selectable subB = selectedButton.FindSelectableOnLeft();
                    subB.targetGraphic.CrossFadeColor(subB.colors.normalColor, subB.colors.fadeDuration, true, true);

                    UnitButton ub = selectedButton.GetComponent<UnitButton>();

                    // Change the dinosaur displayed in HUD if button
                    // changed to is a unit button
                    imageSelectedDino.GetComponent<Image>().sprite = ub.getSpriteInFrame();
                }
                // Attribute Upgrade button
                else if (selectedButton.GetComponent<WDHudAttributeButton>())
                {
                    // Unpress add button
                    Selectable addB = selectedButton.FindSelectableOnLeft();
                    addB.targetGraphic.CrossFadeColor(addB.colors.normalColor, addB.colors.fadeDuration, true, true);
                }
            }
        }
        // ------------------------------------------------------------------------------------


        // ------ KEY LEFT --------------------------------------------------------------------
        if (Input.GetKeyDown(keyLeft))
        {
            // If button is
            // Unit button
            if (selectedButton.GetComponent<UnitButton>())
            {
                UnitButton ub = selectedButton.GetComponent<UnitButton>();
                string dinoname = ub.getDinosaur();
                ub.decQuantityOnGroup();
                Debug.Log("[Pressionou Esquerda] Removeu " + dinoname + " do grupo");
                // Press sub button
                Selectable subB = selectedButton.FindSelectableOnLeft();
                subB.targetGraphic.CrossFadeColor(subB.colors.pressedColor, subB.colors.fadeDuration, true, true);
            }
            // Upgrade Attribute button
            else if (selectedButton.GetComponent<WDHudAttributeButton>()) {
                Debug.Log("Fez upgrade no atributo " + selectedButton.GetComponent<WDHudAttributeButton>().getAttribute() +
                    " nos dinossauros do tipo " + lastSelectedUpgradeUnitButton.GetComponent<WDHudUpgradeButton>().getDinosaur());
                // Press add button
                Selectable addB = selectedButton.FindSelectableOnLeft();
                addB.targetGraphic.CrossFadeColor(addB.colors.pressedColor, addB.colors.fadeDuration, true, true);
            }
        }
        if (Input.GetKeyUp(keyLeft))
        {
            // If button is
            // Upgrade Attribute button
            if (selectedButton.GetComponent<WDHudAttributeButton>())
            {
                // Unpress add button
                Selectable addB = selectedButton.FindSelectableOnLeft();
                addB.targetGraphic.CrossFadeColor(addB.colors.normalColor, addB.colors.fadeDuration, true, true);
            }
            // Upgrade Unit button
            else if (selectedButton.GetComponent<UnitButton>())
            {
                // Unpress sub button
                Selectable subB = selectedButton.FindSelectableOnLeft();
                subB.targetGraphic.CrossFadeColor(subB.colors.normalColor, subB.colors.fadeDuration, true, true);
            }
        }
        // ------------------------------------------------------------------------------------


        // ------ KEY RIGHT --------------------------------------------------------------------
        if (Input.GetKeyDown(keyRight))
        {
            // If button is
            // Unit button
            if (selectedButton.GetComponent<UnitButton>())
            {
                UnitButton ub = selectedButton.GetComponent<UnitButton>();
                string dinoname = ub.getDinosaur();
                ub.incQuantityOnGroup();
                Debug.Log("[Pressionou Direita] Adicionou " + dinoname + " ao grupo");
                // Press add button
                Selectable addB = selectedButton.FindSelectableOnRight();
                addB.targetGraphic.CrossFadeColor(addB.colors.pressedColor, addB.colors.fadeDuration, true, true);
            }
        }
        if (Input.GetKeyUp(keyRight))
        {
            // If button is
            // Unit button
            if (selectedButton.GetComponent<UnitButton>())
            {
                // Unpress add button
                Selectable addB = selectedButton.FindSelectableOnRight();
                addB.targetGraphic.CrossFadeColor(addB.colors.normalColor, addB.colors.fadeDuration, true, true);
            }
        }
        // ------------------------------------------------------------------------------------


        // ----- KEY CONFIRM -------------------------------------------------------------------
        if (Input.GetKeyDown(keyConfirm))
        {
            Button bt = (Button)selectedButton;
            bt.targetGraphic.CrossFadeColor(
                bt.colors.pressedColor,
                bt.colors.fadeDuration,
                true,
                true
            );
        }
        if (Input.GetKeyUp(keyConfirm))
        {
            Button bt = (Button)selectedButton;
            bt.targetGraphic.CrossFadeColor(
                bt.colors.highlightedColor,
                bt.colors.fadeDuration,
                true,
                true
            );

            // If button is
            // Unit button
            if (selectedButton.GetComponent<UnitButton>())
            {
                // Unpress add button
                Selectable addB = selectedButton.FindSelectableOnRight();
                addB.targetGraphic.CrossFadeColor(addB.colors.normalColor, addB.colors.fadeDuration, true, true);
                // Unpress sub button
                Selectable subB = selectedButton.FindSelectableOnLeft();
                subB.targetGraphic.CrossFadeColor(subB.colors.normalColor, subB.colors.fadeDuration, true, true);

                UnitButton ub = selectedButton.GetComponent<UnitButton>();
                string dinoname = ub.getDinosaur();
                Debug.Log("[Pressionou " + dinoname + "] Despachou grupo na Lane[" + lastSelectedLaneButton.GetComponent<LaneButton>().getNumber() + "]");



                // Instantiate the group to be spawned
                GroupController gc = Instantiate(dinoGroupPrefab).GetComponent<GroupController>();

                // Set parameters to initialize the group and Reset dinosaurs quantities in the HUD
                GroupController.DinoType[] dinos = new GroupController.DinoType[4];

                int q = buttonUnitTiranossauro.GetComponent<UnitButton>().getQuantityOnGroup();
                int i = 0;
                int j = 0;
                while (i < 4 && j < q)
                {
                    dinos[i] = GroupController.DinoType.TREX;
                    j++;
                    i++;
                }
                buttonUnitTiranossauro.GetComponent<UnitButton>().resetQuantityOnGroup();

                q = buttonUnitTriceratopo.GetComponent<UnitButton>().getQuantityOnGroup();
                j = 0;
                while (i < 4 && j < q)
                {
                    dinos[i] = GroupController.DinoType.TRICERATOPO;
                    j++;
                    i++;
                }
                buttonUnitTriceratopo.GetComponent<UnitButton>().resetQuantityOnGroup();

                q = buttonUnitEstegossauro.GetComponent<UnitButton>().getQuantityOnGroup();
                j = 0;
                while (i < 4 && j < q)
                {
                    dinos[i] = GroupController.DinoType.ESTEGOSSAURO;
                    j++;
                    i++;
                }
                buttonUnitEstegossauro.GetComponent<UnitButton>().resetQuantityOnGroup();

                q = buttonUnitApatossauro.GetComponent<UnitButton>().getQuantityOnGroup();
                j = 0;
                while (i < 4 && j < q)
                {
                    dinos[i] = GroupController.DinoType.APATOSSAURO;
                    j++;
                    i++;
                }
                buttonUnitApatossauro.GetComponent<UnitButton>().resetQuantityOnGroup();

                q = buttonUnitPterodactilo.GetComponent<UnitButton>().getQuantityOnGroup();
                j = 0;
                while (i < 4 && j < q)
                {
                    dinos[i] = GroupController.DinoType.PTERODACTILO;
                    j++;
                    i++;
                }
                buttonUnitPterodactilo.GetComponent<UnitButton>().resetQuantityOnGroup();

                q = buttonUnitVelociraptor.GetComponent<UnitButton>().getQuantityOnGroup();
                j = 0;
                while (i < 4 && j < q)
                {
                    dinos[i] = GroupController.DinoType.RAPTOR;
                    j++;
                    i++;
                }
                buttonUnitVelociraptor.GetComponent<UnitButton>().resetQuantityOnGroup();

                GameObject lb;
                GameObject le;

                int ln = lastSelectedLaneButton.GetComponent<LaneButton>().getNumber();
                if (ln == 1)
                {
                    lb = lane1P1;
                    le = lane1P2;
                }
                else if (ln == 2)
                {
                    lb = lane2P1;
                    le = lane2P2;
                }
                else
                {
                    lb = lane3P1;
                    le = lane3P2;
                }

                if (player == 1)
                    gc.initGroup(1, lb, le, dinos);
                else
                    gc.initGroup(2, le, lb, dinos);
                // Change the cursor from unit selection to lane selection
                lastSelectedUnitButton = selectedButton;
                changeButton(lastSelectedLaneButton, false);
            }
            // Lane button
            else if (selectedButton.GetComponent<LaneButton>())
            {
                // Change the cursor from lane selection to unit selection
                lastSelectedLaneButton = selectedButton;
                changeButton(lastSelectedUnitButton, true);
            }
            // Upgrade unit button
            else if (selectedButton.GetComponent<WDHudUpgradeButton>())
            {
                // Change the cursor from upgrade unit selection to upgrade attribute selection
                lastSelectedUpgradeUnitButton = selectedButton;
                changeButton(lastSelectedUpgradeAttributeButton, true);
            }
            // Upgrade Attribute button
            else if (selectedButton.GetComponent<WDHudAttributeButton>())
            {
                // Unpress add button
                Selectable addB = selectedButton.FindSelectableOnLeft();
                addB.targetGraphic.CrossFadeColor(addB.colors.normalColor, addB.colors.fadeDuration, true, true);
                // Change the cursor from upgrade attribute selection to upgrade unit selection
                lastSelectedUpgradeAttributeButton = selectedButton;
                changeButton(lastSelectedUpgradeUnitButton, false);
            }
            // Change Menu button
            else if (selectedButton.GetComponent<changeMenuButton>()) {
                if (selectedButton.GetComponent<changeMenuButton>().Menu == 1)
                    switchToUpgradesMenu();
                else
                    switchToUnitsMenu();
            }
        }
        // ------------------------------------------------------------------------------------
    }


    private void changeButton(Button nextButton, bool keepHighlited)
    {
        if (keepHighlited)
            selectedButton.targetGraphic.CrossFadeColor(selectedButton.colors.pressedColor, selectedButton.colors.fadeDuration, true, true);
        else
            selectedButton.targetGraphic.CrossFadeColor(selectedButton.colors.normalColor, selectedButton.colors.fadeDuration, true, true);
        selectedButton = nextButton;
        selectedButton.targetGraphic.CrossFadeColor(selectedButton.colors.highlightedColor, selectedButton.colors.fadeDuration, true, true);
    }


    IEnumerator fadeOutCanvas(CanvasGroup cg)
    {
        doingFadeOut = true;
        float time = 0.35f;
        while (cg.alpha > 0.0f) {
            cg.alpha -= Time.deltaTime / time;
            yield return null;
        }
        cg.alpha = 0.0f;
        doingFadeOut = false;
    }

    IEnumerator fadeInCanvas(CanvasGroup cg)
    {
        doingFadeIn = true;
        float time = 0.35f;
        while (cg.alpha < 1.0f)
        {
            cg.alpha += Time.deltaTime / time;
            yield return null;
        }
        cg.alpha = 1.0f;
        doingFadeIn = false;
    }

    IEnumerator translateInCanvas(CanvasGroup cg)
    {
        doingTranslateIn = true;
        float time = 0.35f;
        float s = 1.0f;
        Vector3 newLocalPosition = cg.transform.localPosition;
        while (s > 0.0f)
        {
            s -= Time.deltaTime / time;
            newLocalPosition.x = panelUpgradesClosedX*s + panelUpgradesOpenX*(1.0f-s);
            cg.transform.localPosition = newLocalPosition;
            yield return null;
        }
        newLocalPosition.x = panelUpgradesOpenX;
        doingTranslateIn = false;
    }

    IEnumerator translateOutCanvas(CanvasGroup cg)
    {
        doingTranslateOut = true;
        float time = 0.35f;
        float s = 1.0f;
        Vector3 newLocalPosition = cg.transform.localPosition;
        while (s > 0.0f)
        {
            s -= Time.deltaTime / time;
            newLocalPosition.x = panelUpgradesClosedX * (1.0f - s) + panelUpgradesOpenX * s;
            cg.transform.localPosition = newLocalPosition;
            yield return null;
        }
        newLocalPosition.x = panelUpgradesClosedX;
        doingTranslateOut = false;
    }

    private bool isAnimating ()
    {
        return (
            doingFadeIn || doingFadeOut ||
            doingTranslateIn || doingTranslateOut );
    }

    private void switchToUnitsMenu()
    {
        if (!isAnimating())
        {
            lastSelectedUpgradeUnitButton = selectedButton;
            // Animation
            doingFadeIn = true;
            doingTranslateOut = true;
            CanvasGroup cgunits = panelUnits.GetComponent<CanvasGroup>();
            CanvasGroup cgupgrades = panelUpgrades.GetComponent<CanvasGroup>();
            StartCoroutine(routine: fadeInCanvas(cgunits));
            StartCoroutine(routine: translateOutCanvas(cgupgrades));

            changeButton(lastSelectedLaneButton, false);
            Debug.Log("[Pressionou Direita] Fechou a loja");
        }
    }

    private void switchToUpgradesMenu()
    {
        if (!isAnimating())
        {
            lastSelectedLaneButton = selectedButton;
            // Animation
            doingFadeOut = true;
            doingTranslateIn = true;
            CanvasGroup cgunits = panelUnits.GetComponent<CanvasGroup>();
            CanvasGroup cgupgrades = panelUpgrades.GetComponent<CanvasGroup>();
            StartCoroutine(routine: fadeOutCanvas(cgunits));
            StartCoroutine(routine: translateInCanvas(cgupgrades));

            changeButton(lastSelectedUpgradeUnitButton, false);
            Debug.Log("[Pressionou Direita] Abriu a loja");
        }
    }
}