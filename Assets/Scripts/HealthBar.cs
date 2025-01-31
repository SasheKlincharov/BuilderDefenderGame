using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    [SerializeField] private HealthSystem healthSystem;

    private Transform barTransform;

    private void Awake() {
        barTransform = transform.Find("bar");
        UpdateBar();
    }

    private void Start() {
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        UpdateHealthBarVisible();
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e) {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void UpdateBar() {
        barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized(), 1, 1);
    }

    public void UpdateHealthBarVisible() {
        if (healthSystem.IsFullHealth()) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
        }
    }
}
