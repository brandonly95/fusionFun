using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickController : MonoBehaviour {


	public float TickTime = .2;

	private float oldTime;


	// Use this for initialization
	void Start () {
		oldTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {


		if (Time.time - oldTime > .2) {
		
		
			GameObject[] gos = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
			foreach (GameObject go in gos) {
				if (go && go.transform.parent == null) {
					go.gameObject.BroadcastMessage("Tick");
				}
			}

			oldTime = Time.time;
		
		}
		
	}

	IEnumerator ticker () {
	




	
	}
}
