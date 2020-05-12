using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveBehaviourScript : MonoBehaviour {

	public int playerSpeed = 10;
	public float targetRadius = 1.5f;

	Transform target;
	// Use this for initialization
	void Start () {
		target = transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
		//Con esto capturo el vector hacia donde se va a mover el player
		Vector3 playerDirection = new Vector3 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		playerDirection = transform.TransformDirection(playerDirection);
		transform.Translate(playerDirection * Time.deltaTime * playerSpeed);
		transform.rotation = Quaternion.identity;

		//Con esto capturo el vector hacia donde se va a mover el apuntador
		Vector3 targetDirection = new Vector3 (Input.GetAxis("Right Stick Horizontal"), Input.GetAxis("Right Stick Vertical"), 0);
		if (targetDirection.x != 0 || targetDirection.y != 0) {
			//Vector3 a = transform.GetChild(0).transform.localPosition.normalized - targetDirection;
			//transform.GetChild(0).transform.RotateAround(transform.position, Vector3.forward, a.magnitude * targerPointerSpeed);

			//Esto es para hacer que gire la flecha alrededor del player
			target.transform.right = targetDirection;
			Vector3 pos = (Vector3) transform.position + targetDirection * targetRadius;
			target.position = pos;
				
		}

		if (Input.GetButton("Fire1")) {
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Enemy") {
			print("You Lost");
		}
	}

}
