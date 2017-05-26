using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUDController : MonoBehaviour
{
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

    private Selectable selectedButton;
    private Selectable lastSelectedUnitButton;
    private Selectable lastSelectedLaneButton;

    // Use this for initialization
    void Start()
    {
        selectedButton = buttonLane1;
        lastSelectedUnitButton = buttonUnitVelociraptor;
        lastSelectedLaneButton = buttonLane1;
        selectedButton.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (selectedButton.FindSelectableOnDown())
            {
                selectedButton = selectedButton.FindSelectableOnDown();
                selectedButton.Select();

                // Change the dinosaur displayed in HUD if button
                // changed to is a unit button
                if (selectedButton.GetComponent<UnitButton>()) {
                    UnitButton ub = selectedButton.GetComponent<UnitButton>();
                    imageSelectedDino.GetComponent<Image>().sprite = ub.getSpriteInFrame();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectedButton.FindSelectableOnUp())
            {
                selectedButton = selectedButton.FindSelectableOnUp();
                selectedButton.Select();

                // Change the dinosaur displayed in HUD if button
                // changed to is a unit button
                if (selectedButton.GetComponent<UnitButton>())
                {
                    UnitButton ub = selectedButton.GetComponent<UnitButton>();
                    imageSelectedDino.GetComponent<Image>().sprite = ub.getSpriteInFrame();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
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
        if (Input.GetKeyDown(KeyCode.RightArrow))
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
        if (Input.GetKeyDown(KeyCode.K))
        {
            Button bt = (Button)selectedButton;
            bt.onClick.Invoke();
            bt.targetGraphic.CrossFadeColor(
                bt.colors.pressedColor,
                bt.colors.fadeDuration,
                true,
                true
            );
        }
        if (Input.GetKeyUp(KeyCode.K))
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
                selectedButton = lastSelectedUnitButton;
                selectedButton.Select();
            }
            else if (selectedButton.FindSelectableOnRight())
            {
                lastSelectedUnitButton = selectedButton;
                selectedButton = lastSelectedLaneButton;
                selectedButton.Select();
            }
        }
    }

    public void TaskButton3()
    {
        Debug.Log("CHEEKI BREEKI!!!");
    }
}
