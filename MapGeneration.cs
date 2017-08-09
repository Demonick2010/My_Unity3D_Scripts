using UnityEngine;
using System.Collections;
using System;

public class MapGeneration : MonoBehaviour {


    public int width;
    public int height;

    [Range(0.2f, 1f)]//
    public float sqSize;//
    public int xt, zt;//
    public Vector3 fw;//

    public string seed;
    public bool UseRandomSeed;

    [Range (0, 100)]
    public int RandomFillPercent;

    public int[,] map;


	void Start ()
    {

        GenerateMap();
	
	}
	
	
    /*void DestructMap()
    {
        fw = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        xt = (int)(fw.x / sqSize + (width / 2));
        zt = (int)(fw.z / sqSize + (width / 2));

        if (((xt > 0) || (xt < width - 1) || (zt > 0) || (zt < height - 1) && (map[xt, zt] == 1)))
        {
            map[xt, zt] = 0;

            MeshGeneration meshGen = GetComponent<MeshGeneration>();
            meshGen.GenerateMesh(map, sqSize);
        }
    }

    void ConstructMap()
    {
        fw = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        xt = (int)(fw.x / sqSize + (width / 2));
        zt = (int)(fw.z / sqSize + (height / 2));

        if (((xt > 0) || (xt < width - 1) || (zt < height - 1) && (map[xt, zt] == 0)))
        {
            map[xt, zt] = 1;

            MeshGeneration meshGen = GetComponent<MeshGeneration>();
            meshGen.GenerateMesh(map, sqSize);
        }
    }*/

    void GenerateMap ()
    {
        map = new int[width, height];
        RandomFillMap();

        for (int i = 0; i < 5; i++)
        {
            SmoothMap();
        }

        MeshGeneration mashGen = GetComponent<MeshGeneration>();
        mashGen.GenerateMesh(map, 1f); // 0.1f
    }

    void RandomFillMap ()
    {
        if (UseRandomSeed)
        {
            seed = Time.time.ToString();
        }

        System.Random PseudoRandom = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++) //width
            {
                if ((x == 0) || (width == -1) || (y == 0) || (y == height -1))
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = (PseudoRandom.Next(0, 100) < RandomFillPercent) ? 1 : 0;
                }
            }
        }
    }

    void SmoothMap ()
    {
        for (int x = 0; x < width; x++)
            for(int y = 0; y < height; y++)
            {
                int neighbourWallTiles = GetSorroundWallCount(x, y);

                if (neighbourWallTiles > 4)
                    map[x, y] = 1;
                else if (neighbourWallTiles < 4)
                    map[x, y] = 0;
            }
    }

    int GetSorroundWallCount(int GridX, int GridY)
    {
        int WallCount = 0;
            for (int neighbourX = GridX - 1; neighbourX <= GridX + 1; neighbourX ++)
                for (int neighbourY = GridY - 1; neighbourY <= GridY + 1; neighbourY ++)
            {
                if ((neighbourX >= 0) && (neighbourX < width) && (neighbourY >= 0) && (neighbourY < height))
                {
                    if ((neighbourX != GridX) || (neighbourY != GridY))
                        WallCount += map[neighbourX, neighbourY];
                }
                else
                {
                    WallCount++;
                }
            }
        return WallCount;
    }
}
