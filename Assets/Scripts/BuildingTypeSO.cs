﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject {

    public string nameString;
    public Transform prefab;
    public bool hasResourceGeneratorData;
    public ResourceGeneratorData resourceGeneratorData;
    public Sprite sprite;
    public float minConstructionRadius;
    public ResourceAmount[] constructionResourceCostArray;
    public int healthAmountMax;

    public string GetConstructionResourceCostString() {
        string str = "";
        foreach (ResourceAmount resourceAmount in constructionResourceCostArray) {
            str += "<color=#" + resourceAmount.resourceTypeSO.colorHex + ">" + resourceAmount.resourceTypeSO.nameShort + resourceAmount.amount + "</color> ";
        }

        return str;
    }

}
