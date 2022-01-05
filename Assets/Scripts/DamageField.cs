using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageField : MonoBehaviour {

    public float damagePerSecond;

    private void OnTriggerStay2D(Collider2D collision) {
        Killable killable = collision.gameObject.GetComponent<Killable>();
        if (killable != null) {
            killable.Damage(damagePerSecond*Time.deltaTime);
        }
    }

}
