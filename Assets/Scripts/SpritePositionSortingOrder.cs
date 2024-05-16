using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePositionSortingOrder : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    [SerializeField] private bool runOnce;
    [SerializeField] private float positionOffsetY;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    private void LateUpdate() {
        float precisionMultiplier = 5f;
        spriteRenderer.sortingOrder = (int) (-(transform.position.y + positionOffsetY) * precisionMultiplier);

        if (runOnce) {
            Destroy(this);
        }
    }
}
