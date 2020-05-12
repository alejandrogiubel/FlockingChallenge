using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootBehaviourScript : MonoBehaviour {

public GameObject bullet;
Transform target;
	// Use this for initialization
	void Start () {
		target = transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("RB Joystick")) {
			GameObject newBullet = Instantiate(bullet);
			//Aqui asigno la posicion de la bala para que sea la misma que la de la flecha
			newBullet.transform.position = target.position;
			//Esto es pa q la bala tenga el mismo sentido que la flecha
			newBullet.transform.right = target.transform.right;
			transform.GetComponent<AudioSource> ().Play();
		}
		//Esto es viejo era el movimiento de las balas.
		// for (int i = 0; i < bullets.Count; i++) {
		// 	bullets [i].transform.Translate(Vector3.right * Time.deltaTime * bulletSpeed);
		//  	print(transform.localPosition);
		//  	print(transform.name);
		// }

	}
}
