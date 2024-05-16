using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public static Enemy Create(Vector3 position) {
        Transform pfEnemyPrefab = Resources.Load<Transform>("pfEnemy");
        Transform enemyTransform = Instantiate(pfEnemyPrefab, position, Quaternion.identity);

        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        return enemy;
    }

    private Transform targetTransform;
    private Rigidbody2D rigidbody2d;
    private HealthSystem healthSystem;

    private float lookForTargetTimer;
    private float lookForTargetTimerMax = .3f;

    private void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDied += HealthSystem_OnDied;
        // For 10 enemies if they spawn at the exact same position, they will all have (hopefully) different timers
        lookForTargetTimer = Random.Range(0f, lookForTargetTimerMax);
    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e) {
        Destroy(gameObject);
    }

    private void Update() {
        HandleMovement();
        HandleTargeting();
    }

    private void HandleMovement() {
        if (targetTransform != null) {
            Vector3 moveDir = (targetTransform.position - transform.position).normalized;

            float moveSpeed = 6f;
            rigidbody2d.velocity = moveDir * moveSpeed;
        } else {
            rigidbody2d.velocity = Vector3.zero;
        }
    }

    private void HandleTargeting() {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer <= 0f) {
            lookForTargetTimer += lookForTargetTimerMax;
            LookForTargets();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Building building = collision.gameObject.GetComponent<Building>();
        if (building != null) {
            // We have collided with a building!
            building.GetComponent<HealthSystem>().Damage(10);
            Destroy(gameObject);
        }
    }

    private void LookForTargets() {
        float targetMaxRadius = 10f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);
        foreach (Collider2D collider in collider2DArray) {
            Building building = collider.GetComponent<Building>();

            if (building != null) {
                // Is a building!
                if (targetTransform == null) {
                    targetTransform = building.transform;
                } else {
                    // We already have target, we need to check that distance with the building distance we iterate
                    if (Vector3.Distance(transform.position, targetTransform.position) >
                        Vector3.Distance(transform.position, building.transform.position)) {
                        // Closer!
                        targetTransform = building.transform;
                    }
                }
            }
        }

        if (targetTransform == null) {
            // Found no targets within range
            targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
        }
    }
}
