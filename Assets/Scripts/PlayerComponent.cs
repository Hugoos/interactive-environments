using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class PlayerComponent : MonoBehaviour {

	public float speed; //speed is given at the player inspector
	public Text countText;
	public Text winCondition;
	public Text timer_counter;

		
	private Rigidbody rb;
	private int count;
	private float start_time;
	private float current_time;
	private bool race_start = false;
	private bool race_finish = false;


	void Start(){
		rb = GetComponent<Rigidbody> ();
		count = 0;




	}
	void Update(){
		if (race_start && !race_finish) { //print each frame the time
			current_time = Time.time - start_time;
			timer_counter.text = "Time: " + current_time.ToString("0.00");
		}		
	}
	void FixedUpdate(){
		float move_horizontal = Input.GetAxis("Horizontal");
		float move_vertical = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3 (move_horizontal, 0, move_vertical);
		rb.AddForce (movement*speed);

		//timer_counter.text = "Time: " + start_time;


	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("Pick Up")) {//Detect end game?
			other.gameObject.SetActive(false);
			count++;
			setWinCondition ();
		}
		if (other.gameObject.CompareTag ("StartLine")) {//Start counting time
			race_start = true;
			other.gameObject.SetActive(false);
			start_time = Time.time;


		}
		if (other.gameObject.CompareTag ("Lava")) {//User fell from the level
			//Debug.Log("Lava touched");

			//reset game
			Scene current_scene = SceneManager.GetActiveScene();
			SceneManager.LoadScene (current_scene.name);


			//transform.position = new Vector3 (-0.42f, 0.547f, -16.01f); //update position
			//reset force
			//rb.velocity = Vector3.zero;
			//rb.angularVelocity = Vector3.zero;
		}

	}
	void setWinCondition(){
		//countText.text = "Count: " + count.ToString ();
		if (count == 1) {
			race_finish = true;
			//Display win
			winCondition.text = "You won!";
			//Stop Time, get final time
			timer_counter.text = "Time: " + (Time.time - start_time).ToString("0.00");



			//Write to file the data
			File.AppendAllText("run_data.txt", "Text to append" + Environment.NewLine);


		}
	}
}
