using System;


public class Maze
{

    public class Cell
    {
        public bool wayNorth;
        public bool waySouth;
        public bool wayEast;
        public bool wayWest;
        public bool processed;
    }

    private Cell[] cells;

    private int width;
    private int height;

	private delegate void CardinalPoint(int x, int y);
	private CardinalPoint[] cardinalPoints;


    public int Width
    {
        get
        {
            return this.width;
        }
    }

    public int Height
    {
        get
        {
            return this.height;
        }
    }

    private Random rand;

    public Maze(int width, int height)
    {
        this.width = width;
        this.height = height;

		// fill the cells array
        this.cells = new Cell[width * height];
        for (int i = 0; i < (width * height); i++)
        {
            this.cells[i] = new Cell();
        }

		this.cardinalPoints = new CardinalPoint[] {
			this.ManageNorth,
			this.ManageSouth,
			this.ManageEast,
			this.ManageWest
		};

		this.rand = new System.Random();

        this.RandomCarve();
    }

    public Cell GetCell(int x, int y)
    {
        if (x < 0 || x > this.width - 1)
            throw new Exception("Invalid x coordinate for Maze cell");

        if (y < 0 || y > this.height - 1)
            throw new Exception("Invalid y coordinate for Maze cell");

        int pos = (y * this.width) + x;

        return this.cells[pos];
    }

	// choose a random starting cell to carve
    private void RandomCarve()
    {
        int randomCell = this.rand.Next(this.cells.Length);
        int x = randomCell % this.width;
        int y = randomCell / this.width;

        this.ManageCell(x, y);
    }

    private void ManageCell(int x, int y)
    {

		// shuffle the cardinal points array of funcs
		for (int i = 0; i < cardinalPoints.Length; i++)
        {
			int randomPos = this.rand.Next(i, cardinalPoints.Length);
			CardinalPoint tmpPoint = cardinalPoints[randomPos];
			cardinalPoints[randomPos] = cardinalPoints[i];
			cardinalPoints[i] = tmpPoint;
        }

		// call each of the cardinal point funcs
		foreach(CardinalPoint pointFunc in cardinalPoints)
			pointFunc(x, y);

    }

	// mark a cell as processed
    private void MarkCell(int x, int y)
    {
        int pos = (y * this.width) + x;

        this.cells[pos].processed = true;
    }

	// carve the north wall
    private void OpenNorth(int x, int y)
    {
        int pos = (y * this.width) + x;

        this.cells[pos].wayNorth = true;
        this.MarkCell(x, y);
    }

	// carve the south wall
    private void OpenSouth(int x, int y)
    {
        int pos = (y * this.width) + x;

        this.cells[pos].waySouth = true;
        this.MarkCell(x, y);
    }

	// carve the east wall
    private void OpenEast(int x, int y)
    {
        int pos = (y * this.width) + x;

        this.cells[pos].wayEast = true;
        this.MarkCell(x, y);
    }

	// carve the west wall
    private void OpenWest(int x, int y)
    {
        int pos = (y * this.width) + x;

        this.cells[pos].wayWest = true;
        this.MarkCell(x, y);
    }


    private void ManageNorth(int x, int y)
    {
        if (y < 1)
            return;

        Cell northCell = GetCell(x, y - 1);

        if (northCell.processed)
            return;

        this.OpenNorth(x, y);
        this.OpenSouth(x, y - 1);

        this.ManageCell(x, y - 1);
    }

    private void ManageSouth(int x, int y)
    {
        if (y >= this.height - 1)
            return;

        Cell southCell = this.GetCell(x, y + 1);

        if (southCell.processed)
            return;

        this.OpenSouth(x, y);
	this.OpenNorth(x, y + 1);

        this.ManageCell(x, y + 1);
	}

    private void ManageEast(int x, int y)
    {
        if (x >= this.width - 1)
            return;

		Cell eastCell = this.GetCell(x + 1, y);

        if (eastCell.processed)
            return;

        this.OpenEast(x, y);
        this.OpenWest(x + 1, y);

        this.ManageCell(x + 1, y);
    }

    private void ManageWest(int x, int y)
    {
        if (x < 1)
            return;

        Cell westCell = this.GetCell(x - 1, y);

        if (westCell.processed)
            return;

        this.OpenWest(x, y);
        this.OpenEast(x - 1, y);

        this.ManageCell(x - 1, y);
    }

}
