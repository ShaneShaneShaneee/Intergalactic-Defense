using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefab, upgradedPrefab;
    public int cost, upgradeCost;

    public int getSellPrice()
    {
        return cost / 2;
    }
}
