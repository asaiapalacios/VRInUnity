// Scoreboard isn't counting baskets b/c it doesn't have any way to detect if the ball went into the hoop
// --> Creat a collider and a script to handle basket detection

using UnityEngine;
using System.Collections;

public class BasketDetector : MonoBehaviour {

	// Method checks when basket STOPS touching the collider
	void OnTriggerExit(Collider other) {

	// Check the velocity of the bball's rigid body to make sure it's traveling in the negative y direction (moving downwards the court)
	// In other words, check if the y velocity of the collider's rigid body (bball or 'other') is less than 0
		if (other.attachedRigidbody.velocity.y < 0f) { // If the y velocity is less than 0, it means y velocity is a neg value as bball is exiting the collider
													// In other words, the bball is traveling in the down direction as it's exiting the hoop-->award a point
			GameManager.score++; // Add to the static score variable in the game manager (award a point when bball is detected to be in downward direction)

		}
	}
}
