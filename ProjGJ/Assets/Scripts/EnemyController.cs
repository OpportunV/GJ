using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public Transform[] waypoints;
    public GameObject weapon;

    private Transform targetWaypoint;
    private int targetIndex;

	void Start () {
        targetWaypoint = waypoints[1];
	}
	
	void Update () {
		
	}
}
