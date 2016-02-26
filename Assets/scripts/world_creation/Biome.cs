using UnityEngine;
using System.Collections;

interface Biome {
	string get_tile (int adj_tiles);

	string get_biome_room ();
}
