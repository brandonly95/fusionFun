using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickController : MonoBehaviour {


	public float TickTime = .2f;

	private float oldTime;


	// Use this for initialization
	void Start () {
		oldTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {


		if (Time.time - oldTime > .4) {
		
		
			GameObject[] gos = (GameObject[])GameObject.FindGameObjectsWithTag ("Tickable");
			foreach (GameObject go in gos) {

				Debug.Log (go.name);

				if (go && go.transform.parent == null) {
					go.gameObject.BroadcastMessage("Tick");
				}
			}

			oldTime = Time.time;
		
		}
		
	}
}
