using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour {
    [SerializeField] private ResourceGenerator resourceGenerator;
    private Transform barTransform;

    private void Start() {
        barTransform = transform.Find("bar");

        ResourceGeneratorData resourceGeneratorData = resourceGenerator.GetResourceGeneratorData();
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
        transform.Find("text").GetComponent<TextMeshPro>().SetText(resourceGenerator.GetAmountGeneratedPerSecond().ToString("F1"));
    }

    private void Update() {
        barTransform.localScale = new Vector3(1 - resourceGenerator.GetTimerNormalized(), 1, 1);
    }
}
