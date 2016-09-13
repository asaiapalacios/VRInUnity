using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class VRAdjustment : MonoBehaviour {
	// VR CAMERA SET UP

	private Vector3 monitorRotation = new Vector3 (-12f, 0f, 0f);
	private Vector3 openVRPosition = new Vector3 (0f, -1.75f, 0f);


	// Use this for initialization
	void Awake () {

		// If there is no VR device at the start of the game...
		if (!VRDevice.isPresent) {

			// ...tilt the camera upward so the monitor can display the hoop.
			transform.localRotation = Quaternion.Euler (monitorRotation);

		} else {

			// Otherwise, if this VR device is the HTC Vive...
			if (VRSettings.loadedDeviceName == "OpenVR") {

				// ...move the camera to the floor, because that's where the tracking volume will be located.
				transform.localPosition = openVRPosition;

			}
		}
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp (KeyCode.R)) {

			InputTracking.Recenter (); // This will reset the virtual camera to be pointing in the forward direction of wherever the player is looking in the HMD

		}

	}


	void OnApplicationQuit () { // The model behavior message OnApplicationQuit is called when the application quits.

		// Disable the VR device when the application quits. This clears any leftover VR settings.
		VRSettings.enabled = false; // Essentially turns off VR for the game.

	}


}
