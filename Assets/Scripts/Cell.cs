using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public bool N { get; set; }
    public bool E { get; set; }
    public bool S { get; set; }
    public bool W { get; set; }
    public bool Frontier { get; set; }
    public bool Maze { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int TileNumber()
    {
        int count = 0;
        if (N)
        {
            count += 1;
        }
        if (S)
        {
            count += 2;
        }
        if (E)
        {
            count += 4;
        }
        if (W)
        {
            count += 8;
        }

        return count;
    }

    public Cell(bool north, bool east, bool south, bool west, bool frontier, bool maze, int x, int y)
    {
        north = N;
        east = E;
        south = S;
        west = W;
        frontier = Frontier;
        maze = Maze;
        x = X;
        y = Y;
    }
}