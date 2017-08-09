using UnityEngine;
using System.Collections;

public class MapGen : MonoBehaviour {

    int size_x;
    int size_z;
    float tileSize;
    //int[,] caveMap;

    public MapGeneration mapSize;
	void Start ()
    {
        size_x = mapSize.height;
        size_z = mapSize.width;
        tileSize = mapSize.sqSize;
        //caveMap = mapSize.map

        BuildMesh();
        BuildTexture();
	}

    void BuildTexture()
    {
        int texWidth = size_x;
        int texHeight = size_z;

        Texture2D texture = new Texture2D(texWidth, texHeight);

        for (int x = 0; x < size_x; x++)
            for (int z = 0; z < size_z; z++)
            {
                Color p = new Color(0.3f, 0.9f, 0.1f);
                texture.SetPixel(x, z, p);
            }

        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.Apply();

        MeshRenderer mesh_renderer = GetComponent<MeshRenderer>();
        mesh_renderer.sharedMaterials[0].mainTexture = texture;
    }

    public void BuildMesh()
    {
        int vsize_x = size_x + 1;
        int vsize_z = size_z + 1;
        int numVerts = vsize_x * vsize_z;
        int numTiles = size_x * size_z;
        int numTris = numTiles * 2;

        Vector3[] vertices = new Vector3[numVerts];
        Vector3[] normals = new Vector3[numVerts];
        Vector2[] uv = new Vector2[numVerts];

        int[] triangles = new int[numTris * 3];
        int x, z;

        for (z = 0; z < vsize_z; z++)
            for (x = 0; x < vsize_x; x++)
            {
                vertices[z * vsize_x + x] = new Vector3(-size_x / 2 + x * tileSize, 0, -size_z / 2 + z * tileSize);
                normals[z * vsize_x + x] = Vector3.up;
                uv[z * vsize_x + x] = new Vector2((float)x / size_x, (float)z / size_z);
            }

        for (z = 0; z < size_z; z++)
            for (x = 0; x < size_x; x++)
            {
                int squareIndex = z * size_x + x;
                int triOffset = squareIndex * 6;

                triangles[triOffset + 0] = z * vsize_x + x +            0;
                triangles[triOffset + 1] = z * vsize_x + x + vsize_x +  0;
                triangles[triOffset + 2] = z * vsize_x + x + vsize_x +  1;

                triangles[triOffset + 3] = z * vsize_x + x +            0;
                triangles[triOffset + 4] = z * vsize_x + x + vsize_x +  1;
                triangles[triOffset + 5] = z * vsize_x + x +            1;
            }


        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;

        MeshFilter mesh_filter = GetComponent<MeshFilter>();
        mesh_filter.mesh = mesh;

        MeshCollider mesh_collider = GetComponent<MeshCollider>();
        mesh_collider.sharedMesh = mesh;

    }

   
}
