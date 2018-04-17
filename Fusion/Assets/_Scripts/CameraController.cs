using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


	public GameObject snakeMouth;

	private Vector3 offset;

	private float cameraSize;

	public float speed;

	// Use this for initialization
	void Start () {

		offset = this.transform.position;

		cameraSize = this.GetComponent<Camera> ().orthographicSize;


	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Vector3 finalPos = snakeMouth.transform.position + offset;


		this.transform.position = Vector3.Slerp(this.transform.position,finalPos,speed);

		this.GetComponent<Camera> ().orthographicSize = cameraSize + .002f * Mathf.Pow(Vector2.Distance (this.transform.position, finalPos),2f);

	}
}
