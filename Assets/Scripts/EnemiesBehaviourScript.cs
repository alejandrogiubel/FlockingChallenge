using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesBehaviourScript : MonoBehaviour {

	public GameObject ball;
	public int enemyNumber = 50;
	public int enemyVelocity = 1;
	List<GameObject> balls = new List<GameObject>();
	float unitY;
	float unitX;



	// Use this for initialization
	void Start () {
		for (int i = 0; i < enemyNumber; i++) {
			GameObject newBall = Instantiate (ball);
			balls.Add (newBall);
			newBall.transform.localScale = new Vector2 (0.3f, 0.3f);
			//La Camera.main.ortographicSize es la distancia desde el centro de la camara al bore superior
			// y desde el centro de la camra la borde inferior
			unitY = Camera.main.orthographicSize;
			//Aqui hallo la cantidad de unidades que hay desde el centro de la camara al borde derecho o izquierdo
			unitX = unitY * ((float)Screen.width / (float)Screen.height);
			newBall.transform.position = new Vector2 (Random.Range(-unitX, unitX), Random.Range(-unitY, unitY));
		}


	}
	
	// Update is called once per frame
	void Update () {
		MoveAllBallsToNewPosition ();
	}


	void MoveAllBallsToNewPosition (){
		Vector2 rule1;
		Vector2 rule2;
		Vector2 rule3;
		Vector2 rule4;
		for (int i = 0; i < balls.Count; i++) {
			rule1 = Rule1 (balls [i]);
			rule2 = Rule2 (balls [i]);
			rule3 = Rule3 (balls [i]);
			rule4 = Rule4 ();
			Vector2 velocity = balls [i].GetComponent<Rigidbody2D> ().velocity + rule1 + rule2 + rule3;
			balls [i].GetComponent<Rigidbody2D> ().velocity = velocity.normalized * enemyVelocity;
			Vector2 ballPosition = balls [i].transform.position;
			Vector2 direction = ballPosition + velocity;
			//balls [i].transform.position = Vector2.Lerp(ballPosition, rule4, Time.deltaTime * 0.5f);
			balls [i].transform.Translate (direction.normalized * Time.deltaTime);
		}
	}

	Vector2 CenterOfTheMass(){
		Vector2 c = new Vector2();
		for (int i = 0; i < balls.Count; i++) {
			Vector2 ballsPosition = balls [i].transform.position;
			c += ballsPosition;
		}
		c = c / balls.Count;
		return c;
	}

	Vector2 PerceivedCenter (){
		Vector2 pc = new Vector2 ();
		for (int i = 0; i < balls.Count; i++) {
			Vector2 ballsPosition = balls [i].transform.position;
			pc += ballsPosition;
		}
		pc = pc / (balls.Count - 1);
		return pc;
	}

	//Tratan de moverce cerca del centro de masa de los vecinos
	Vector2 Rule1 (GameObject ball){
		Vector2 pc = new Vector3();
		for (int i = 0; i < balls.Count; i++) {
			if (ball != balls[i]) {
				Vector2 ballsPosition = balls [i].transform.position;
				pc = pc + ballsPosition;
			}
		}
		pc = pc / (balls.Count - 1);
		Vector2 ballPosition = ball.transform.position;
		var a = (pc - ballPosition) / 100;
		return a;
	}

	//Tratan de mantener la distancia entre ellas
	Vector2 Rule2 (GameObject ball){

		//Esto es para eliminar todos los game object dentro de ball
		//que son los que contien el line render (que son las lineas)
		int count = ball.transform.childCount;
		for (int i = 0; i < count; i++) {
			GameObject.Destroy (ball.transform.GetChild (i).gameObject);
		}


		Vector2 c = new Vector2 ();
		for (int i = 0; i < balls.Count; i++) {
			if (ball != balls [i]) {
				//Este 1 es la cantidad de unidades entre bolitas. Debo parametrizarlo.
				//Invertir estas dos diferencias para poner el modo zombie. Se comen entre ellas.
				if (Vector2.Distance (balls [i].transform.position, ball.transform.position) <= 1) {
					//Esto es lo mismo que lo que esta en el comentario de abajo, pero bien hecho.
					Vector2 ballsPosition = balls [i].transform.position;
					Vector2 ballPosition = ball.transform.position;
					c = c - (ballsPosition - ballPosition);
					//c = c - (balls [i].transform.position - ball.transform.position);
				}
				if (Vector3.Distance (balls [i].transform.position, ball.transform.position) <= 1.5f) {
					GameObject gameObjectForLineRender = new GameObject ();
					gameObjectForLineRender.transform.SetParent (ball.transform);
					LineRenderer line = gameObjectForLineRender.AddComponent<LineRenderer> ();
					line.startColor = Color.red;
					line.startWidth = 0.1f;
					line.positionCount = 2;
					line.SetPosition (0, ball.transform.position);
					line.SetPosition (1, balls [i].transform.position);
				}
			}
		}
		return c;
	}

	//Tratan de tener todas la misma velocidad
	Vector2 Rule3 (GameObject ball){
		//Perceived velocity
		Vector2 pv = new Vector2 ();
		for (int i = 0; i < balls.Count; i++) {
			if (ball != balls [i]) {
				pv = pv + balls [i].GetComponent<Rigidbody2D> ().velocity;
			}
		}
		pv = pv / (balls.Count - 1);
		return (pv - ball.GetComponent<Rigidbody2D> ().velocity) / 8;
	}

	Vector2 Rule4 () {
		//Player position
		Vector2 pp = new Vector2 ();
		GameObject player = GameObject.Find("player");
		pp = player.transform.position;
		return pp;
	}

//Esto esta super mal. Hay que hacerlo mas eficiente.
	public void DestroyBall (GameObject ball) {
		balls.Remove(ball);
	}
}
