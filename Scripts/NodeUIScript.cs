using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUIScript : MonoBehaviour
{
    [SerializeField] GameObject UI;
    [SerializeField] Text upgradeCostText, sellAmountText;
    [SerializeField] Button upgradeButton;
    NodeScript target;

    public void setTarget(NodeScript node)
    {
        target = node;

        transform.position = target.getPosition();

        if(!target.isUpgraded)
        {
            upgradeCostText.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCostText.text = "Maxed";
            upgradeButton.interactable = false;
        }

        sellAmountText.text = "$" + target.turretBlueprint.getSellPrice();

        UI.SetActive(true);
    }

    public void hide()
    {
        UI.SetActive(false);
    }

    public void upgrade()
    {
        target.UpgradeTurret();
        BuildMonitor.instance.DeselectNode();
    }

    public void sell()
    {
        target.SellTurret();
        BuildMonitor.instance.DeselectNode();
    }
}
