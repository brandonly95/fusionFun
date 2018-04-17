using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


	public GameObject snakeMouth;

	private Vector3 offset;

	private float DEF_CAMERA_SIZE;

	public float speed;

	public float maxZoom = 20;

	public float currentZoom;

	public float ZOOM_PERCENT = .3f;

	public bool zoomSequence = false;

	public float ZOOM_TOTAL_TICKS = 90;

	public float zoomTicks = 0;

	public AnimationCurve speedCurve;

	public AnimationCurve zoomCurve;

	// Use this for initialization
	void Start () {

		offset = this.transform.position;

		DEF_CAMERA_SIZE = this.GetComponent<Camera> ().orthographicSize;

		currentZoom = DEF_CAMERA_SIZE;


	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Vector3 finalPos = snakeMouth.transform.position + offset;

		//Debug.Log (speedCurve.Evaluate (Mathf.Abs(Vector2.Distance (finalPos, this.transform.position))));

		this.transform.position = Vector3.Slerp (this.transform.position, finalPos, speedCurve.Evaluate (Mathf.Abs(Vector2.Distance (finalPos, this.transform.position))));


		if (!zoomSequence && Vector2.Distance(finalPos, this.transform.position) > 30) {
			zoomTicks = 0;
			zoomSequence = true;
		}


		if (zoomSequence) {

			if (zoomTicks / ZOOM_TOTAL_TICKS > .98) {
			
				this.GetComponent<Camera> ().orthographicSize = DEF_CAMERA_SIZE;
				zoomSequence = false;
			
			} else {
				
				//Debug.Log ((zoomTicks / ZOOM_TOTAL_TICKS));
			
				currentZoom = (maxZoom - DEF_CAMERA_SIZE) * zoomCurve.Evaluate(zoomTicks/ZOOM_TOTAL_TICKS);


				this.GetComponent<Camera> ().orthographicSize = currentZoom + DEF_CAMERA_SIZE;
			}

			zoomTicks++;
		}

	}
}
