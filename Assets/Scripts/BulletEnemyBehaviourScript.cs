using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyBehaviourScript : MonoBehaviour
{
    public float bulletSpeed = 10f;
    // Start is called before the first frame update
    void Start () {
		Invoke("AutoDestroy", 3);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector2.right * Time.deltaTime * bulletSpeed);
	}

	private void AutoDestroy() {
		GameObject.Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag != "Limit" && other.gameObject.tag != "Enemy") {
			GameObject.Destroy(other.gameObject);
			//Esto esta mal cantidad. Hay que cambiarlo por algo optimo o mas eficiente.
			GameObject.Find("BallsGenerator").GetComponent<EnemiesBehaviourScript> ().DestroyBall(other.gameObject);
			GameObject.Destroy(this.gameObject);
		}
	}
}
