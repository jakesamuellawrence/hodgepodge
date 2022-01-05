using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikey : MonoBehaviour {

    private Rigidbody2D rigidBody;

    private Vector2 currentVel;

    public float movespeed;
    public float movementSmoothTime;
    public float damage;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        Vector2 directionToCore = (GameManager.instance.core.transform.position - transform.position).normalized;
        rigidBody.velocity = Vector2.SmoothDamp(rigidBody.velocity, directionToCore * movespeed, ref currentVel, movementSmoothTime);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Module module = collision.gameObject.GetComponent<Module>();
        if (module != null) {
            module.Damage(damage);
        }
    }
}
