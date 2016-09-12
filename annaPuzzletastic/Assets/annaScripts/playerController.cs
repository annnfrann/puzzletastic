using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerController : MonoBehaviour {

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
		if (other.gameObject.CompareTag("gameEnd")) {
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
			if (count < 17) {
				winText.text = "you did not collect all the objects. please try again.";
				StartCoroutine (RepeatLevel());

			}
		}
	}

	void SetCountText(){
		countText.text = "Count: " + count.ToString ();
		if (count >= 17) {
			winText.text = "Hooray!";
			StartCoroutine (WinScreen ());
		}
	}

	IEnumerator RepeatLevel(){
		yield return new WaitForSeconds (3);
		Application.LoadLevel ("tube and ring level");
	}

	IEnumerator WinScreen(){
		yield return new WaitForSeconds (3);
		Application.LoadLevel ("winScreen");
	}
		
}