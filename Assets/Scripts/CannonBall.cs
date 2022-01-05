using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {

    public float damage;

    private void OnCollisionEnter2D(Collision2D collision) {
        Module module = collision.gameObject.GetComponent<Module>();
        if (module != null){
            module.Damage(damage);
        }

        Killable killable = collision.gameObject.GetComponent<Killable>();
        if (killable != null) {
            killable.Damage(damage);
        }

        Destroy(gameObject);
    }

}
