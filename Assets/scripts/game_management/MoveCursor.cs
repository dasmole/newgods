using UnityEngine;
using System.Collections;

public class MoveCursor : MonoBehaviour {
	int play_selection;
	public GameObject gm_object;


	// Use this for initialization
	void Start () {
		play_selection = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("up")) {
			if (play_selection > 1) {
				play_selection -= 1;
				transform.position += Vector3.up * 20;
			}
		}

		if (Input.GetKeyDown("down")) {
			if (play_selection < 3) {
				play_selection += 1;
				transform.position += Vector3.down * 20;
			}
		} 

		if (Input.GetKeyDown (KeyCode.Return)) {


			//if 0 do 1 player, 1 do 2 player, 2 do exit
			if (play_selection == 3) {
				Debug.Log ("Should be quitting!");
				Application.Quit ();
			} else {
				PlayerPrefs.SetInt ("title_selection", play_selection);
				GameObject title = GameObject.FindGameObjectWithTag ("Title");
				Destroy (title);
				//Application.LoadLevel ("blackscreen");

				GameObject thegm_go = (GameObject) Instantiate (gm_object, Vector3.zero, Quaternion.identity);
				Game_manager thegm = thegm_go.GetComponent<Game_manager> ();

				thegm.load_continent (1);
			}
		}
	}
}
