using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class levelOnePlayerController : MonoBehaviour {

	public float speed;
	public Text winText;

	private Rigidbody rb;
	private int count;

	void Start(){
		rb = GetComponent<Rigidbody> ();
		winText.text = "collect the cube";
	}

	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);

		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("PickUp")) {
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
	}

	void SetCountText(){
		if (count >= 1) {
			winText.text = "you've completed this level!";
			//call the coroutine here
			StartCoroutine(NextLevel());
		}
	}

	IEnumerator NextLevel(){
		yield return new WaitForSeconds(3);			
		Application.LoadLevel ("second level");
	}
}
