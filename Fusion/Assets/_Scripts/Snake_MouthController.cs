using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snake_MouthController : MonoBehaviour
{

	public GameObject snakeBody;

	public int SNAKE_DISTANCE;

	public List<snakeSegment> segmentList;


	public float moveSpeed = .1f;

	public float minimumMovement = .259f;

	public int WAIT_TIME = 100;

	private int timeWait = 0;

	// 0 is right, increase to 3, which is up, counting clockwise
	public static int direction = 3;

	private int food = 0;

	public float PHASE_DISTANCE = 50;

	public AnimationCurve phaseCurve;

	public float PHASE_TICKS = 30;

	public float phaseTicks;

	public float phaseDistLeft;

	public float bodyOpacity = 1;

	public Text score;

	int currScore;

	public Text foodLeft;

	int currFoodLeft;

	public List<Vector2> posList;


	Rigidbody2D localRB;

	//audio
	public AudioSource audioSource;


	void Start ()
	{
		
	
		localRB = this.GetComponent<Rigidbody2D> ();

		segmentList = new List<snakeSegment> ();

		phaseDistLeft = PHASE_DISTANCE;

		phaseTicks = PHASE_TICKS;
	
		score.text = "Score: 0";

		foodLeft.text = "Food Left: 195";

		currFoodLeft = 195;

		currScore = 0;



	}

	void increaseScore(){
		currScore++;
		currFoodLeft = 195;
		updateSnakeNums ();
		food = 0;

	}


	void updateSnakeNums()
	{
		score.text = "Score: " + currScore;
		foodLeft.text = "Snake Length: " + currFoodLeft;
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.W) && direction != 1 || Input.GetKeyDown(KeyCode.UpArrow) && direction != 1) {
		
			direction = 3;
			transform.rotation = Quaternion.Euler (0, 0, 90);
		
		}
		if (Input.GetKeyDown (KeyCode.A) && direction != 0 || Input.GetKeyDown(KeyCode.LeftArrow) && direction != 0) {

			direction = 2;
			transform.rotation = Quaternion.Euler (0, 0, 180);

		}
		if (Input.GetKeyDown (KeyCode.S) && direction != 3 || Input.GetKeyDown(KeyCode.DownArrow) && direction != 3) {

			direction = 1;
			transform.rotation = Quaternion.Euler (0, 0, 270);

		}
		if (Input.GetKeyDown (KeyCode.D) && direction != 2 || Input.GetKeyDown(KeyCode.RightArrow) && direction != 2) {

			direction = 0;
			transform.rotation = Quaternion.Euler (0, 0, 0);

		}
		if (Input.GetKeyDown(KeyCode.Space) && phaseDistLeft == 0 && posList.Count != 0) {
		
			phaseDistLeft = PHASE_DISTANCE;
			phaseTicks = 0;
		
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



		if (posList.Count == 0 || Mathf.Abs (((Vector2)this.transform.position - posList [0]).magnitude) > minimumMovement) {

			posList.Insert (0, this.transform.position);
		
			if (posList.Count > 1 && phaseDistLeft > 0) {
				phaseDistLeft -= Mathf.Abs(Vector2.Distance (this.transform.position, posList [1]));
				if (phaseDistLeft < 0) {

					phaseDistLeft = 0;

				}
			}
		
		
		}


		if (posList.Count > SNAKE_DISTANCE * (segmentList.Count + 2) + 1) {

			posList.RemoveAt (SNAKE_DISTANCE * (segmentList.Count + 2));

		}

		if (timeWait > 0) {
			localRB.velocity = Vector2.zero;
			timeWait--;
		}

		updateBody ();

		updateSnakeNums ();


		if (phaseTicks < PHASE_TICKS) {

			Debug.Log (phaseTicks / PHASE_TICKS);
		
			phaseTicks++;
		
		}

		if (currFoodLeft == 0) {
			increaseScore ();
		}

	
	}


	void OnTriggerEnter2D (Collider2D trigger)
	{


	
		if (trigger.gameObject.tag == "Food") {

			//Debug.Log ("Ball eaten");

			//add sound for eating
			audioSource.Play();

			trigger.gameObject.SetActive (false);

			food++;

			currFoodLeft--;
			
			addBody ();

		}

		if (trigger.gameObject.name == "Top") {
		

			this.transform.position = new Vector2 (this.transform.position.x, -1 * this.transform.position.y + 2);
			timeWait = WAIT_TIME;
		
		
		}

		if	(trigger.gameObject.name == "Bottom") {
		
			this.transform.position = new Vector2 (this.transform.position.x, -1 * this.transform.position.y - 2);
			timeWait = WAIT_TIME;

		}


		if (trigger.gameObject.name == "Left") {
		
			this.transform.position = new Vector2 (this.transform.position.x * -1 - 2, this.transform.position.y);
			timeWait = WAIT_TIME;
		
		}

		if (trigger.gameObject.name == "Right") {

			this.transform.position = new Vector2 (this.transform.position.x * -1 + 2, this.transform.position.y);
			timeWait = WAIT_TIME;

		}


	
	}


	public class snakeSegment
	{


		public GameObject GO;

		public float opacity;

		public void updatePos (Vector2 newPosition)
		{

			GO.transform.position = newPosition;

		}

		public void setOpacity (float opacity) {
		
			GO.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, opacity);
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

			segment.updatePos (posList[SNAKE_DISTANCE * counter - 1]);

			if (phaseTicks < PHASE_TICKS) {

				segment.setOpacity (phaseCurve.Evaluate (phaseTicks / PHASE_TICKS));

			} else {
				segment.setOpacity (1);
			}

			counter++;
		}


	}
}
