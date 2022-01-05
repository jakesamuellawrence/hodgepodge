using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : Module {

    protected override void Awake() {
        base.Awake();
        isAttached = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Ladder ladder = collision.gameObject.GetComponent<Ladder>();
        if (ladder != null) {
            ladder.Descend();
        }
    }

    public override void Activate() {
    }

    protected override void OnConnect() {
    }

    protected override void OnDiscconnect() {
        Debug.Log("You lose!");
    }
}
