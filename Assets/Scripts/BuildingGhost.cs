using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour {

    private GameObject spriteGameObject;
    private ResourceNearbyOverlay resourceNearbyOverlay; 
    private void Awake() {
        spriteGameObject = transform.Find("sprite").gameObject;
        resourceNearbyOverlay = transform.Find("pfResourceNearbyOverlay").GetComponent<ResourceNearbyOverlay>();
        Hide();
    }

    private void Start() {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs eventArgs) {
        if (eventArgs.activeBuildingType == null) {
            Hide();
            resourceNearbyOverlay.Hide();
        } else {
            Show(eventArgs.activeBuildingType.sprite);
            if (eventArgs.activeBuildingType.hasResourceGeneratorData) {
                resourceNearbyOverlay.Show(eventArgs.activeBuildingType.resourceGeneratorData);
            } else {
                resourceNearbyOverlay.Hide();
            }
        }
    }

    private void Update() {
        transform.position = UtilsClass.GetMouseWorldPosition();
    }


    private void Show(Sprite ghostSprite) {
        spriteGameObject.SetActive(true);
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }

    private void Hide() {
        spriteGameObject.SetActive(false);
    }
}
