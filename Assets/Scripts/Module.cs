using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Module : MonoBehaviour {

    public float maxHealth;
    private float currentHealth;

    protected Rigidbody2D rigidBody;
    protected Animator animator;

    protected bool isAttached = false;

    protected virtual void OnConnect() { }
    protected virtual void OnDiscconnect() { }
    public virtual void Activate() {
        animator.SetTrigger("Activate");
    }

    protected virtual void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<Module>() != null) {
            Attach(collision);
        }
    }

    private void Attach(Collision2D collision) {
        if (isAttached || !collision.gameObject.GetComponent<Module>().IsAttached()) return;

        FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
        joint.connectedBody = collision.rigidbody;
        joint.anchor = transform.InverseTransformPoint(collision.GetContact(0).point);
        joint.connectedAnchor = transform.InverseTransformPoint(collision.GetContact(0).point);
        joint.autoConfigureConnectedAnchor = false;

        Rigidbody2D coreBody = GameObject.FindGameObjectsWithTag("Core")[0].GetComponent<Rigidbody2D>();
        Vector2 newCoM = rigidBody.transform.InverseTransformDirection(coreBody.position - rigidBody.position);
        rigidBody.centerOfMass = newCoM;
        rigidBody.inertia = 1;

        transform.parent = collision.transform;
        rigidBody.bodyType = RigidbodyType2D.Dynamic;
        isAttached = true;
        OnConnect();
    }

    public void Damage(float damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            Break();
        }
        animator.SetTrigger("Hurt");
    }

    public void Break () {
        foreach (Module child in GetComponentsInChildren<Module>()) {
            if (child != this) {
                child.Break();
            }
        }
        OnDiscconnect();
        animator.SetTrigger("Die");
    }

    public void DestroySelf() {
        Destroy(gameObject);
    }

    public bool IsAttached() {
        return isAttached;
    }

}
