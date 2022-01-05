using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    private Rigidbody2D rigidBody;

    public float rotationSpeed;

    private bool clockwise;

    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        clockwise = Random.value > 0.5f;
    }

    private void Update() {
        if (clockwise)
            rigidBody.MoveRotation(rigidBody.rotation + rotationSpeed);
        else {
            rigidBody.MoveRotation(rigidBody.rotation - rotationSpeed);
        }
    }

}
