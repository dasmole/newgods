using UnityEngine;
using System.Collections;

public class Camera_follow : MonoBehaviour {

	public Vector3 myPos;
	public Transform myPlay;

	// Use this for initialization
	void Start () {
	}
	//var myPos : Vector3;
	//var myPlay : Transform;



	void Update()
	{
		transform.position = myPlay.position + myPos;

	}
}
