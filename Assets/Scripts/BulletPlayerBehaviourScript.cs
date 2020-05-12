using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletPlayerBehaviourScript : MonoBehaviour {

public float bulletSpeed = 10f;
	// Use this for initialization
	void Start () {
		Invoke("AutoDestroy", 3);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.right * Time.deltaTime * bulletSpeed);
	}

	private void AutoDestroy() {
		GameObject.Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Enemy") {
			GameObject.Destroy(other.gameObject);
			//Esto esta mal cantidad. Hay que cambiarlo por algo optimo o mas eficiente.
			GameObject.Find("BallsGenerator").GetComponent<EnemiesBehaviourScript> ().DestroyBall(other.gameObject);
			GameObject.Destroy(this.gameObject);
			GameObject.Find("player").GetComponent<PlayerPoints> ().SetPoints(1);
			int p = GameObject.Find("player").GetComponent<PlayerPoints> ().GetPoints();
			GameObject.Find("Points").GetComponent<Text> ().text = "Points: " + p;
		}
	}
}
