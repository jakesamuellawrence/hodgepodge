using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public Core core;

    public int floorsPerWorld;
    private int floorsCompletedThisWorld = -1;
    private int currentWorld = 0;

    [SerializeField]
    private List<int> world01;
    [SerializeField]
    private List<int> world02;
    [SerializeField]
    private List<int> winWorld;

    private Killable[] enemiesInCurrentRoom;
    private CinemachineVirtualCamera cinemachineCam;
    private PlayerSpawnPoint playerSpawnPoint;

    private void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this) {
            Destroy(instance.gameObject);  // Ensures only one GameManager can ever exist
            instance = this;
        }

        SceneManager.sceneLoaded += OnRoomStart;
    }

    public bool roomIsComplete() {
        foreach (Killable enemy in enemiesInCurrentRoom) {
            if (enemy != null) {
                return false;
            }
        }
        return true;
    }

    private void OnQuit(InputValue value) {
        Debug.Log("Quitting!");
        Application.Quit(0);
    }

    //private void OnRestart(InputValue value) {
    //    if (value.Get<float>() == 1) {
    //        foreach (DontDestroyOnLoad item in FindObjectsOfType<DontDestroyOnLoad>()) {
    //            Debug.Log(item);
    //            if (!item.gameObject.Equals(gameObject)) {
    //                Debug.Log("Destroying " + item);
    //                Destroy(item.gameObject);
    //            }
    //        }
    //        SceneManager.LoadScene(0);
    //    }
    //}

    public void LoadNextRoom() {
        if (floorsCompletedThisWorld == floorsPerWorld) {
            currentWorld += 1;
            floorsCompletedThisWorld = 0;
        }

        List<int> currentWorldList;
        if (currentWorld == 0) currentWorldList = world01;
        else if (currentWorld == 1) currentWorldList = world02;
        else currentWorldList = winWorld;

        int i = Random.Range(0, currentWorldList.Count);
        SceneManager.LoadScene(currentWorldList[i]);
        currentWorldList.RemoveAt(i);
    }

    private void OnRoomStart(Scene scene, LoadSceneMode mode) {
        floorsCompletedThisWorld += 1;

        cinemachineCam = FindObjectOfType<CinemachineVirtualCamera>();
        cinemachineCam.Follow = core.transform;

        playerSpawnPoint = FindObjectOfType<PlayerSpawnPoint>();
        core.transform.position = playerSpawnPoint.transform.position;

        enemiesInCurrentRoom = FindObjectsOfType<Killable>();
    }

}
