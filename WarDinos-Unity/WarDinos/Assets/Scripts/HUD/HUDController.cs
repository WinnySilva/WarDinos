using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public int player;
    // GameObject of the player
    public GameObject pgo;
    private Player pgoPlayer;
    private Dinossauro pgoPlayerApato;
    private Dinossauro pgoPlayerVeloci;
    private Dinossauro pgoPlayerTricera;
    private Dinossauro pgoPlayerTRex;
    private Dinossauro pgoPlayerEstego;
    private Dinossauro pgoPlayerPtero;

    public GameObject dinoGroupPrefab;
    public GameObject lane1P1;
    public GameObject lane1P2;
    public GameObject lane2P1;
    public GameObject lane2P2;
    public GameObject lane3P1;
    public GameObject lane3P2;
   
    private int laneToSpawn;

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
    private UnitButton buttonUnitVelociraptorUB;
    private UnitButton buttonUnitEstegossauroUB;
    private UnitButton buttonUnitTriceratopoUB;
    private UnitButton buttonUnitPterodactiloUB;
    private UnitButton buttonUnitApatossauroUB;
    private UnitButton buttonUnitTiranossauroUB;
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

    public Text informationText;
    private Coroutine lastMessageCoroutine = null;
    private string lastMessageString = " ";
    private int lastMessageFontSize = 16;
    private bool isMessaging = false;

    public Text tooltipLojaText;
    private Coroutine lastMessageLojaCoroutine = null;
    private string lastMessageStringLoja = " ";
    private int lastMessageFontSizeLoja = 16;
    private bool isMessagingLoja = false;

    public GameObject dinoSkill;
    public Sprite maxLevelSprite;

    public Image[] dinoGroupFrames;
    public Sprite dinoGroupFreeSlotSprite;

    [System.Serializable]
    public struct DinoPanelTexts {
        public Text Vida;
        public Text Atk;
        public Text VelAtk;
        public Text Vel;
        public Text Tam;
    }

    // For pressing DOWN buttons
    private bool pressingRight = false;
    private bool pressingLeft = false;
    private bool pressingUp = false;
    private bool pressingDown = false;

    // For pressing UP buttons
    private bool pressingRight2 = false;
    private bool pressingLeft2 = false;
    private bool pressingUp2 = false;
    private bool pressingDown2 = false;

    public DinoPanelTexts dpt;

    private int freeSlots = 4;

    // Use this for initialization
    void Start()
    {
        pgoPlayer = pgo.GetComponent<Player>();
        pgoPlayerApato = pgoPlayer.goApatossauro.GetComponent<Dinossauro>();
        pgoPlayerPtero = pgoPlayer.goPterodactilo.GetComponent<Dinossauro>();
        pgoPlayerTRex = pgoPlayer.goTrex.GetComponent<Dinossauro>();
        pgoPlayerEstego = pgoPlayer.goEstegossauro.GetComponent<Dinossauro>();
        pgoPlayerTricera = pgoPlayer.goTriceratopo.GetComponent<Dinossauro>();
        pgoPlayerVeloci = pgoPlayer.goVelociraptor.GetComponent<Dinossauro>();

        selectedButton = buttonLane1;
        lastSelectedUnitButton = buttonUnitVelociraptor;
        lastSelectedLaneButton = buttonLane1;
        lastSelectedUpgradeUnitButton = changeToUnidades;
        lastSelectedUpgradeAttributeButton = buttonUpgradeVida;
        changeButton(selectedButton, false);

        panelUpgradesClosedX = panelUpgrades.transform.localPosition.y;
        panelUpgradesOpenX = panelUpgrades.transform.localPosition.y + PanelUpgradesXTranslation;

        buttonUnitVelociraptorUB = buttonUnitVelociraptor.GetComponent<UnitButton>();
        buttonUnitEstegossauroUB = buttonUnitEstegossauro.GetComponent<UnitButton>();
        buttonUnitTriceratopoUB = buttonUnitTriceratopo.GetComponent<UnitButton>();
        buttonUnitPterodactiloUB = buttonUnitPterodactilo.GetComponent<UnitButton>();
        buttonUnitApatossauroUB = buttonUnitApatossauro.GetComponent<UnitButton>();
        buttonUnitTiranossauroUB = buttonUnitTiranossauro.GetComponent<UnitButton>();

        updateDinoFrameInfo();
        changeTooltipText("Seleciona Lane " + selectedButton.GetComponent<LaneButton>().getNumber() + " para Despacho", 14);
        displayMessage("Initiating Battlefield Control...", 4.0f, 15);
    }


    // Update is called once per frame
    void Update()
    {
        // ------ KEY RIGHT --------------------------------------------------------------------
        if (getAxisRightDown())
        {
            if (selectedButton.FindSelectableOnDown())
            {
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

                    changeButton((Button)selectedButton.FindSelectableOnDown(), false);

                    UnitButton ub = selectedButton.GetComponent<UnitButton>();

                    // Change the dinosaur displayed in HUD if button
                    // changed to is a unit button
                    updateDinoFrameInfo();
                }
                // Attribute Upgrade button
                else if (selectedButton.GetComponent<WDHudAttributeButton>()) {
                    // Unpress add button
                    Selectable addB = selectedButton.FindSelectableOnLeft();
                    addB.targetGraphic.CrossFadeColor(addB.colors.normalColor, addB.colors.fadeDuration, true, true);

                    changeButton((Button)selectedButton.FindSelectableOnDown(), false);
                    changeTooltipLojaText(selectedButton.GetComponent<WDHudAttributeButton>().getAttribute(), 16);
                    if (selectedButton.GetComponent<WDHudAttributeButton>().AttrType == Attributes.HAB)
                        changeTooltipLojaText(lastSelectedUpgradeUnitButton.GetComponent<WDHudUpgradeButton>().descricaoHabilidade, 15);
                }
                // Upgrade Unit button
                else if (selectedButton.GetComponent<WDHudUpgradeButton>())
                {
                    changeButton((Button)selectedButton.FindSelectableOnDown(), false);
                    WDHudUpgradeButton wdhub = selectedButton.GetComponent<WDHudUpgradeButton>();
                    if (wdhub != null) {
                        changeTooltipLojaText(wdhub.getDinosaur(), 16);
                        // Change the ability icon
                        dinoSkill.GetComponent<Image>().sprite = wdhub.SpriteHabilidade;
                        updateUpgradeIcons();
                    }
                    else
                        changeTooltipLojaText("Para Menu de Unidades", 14);
                }
                // Change Menu Button
                else if (selectedButton.GetComponent<changeMenuButton>()) {
                    changeMenuButton cmb = selectedButton.GetComponent<changeMenuButton>();
                    changeButton((Button)selectedButton.FindSelectableOnDown(), false);
                    WDHudUpgradeButton wdhub = selectedButton.GetComponent<WDHudUpgradeButton>();
                    if (wdhub != null)
                    {
                        changeTooltipLojaText(wdhub.getDinosaur(), 16);
                        // Change the ability icon
                        dinoSkill.GetComponent<Image>().sprite = wdhub.SpriteHabilidade;
                        updateUpgradeIcons();
                    }
                    if (cmb.Menu == 2)
                        changeTooltipLojaText(selectedButton.GetComponent<WDHudUpgradeButton>().getDinosaur(), 16);
                    else
                        changeTooltipText("Seleciona Lane " + selectedButton.GetComponent<LaneButton>().getNumber() + " para Despacho", 14);
                }
                // Lane Button
                else if (selectedButton.GetComponent<LaneButton>())
                {
                    changeButton((Button)selectedButton.FindSelectableOnDown(), false);
                    LaneButton lb = selectedButton.GetComponent<LaneButton>();
                    if (lb != null)
                        changeTooltipText("Seleciona Lane " + lb.getNumber() + " para Despacho", 14);
                    else
                        changeTooltipText("Para Menu de Pesquisas", 14);
                }
                else {
                    changeButton((Button)selectedButton.FindSelectableOnDown(), false);
                }
            }
        }
        // ------------------------------------------------------------------------------------


        // ------- KEY LEFT ---------------------------------------------------------------------
        //if ((Input.GetButtonDown("HorizontalP1") && horiP1 < 0 && player == 1) || (Input.GetButtonDown("HorizontalP2") && horiP2 < 0 && player == 2))
        if (getAxisLeftDown())
        {
            if (selectedButton.FindSelectableOnUp())
            {
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

                    changeButton((Button)selectedButton.FindSelectableOnUp(), false);

                    UnitButton ub = selectedButton.GetComponent<UnitButton>();

                    // Change the dinosaur displayed in HUD if button
                    // changed to is a unit button
                    updateDinoFrameInfo();
                }
                // Attribute Upgrade button
                else if (selectedButton.GetComponent<WDHudAttributeButton>())
                {
                    // Unpress add button
                    Selectable addB = selectedButton.FindSelectableOnLeft();
                    addB.targetGraphic.CrossFadeColor(addB.colors.normalColor, addB.colors.fadeDuration, true, true);

                    changeButton((Button)selectedButton.FindSelectableOnUp(), false);
                    changeTooltipLojaText(selectedButton.GetComponent<WDHudAttributeButton>().getAttribute(), 16);
                    if (selectedButton.GetComponent<WDHudAttributeButton>().AttrType == Attributes.HAB)
                        changeTooltipLojaText(lastSelectedUpgradeUnitButton.GetComponent<WDHudUpgradeButton>().descricaoHabilidade, 15);
                }
                else if (selectedButton.GetComponent<WDHudUpgradeButton>())
                {
                    changeButton((Button)selectedButton.FindSelectableOnUp(), false);
                    WDHudUpgradeButton wdhub = selectedButton.GetComponent<WDHudUpgradeButton>();
                    if (wdhub != null) {
                        changeTooltipLojaText(wdhub.getDinosaur(), 16);
                        // Change the ability icon
                        dinoSkill.GetComponent<Image>().sprite = wdhub.SpriteHabilidade;
                        updateUpgradeIcons();
                    }
                    else
                    {
                        changeTooltipLojaText("Para Menu de Unidades", 14);
                    }
                }
                // Change Menu Button
                else if (selectedButton.GetComponent<changeMenuButton>())
                {
                    changeMenuButton cmb = selectedButton.GetComponent<changeMenuButton>();
                    changeButton((Button)selectedButton.FindSelectableOnUp(), false);
                    WDHudUpgradeButton wdhub = selectedButton.GetComponent<WDHudUpgradeButton>();
                    if (wdhub != null)
                    {
                        changeTooltipLojaText(wdhub.getDinosaur(), 16);
                        // Change the ability icon
                        dinoSkill.GetComponent<Image>().sprite = wdhub.SpriteHabilidade;
                        updateUpgradeIcons();
                    }
                    if (cmb.Menu == 2) {
                        changeTooltipLojaText(selectedButton.GetComponent<WDHudUpgradeButton>().getDinosaur(), 16);
                    }
                    else
                        changeTooltipText("Seleciona Lane " + selectedButton.GetComponent<LaneButton>().getNumber() + " para Despacho", 14);
                }
                // Lane Button
                else if (selectedButton.GetComponent<LaneButton>())
                {
                    changeButton((Button)selectedButton.FindSelectableOnUp(), false);
                    LaneButton lb = selectedButton.GetComponent<LaneButton>();
                    if (lb != null)
                        changeTooltipText("Seleciona Lane " + lb.getNumber() + " para Despacho", 14);
                    else
                        changeTooltipText("Para Menu de Pesquisas", 14);
                }
                else
                {
                    changeButton((Button)selectedButton.FindSelectableOnUp(), false);
                }
            }
        }
        // ------------------------------------------------------------------------------------


        // ------ KEY DOWN --------------------------------------------------------------------
        if (getAxisDownDown())
        {
            // If button is
            // Unit button
            if (selectedButton.GetComponent<UnitButton>())
            {
                // Remove a dinosaur from HUDs group (group used for display porpuses only)
                int slots = slotsOccupiedByDino();
                Debug.Log("freeSlots antes: " + freeSlots);
                UnitButton ub = selectedButton.GetComponent<UnitButton>();
                if (freeSlots + slots <= 4 && ub.getQuantityOnGroup() > 0) {
                    freeSlots += slots;
                    string dinoname = ub.getDinosaur();
                    ub.decQuantityOnGroup();
                    Debug.Log("[Pressionou Esquerda] Removeu " + dinoname + " do grupo");
                    updateDinoGroupInfo();
                    Debug.Log("freeSlots depois: " + freeSlots);
                }

                // Press sub button
                Selectable subB = selectedButton.FindSelectableOnLeft();
                subB.targetGraphic.CrossFadeColor(subB.colors.pressedColor, subB.colors.fadeDuration, true, true);
            }
        }
        if (getAxisDownUp())
        {
            // If button is
            // Upgrade Unit button
            if (selectedButton.GetComponent<UnitButton>())
            {
                // Unpress sub button
                Selectable subB = selectedButton.FindSelectableOnLeft();
                subB.targetGraphic.CrossFadeColor(subB.colors.normalColor, subB.colors.fadeDuration, true, true);
            }
        }
        // ------------------------------------------------------------------------------------


        // ------ KEY UP --------------------------------------------------------------------
        if (getAxisUpDown())
        {
            // If button is
            // Unit button
            if (selectedButton.GetComponent<UnitButton>())
            {
                // Add a dinosaur to HUDs group (group used for display porpuses only)
                int slots = slotsOccupiedByDino();
                Debug.Log("freeSlots antes: " + freeSlots);
                if (freeSlots >= slots) {
                    freeSlots -= slots;
                    UnitButton ub = selectedButton.GetComponent<UnitButton>();
                    string dinoname = ub.getDinosaur();
                    ub.incQuantityOnGroup();
                    Debug.Log("[Pressionou Direita] Adicionou " + dinoname + " ao grupo");
                    updateDinoGroupInfo();
                    Debug.Log("freeSlots depois: " + freeSlots);
                }

                // Press add button
                Selectable addB = selectedButton.FindSelectableOnRight();
                addB.targetGraphic.CrossFadeColor(addB.colors.pressedColor, addB.colors.fadeDuration, true, true);
            }
            // Upgrade Attribute button
            else if (selectedButton.GetComponent<WDHudAttributeButton>())
            {
                Debug.Log("Fez upgrade no atributo " + selectedButton.GetComponent<WDHudAttributeButton>().getAttribute() +
                    " nos dinossauros do tipo " + lastSelectedUpgradeUnitButton.GetComponent<WDHudUpgradeButton>().getDinosaur());
                // Press add button
                Selectable addB = selectedButton.FindSelectableOnLeft();
                addB.targetGraphic.CrossFadeColor(addB.colors.pressedColor, addB.colors.fadeDuration, true, true);

                // Efetua a compra caso haja recursos. Caso contrario emite uma mensagem avisando que nao tem recursos
                Debug.Log(selectedButton + " ___ " + selectedButton.GetComponent<WDHudAttributeButton>());
                int upgradeFlag = pgoPlayer.Upgrade(lastSelectedUpgradeUnitButton.GetComponent<WDHudUpgradeButton>().DinoType,
                    selectedButton.GetComponent<WDHudAttributeButton>().AttrType);
                if (upgradeFlag == -1)
                    displayMessageLoja("Requer mais DODO METH", 4.0f, 16);
                else if (upgradeFlag == -2) {
                    displayMessageLoja("Impossivel, Nivel Maximo", 4.0f, 14);
                }
                else if (upgradeFlag == 2) {
                    lastSelectedUpgradeUnitButton.GetComponent<WDHudUpgradeButton>().attributesInMaxLevel[(int)selectedButton.GetComponent<WDHudAttributeButton>().AttrType] = true; // PRO CODING INC
                    selectedButton.GetComponentsInChildren<Image>()[1].sprite = maxLevelSprite;
                }

                Debug.Log(pgoPlayer.GetComponent<Player>().Recursos);

            }
        }
        if (getAxisUpUp())
        {
            // If button is
            // Unit button
            if (selectedButton.GetComponent<UnitButton>())
            {
                // Unpress add button
                Selectable addB = selectedButton.FindSelectableOnRight();
                addB.targetGraphic.CrossFadeColor(addB.colors.normalColor, addB.colors.fadeDuration, true, true);
            }
            // Upgrade Attribute button
            else if (selectedButton.GetComponent<WDHudAttributeButton>())
            {
                // Unpress add button
                Selectable addB = selectedButton.FindSelectableOnLeft();
                addB.targetGraphic.CrossFadeColor(addB.colors.normalColor, addB.colors.fadeDuration, true, true);
            }
        }
        // ------------------------------------------------------------------------------------


        // ----- KEY CONFIRM -------------------------------------------------------------------
        if ((Input.GetButtonDown("ConfirmP1") && player == 1) || (Input.GetButtonDown("ConfirmP2") && player == 2))
        {
            Button bt = (Button)selectedButton;
            bt.targetGraphic.CrossFadeColor(
                bt.colors.pressedColor,
                bt.colors.fadeDuration,
                true,
                true
            );
        }
        if ((Input.GetButtonUp("ConfirmP1") && player == 1) || (Input.GetButtonUp("ConfirmP2") && player == 2))
        {
            // Update the info in dinos frame, so if the player upgraded his/her dinos
            // the information is correct
            updateDinoFrameInfo();

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


                // Set parameters to initialize the group and Reset dinosaurs quantities in the HUD
                GroupController.DinoType[] dinos = new GroupController.DinoType[4];

                int q = buttonUnitTiranossauro.GetComponent<UnitButton>().getQuantityOnGroup();
                int i = 0;
                int j = 0;
                int totalCost = 0;
                while (i < 4 && j < q) {
                    dinos[i] = GroupController.DinoType.TREX;
                    j++;
                    i++;
                    totalCost += pgoPlayer.goTrex.GetComponent<Dinossauro>().Custo;
                }
                buttonUnitTiranossauro.GetComponent<UnitButton>().resetQuantityOnGroup();

                q = buttonUnitTriceratopo.GetComponent<UnitButton>().getQuantityOnGroup();
                j = 0;
                while (i < 4 && j < q) {
                    dinos[i] = GroupController.DinoType.TRICERATOPO;
                    j++;
                    i++;
                    totalCost += pgoPlayer.goTriceratopo.GetComponent<Dinossauro>().Custo;
                }
                buttonUnitTriceratopo.GetComponent<UnitButton>().resetQuantityOnGroup();

                q = buttonUnitEstegossauro.GetComponent<UnitButton>().getQuantityOnGroup();
                j = 0;
                while (i < 4 && j < q) {
                    dinos[i] = GroupController.DinoType.ESTEGOSSAURO;
                    j++;
                    i++;
                    totalCost += pgoPlayer.goEstegossauro.GetComponent<Dinossauro>().Custo;
                }
                buttonUnitEstegossauro.GetComponent<UnitButton>().resetQuantityOnGroup();

                q = buttonUnitApatossauro.GetComponent<UnitButton>().getQuantityOnGroup();
                j = 0;
                while (i < 4 && j < q) {
                    dinos[i] = GroupController.DinoType.APATOSSAURO;
                    j++;
                    i++;
                    totalCost += pgoPlayer.goApatossauro.GetComponent<Dinossauro>().Custo;
                }
                buttonUnitApatossauro.GetComponent<UnitButton>().resetQuantityOnGroup();

                q = buttonUnitPterodactilo.GetComponent<UnitButton>().getQuantityOnGroup();
                j = 0;
                while (i < 4 && j < q) {
                    dinos[i] = GroupController.DinoType.PTERODACTILO;
                    j++;
                    i++;
                    totalCost += pgoPlayer.goPterodactilo.GetComponent<Dinossauro>().Custo;
                }
                buttonUnitPterodactilo.GetComponent<UnitButton>().resetQuantityOnGroup();

                q = buttonUnitVelociraptor.GetComponent<UnitButton>().getQuantityOnGroup();
                j = 0;
                while (i < 4 && j < q) {
                    dinos[i] = GroupController.DinoType.RAPTOR;
                    j++;
                    i++;
                    totalCost += pgoPlayer.goVelociraptor.GetComponent<Dinossauro>().Custo;
                }
                buttonUnitVelociraptor.GetComponent<UnitButton>().resetQuantityOnGroup();

                freeSlots = 4;

                Debug.Log("CUSTO DO GRUPO: " + totalCost);

                GameObject lb;
                GameObject le;

                int ln = lastSelectedLaneButton.GetComponent<LaneButton>().getNumber();
                if (ln == 1) {
                    lb = lane1P1;
                    le = lane1P2;
                }
                else if (ln == 2) {
                    lb = lane2P1;
                    le = lane2P2;
                }
                else {
                    lb = lane3P1;
                    le = lane3P2;
                }

                if (totalCost <= pgoPlayer.Recursos) {
                    // Instantiate the group to be spawned
                    GroupController gc = Instantiate(dinoGroupPrefab).GetComponent<GroupController>();

                    pgoPlayer.reduzirRecursos(totalCost);
                    if (player == 1)
                        gc.initGroup(1, lb, le, dinos,
                        pgoPlayer.goVelociraptor,
                        pgoPlayer.goEstegossauro,
                        pgoPlayer.goApatossauro,
                        pgoPlayer.goPterodactilo,
                        pgoPlayer.goTriceratopo,
                        pgoPlayer.goTrex);
                    else
                        gc.initGroup(2, le, lb, dinos,
                        pgoPlayer.goVelociraptor,
                        pgoPlayer.goEstegossauro,
                        pgoPlayer.goApatossauro,
                        pgoPlayer.goPterodactilo,
                        pgoPlayer.goTriceratopo,
                        pgoPlayer.goTrex);
                }
                else {
                    displayMessage("Requer mais DODO METH", 4.0f, 16);
                }

                // Clean group being displayed
                updateDinoGroupInfo();

                // Change the cursor from unit selection to lane selection
                lastSelectedUnitButton = selectedButton;
                changeButton(lastSelectedLaneButton, false);

                changeTooltipText("Seleciona Lane " + selectedButton.GetComponent<LaneButton>().getNumber() + " para Despacho", 14);
            }
            // Lane button
            else if (selectedButton.GetComponent<LaneButton>())
            {
                // Change the cursor from lane selection to unit selection
                lastSelectedLaneButton = selectedButton;
                changeButton(lastSelectedUnitButton, true);

                updateDinoGroupInfo();
            }
            // Upgrade unit button
            else if (selectedButton.GetComponent<WDHudUpgradeButton>())
            {
                // Change the cursor from upgrade unit selection to upgrade attribute selection
                lastSelectedUpgradeUnitButton = selectedButton;
                changeButton(lastSelectedUpgradeAttributeButton, true);

                changeTooltipLojaText(selectedButton.GetComponent<WDHudAttributeButton>().getAttribute(), 16);
                if (selectedButton.GetComponent<WDHudAttributeButton>().AttrType == Attributes.HAB)
                    changeTooltipLojaText(lastSelectedUpgradeUnitButton.GetComponent<WDHudUpgradeButton>().descricaoHabilidade, 15);
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

                changeTooltipLojaText(selectedButton.GetComponent<WDHudUpgradeButton>().getDinosaur(), 16);
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
            newLocalPosition.y = panelUpgradesClosedX*s + panelUpgradesOpenX*(1.0f-s);
            cg.transform.localPosition = newLocalPosition;
            yield return null;
        }
        newLocalPosition.y = panelUpgradesOpenX;
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
            newLocalPosition.y = panelUpgradesClosedX * (1.0f - s) + panelUpgradesOpenX * s;
            cg.transform.localPosition = newLocalPosition;
            yield return null;
        }
        newLocalPosition.y = panelUpgradesClosedX;
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

    private void updateDinoFrameInfo () {
        UnitButton ub = selectedButton.GetComponent<UnitButton>();
        // If the button currently selected isnt a unit button from the units menu, the frame shows
        // the attributes of the last selected buttons dinosaur
        if (ub == null)
            ub = lastSelectedUnitButton.GetComponent<UnitButton>();
        imageSelectedDino.GetComponent<Image>().sprite = ub.getSpriteInFrame();
        GroupController.DinoType dt = ub.dinosaurType;

        int fontSize = 15;
             
        if (dt == GroupController.DinoType.APATOSSAURO)
        {
            dpt.Vida.text = pgoPlayerApato.Vida.ToString();
            dpt.Atk.text = pgoPlayerApato.Ataque.ToString();
            dpt.VelAtk.text = pgoPlayerApato.VelocidadeAtaque.ToString();
            dpt.Vel.text = pgoPlayerApato.Velocidade_deslocamento.ToString();
            dpt.Tam.text = pgoPlayerApato.NSlot.ToString();
            changeTooltipText("" + ub.description + pgoPlayerApato.Custo + " Dodo Meth", fontSize);
        }
        if (dt == GroupController.DinoType.ESTEGOSSAURO)
        {
            dpt.Vida.text = pgoPlayerEstego.Vida.ToString();
            dpt.Atk.text = pgoPlayerEstego.Ataque.ToString();
            dpt.VelAtk.text = pgoPlayerEstego.VelocidadeAtaque.ToString();
            dpt.Vel.text = pgoPlayerEstego.Velocidade_deslocamento.ToString();
            dpt.Tam.text = pgoPlayerEstego.NSlot.ToString();
            changeTooltipText("" + ub.description + pgoPlayerEstego.Custo + " Dodo Meth", fontSize);
        }
        if (dt == GroupController.DinoType.PTERODACTILO)
        {
            dpt.Vida.text = pgoPlayerPtero.Vida.ToString();
            dpt.Atk.text = pgoPlayerPtero.Ataque.ToString();
            dpt.VelAtk.text = pgoPlayerPtero.VelocidadeAtaque.ToString();
            dpt.Vel.text = pgoPlayerPtero.Velocidade_deslocamento.ToString();
            dpt.Tam.text = pgoPlayerPtero.NSlot.ToString();
            changeTooltipText("" + ub.description + pgoPlayerPtero.Custo + " Dodo Meth", fontSize);
        }
        if (dt == GroupController.DinoType.RAPTOR)
        {
            dpt.Vida.text = pgoPlayerVeloci.Vida.ToString();
            dpt.Atk.text = pgoPlayerVeloci.Ataque.ToString();
            dpt.VelAtk.text = pgoPlayerVeloci.VelocidadeAtaque.ToString();
            dpt.Vel.text = pgoPlayerVeloci.Velocidade_deslocamento.ToString();
            dpt.Tam.text = pgoPlayerVeloci.NSlot.ToString();
            changeTooltipText("" + ub.description + pgoPlayerVeloci.Custo + " Dodo Meth", fontSize);
        }
        if (dt == GroupController.DinoType.TREX)
        {
            dpt.Vida.text = pgoPlayerTRex.Vida.ToString();
            dpt.Atk.text = pgoPlayerTRex.Ataque.ToString();
            dpt.VelAtk.text = pgoPlayerTRex.VelocidadeAtaque.ToString();
            dpt.Vel.text = pgoPlayerTRex.Velocidade_deslocamento.ToString();
            dpt.Tam.text = pgoPlayerTRex.NSlot.ToString();
            changeTooltipText("" + ub.description + pgoPlayerTRex.Custo + " Dodo Meth", fontSize);
        }
        if (dt == GroupController.DinoType.TRICERATOPO)
        {
            dpt.Vida.text = pgoPlayerTricera.Vida.ToString();
            dpt.Atk.text = pgoPlayerTricera.Ataque.ToString();
            dpt.VelAtk.text = pgoPlayerTricera.VelocidadeAtaque.ToString();
            dpt.Vel.text = pgoPlayerTricera.Velocidade_deslocamento.ToString();
            dpt.Tam.text = pgoPlayerTricera.NSlot.ToString();
            changeTooltipText("" + ub.description + pgoPlayerTricera.Custo + " Dodo Meth", fontSize);
        }
    }

    private int slotsOccupiedByDino() {
        UnitButton ub = selectedButton.GetComponent<UnitButton>();
        string dinoname = ub.getDinosaur();

        if (ub.DinosaurType == GroupController.DinoType.APATOSSAURO)
            return pgoPlayerApato.GetComponent<Dinossauro>().NSlot;
        else if (ub.DinosaurType == GroupController.DinoType.ESTEGOSSAURO)
            return pgoPlayerEstego.GetComponent<Dinossauro>().NSlot;
        else if (ub.DinosaurType == GroupController.DinoType.PTERODACTILO)
            return pgoPlayerPtero.GetComponent<Dinossauro>().NSlot;
        else if (ub.DinosaurType == GroupController.DinoType.RAPTOR)
            return pgoPlayerVeloci.GetComponent<Dinossauro>().NSlot;
        else if (ub.DinosaurType == GroupController.DinoType.TREX)
            return pgoPlayerTRex.GetComponent<Dinossauro>().NSlot;
        else if (ub.DinosaurType == GroupController.DinoType.TRICERATOPO)
            return pgoPlayerTricera.GetComponent<Dinossauro>().NSlot;
        else return -1;
    }

    private void updateDinoGroupInfo () {
        int dinoQuantity = 0;
        for (int i=0; i<buttonUnitTiranossauroUB.getQuantityOnGroup(); i++) {
            dinoGroupFrames[dinoQuantity].sprite = buttonUnitTiranossauroUB.getSpriteInFrame();
            dinoQuantity++;
        }
        for (int i = 0; i < buttonUnitTriceratopoUB.getQuantityOnGroup(); i++) {
            dinoGroupFrames[dinoQuantity].sprite = buttonUnitTriceratopoUB.getSpriteInFrame();
            dinoQuantity++;
        }
        for (int i = 0; i < buttonUnitEstegossauroUB.getQuantityOnGroup(); i++) {
            dinoGroupFrames[dinoQuantity].sprite = buttonUnitEstegossauroUB.getSpriteInFrame();
            dinoQuantity++;
        }
        for (int i = 0; i < buttonUnitApatossauroUB.getQuantityOnGroup(); i++) {
            dinoGroupFrames[dinoQuantity].sprite = buttonUnitApatossauroUB.getSpriteInFrame();
            dinoQuantity++;
        }
        for (int i = 0; i < buttonUnitVelociraptorUB.getQuantityOnGroup(); i++) {
            dinoGroupFrames[dinoQuantity].sprite = buttonUnitVelociraptorUB.getSpriteInFrame();
            dinoQuantity++;
        }
        for (int i = 0; i < buttonUnitPterodactiloUB.getQuantityOnGroup(); i++) {
            dinoGroupFrames[dinoQuantity].sprite = buttonUnitPterodactiloUB.getSpriteInFrame();
            dinoQuantity++;
        }

        // Preenche o que sobrou de espaço vazios no grupo com nenhuma imagem
        for (int i = dinoQuantity; i<4; i++) {
            dinoGroupFrames[i].sprite = dinoGroupFreeSlotSprite;
        }
    }

    void changeTooltipText(string message, int fontSize)
    {
        lastMessageString = message;
        lastMessageFontSize = fontSize;
        if (!isMessaging) {
            informationText.fontSize = fontSize;
            informationText.text = message;
        }
    }

    void displayMessage (string message, float duration, int fontSize) {
        // Blocks the HUD from showing tooltips
        isMessaging = true;

        // Erase the message that is being displayed
        if (lastMessageCoroutine != null) 
            StopCoroutine(lastMessageCoroutine);

        // Display the new message
        informationText.fontSize = fontSize;
        lastMessageCoroutine = StartCoroutine(routine: displayMessageCoroutine(message, duration));
    }

    IEnumerator displayMessageCoroutine(string message, float duration)
    {
        informationText.text = message;
        yield return new WaitForSeconds(duration);
        isMessaging = false;
        informationText.text = lastMessageString;
        informationText.fontSize = lastMessageFontSize;
    }

    void changeTooltipLojaText(string message, int fontSize) {
        lastMessageStringLoja = message;
        lastMessageFontSizeLoja = fontSize;
        if (!isMessagingLoja) {
            tooltipLojaText.fontSize = fontSize;
            tooltipLojaText.text = message;
        }
    }

    void displayMessageLoja(string message, float duration, int fontSize)
    {
        // Blocks the HUD from showing tooltips
        isMessaging = true;

        // Erase the message that is being displayed
        if (lastMessageLojaCoroutine != null)
            StopCoroutine(lastMessageCoroutine);

        // Display the new message
        tooltipLojaText.fontSize = fontSize;
        lastMessageLojaCoroutine = StartCoroutine(routine: displayMessageLojaCoroutine(message, duration));
    }

    IEnumerator displayMessageLojaCoroutine(string message, float duration)
    {
        tooltipLojaText.text = message;
        yield return new WaitForSeconds(duration);
        isMessaging = false;
        tooltipLojaText.text = lastMessageStringLoja;
        tooltipLojaText.fontSize = lastMessageFontSizeLoja;
    }

    private bool getAxisRightDown () {
        float horiP1 = Input.GetAxis("HorizontalP1");
        float horiP2 = Input.GetAxis("HorizontalP2");
        
        if ((pressingRight == false) && ((horiP1 > 0 && player == 1) || (horiP2 > 0 && player == 2)))
        {
            pressingRight = true;
            return true;
        }
        else if ((pressingRight == true) && ((horiP1 == 0 && player == 1) || (horiP2 == 0 && player == 2)))
        {
            pressingRight = false;
            return false;
        }
        else return false;
    }

    private bool getAxisLeftDown()
    {
        float horiP1 = Input.GetAxis("HorizontalP1");
        float horiP2 = Input.GetAxis("HorizontalP2");

        if ((pressingLeft == false) && ((horiP1 < 0 && player == 1) || (horiP2 < 0 && player == 2)))
        {
            pressingLeft = true;
            return true;
        }
        else if ((pressingLeft == true) && ((horiP1 == 0 && player == 1) || (horiP2 == 0 && player == 2)))
        {
            pressingLeft = false;
            return false;
        }
        else return false;
    }

    private bool getAxisUpDown()
    {
        float horiP1 = Input.GetAxis("VerticalP1");
        float horiP2 = Input.GetAxis("VerticalP2");

        if ((pressingUp == false) && ((horiP1 > 0 && player == 1) || (horiP2 > 0 && player == 2)))
        {
            pressingUp = true;
            return true;
        }
        else if ((pressingUp == true) && ((horiP1 == 0 && player == 1) || (horiP2 == 0 && player == 2)))
        {
            pressingUp = false;
            return false;
        }
        else return false;
    }

    private bool getAxisDownDown()
    {
        float horiP1 = Input.GetAxis("VerticalP1");
        float horiP2 = Input.GetAxis("VerticalP2");

        if ((pressingDown == false) && ((horiP1 < 0 && player == 1) || (horiP2 < 0 && player == 2)))
        {
            pressingDown = true;
            return true;
        }
        else if ((pressingDown == true) && ((horiP1 == 0 && player == 1) || (horiP2 == 0 && player == 2)))
        {
            pressingDown = false;
            return false;
        }
        else return false;
    }

    private bool getAxisUpUp()
    {
        float horiP1 = Input.GetAxis("VerticalP1");
        float horiP2 = Input.GetAxis("VerticalP2");

        if ((pressingUp2 == false) && ((horiP1 > 0 && player == 1) || (horiP2 > 0 && player == 2)))
        {
            pressingUp2 = true;
            return false;
        }
        else if ((pressingUp2 == true) && ((horiP1 == 0 && player == 1) || (horiP2 == 0 && player == 2)))
        {
            pressingUp2 = false;
            return true;
        }
        else return false;
    }

    private bool getAxisDownUp()
    {
        float horiP1 = Input.GetAxis("VerticalP1");
        float horiP2 = Input.GetAxis("VerticalP2");

        if ((pressingDown2 == false) && ((horiP1 < 0 && player == 1) || (horiP2 < 0 && player == 2)))
        {
            pressingDown2 = true;
            return false;
        }
        else if ((pressingDown2 == true) && ((horiP1 == 0 && player == 1) || (horiP2 == 0 && player == 2)))
        {
            pressingDown2 = false;
            return true;
        }
        else return false;
    }

    private bool getAxisRightUp()
    {
        float horiP1 = Input.GetAxis("HorizontalP1");
        float horiP2 = Input.GetAxis("HorizontalP2");

        if ((pressingRight2 == false) && ((horiP1 > 0 && player == 1) || (horiP2 > 0 && player == 2)))
        {
            pressingRight2 = true;
            return false;
        }
        else if ((pressingRight2 == true) && ((horiP1 == 0 && player == 1) || (horiP2 == 0 && player == 2)))
        {
            pressingRight2 = false;
            return true;
        }
        else return false;
    }

    private bool getAxisLeftUp()
    {
        float horiP1 = Input.GetAxis("HorizontalP1");
        float horiP2 = Input.GetAxis("HorizontalP2");

        if ((pressingLeft2 == false) && ((horiP1 < 0 && player == 1) || (horiP2 < 0 && player == 2)))
        {
            pressingLeft2 = true;
            return false;
        }
        else if ((pressingLeft2 == true) && ((horiP1 == 0 && player == 1) || (horiP2 == 0 && player == 2)))
        {
            pressingLeft2 = false;
            return true;
        }
        else return false;
    }

    private void updateUpgradeIcons () {
        bool[] attrs = selectedButton.GetComponent<WDHudUpgradeButton>().attributesInMaxLevel;

        if (!attrs[(int)Attributes.ATK])
            buttonUpgradePAtaque.GetComponentsInChildren<Image>()[1].sprite = buttonUpgradePAtaque.GetComponent<WDHudAttributeButton>().SpriteAttribute;
        else
            buttonUpgradePAtaque.GetComponentsInChildren<Image>()[1].sprite = maxLevelSprite;

        if (!attrs[(int)Attributes.HAB])
            { }
        else
            buttonUpgradeHabilidade.GetComponentsInChildren<Image>()[1].sprite = maxLevelSprite;

        if (!attrs[(int)Attributes.VEL_ATK])
            buttonUpgradeVelAtaque.GetComponentsInChildren<Image>()[1].sprite = buttonUpgradeVelAtaque.GetComponent<WDHudAttributeButton>().SpriteAttribute;
        else
            buttonUpgradeVelAtaque.GetComponentsInChildren<Image>()[1].sprite = maxLevelSprite;

        if (!attrs[(int)Attributes.VEL_DES])
            buttonUpgradeVelDeslocamento.GetComponentsInChildren<Image>()[1].sprite = buttonUpgradeVelDeslocamento.GetComponent<WDHudAttributeButton>().SpriteAttribute;
        else
            buttonUpgradeVelDeslocamento.GetComponentsInChildren<Image>()[1].sprite = maxLevelSprite;

        if (!attrs[(int)Attributes.VIDA])
            buttonUpgradeVida.GetComponentsInChildren<Image>()[1].sprite = buttonUpgradeVida.GetComponent<WDHudAttributeButton>().SpriteAttribute;
        else
            buttonUpgradeVida.GetComponentsInChildren<Image>()[1].sprite = maxLevelSprite;
    }
}