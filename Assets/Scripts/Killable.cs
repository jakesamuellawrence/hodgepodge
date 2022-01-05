using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour {

    private Animator animator;

    public float maxHealth;

    private float currentHealth;

    private void Awake() {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void Damage(float damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            Die();
        } else {
            animator.SetTrigger("Hurt");
        }
    }

    private void Die() {
        animator.SetTrigger("Die");
    }

    private void DestroySelf() {
        Destroy(gameObject);
    }

}
