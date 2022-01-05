using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SkullDoors : MonoBehaviour {

    public bool unlocked = false;

    private TilemapRenderer tilemapRenderer;
    private TilemapCollider2D tilemapCollider;

    private void Awake() {
        tilemapRenderer = GetComponent<TilemapRenderer>();
        tilemapCollider = GetComponent<TilemapCollider2D>();
    }

    private void Update() {
        if (unlocked) return;

        if (GameManager.instance.roomIsComplete()) {
            Unlock();
        }
    }

    public void Unlock() {
        unlocked = true;
        tilemapRenderer.enabled = false;
        tilemapCollider.enabled = false;
    }

}
