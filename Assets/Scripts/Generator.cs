using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public int dimensionX;
    public int dimensionY;
    public float tileSize;

    public GameObject[] tiles;
    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel(dimensionX, dimensionY);
    }

    private void GenerateLevel(int dimX, int dimY)
    {
        Cell[,] grid = new Cell[dimX, dimY];
        List<Cell> maze = new List<Cell>();
        List<Cell> frontier = new List<Cell>();
        for (int y = 0; y < dimY; y++)
        {
            for (int x = 0; x < dimX; x++)
            {
                grid[x, y] = new Cell(false, false, false, false, false, false, x, y);
                grid[x, y].X = x;
                grid[x, y].Y = y;
            }
        }
        Cell randomCell = grid[Random.Range(0, dimX), Random.Range(0, dimY)];
        //print(randomCell.X + ", " + randomCell.Y);
        
        AddToMaze(randomCell);

        for (int i = 0; i <( dimX * dimY) - 1; i++)
        {
            FrontierCarve();
        }

        for (int y = 0; y < dimY; y++)
        {
            for (int x = 0; x < dimX; x++)
            {
                int directions = grid[x, y].TileNumber();
                transform.position = new Vector3(x * tileSize, 0, y * tileSize);
                Instantiate(tiles[directions - 1], transform.position, transform.rotation);
                //switch (directions)
                //{
                //    case 1:
                //        print("1");
                //        break;
                //    case 2:
                //        print("2");
                //        break;
                //    case 3:
                //        print("3");
                //        break;
                //    case 4:
                //        print("4");
                //        break;
                //    case 5:
                //        print("5");
                //        break;
                //    case 6:
                //        print("6");
                //        break;
                //    case 7:
                //        print("7");
                //        break;
                //    case 8:
                //        print("8");
                //        break;
                //    case 9:
                //        print("9");
                //        break;
                //    case 10:
                //        print("10");
                //        break;
                //    case 11:
                //        print("11");
                //        break;
                //    case 12:
                //        print("12");
                //        break;
                //    case 13:
                //        print("13");
                //        break;
                //    case 14:
                //        print("14");
                //        break;
                //    case 15:
                //        print("15");
                //        break;
                //    default:
                //        print("oops");
                //        break;
                //}
            }
        }

        void AddToMaze(Cell cell)
        {
            cell.Maze = true;
            cell.Frontier = false;
            frontier.Remove(cell);
            maze.Add(cell);

            if (cell.X > 0 && grid[cell.X - 1, cell.Y].Maze == false && grid[cell.X - 1, cell.Y].Frontier == false)
            {
                grid[cell.X - 1, cell.Y].Frontier = true;
                frontier.Add(grid[cell.X - 1, cell.Y]);
            }
            if (cell.X < dimX - 1 && grid[cell.X + 1, cell.Y].Maze == false && grid[cell.X + 1, cell.Y].Frontier == false)
            {
                grid[cell.X + 1, cell.Y].Frontier = true;
                frontier.Add(grid[cell.X + 1, cell.Y]);
            }
            if (cell.Y > 0 && grid[cell.X, cell.Y - 1].Maze == false && grid[cell.X, cell.Y - 1].Frontier == false)
            {
                grid[cell.X, cell.Y - 1].Frontier = true;
                frontier.Add(grid[cell.X, cell.Y - 1]);
            }
            if (cell.Y < dimY - 1 && grid[cell.X, cell.Y + 1].Maze == false && grid[cell.X, cell.Y + 1].Frontier == false)
            {
                grid[cell.X, cell.Y + 1].Frontier = true;
                frontier.Add(grid[cell.X, cell.Y + 1]);
            }
            //print(frontier.Count);


        }
        void FrontierCarve()
        {
            if (frontier.Count > 0)
            {

            }
            Cell cell = frontier[Random.Range(0, frontier.Count)];
            List<Cell> options = new List<Cell>();
            if (cell.X > 0 && grid[cell.X - 1, cell.Y].Maze == true)
            {
                options.Add(grid[cell.X - 1, cell.Y]);
            }
            if (cell.X < dimX - 1 && grid[cell.X + 1, cell.Y].Maze == true)
            {
                options.Add(grid[cell.X + 1, cell.Y]);
            }
            if (cell.Y > 0 && grid[cell.X, cell.Y - 1].Maze == true)
            {
                options.Add(grid[cell.X, cell.Y - 1]);
            }
            if (cell.Y < dimY - 1 && grid[cell.X, cell.Y + 1].Maze == true)
            {
                options.Add(grid[cell.X, cell.Y + 1]);
            }

            //print(options.Count);
            Cell mazeCell = options[Random.Range(0, options.Count)];

            if (cell.X == mazeCell.X)
            {
                if (cell.Y > mazeCell.Y)
                {
                    cell.S = true;
                    mazeCell.N = true;
                }
                else
                {
                    cell.N = true;
                    mazeCell.S = true;
                }
            }
            else
            {
                if (cell.X > mazeCell.X)
                {
                    cell.W = true;
                    mazeCell.E = true;
                }
                else
                {
                    cell.E = true;
                    mazeCell.W = true;
                }
            }

            AddToMaze(cell);

        }
    }


}
