using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Module {

    public GameObject cannonBallPrefab;
    public GameObject bulletSpawnPoint;
    public float fireVelocity;

    public override void Activate() {
        base.Activate();
        GameObject cannonBall = Instantiate(cannonBallPrefab, bulletSpawnPoint.transform.position, Quaternion.identity);
        cannonBall.GetComponent<Rigidbody2D>().velocity = transform.up * fireVelocity;
    }

    protected override void OnConnect() {
        Debug.Log("Connected cannon");
    }

    protected override void OnDiscconnect() {
    }
}
