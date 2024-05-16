using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    [SerializeField] private float shootTimerMax;
    private float shootTimer;

    private Enemy targetEnemy;
    private Vector3 projectileSpawnPosition;

    private float lookForTargetTimer;
    private float lookForTargetTimerMax = .3f;


    private void Awake() {
        projectileSpawnPosition = transform.Find("projectileSpawnPosition").position;
    }
    private void Update() {
        HandleTargeting();
        HandleShooting();
    }

    private void HandleTargeting() {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer <= 0f) {
            lookForTargetTimer += lookForTargetTimerMax;
            LookForTargets();
        }
    }

    private void HandleShooting() {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f) {
            shootTimer += lookForTargetTimerMax;

            if (targetEnemy != null) {
                ArrowProjectile.Create(projectileSpawnPosition, targetEnemy);
            }
        }

        
    }

    private void LookForTargets() {
        float targetMaxRadius = 24f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);
        foreach (Collider2D collider in collider2DArray) {
            Enemy enemy = collider.GetComponent<Enemy>();

            if (enemy != null) {
                // Is a enemy!
                if (targetEnemy == null) {
                    targetEnemy = enemy;
                } else {
                    // We already have target, we need to check that distance with the enemy distance we iterate
                    if (Vector3.Distance(transform.position, targetEnemy.transform.position) >
                        Vector3.Distance(transform.position, enemy.transform.position)) {
                        // Closer!
                        targetEnemy = enemy;
                    }
                }
            }
        }
    }
}
