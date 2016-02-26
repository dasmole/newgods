using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Game_manager : MonoBehaviour {
	public int players = 1;
	public GameObject p1cam;
	public GameObject p1coopcam;
	public GameObject p2coopcam;
	public Transform player1;
	public Transform player2;
	private GameObject cam1;
	private GameObject cam2;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);


		players = PlayerPrefs.GetInt ("title_selection");
		//players = 1;



	}
	
	// Update is called once per frame
	void Update () {


	}

	public void load_continent(int cnt_num) {
		SceneManager.LoadScene ("blackscreen");

		if (cnt_num == 1) {
			Application.LoadLevel ("scene_cnt1");
		}

	}

	public void init_players_and_cams() {
		//if just one player, make a full screen camera and assign it to created player1

		if (players == 1) {
			Debug.Log ("about to load for a player 1");
			player1 = Instantiate (player1, new Vector3(0, 0, -1), Quaternion.identity) as Transform;

			cam1 = (GameObject) Instantiate (p1cam, new Vector3(0, 0, -10), Quaternion.identity);

			if (cam1 != null) {
				Debug.Log ("created cam1");
			} else {
				Debug.Log ("Couldn't create cam1");
			}

			Camera_follow camfollow_script = cam1.GetComponent<Camera_follow> ();

			if (camfollow_script != null) {
				Debug.Log ("Found the Camera_follow component");
			} else {
				Debug.Log ("Couldn't get the Camera_follow component");
			}

			camfollow_script.myPlay = player1;
			//if 2 players, create two cameras and assign them to players 1 and 2
		} else if (players == 2) {
			player1 = Instantiate (player1, new Vector3(-32, 0, -1), Quaternion.identity) as Transform;
			player2 = Instantiate (player2, new Vector3(32, 0, -1), Quaternion.identity) as Transform;

			cam1 = (GameObject) Instantiate (p1coopcam, new Vector3(0, 0, -10), Quaternion.identity);
			cam2 = (GameObject) Instantiate (p2coopcam, new Vector3(0, 0, -10), Quaternion.identity);

			Camera_follow camfollow_script1 = cam1.GetComponent<Camera_follow> ();
			Camera_follow camfollow_script2 = cam2.GetComponent<Camera_follow> ();

			camfollow_script1.myPlay = player1;
			camfollow_script2.myPlay = player2;


		}
	}
}
