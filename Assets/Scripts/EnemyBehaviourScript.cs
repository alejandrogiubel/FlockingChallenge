using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourScript : MonoBehaviour {
	public GameObject enemyBullet;
	GameObject player;
	float unitY;
	float unitX;
	Vector2 direction;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("player");
		//La Camera.main.ortographicSize es la distancia desde el centro de la camara al bore superior
		// y desde el centro de la camra la borde inferior
		unitY = Camera.main.orthographicSize;
		//Aqui hallo la cantidad de unidades que hay desde el centro de la camara al borde derecho o izquierdo
		unitX = unitY * ((float)Screen.width / (float)Screen.height);
		direction = new Vector3 (1, 1);
		Invoke ("CalculateNewRandomDirection", 0);
		Invoke("ShootToPlayer", Random.Range(8, 10));
	}
	
	// Update is called once per frame
	void Update () {
		//Vector2 pp = GameObject.Find("player").transform.position;
		//transform.position = Vector2.Lerp(transform.position, pp, Time.deltaTime);
		/*if (transform.position.y >= unitY || transform.position.y <= -unitY ||
			transform.position.x >= unitX || transform.position.x <= -unitX) {
			OpositeDirection();
			Invoke ("CalculateNewRandomDirection", 0);
			transform.Translate (direction * Time.deltaTime);
		} else {
			transform.Translate (direction * Time.deltaTime);
		}*/
	}

	void CalculateNewRandomDirection(){
		direction = new Vector2 (Random.Range (-unitX, unitY), Random.Range (-unitX, unitY));
		//Invoke ("CalculateNewDirection", 3);
	}

	void OpositeDirection(){
		direction = Vector2.Scale (direction, new Vector2(-1, -1));
	}

	void ShootToPlayer () {
		GameObject newBullet = Instantiate(enemyBullet);
		newBullet.transform.position = transform.position;
		newBullet.transform.right = player.transform.position - transform.position;
		Invoke("ShootToPlayer", Random.Range(8, 10));
	}

}
