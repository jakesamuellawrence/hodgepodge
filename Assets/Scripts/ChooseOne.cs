using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseOne : MonoBehaviour {


    private Module moduleA;
    private Module moduleB;

    public GameObject moduleASpawn;
    public GameObject moduleBSpawn;

    public List<GameObject> moduleOptionPrefabs;

    public float radiusToSpawn;


    private void Awake() {
        if (moduleOptionPrefabs.Count < 2) {
            Debug.LogError("ChooseOne must have at least two options");
        }

        SpawnModules();
    }

    private void SpawnModules() {
        // Spawn first module
        int i = Random.Range(0, moduleOptionPrefabs.Count);
        moduleA = Instantiate(moduleOptionPrefabs[i], moduleASpawn.transform).GetComponent<Module>();
        moduleOptionPrefabs.RemoveAt(i); // ensure that the two things given are different

        // Spawn second module
        i = Random.Range(0, moduleOptionPrefabs.Count);
        moduleB = Instantiate(moduleOptionPrefabs[i], moduleBSpawn.transform).GetComponent<Module>();
    }

    private void Update() {
        // Check if an option has been taken, destroy the other if so
        if (moduleA.IsAttached()) {
            moduleB.Break();
            this.enabled = false;
        } else if (moduleB.IsAttached()) {
            moduleA.Break();
            this.enabled = false;
        }
    }

}
