using UnityEngine;
using System.Collections;

public class MazeGenerator : MonoBehaviour {

	private Maze maze;

	private const float planeBaseSize = 10;

	public int mazeWidth = 10;
	public int mazeHeight = 10;

	public float wallSize = 1;
	public float wallHeight = 1;

	public float wallThin = 0.05f;

	public Material[] wallsMaterials;

	private GameObject mazeRoot;


	// Use this for initialization
	void Start() {
		maze = new Maze(mazeWidth, mazeHeight);

		mazeRoot = new GameObject("Maze");

		DrawMaze();
	}
	
	// Update is called once per frame
	void Update() {
	
	}

	private void DrawHorizontalWall(float x, float y, Material material = null) {
		GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
		wall.transform.localScale = new Vector3(wallSize, wallHeight, wallThin);
		wall.transform.position = new Vector3(x, wallHeight / 2, y);
		wall.name = "Horizontal Wall at " + x + "/" + y;
		if (material)
			wall.GetComponent<Renderer>().material = material;
		wall.transform.SetParent(mazeRoot.transform);
	}

	private void DrawVerticalWall(float x, float y, Material material = null) {
		GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
		wall.transform.localScale = new Vector3(wallThin, wallHeight, wallSize);
		wall.transform.position = new Vector3(x, wallHeight / 2, y);
		wall.name = "Vertical Wall at " + x + "/" + y;
		if (material)
			wall.GetComponent<Renderer>().material = material;
		wall.transform.SetParent(mazeRoot.transform);
	}

	private void DrawMaze() {
		for (float y = 0; y < maze.Height; y++) {
			for (float x = 0; x < maze.Width; x++) {
               
				Maze.Cell cell = maze.GetCell((int)x, (int)y);

				if (!cell.wayNorth && y == 0) {
					float wallX = x * wallSize - maze.Width * wallSize / 2f + wallSize / 2f;
					float wallY = -y * wallSize + maze.Height * wallSize / 2f - wallThin / 2f;
					this.DrawHorizontalWall(wallX, wallY, GetRandomMaterial());
				}

				if (!cell.waySouth) {
					float wallX = x * wallSize - maze.Width * wallSize / 2f + wallSize / 2f;
					float wallY = -y * wallSize + maze.Height * wallSize / 2f - wallSize + wallThin / 2f;
					this.DrawHorizontalWall(wallX, wallY, GetRandomMaterial());
				}
               
				if (!cell.wayWest && x == 0) {
					float wallX = x * wallSize - maze.Width * wallSize / 2f + wallThin / 2f;
					float wallY = -y * wallSize + maze.Height * wallSize / 2f - wallSize / 2f;
					this.DrawVerticalWall(wallX, wallY, GetRandomMaterial());
				}

				if (!cell.wayEast) {
					float wallX = x * wallSize - maze.Width * wallSize / 2f + wallSize - wallThin / 2f;
					float wallY = -y * wallSize + maze.Height * wallSize / 2f - wallSize / 2f;
					this.DrawVerticalWall(wallX, wallY, GetRandomMaterial());
				}


			}
		}
	}

	private Material GetRandomMaterial() {
		if (wallsMaterials.Length == 0)
			return null;
		return wallsMaterials [Random.Range(0, wallsMaterials.Length)];
	}
}
