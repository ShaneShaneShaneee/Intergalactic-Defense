using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ShopMonitor : MonoBehaviour
{
    BuildMonitor monitor;
    public TurretBlueprint[] Turrets;
    [SerializeField] Text costText;

    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        monitor = BuildMonitor.instance;
    }
    public void selectStandardTurret()
    {
        monitor.selectTurretToBuild(Turrets[0]);
        audioManager.Play("Click");
    }

    public void selectMissileTurret()
    {
        monitor.selectTurretToBuild(Turrets[1]);
        audioManager.Play("Click");
    }

    public void selectLaserTurret()
    {
        monitor.selectTurretToBuild(Turrets[2]);
        audioManager.Play("Click");
    }

    public void selectPoisonTurret()
    {
        monitor.selectTurretToBuild(Turrets[3]);
        audioManager.Play("Click");
    }

    void Update()
    {

    }
}
