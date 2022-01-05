using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overclock : Module {

    public float multiplier = 0.5f;

    protected override void OnConnect() {
        base.OnConnect();
        GameManager.instance.core.GetComponent<PlayerController>().fireDelay *= multiplier;
    }

    protected override void OnDiscconnect() {
        base.OnDiscconnect();
        GameManager.instance.core.GetComponent<PlayerController>().fireDelay /= multiplier;
    }

}
