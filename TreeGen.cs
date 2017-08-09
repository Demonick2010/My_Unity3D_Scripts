using UnityEngine;
using System.Collections;

public class TreeGen : MonoBehaviour
{

    int kolTree;
    int width;
    int height;
    float treeSize;

    public int [,] mapTree;
    int kol0;

    public MapGeneration _map;

    void Start ()
    {
        mapTree = _map.map;
        width = _map.width;
        height = _map.height;
        treeSize = _map.sqSize;

        
        PercentCount();
        TreeGenerate();
	}

    void PercentCount()
    {
        for (int x = 1; x < mapTree.GetLength(0) - 1; x++)
            for (int z = 1; z < mapTree.GetLength(1) - 1; z++)
            {
                if ((mapTree[x, z] == 0) && (mapTree[x + 1, z] == 0) && (mapTree[x - 1, z] == 0) &&
                    (mapTree[x, z + 1] == 0) && (mapTree[x + 1, z + 1] == 0) && (mapTree[x - 1, z + 1] == 0) &&
                    (mapTree[x, z - 1] == 0) && (mapTree[x + 1, z - 1] == 0) && (mapTree[x - 1, z - 1] == 0))
                    kol0++; 
            }
        kolTree = (int)kol0 / 10;
    }

    void TreeGenerate()
    {
        for (int i = 0; i < kolTree; i++)
        {
            int xC = Random.Range(1, mapTree.GetLength(0) - 1);
            int zC = Random.Range(1, mapTree.GetLength(1) - 1);

            if ((mapTree[xC, zC] == 1) && (i > 0))
                i--;

            if ((mapTree[xC , zC] == 0) && (mapTree[xC + 1, zC] == 0) && (mapTree[xC - 1, zC] == 0) &&
                    (mapTree[xC, zC + 1] == 0) && (mapTree[xC + 1, zC + 1] == 0) && (mapTree[xC - 1, zC + 1] == 0) &&
                    (mapTree[xC, zC - 1] == 0) && (mapTree[xC + 1, zC - 1] == 0) && (mapTree[xC - 1, zC - 1] == 0))
            {
                GameObject _tree = (GameObject)Instantiate(Resources.Load("Tree"), new Vector3(-width / 2 + xC * treeSize + 0.5f, -0.5f, -height / 2 + zC * treeSize + 0.5f), transform.rotation);
                _tree.transform.localScale = new Vector3(_tree.transform.localScale.x * treeSize, _tree.transform.localScale.z * treeSize, _tree.transform.localScale.z * treeSize);

                mapTree[xC, zC] = 1;

                _map.map[xC, zC] = 2;
                _map.map[xC + 1, zC] = 2;
                _map.map[xC - 1, zC] = 2;
                _map.map[xC, zC + 1] = 2;
                _map.map[xC + 1, zC + 1] = 2;
                _map.map[xC - 1, zC + 1] = 2;
                _map.map[xC, zC - 1] = 2;
                _map.map[xC + 1, zC - 1] = 2;
                _map.map[xC - 1, zC - 1] = 2;
            }
        }
    }
}
