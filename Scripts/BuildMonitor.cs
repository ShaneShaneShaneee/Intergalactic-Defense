using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BuildMonitor : MonoBehaviour
{
    TurretBlueprint turretToBuild;
    NodeScript selectedNode;

    public static BuildMonitor instance;
    [SerializeField] NodeUIScript NodeUI;

    [SerializeField] public GameObject buildEffect, sellEffect;

    private void Awake()
    {
        instance = this;
    }

    public bool canBuild { get { return turretToBuild != null; } }

    public bool hasMoney { get { return PlayerStats.money >= turretToBuild.cost; } }

    public void selectTurretToBuild(TurretBlueprint Turret)
    {
        turretToBuild = Turret;
        DeselectNode();
    }

    public void selectNode(NodeScript node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;
        NodeUI.setTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        NodeUI.hide();
    }

    public TurretBlueprint getTurretToBuild()
    {
        return turretToBuild;
    }
}
