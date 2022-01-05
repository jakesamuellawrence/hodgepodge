using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour {

    private Rigidbody2D rigidBody;

    public float speed;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        int direction = Random.Range(0, 4);
        Vector3 initialDirection = new Vector2(1, 1).normalized;
        for (int i = 0; i < direction; i++) {
            initialDirection = Quaternion.Euler(0f, 0f, 90) * initialDirection;
        }

        rigidBody.velocity = initialDirection * speed;
        rigidBody.drag = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        CannonBall cannonBall = collision.gameObject.GetComponent<CannonBall>();
        if (cannonBall == null) {
            rigidBody.velocity = Vector3.Reflect(rigidBody.velocity, collision.contacts[0].normal);
        }
    }

}
