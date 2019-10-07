using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerGUIManager : MonoBehaviour
{
    Vector3 towerGUIOffset = new Vector3(0.7f, 0.55f, 0.0f);

    TowerController currentTowerController = null;
    UpgradeTree currentUpgradeTree = null;
    TowerStats currentTowerStats = null;
    string currentTowerName = null;
    bool towerSelected = false;

    public Canvas gameOverlayCanvas = null;
    public GameObject towerGUIHolder = null;
    public EventSystem eventSystem = null;
    GameObject background = null;
    GameObject upgradesMenu = null;
    GameObject infoMenu = null;

    Button firstButton = null;
    Button closestButton = null;
    Button lastButton = null;

    bool firstIsPressed = false;
    bool closestIsPressed = false;
    bool lastIsPressed = false;

    private void Start()
    {
        background = towerGUIHolder.transform.Find("Background").gameObject;
        upgradesMenu = towerGUIHolder.transform.Find("UpgradesMenu").gameObject;
        infoMenu = towerGUIHolder.transform.Find("InfoMenu").gameObject;
        Transform targetingTypeObj = infoMenu.transform.Find("TargetingType");
        firstButton = targetingTypeObj.Find("First").GetComponent<Button>();
        closestButton = targetingTypeObj.Find("Closest").GetComponent<Button>();
        lastButton = targetingTypeObj.Find("Last").GetComponent<Button>();
    }

    private void Update()
    {
        if(towerSelected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Input.mousePosition;
                UnityEngine.EventSystems.PointerEventData ped = new UnityEngine.EventSystems.PointerEventData(eventSystem);
                ped.position = mousePos;
                List<RaycastResult> resultList = new List<RaycastResult>();
                gameOverlayCanvas.GetComponent<GraphicRaycaster>().Raycast(ped, resultList);
                if(resultList.Count == 0 && !currentTowerController.isHovered)
                {
                    towerSelected = false;
                    currentTowerController.selected = false;
                    towerGUIHolder.SetActive(false);
                    upgradesMenu.SetActive(false);
                    infoMenu.SetActive(false);
                    background.SetActive(false);
                }
            }
        }
    }

    public void PopulateMenus(UpgradeTree upgradeTree, TowerStats towerStats)
    {
        currentUpgradeTree = upgradeTree;
        currentTowerStats = towerStats;
        TowerUpgrade upgrade1 = upgradeTree.GetNextInLine(1);
        TowerUpgrade upgrade2 = upgradeTree.GetNextInLine(2);
        //Upgrade Menu
        Image image1 = upgradesMenu.transform.Find("Upgrade1").GetComponent<Image>();
        image1.sprite = upgrade1.upgradeSprite;
        Image image2 = upgradesMenu.transform.Find("Upgrade2").GetComponent<Image>();
        image2.sprite = upgrade2.upgradeSprite;
        Text headerText = upgradesMenu.transform.Find("Header").GetComponent<Text>();
        headerText.text = upgrade1.upgradeName;
        Text descriptionText = upgradesMenu.transform.Find("Description").GetComponent<Text>();
        descriptionText.text = upgrade1.upgradeDescription;

        //Info Menu
        PopulateInfo();

        bool hasTargetingType = towerStats.targetingType != ETargetingType.None;
        Transform targetingTypeObj = infoMenu.transform.Find("TargetingType");
        if (hasTargetingType)
        {
            targetingTypeObj.gameObject.SetActive(true);
            if (firstIsPressed)
                firstButton.onClick.Invoke();
            if (closestIsPressed)
                closestButton.onClick.Invoke();
            if (lastIsPressed)
                lastButton.onClick.Invoke();
            switch (towerStats.targetingType)
            {
                case ETargetingType.First:
                    firstButton.onClick.Invoke();
                    break;
                case ETargetingType.Closest:
                    closestButton.onClick.Invoke();
                    break;
                case ETargetingType.Last:
                    lastButton.onClick.Invoke();
                    break;
            }
        }
        else
            targetingTypeObj.gameObject.SetActive(false);
    }

    private void PopulateInfo()
    {
        Text damageText = infoMenu.transform.Find("Damage").Find("DamageVal").GetComponent<Text>();
        damageText.text = currentTowerStats.damage.ToString();
        Text attackRangeText = infoMenu.transform.Find("AttackRange").Find("AttackRangeVal").GetComponent<Text>();
        attackRangeText.text = currentTowerStats.attackRange.ToString();
        Text attackSpeedText = infoMenu.transform.Find("AttackSpeed").Find("AttackSpeedVal").GetComponent<Text>();
        attackSpeedText.text = currentTowerStats.attackSpeed.ToString();
    }

    internal void TowerClicked(TowerController towerController, UpgradeTree upgradeTree, TowerStats towerStats, Vector3 position)
    {
        towerSelected = true;
        towerGUIHolder.transform.position = Camera.main.WorldToScreenPoint(position + towerGUIOffset);
        towerGUIHolder.SetActive(true);

        PopulateMenus(upgradeTree, towerStats);

        currentTowerController = towerController;
        currentTowerController.selected = true;
    }

    public void Upgrade1Button_OnMouseEnter()
    {
        TowerUpgrade upgrade = currentUpgradeTree.GetNextInLine(1);
        Text headerText = upgradesMenu.transform.Find("Header").GetComponent<Text>();
        headerText.text = upgrade.upgradeName;
        Text descriptionText = upgradesMenu.transform.Find("Description").GetComponent<Text>();
        descriptionText.text = upgrade.upgradeDescription;
    }
    public void Upgrade2Button_OnMouseEnter()
    {
        TowerUpgrade upgrade = currentUpgradeTree.GetNextInLine(2);
        Text headerText = upgradesMenu.transform.Find("Header").GetComponent<Text>();
        headerText.text = upgrade.upgradeName;
        Text descriptionText = upgradesMenu.transform.Find("Description").GetComponent<Text>();
        descriptionText.text = upgrade.upgradeDescription;
    }
    public void Upgrade1Button_OnClick()
    {
        currentTowerController.Upgrade(1);
        PopulateMenus(currentUpgradeTree, currentTowerStats);
    }
    public void Upgrade2Button_OnClick()
    {
        currentTowerController.Upgrade(2);
        PopulateInfo();
        PopulateMenus(currentUpgradeTree, currentTowerStats);
    }

    public void UpgradesButton_OnClick()
    {
        if (upgradesMenu.activeInHierarchy)
        {
            upgradesMenu.SetActive(false);
            background.SetActive(false);
        }
        else if (infoMenu.activeInHierarchy)
        {
            infoMenu.SetActive(false);
            upgradesMenu.SetActive(true);
        }
        else
        {
            upgradesMenu.SetActive(true);
            if (!background.activeInHierarchy)
                background.SetActive(true);
        }
    }
    public void InfoButton_OnClick()
    {
        if (infoMenu.activeInHierarchy)
        {
            infoMenu.SetActive(false);
            background.SetActive(false);
        }
        else if (upgradesMenu.activeInHierarchy)
        {
            upgradesMenu.SetActive(false);
            infoMenu.SetActive(true);
        }
        else
        {
            infoMenu.SetActive(true);
            if (!background.activeInHierarchy)
                background.SetActive(true);
        }
    }
    public void SellButton_OnClick()
    {

    }

    Color pressedColor = new Color(0.2f, 0.2f, 0.2f, 0.0f);
    public void FirstButton_OnClick()
    {
        if (firstIsPressed)
            firstButton.GetComponent<Image>().color = firstButton.colors.normalColor;
        else
            firstButton.GetComponent<Image>().color = firstButton.colors.normalColor - pressedColor;

        firstIsPressed = !firstIsPressed;
        if (closestIsPressed)
        {
            closestIsPressed = false;
            closestButton.GetComponent<Image>().color = Color.white;
        }
        if(lastIsPressed)
        {
            lastIsPressed = false;
            lastButton.GetComponent<Image>().color = Color.white;
        }
    }
    public void ClosestButton_OnClick()
    {
        if (closestIsPressed)
            closestButton.GetComponent<Image>().color = closestButton.colors.normalColor;
        else
            closestButton.GetComponent<Image>().color = closestButton.colors.normalColor - pressedColor;

        closestIsPressed = !closestIsPressed;
        if (firstIsPressed)
        {
            firstIsPressed = false;
            firstButton.GetComponent<Image>().color = Color.white;
        }
        if (lastIsPressed)
        {
            lastIsPressed = false;
            lastButton.GetComponent<Image>().color = Color.white;
        }
    }
    public void LastButton_OnClick()
    {
        if (lastIsPressed)
            lastButton.GetComponent<Image>().color = lastButton.colors.normalColor;
        else
            lastButton.GetComponent<Image>().color = lastButton.colors.normalColor - pressedColor;

        lastIsPressed = !lastIsPressed;
        if (firstIsPressed)
        {
            firstIsPressed = false;
            firstButton.GetComponent<Image>().color = Color.white;
        }
        if (closestIsPressed)
        {
            closestIsPressed = false;
            closestButton.GetComponent<Image>().color = Color.white;
        }
    }
}
