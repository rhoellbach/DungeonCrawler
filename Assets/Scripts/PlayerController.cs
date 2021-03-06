using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	//Bewegungsrichtung
	public float moveSpeed = 5.0F;
	//Drehgeschwindigkeit
	public float rotationSpeed = 300.0F;
	//Bewegungsrichtung
	private Vector3 moveDirection = Vector3.zero;
	//CharacterController-Speichervariable
	private CharacterController controller;
	//Zieldrehungsausrichtung
	private Quaternion destRotation;

	void Start() {
		controller = GetComponent<CharacterController>();
		//Initialisierung: Anfangsausrichtung als Zieldrehung
		destRotation = transform.rotation;
	}

	void Update () {
		//Bewegungstasten abfragen und der Variablen zuweisen
		moveDirection = new Vector3 (Input.GetAxis ("Horizontal"),
			              0, Input.GetAxis ("Vertical"));
		//Umrechnung der relativen Richtung in globale Richtung
		moveDirection = transform.TransformDirection(moveDirection);
		//Geschwindigkeitsfaktor einbeziehen
		moveDirection = moveDirection * moveSpeed;
		//Controller mit SimpleMove steuern
		controller.SimpleMove(moveDirection);
		//Wenn Taste "A" gedrückt wird
		if (Input.GetKeyDown ("q")) {
			//Dann Zieldrehung 90 Grad nach links
			destRotation.eulerAngles = destRotation.eulerAngles - new Vector3 (0, 90, 0);
		}
		//Wenn Taste "D" gedrückt wird
		if (Input.GetKeyDown ("e")) {
			//Dann Zieldrehung 90 Grad nach rechts
			destRotation.eulerAngles = destRotation.eulerAngles + new Vector3 (0, 90, 0);
		}
		//Schrittweite für den aktuellen Rotationsschritt berechnen
		float step = rotationSpeed * Time.deltaTime;
		//Transform drehen
		transform.rotation = Quaternion.RotateTowards(transform.rotation, destRotation, step);
	}
}
