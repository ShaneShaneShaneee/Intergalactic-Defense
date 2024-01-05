using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeScript : MonoBehaviour
{
    [SerializeField] Color hoverColor, noMoneyColor, hasTurretColor;

    public GameObject currTurret;

    public TurretBlueprint turretBlueprint;

    [HideInInspector]
    public bool isUpgraded;

    [SerializeField] Vector3 positionOffset;

    BuildMonitor monitor;

    AudioManager audioManager;

    Renderer rend;
    Color startColor;

    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        startColor = rend.material.color;
        monitor = BuildMonitor.instance;
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!monitor.canBuild)
        {
            return;
        }

        if (monitor.hasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = noMoneyColor;
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        if(currTurret != null)
        {
            monitor.selectNode(this);
            return;
        }

        if (!monitor.canBuild)
        {
            return;
        }

        BuildTurret(monitor.getTurretToBuild());
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.money < blueprint.cost)
        {
            audioManager.Play("NoMoney");
            return;
        }

        PlayerStats.money -= blueprint.cost;

        GameObject _currTurret = (GameObject)Instantiate(blueprint.prefab, getPosition(), Quaternion.identity);
        currTurret = _currTurret;

        turretBlueprint = blueprint;
        GameObject effect = (GameObject)Instantiate(monitor.buildEffect, getPosition(), Quaternion.identity);
        audioManager.Play("Buy");
        Destroy(effect, 3f);
    }

    public void UpgradeTurret()
    {
        if(PlayerStats.money < turretBlueprint.upgradeCost)
        {
            return;
        }
        PlayerStats.money -= turretBlueprint.upgradeCost;

        Destroy(currTurret);

        GameObject _currTurret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, getPosition(), Quaternion.identity);
        currTurret = _currTurret;
        GameObject effect = (GameObject)Instantiate(monitor.buildEffect, getPosition(), Quaternion.identity);
        audioManager.Play("Buy");
        Destroy(effect, 3f);

        isUpgraded = true;
    }

    public void SellTurret()
    {
        PlayerStats.money += turretBlueprint.getSellPrice();

        GameObject effect = (GameObject)Instantiate(monitor.sellEffect, getPosition(), Quaternion.identity);
        Destroy(effect, 3f);
        audioManager.Play("Sell");
        Destroy(currTurret);
        turretBlueprint = null;
    }

    public Vector3 getPosition()
    {
        return transform.position + positionOffset;
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.tag == "Build")
        {
            rend.material.color = hasTurretColor;
        }

        else if(other.gameObject.tag == "Sell")
        {
            rend.material.color = startColor;
        }
    }
}
