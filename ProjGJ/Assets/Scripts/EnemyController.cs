using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public Transform[] waypoints;
    public GameObject weapon;
    public float speed = 10f;

    private Vector3 targetWaypoint;
    private int targetIndex = 0;

	void Start () {
        GetNextWaypoint();
        StartCoroutine(Patrol(Time.fixedDeltaTime));
	}
	
	IEnumerator Patrol(float delay) {
        while (true) {
            if (Mathf.Abs(transform.position.x - targetWaypoint.x) < 0.1f) {
                GetNextWaypoint();
                yield return new WaitForSeconds(5 * delay);
            }
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.fixedDeltaTime);
            yield return new WaitForSeconds(delay);
        }
    }

    void GetNextWaypoint() {
        targetIndex = (targetIndex + 1) % waypoints.Length;
        targetWaypoint = new Vector2(waypoints[targetIndex].position.x, transform.position.y);
    }
}
