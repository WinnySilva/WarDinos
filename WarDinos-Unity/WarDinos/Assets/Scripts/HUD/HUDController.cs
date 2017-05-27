using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public KeyCode keyUp;
    public KeyCode keyDown;
    public KeyCode keyLeft;
    public KeyCode keyRight;
    public KeyCode keyConfirm;

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

    private Button selectedButton;
    private Button lastSelectedUnitButton;
    private Button lastSelectedLaneButton;

    // Use this for initialization
    void Start()
    {
        selectedButton = buttonLane1;
        lastSelectedUnitButton = buttonUnitVelociraptor;
        lastSelectedLaneButton = buttonLane1;
        changeButton(selectedButton);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyDown))
        {
            if (selectedButton.FindSelectableOnDown())
            {
                changeButton((Button)selectedButton.FindSelectableOnDown());

                // Change the dinosaur displayed in HUD if button
                // changed to is a unit button
                if (selectedButton.GetComponent<UnitButton>()) {
                    UnitButton ub = selectedButton.GetComponent<UnitButton>();
                    imageSelectedDino.GetComponent<Image>().sprite = ub.getSpriteInFrame();
                }
            }
        }
        if (Input.GetKeyDown(keyUp))
        {
            if (selectedButton.FindSelectableOnUp())
            {
                changeButton((Button)selectedButton.FindSelectableOnUp());

                // Change the dinosaur displayed in HUD if button
                // changed to is a unit button
                if (selectedButton.GetComponent<UnitButton>())
                {
                    UnitButton ub = selectedButton.GetComponent<UnitButton>();
                    imageSelectedDino.GetComponent<Image>().sprite = ub.getSpriteInFrame();
                }
            }
        }
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
            }
        }
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
            }
        }
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
            if (selectedButton.GetComponent<UnitButton>()) {
                UnitButton ub = selectedButton.GetComponent<UnitButton>();
                string dinoname = ub.getDinosaur();
                Debug.Log("[Pressionou " + dinoname + "] Despachou grupo");

                // Reset dinosaurs quantities in groups
                buttonUnitVelociraptor.GetComponent<UnitButton>().resetQuantityOnGroup();
                buttonUnitEstegossauro.GetComponent<UnitButton>().resetQuantityOnGroup();
                buttonUnitTriceratopo.GetComponent<UnitButton>().resetQuantityOnGroup();
                buttonUnitPterodactilo.GetComponent<UnitButton>().resetQuantityOnGroup();
                buttonUnitApatossauro.GetComponent<UnitButton>().resetQuantityOnGroup();
                buttonUnitTiranossauro.GetComponent<UnitButton>().resetQuantityOnGroup();
}
            // Lane button
            else if (false) {
            }

            // Change the cursor from lane selection to unit selection
            // or the opposite analogue
            if (selectedButton.FindSelectableOnLeft())
            {
                lastSelectedLaneButton = selectedButton;
                changeButton(lastSelectedUnitButton);
            }
            else if (selectedButton.FindSelectableOnRight())
            {
                lastSelectedUnitButton = selectedButton;
                changeButton(lastSelectedLaneButton);
            }
        }
    }

    public void TaskButton3()
    {
        Debug.Log("CHEEKI BREEKI!!!");
    }

    private void changeButton (Button nextButton)
    {
        selectedButton.targetGraphic.CrossFadeColor(selectedButton.colors.normalColor, selectedButton.colors.fadeDuration, true, true);
        selectedButton = nextButton;
        selectedButton.targetGraphic.CrossFadeColor(selectedButton.colors.highlightedColor, selectedButton.colors.fadeDuration, true, true);
    }
}
