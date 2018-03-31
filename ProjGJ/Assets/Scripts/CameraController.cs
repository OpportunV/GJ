using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Vector3 camOffset;
    public Transform target;
    public float smoothTime = .5f;

    private Vector3 dampVelocity;
	
	void LateUpdate () {
        transform.position = Vector3.SmoothDamp(transform.position, target.position + camOffset, ref dampVelocity, smoothTime);
	}
}
