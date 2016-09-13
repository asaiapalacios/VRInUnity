// SCRIPT TO APPLY FORCE TO THE BALL TO SIMULATE THROWING THE BALL

using UnityEngine;
using System.Collections;

public class ThrowObject : MonoBehaviour {

	[SerializeField]
	private GameObject throwingObject;

	private Rigidbody objectRigidBody;

	private Vector3 startingPosition;
	private Quaternion startingRotation;

	private float objectForce = 0f;
	private float maximumForce = 600f;
	private float delay = 1f; // Over 1 second the ball will go from zero to the max amount of force of 600
	private float timer; // Use it to count upwards; no default

	[SerializeField] // Modify in the inspector if we want the ball to return faster or slower
	private float objectReturnSpeed = 25f;


	// Use this for initialization
	void Start () {

		objectRigidBody = throwingObject.GetComponent<Rigidbody> ();
		startingPosition = throwingObject.transform.localPosition;
		startingRotation = throwingObject.transform.localRotation;
		objectRigidBody.isKinematic = true;

	}

	// Update is called once per frame
	void Update () {

		// If the player has started to click the mouse button...
		if (Input.GetMouseButtonDown(0)) { // Method takes an index of mouse buttons 0, the left one

			BeginThrow ();

		}

		// If the player is continuing to hold down the mouse button...
		if (Input.GetMouseButton(0)) {

			PowerUpThrow ();

		}

		// If the player releases the mouse button...
		if (Input.GetMouseButtonUp(0)) {

			ReleaseObject ();

		}


	}


	private void BeginThrow () {

		// Reset physics
		objectRigidBody.isKinematic = true;
		objectRigidBody.velocity = Vector3.zero;
		objectRigidBody.angularVelocity = Vector3.zero;

		// Return the object (ball) to the start position (i.e., parent bball to the virtual hands again - the gameObect)
		throwingObject.transform.parent = transform; // Hands transform; We're parenting the basketball to the hands
		throwingObject.transform.localRotation = startingRotation; // Reset the ball to the starting rotation
		StartCoroutine (ReturnObject ()); // Reset the ball's position by having the ball fly smoothly back to the starting position; StartC.. = method, passing method returnObject() <-- also a coroutine method

	}


	private void ReleaseObject() {

		// Launch the object (the player has released the mouse button, i.e. throw the basketball)
		objectRigidBody.isKinematic = false; // The ball is in the hands of physics
		throwingObject.transform.parent = null; // The bball doesn't have a parent anymore, is @ root of hierarchy; ball will be free
		objectRigidBody.AddRelativeForce(throwingObject.transform.forward * objectForce); // Adding force to ball & multiplying vector by some value to tell the AddRelativeForce method how hard we want to throw ball

		// Reset the force and timer
		objectForce = 0f;
		timer = 0f;

		StopCoroutine (ReturnObject ());

	}

	// Next: work on the PowerUpThrow method where we'll set this variable and determine how hard to throw the ball
	private void PowerUpThrow() { // Method will be executed over and over again the longer the player holds down the mouse button; when mouse is released, apply stored force to the ball

		// Increment timer once per frame..
		timer += Time.deltaTime; // Each frame that PowerUpThrow is running, the timer will add Time.deltaTime to itself (to the value of timer)
		if (timer > delay)
			timer = delay; // The timer will be assigned to the same value as the delay (set to 1)

		// Lerp (linearly interpolate) the object force (i.e., the longer the player is holding down the mouse button, the more force there will be)
		float perc = timer / delay; // A locally scoped float variable; will give us the %age of which the timer has counted up towards the delay from zero

		objectForce = Mathf.Lerp (0f, maximumForce, perc); // Percentage of time going from 0 to max force

	}

	IEnumerator ReturnObject() { // Return bball to the player; ReturnObject is name of our coroutine

		float distanceThreshold = 0.1f; // Locally scope float variable

		// While there is still a small amount of distance between the thrown object and starting position...
		while (Vector3.Distance(throwingObject.transform.localPosition, startingPosition) > distanceThreshold) {

			// ...move the ball toward the starting posiiton...
			throwingObject.transform.localPosition = Vector3.Lerp(throwingObject.transform.localPosition, // Lerp method is used to gradually change the value of the ball's local position to the start position over a period of time
																  startingPosition,
																  Time.deltaTime * objectReturnSpeed);

			// ...if the thrown object is now close to the start position, reset the position directly.
			if (Vector3.Distance(throwingObject.transform.localPosition, startingPosition) < distanceThreshold) {

				throwingObject.transform.localPosition = startingPosition;
			}


			// ...yield back control to the main script.
			yield return null; // This will allow the while statement to keep running if it needs to keep moving the object (bbal to start position)

		}

	}

}
