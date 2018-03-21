using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_MouthController : MonoBehaviour
{


	// 0 is right, increase to 3, which is up, counting clockwise
	public static int direction = 0;

	public float moveDistance = 2f;

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.W)) {
		
			direction = 3;
			transform.rotation = Quaternion.Euler (0, 0, 90);
		
		}
		if (Input.GetKeyDown (KeyCode.A)) {

			direction = 2;
			transform.rotation = Quaternion.Euler (0, 0, 180);

		}
		if (Input.GetKeyDown (KeyCode.S)) {

			direction = 1;
			transform.rotation = Quaternion.Euler (0, 0, 270);

		}
		if (Input.GetKeyDown (KeyCode.D)) {

			direction = 0;
			transform.rotation = Quaternion.Euler (0, 0, 0);

		}
	
	
	
	}

	void Tick ()
	{
		
		if (direction == 0) {
		
			transform.position = transform.position + new Vector3 (moveDistance, 0, 0);
		
		}

		if (direction == 1) {

			transform.position = transform.position + new Vector3 (0, -1 * moveDistance, 0);

		}

		if (direction == 2) {

			transform.position = transform.position + new Vector3 (-1 * moveDistance, 0, 0);

		}

		if (direction == 3) {

			transform.position = transform.position + new Vector3 (0, moveDistance, 0);

		}
	
	}
}
