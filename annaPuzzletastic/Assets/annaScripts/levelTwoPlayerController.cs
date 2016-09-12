using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class levelTwoPlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;

	private Rigidbody rb;
	private int count;

	void Start(){
		rb = GetComponent<Rigidbody> ();
		count = 0;
		SetCountText ();
		winText.text = "";
	}

	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);

		rb.AddForce (movement * speed);
	}

	void Update(){
		if (Input.GetKeyDown ("space")) {
			Vector3 jump = new Vector3 (0.0f, 200f, 0.0f);
			GetComponent <Rigidbody> ().AddForce (jump);
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("PickUp")) {
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
		else if (other.gameObject.CompareTag ("gameEnd")) {
			if (count >= 9) {
				winText.text = "cool. you win.";
				StartCoroutine (NextLevel ());
			} else {
				winText.text = "you didn't collect all the objects. try again.";
				StartCoroutine (RepeatLevel ());
			}
		}
	}

	void SetCountText(){
		countText.text = "Count: " + count.ToString ();
	}

	IEnumerator NextLevel(){
		yield return new WaitForSeconds(3);			
		Application.LoadLevel ("tube and ring level");
	}

	IEnumerator RepeatLevel(){
		yield return new WaitForSeconds(3);			
		Application.LoadLevel ("second level");
	}

}