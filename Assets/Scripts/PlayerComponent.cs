using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerComponent : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winCondition;
	public Text timer_counter;

	private Rigidbody rb;
	private int count;
	private float start_time;


	void Start(){
		rb = GetComponent<Rigidbody> ();
		count = 0;
		setCountText ();
		start_time = Time.time;
	}
	void Update(){
		
	}
	void FixedUpdate(){
		float move_horizontal = Input.GetAxis("Horizontal");
		float move_vertical = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3 (move_horizontal, 0, move_vertical);
		rb.AddForce (movement*speed);

		//timer_counter.text = "Time: " + start_time;


	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag("Pick Up")) {
			other.gameObject.SetActive(false);
			count++;
			setCountText ();
		}
	}

	void setCountText(){
		countText.text = "Count: " + count.ToString ();
		if (count == 16) {
			winCondition.text = "You won!";
			timer_counter.text = "Time: " + (Time.time - start_time);
		}
	}
}
