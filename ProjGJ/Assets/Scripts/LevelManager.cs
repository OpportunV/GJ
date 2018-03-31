using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    #region Singleton
    public static LevelManager instance;
    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    #endregion

    public Bugs bugs = new Bugs();

    public GameObject player;

	void Start () {
	}
	
}
