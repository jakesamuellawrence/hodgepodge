using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

    public float movespeed;
    public float movementSmoothTime;
    public float rotationSpeed;
    public float rotationSmoothTime;
    public float fireDelay;

    private Vector2 movingDirection;
    private Vector2 currentVel;
    private float rotating;
    private float currentAngularVel;
    private bool isFiring;
    private float fireCooldown;

    private void OnMove(InputValue value) {
        movingDirection = value.Get<Vector2>();
    }

    private void OnRotate(InputValue value) {
        rotating = -value.Get<float>();
        if (rotating != 0) {
            GetComponent<Rigidbody2D>().freezeRotation = false;
        } else {
            GetComponent<Rigidbody2D>().freezeRotation = true;
        }
    }

    private void OnFire(InputValue value) {
        isFiring = value.Get<float>() != 0;
    }

    private void FixedUpdate() {
        // Move
        Vector2 targetVel = movingDirection * movespeed;
        Rigidbody2D[] rigidBodies = GetComponentsInChildren<Rigidbody2D>();
        foreach (Rigidbody2D rigidBody in rigidBodies) {
            rigidBody.velocity = Vector2.SmoothDamp(rigidBody.velocity, targetVel, ref currentVel, movementSmoothTime);
        }

        // Rotate
        foreach(Rigidbody2D rigidBody in rigidBodies) {
            float targetAngularVel = rotating * rotationSpeed;
            rigidBody.angularVelocity = Mathf.SmoothDamp(rigidBody.angularVelocity, targetAngularVel, ref currentAngularVel, rotationSmoothTime);
        }
    }

    private void Update() {
        // Fire
        fireCooldown -= Time.deltaTime;
        if (isFiring) {
            if (fireCooldown <= 0) {
                foreach (Module module in GetComponentsInChildren<Module>()) {
                    module.Activate();
                }
                fireCooldown = fireDelay;
            }
        }
    }
}
