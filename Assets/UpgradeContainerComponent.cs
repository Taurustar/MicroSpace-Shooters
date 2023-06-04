using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeContainerComponent : MonoBehaviour
{
    public TMP_Text armorLevelLabel, maxHPLabel, armorCostLabel, playerCreditsLabel;
    public Slider armorLevelSlider;

    public Button buyArmorButton, buyLaser1Button, buyLaser2Button;
    public TMP_Text laser1CostLabel, laser2CostLabel;
    public GameObject laser1, laser2;

    public int armorCurrentLevel = 0;
    public int armorNextLevel = 1;

    private void Start()
    {
        armorCurrentLevel = PersistentPlayerConfiguration.Instance.armorIndex;
        armorNextLevel = armorCurrentLevel + 1;


        armorLevelSlider.value = PersistentPlayerConfiguration.Instance.armorIndex;
        armorLevelLabel.text = "Armor Level: " + armorCurrentLevel.ToString();
        playerCreditsLabel.text = "Player Credits: $" + PersistentPlayerConfiguration.Instance.playerCredits.ToString();
        maxHPLabel.text = PersistentPlayerConfiguration.Instance.playerConfigurations[armorCurrentLevel].startingHealth.ToString() + "HP";

        
        
        if (armorNextLevel == PersistentPlayerConfiguration.Instance.playerConfigurations.Count)
        {
            armorCostLabel.text = "Max Reached";
            buyArmorButton.interactable = false;
        }
        else
        {
            armorCostLabel.text = "Cost: $" + PersistentPlayerConfiguration.Instance.playerConfigurations[armorNextLevel].creditCost.ToString();
        }

        if (PersistentPlayerConfiguration.Instance.playerWeapons.Any(x=> x == laser1))
        {
            buyLaser1Button.interactable = false;
        }
        else
        {
            laser1CostLabel.text = "Cost: $" + laser1.GetComponent<Laser>().weaponCost.ToString();

        }

        if (PersistentPlayerConfiguration.Instance.playerWeapons.Any(x => x == laser2))
        {
            buyLaser2Button.interactable = false;
        }
        else
        {
            laser2CostLabel.text = "Cost: $" + laser2.GetComponent<Laser>().weaponCost.ToString();
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }


    public void BuyArmor()
    {
        if(PersistentPlayerConfiguration.Instance.playerCredits >= PersistentPlayerConfiguration.Instance.playerConfigurations[armorNextLevel].creditCost)
        {
            if (armorNextLevel < PersistentPlayerConfiguration.Instance.playerConfigurations.Count)
            {
                armorCurrentLevel = armorNextLevel;
                armorNextLevel++;
                PersistentPlayerConfiguration.Instance.armorIndex = armorCurrentLevel;
                PersistentPlayerConfiguration.Instance.playerCredits -= PersistentPlayerConfiguration.Instance.playerConfigurations[armorCurrentLevel].creditCost;

                
                armorLevelSlider.value = PersistentPlayerConfiguration.Instance.armorIndex;
                armorLevelLabel.text = "Armor Level: " + armorCurrentLevel.ToString();
                maxHPLabel.text = PersistentPlayerConfiguration.Instance.playerConfigurations[armorCurrentLevel].startingHealth.ToString() + "HP";
                playerCreditsLabel.text = "Player Credits: $" + PersistentPlayerConfiguration.Instance.playerCredits.ToString();

                if (armorCurrentLevel < PersistentPlayerConfiguration.Instance.playerConfigurations.Count - 1)
                    armorCostLabel.text = "Cost: $" + PersistentPlayerConfiguration.Instance.playerConfigurations[armorNextLevel].creditCost.ToString();
                else
                {
                    armorCostLabel.text = "Max Reached";
                    buyArmorButton.interactable = false;
                }

            }
        }
        else
        {
            playerCreditsLabel.text = "Player Credits: $" + PersistentPlayerConfiguration.Instance.playerCredits.ToString() + " (Not Enough Credits!) ";
        }

    }

    public void BuyLaser1()
    {
        if(PersistentPlayerConfiguration.Instance.playerCredits >= laser1.GetComponent<Laser>().weaponCost)
        {
            buyLaser1Button.interactable = false;
            PersistentPlayerConfiguration.Instance.playerCredits -= laser1.GetComponent<Laser>().weaponCost;
            PersistentPlayerConfiguration.Instance.playerWeapons.Add(laser1);
            playerCreditsLabel.text = "Player Credits: $" + PersistentPlayerConfiguration.Instance.playerCredits.ToString();

        }

    }

    public void BuyLaser2()
    {
        if (PersistentPlayerConfiguration.Instance.playerCredits >= laser2.GetComponent<Laser>().weaponCost)
        {
            buyLaser2Button.interactable = false;
            PersistentPlayerConfiguration.Instance.playerCredits -= laser2.GetComponent<Laser>().weaponCost;
            PersistentPlayerConfiguration.Instance.playerWeapons.Add(laser2);
            playerCreditsLabel.text = "Player Credits: $" + PersistentPlayerConfiguration.Instance.playerCredits.ToString();

        }

    }
}
