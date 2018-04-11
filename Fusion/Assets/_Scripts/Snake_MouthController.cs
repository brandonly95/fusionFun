using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_MouthController : MonoBehaviour
{

	public GameObject snakeBody;

	public int SNAKE_DISTANCE;


	public class snakeSegment
	{


		public GameObject GO;

		public void update (Vector2 newPosition)
		{
		
			GO.transform.position = newPosition;
		
		}

	}

	void addBody ()
	{

		snakeSegment newSegment;

		newSegment.GO = Instantiate (newSegment);

		segmentList.Add (newSegment);

	}

	void updateBody() {
	
		int counter = 1;



		foreach (snakeSegment segment in segmentList) {

			segment.update (posList[SNAKE_DISTANCE] * counter);
		
			counter++;
		}
	
	
	}

	public float moveSpeed = .1f;

	// 0 is right, increase to 3, which is up, counting clockwise
	public static int direction = 0;

	private int length = 0;

	public List<snakeSegment> segmentList;

	public List<Vector2> posList;


	Rigidbody2D localRB;

	void Start ()
	{
	
		localRB = this.GetComponent<Rigidbody2D> ();
	
	}




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

	void FixedUpdate ()
	{
		
		if (direction == 0) {
		
			localRB.velocity = new Vector2 (moveSpeed, 0);
		
		}

		if (direction == 1) {

			localRB.velocity = new Vector2 (0, -1 * moveSpeed);

		}

		if (direction == 2) {

			localRB.velocity = new Vector2 (-1 * moveSpeed, 0);

		}

		if (direction == 3) {

			localRB.velocity = new Vector2 (0, moveSpeed);

		}

		posList.Add (this.transform.position);


		if (posList.Count > length * 5) {
			
			posList.Remove (posList.FindLast);
		
		}

		updateBody ();
	
	}


	void OnTriggerEnter (Collider trigger)
	{
	
		if (trigger.gameObject.tag == "Food") {
		
			trigger.gameObject.SetActive (false);

			length++;

			if (length % 2 == 0) {
			
				addBody ();
			
			}

		}
	
	}
}
