using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryCannon : MonoBehaviour {

    private Rigidbody2D rigidBody;
    private Animator animator;

    public GameObject cannonBallPrefab;
    public GameObject[] cannonBallSpawnPoints;
    public float InitialFireDelay;
    public float fireDelay;
    public float fireVelocity;

    private float fireCooldown;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start() {
        fireCooldown = InitialFireDelay;
    }

    private void Update() {
        // Fire
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0) {
            Fire();
            fireCooldown = fireDelay;
        }
    }

    private void Fire() {
        foreach (GameObject spawnPoint in cannonBallSpawnPoints) {
            GameObject cannonBall = Instantiate(cannonBallPrefab, spawnPoint.transform.position, Quaternion.identity);
            cannonBall.GetComponent<Rigidbody2D>().velocity = (spawnPoint.transform.position - transform.position).normalized * fireVelocity;
        }

        animator.SetTrigger("Fire");
    }

}
