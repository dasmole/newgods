using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Biome_cleansed : Biome  {

	private string atile;
	public int tile_width = 32;
	public int tile_height = 32;
	private string[] tiles = new string[512]; 
	private List<string> rooms = new List<string>();


	public Biome_cleansed() {
		//set tile values
		tiles [0] = "cleansed_water";
		tiles [1] = "cleansed_water";
		tiles [2] = "cleansed_water";
		tiles [3] = "cleansed_water";
		tiles [4] = "cleansed_water";
		tiles [5] = "cleansed_water";
		tiles [6] = "cleansed_water";
		tiles [7] = "cleansed_water";
		tiles [8] = "cleansed_water";
		tiles [9] = "cleansed_water";
		tiles [10] = "cleansed_water";
		tiles [11] = "cleansed_water";
		tiles [12] = "cleansed_water";
		tiles [15] = "cleansed_water";
		tiles [27] = "cleansed_north_west_with_water";
		tiles [31] = "cleansed_north_west_with_water";
		tiles [36] = "cleansed_water";
		tiles [38] = "cleansed_water";
		tiles [39] = "cleansed_water";
		tiles [54] = "cleansed_north_east_with_water";
		tiles [55] = "cleansed_north_east_with_water";
		tiles [63] = "cleansed_north_water";
		tiles [64] = "cleansed_water";
		tiles [73] = "cleansed_water";
		tiles [72] = "cleansed_water";
		tiles [75] = "cleansed_water";
		tiles [79] = "cleansed_water";
		tiles [91] = "cleansed_north_west_with_water";
		tiles [95] = "cleansed_north_west_with_water";
		tiles [127] = "cleansed_north_water";
		tiles [128] = "cleansed_water";
		tiles [192] = "cleansed_wall_south_west_water";
		tiles [200] = "cleansed_wall_south_west_water";
		tiles [201] = "cleansed_wall_south_west_water";
		tiles [216] = "cleansed_south_west_with_water";
		tiles [217] = "cleansed_south_west_with_water";
		tiles [219] = "cleansed_west_water";
		tiles [223] = "cleansed_west_water";
		tiles [255] = "cleansed_north_west_invert_with_water";
		tiles [256] = "cleansed_water";
		tiles [260] = "cleansed_water";
		tiles [288] = "cleansed_water";
		tiles [292] = "cleansed_water";
		tiles [294] = "cleansed_water";
		tiles [310] = "cleansed_north_east_with_water";
		tiles [311] = "cleansed_north_east_with_water";
		tiles [319] = "cleansed_north_water";
		tiles [320] = "cleansed_water";
		tiles [384] = "cleansed_wall_south_east_water";
		tiles [388] = "cleansed_wall_south_east_water";
		tiles [416] = "cleansed_wall_south_east_water";
		tiles [420] = "cleansed_wall_south_east_water";
		tiles [422] = "cleansed_wall_south_east_water";
		tiles [432] = "cleansed_south_east_with_water";
		tiles [436] = "cleansed_south_east_with_water";
		tiles [438] = "cleansed_east_water";
		tiles [439] = "cleansed_east_water";
		tiles [447] = "cleansed_north_east_invert_with_water";
		tiles [448] = "cleansed_wall_water";
		tiles [456] = "cleansed_wall_water";
		tiles [459] = "cleansed_wall_water";
		tiles [484] = "cleansed_wall_water";
		tiles [472] = "cleansed_south_west_with_water_and_wall";
		tiles [473] = "cleansed_south_west_with_water_and_wall";
		tiles [475] = "cleansed_west_with_wall";
		tiles [479] = "cleansed_west_with_wall";
		tiles [480] = "cleansed_wall_water";
		tiles [484] = "cleansed_wall_water";
		tiles [496] = "cleansed_south_east_with_water_and_wall";
		tiles [500] = "cleansed_south_east_with_water_and_wall";
		tiles [502] = "cleansed_east_with_wall";
		tiles [503] = "cleansed_east_with_wall";
		tiles [504] = "cleansed_south";
		tiles [505] = "cleansed_south";
		tiles [508] = "cleansed_south";
		tiles [507] = "cleansed_south_west_invert";
		tiles [510] = "cleansed_south_east_invert";


		//init rooms
		rooms.Add("Biome_room_template");
		rooms.Add ("Room_cleansed_pit");
	}


	public string get_tile(int adj_tiles) {
		if (tiles [adj_tiles] == null) {
			return "cleansed_mid";
		} else {
			return tiles [adj_tiles];
		}
	}


	public string get_biome_room() {
		int roomnum = UnityEngine.Random.Range (0, rooms.Count);
		return (rooms [roomnum]);
	}
}
