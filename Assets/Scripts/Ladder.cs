using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ladder : MonoBehaviour {

    public float openingDistance;

    private bool open = false;

    public void Descend() {
        if (open)
            GameManager.instance.LoadNextRoom();
    }

    public void Update() {
        float distanceToCore = (GameManager.instance.core.transform.position - transform.position).magnitude;
        if (distanceToCore >= openingDistance) open = true;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, openingDistance);
    }

}
