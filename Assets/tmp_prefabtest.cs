using UnityEngine;
using System.Collections;

public class tmp_prefabtest : MonoBehaviour {
	public GameObject myprefab;
	// Use this for initialization

	void Start () {
		Biome_room prefab_rend = myprefab.GetComponent<Biome_room> ();
		Debug.Log ("prefab width is " + prefab_rend.get_tile_width());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
