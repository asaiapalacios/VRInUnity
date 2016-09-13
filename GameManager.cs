// SCRIPT TURNS PROJECT INTO SOMETHING THAT FEELS MORE LIKE A GAME:
// start game, end game, and timer to name a few);
// add scoreboard which will provide visual cues to the player about what's going on in the game.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


	private bool gameStarted = false; // At the start of the application, the game should not be started
	private float timer; // Float will count down the amount of time the player has left
	// Feed into the timer when the game starts
	[SerializeField] // We're going to want to change this potentially, hence SerializeField
	private float gameLengthInSeconds = 30f;
	//
	public static int score; // Will track the player's score;
							 // will count how many baskets a player has made rather than keeping score;
							 // needs to be a public static variable b/c later on other scripts will need to add to the score;
	// GameManger will update each part of the scoreboard, but in order to do that, we need to create SerializeFields to store each component
	[SerializeField]
	private Text scoreText;

	[SerializeField]
	private Text timerText;

	[SerializeField]
	private Text gameStateText;

	// Use this for initialization
	void Start () {

		gameStateText.text = "HIT SPACE TO PLAY!"; // Mission: at start of game, update scoreboard after the timer has been set
												   // Set gameStatetext to give instructions on how to start game
		timer = gameLengthInSeconds; // Set the timer at the start of the game to the length of time that the game should run;
		// Call UpdateScoreBoard method every frame the game is running b/c the timer & the score could change at any given frame
		UpdateScoreBoard ();

	}

	// Update is called once per frame
	void Update () {

		// If the game has not started and the player pressed the spacebar...
		if (!gameStarted && Input.GetKeyUp(KeyCode.Space)) {

			// ...then start the game (i.e., StartGame method)
			StartGame();

		}

		// If the game has started...
		if (gameStarted) {

			// ...decrement the timer and update the scoreboard
			timer -= Time.deltaTime;
			UpdateScoreBoard (); // Call UpdateScoreBoard method every frame the game is running...
								 // b/c the timer & the score could change at any given frame

		}

		// If the game has started and the timer is less thanor equal to zero...
		if (gameStarted && timer <= 0) {

			// ...then end the game.
			EndGame();
		}

	}

	// When game starts, set score variable to zero and gameStarted variable to true
	private void StartGame() {

		score = 0;
		gameStarted = true;
		gameStateText.text = "GO! GO! GO!"; // Set gameStateText to let the player know they should be shooting baskets

	}

	// Set the gameStarted variable back to false and then reset the timer back to game length in seconds
	private void EndGame() {

		gameStarted = false;
		timer = gameLengthInSeconds;
		gameStateText.text = "TIME'S UP!\nHIT SPACE TO PLAY AGAIN!"; // Set text at end of game to let player know their time is up
																	 // If player wants to play again, they'll have to hit space bar again
	}

	// Method updates the score text and the timer. Can call UpdateScoreBoard method from anywhere in the rest of our sript
	private void UpdateScoreBoard() {
		// String concatenation: taking two strings or variables and putting them together
		scoreText.text = "SCORE\n" + score; // Displaying the word "SCORE"; score variable will add the score num to this string
		timerText.text = "Timer\n" + Mathf.RoundToInt (timer); // Method RoundToInt modifies the display of the timer value (rounds to whole num)
															   // won't actually change the timer itself;
															   // will instead create a new number that's only meant for being displayed.
	}

}
