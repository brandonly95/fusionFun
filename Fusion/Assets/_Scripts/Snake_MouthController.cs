using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_MouthController : MonoBehaviour
{

	public GameObject snakeBody;

	public int SNAKE_DISTANCE;

	public List<snakeSegment> segmentList;


	public float moveSpeed = .1f;

	public float minimumMovement = .259f;

	// 0 is right, increase to 3, which is up, counting clockwise
	public static int direction = 0;

	private int food = 0;



	public List<Vector2> posList;


	Rigidbody2D localRB;

	void Start ()
	{
	
		localRB = this.GetComponent<Rigidbody2D> ();

		segmentList = new List<snakeSegment> ();
	
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

		if (posList.Count == 0 || Mathf.Abs(((Vector2)this.transform.position - posList [0]).magnitude) > minimumMovement)
			posList.Insert (0,this.transform.position);


		if (posList.Count > SNAKE_DISTANCE * (segmentList.Count + 1) + 1) {

			posList.RemoveAt (SNAKE_DISTANCE * (segmentList.Count + 1));

		}


		updateBody ();

	
	}


	void OnTriggerEnter2D (Collider2D trigger)
	{


	
		if (trigger.gameObject.tag == "Food") {

			//Debug.Log ("Ball eaten");

			trigger.gameObject.SetActive (false);

			food++;

			if (food % 2 == 0) {
			
				addBody ();
			
			}

		}
	
	}


	public class snakeSegment
	{


		public GameObject GO;

		public void updatePos (Vector2 newPosition)
		{

			GO.transform.position = newPosition;

		}

	}

	void addBody ()
	{
		//Debug.Log ("Segment added");

		snakeSegment newSegment = new snakeSegment ();

		newSegment.GO = Instantiate (snakeBody);

		newSegment.updatePos (posList[(SNAKE_DISTANCE * segmentList.Count)]);

		newSegment.GO.SetActive (true);

		segmentList.Add (newSegment);

	}

	void updateBody() {

		int counter = 1;

		if (segmentList == null) {

			return;

		}

		foreach (snakeSegment segment in segmentList) {

			//Debug.Log ("Clone " + counter.ToString() + " accessing position: " + (SNAKE_DISTANCE * counter).ToString() + " of: " + (posList.Count -1).ToString ());

			//Debug.Log (posList [SNAKE_DISTANCE * counter -1]);

			segment.updatePos (posList[SNAKE_DISTANCE * counter -1]);

			counter++;
		}


	}
}
