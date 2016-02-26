using UnityEngine;
using System.Collections;

public class nextscene : MonoBehaviour {
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		System.Threading.Thread.Sleep (100);
		Application.LoadLevel ("first");

	}
}
