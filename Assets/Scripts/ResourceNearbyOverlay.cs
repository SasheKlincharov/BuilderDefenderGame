using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceNearbyOverlay : MonoBehaviour {

    private ResourceGeneratorData resourceGeneratorData;

    private void Awake() {
        Hide();
    }
    private void Update() {
        int nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(resourceGeneratorData, transform.position);
        float percent = Mathf.RoundToInt((float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount * 100f);
        transform.Find("text").GetComponent<TextMeshPro>().SetText(percent + "%");
    }

    public void Show(ResourceGeneratorData resourceGeneratorData) {
        this.resourceGeneratorData = resourceGeneratorData;
        this.gameObject.SetActive(true);

        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
    }

    public void Hide() {
        this.gameObject.SetActive(false);
    }
}
