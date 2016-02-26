using UnityEngine;
using System.Collections;

using System;

public class Continent : MonoBehaviour {
	private Biome applied_biome;

	public static int map_width = 240;
	public static int map_height = 240;


	//todo make tile_width and tile_height retrievable from biome
	public int tile_width = 32;
	public int tile_height = 32;

	//todo pretty sure I can get rid of the lines below
	//public int frequency = 1;
	//public int amplitude = 1;


	double[,] map_floats = new double[map_width, map_height];
	bool[,] map_bools = new bool[map_width, map_height];
	bool[,] room_bools = new bool[map_width, map_height];



	// Use this for initialization
	void Start () {
		//persist the game object

		applied_biome = new Biome_cleansed();

		init_map ();
		create_random_map_floats();
		create_map_bools_from_floats(.5f);
		place_rooms (2000);

		render ();

		//spawn our PCs
		GameObject thegm_go = GameObject.FindGameObjectWithTag("Game_manager");
		Game_manager thegm = thegm_go.GetComponent<Game_manager> ();
		thegm.init_players_and_cams ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void init_map () {
		for (int i = 0; i < map_width; i++) {
			for (int j = 0; j < map_height; j++) {
				map_floats [i, j] = 0;
				map_bools [i, j] = false;
			}
		}
	}




	void create_random_map_floats(){
		place_random_circle (1, .7f);
		place_random_circle (2, .5f);
		place_random_circle (4, 1);
		place_random_circle (4, .7f);
		place_random_circle (2, .5f);
	}


	void create_map_bools_from_floats(float cutoff) {
		for (int i = 0; i < map_width; i++) {
			for (int j = 0; j < map_height; j++) {
				if (map_floats [i, j] < cutoff) {
					map_bools [i, j] = false;
				} else {
					map_bools [i, j] = true;
				}
			}
		}
	}


	void print_map() {
		string aline = "";
		for (int i = 0; i < map_width; i++) {
			aline = "";
			for (int j = 0; j < map_height; j++) {
				aline += (map_floats [i, j] + " ");
			}

			Debug.Log (aline);
		}
	}


	void render() {
		int xcoord = 0;
		int ycoord = 0;
		for (int i = 1; i < map_width - 1; i++) {
			for (int j = 1; j < map_height - 1; j++) {
				if (room_bools [i, j] == false) {
					//coords can't start at 0,0, or player will start on lower-left corner, so we offset
					//them by half the map size, and then count up from there.
					xcoord = get_x_coord (i);
					ycoord = get_y_coord (j);


					//this will generate a unique in depending on adjacent tiles, and get (from the biome) the right tile for the tile's positioning

					Instantiate (Resources.Load (applied_biome.get_tile (get_adjacent (i, j))), new Vector2 (xcoord, ycoord), Quaternion.identity);
				} else {
					Debug.Log ("Looks like there's a room at " + i + ", " + j);
				}
			}
		}
	}



	void place_random_circle (float freq, float amp) {
		//allowed deviation should be radius/frequency
		float radius = map_width / 2;
		float allowed_distance = radius - (radius / freq);

		float xoffset = UnityEngine.Random.Range (-allowed_distance, allowed_distance);
		float yoffset = UnityEngine.Random.Range (-allowed_distance, allowed_distance);

		for (int i = 0; i < map_width; i++) {
			for (int j = 0; j < map_height; j++) {
				map_floats[i, j] += get_circular_amp(i, j, xoffset, yoffset, freq, amp);
			}
		}
	}



	//Creates a sine function distribution in a circle around the offset. Offset uses 0,0 as origin.
	float get_circular_amp(int arrayx, int arrayy, float xoffset, float yoffset, float frequency, float max_amplitude) {

		float radius = map_width / 2f;

		//float xcoord = Math.Abs (arrayx - radius);
		//float ycoord = Math.Abs (arrayy - radius);

		float xcoord = arrayx - radius + xoffset;
		float ycoord = arrayy - radius + yoffset;


		double distance = Math.Sqrt (Math.Pow (xcoord, 2) + Math.Pow (ycoord, 2));

		//filter out more > 0 curves than first
		if ((frequency * distance) > radius) {
			return 0f;
		}

		//float ampToReturn = (float) (Math.Sin(((distance /radius) * (Math.PI / 2)) + (Math.PI / 2)));


		/**
		 * The idea of the following formula is that given a frequency and max amplutude of 0, we start
		 * at the origin (offset) with a value of 1, and lower values from that point until we hit 0 at a
		 * distance of the radius away. So, it creates a gradient circle.
		 * 
		 * We multiple by PI/2 to match the frequency (wave-form) of a sine function, and we then add PI/2 to
		 * phase shift the function a bit (sine(0) = 0, and we need our_function(0) = 1. Since sine(PI/2) = 1,
		 * we add PI/2 to shift).
		 * 
		 * Then we multiply amplitude and frequency in the correct places to either make smaller/larger circles (freq)
		 * or brighter/darker circles (amp).
		 */
		float ampToReturn = (float) ((Math.Sin(frequency * ((distance /radius) * (3.1415 / 2)) + (3.1415 / 2))) * max_amplitude);

		return Math.Max(ampToReturn, 0);
	}


	int get_adjacent(int x, int y) {
		int num_to_return = 0;

		//start with lower right and move to upper left
		if (map_bools [x + 1, y - 1]) {
			num_to_return += 1;
		}
		if (map_bools [x , y - 1]) {
			num_to_return += 2;
		}
		if (map_bools [x - 1 , y - 1]) {
			num_to_return += 4;
		}
		if (map_bools [x + 1, y ]) {
			num_to_return += 8;
		}
		if (map_bools [x , y ]) {
			num_to_return += 16;
		}
		if (map_bools [x - 1, y]) {
			num_to_return += 32;
		}
		if (map_bools [x + 1, y + 1]) {
			num_to_return += 64;
		}
		if (map_bools [x , y + 1]) {
			num_to_return += 128;
		}
		if (map_bools [x - 1, y + 1]) {
			num_to_return += 256;
		}

		return num_to_return;
	}

	void place_rooms(int numofrooms) {
		for (int i = 0; i < numofrooms; i++) {
			place_room ();
		}
	}

	/// <summary>
	/// Picks a random location on map and calls Biome.get_room to get a random Biome_room.
	/// Checks the location and tile height and width of room, to see if room can be placed.
	/// 
	/// If the room can be placed, add_room_to_continent is called which handles the book-keeping,
	/// and the function returns True. Upon receiving the True, the biome will usually remove the
	/// room from the random pool of placeable rooms.
	/// </summary>
	/// <returns>The rooms.</returns>
	bool place_room() {
		int xloc = UnityEngine.Random.RandomRange (0, map_width);
		int yloc = UnityEngine.Random.RandomRange (0, map_height);
		Debug.Log ("Will attempt to place at x: " + xloc + ", y: " + yloc);

		GameObject target_room_go = (GameObject) (Instantiate(Resources.Load(applied_biome.get_biome_room ()), Vector3.zero, Quaternion.identity)); 
		Biome_room target_room = target_room_go.GetComponent<Biome_room> ();
		if (map_bools [xloc, yloc]) {
			int room_width = target_room.get_tile_width ();
			int room_height = target_room.get_tile_height ();

			Debug.Log ("room dimensions are " + room_width + "," + room_height);

			//iterate through all tiles, looking for one that we can't place
			for (int i = -1; i < room_width + 1; i++) {
				for (int j = -1; j < room_height + 1; j++) {
					//check for array oob with if/then
					//if (((yloc - j) < 0) || ((xloc + i) > room_width) || ((yloc - j) > room_height) || ((xloc + i) < 0)) {
					//	return false;
					//}

					//check if the room would place on top of another room or outside continent
					if ((!map_bools [i + xloc, yloc - j]) || (room_bools[i+xloc, yloc - j])) {
						Destroy (target_room_go);
						return false;
					}
				}
			}

			add_room_to_continent (xloc, yloc, target_room);
			return true;

		} else {
			Debug.Log ("initial loc wasn't true");
		}
		Destroy (target_room_go);
		return false;
	}


	void add_room_to_continent(int xloc, int yloc, Biome_room target_room) {
		
		Transform room_transform = target_room.GetComponent<Transform> ();
		room_transform.position = new Vector2 (get_x_coord (xloc), get_y_coord (yloc));

		//register room locations
		for (int i = xloc; i < (target_room.tile_width + xloc); i++) {
			for (int j = yloc; j > ( yloc - target_room.tile_height); j--) {
				room_bools[i, j] = true;
			}
		}
				
		
	}

	/// <summary>
	/// gets the x coordinate given tile location in the map
	/// </summary>
	/// <returns>The x coordinate.</returns>
	/// <param name="tile_x">Tile x.</param>
	public int get_x_coord(int tile_x) {
		return (tile_x * tile_width) - (tile_width * (map_width / 2));
	}

	/// <summary>
	/// gets the y coordinate given a tile location in the map
	/// </summary>
	/// <returns>The y coordinate.</returns>
	/// <param name="tile_y">Tile y.</param>
	public int get_y_coord(int tile_y) {
		return (tile_y * tile_height) - (tile_height * (map_height / 2));
	}
}
