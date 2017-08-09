using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic; //

public class MeshGeneration : MonoBehaviour
{

    public SquareGrid squareGrid;
    List<Vector3> vertices; //
    List<int> triangles; //

    public void GenerateMesh(int[,] map, float squareSize)
    {
        squareGrid = new SquareGrid(map, squareSize);

        vertices = new List<Vector3>(); //
        triangles = new List<int>(); //

        for (int x = 0; x < squareGrid.squares.GetLength(0); x++) //
            for (int y = 0; y < squareGrid.squares.GetLength(1); y++) //
            {
                TriangulateSquare(squareGrid.squares[x, y]); //
            }

        Mesh mesh = new Mesh(); //
        GetComponent<MeshFilter>().mesh = mesh; //

        mesh.vertices = vertices.ToArray(); //
        mesh.triangles = triangles.ToArray(); //
        mesh.RecalculateNormals(); //

        MeshCollider mesh_collider = GetComponent<MeshCollider>();
        mesh_collider.sharedMesh = mesh;
    }


   /**/ void TriangulateSquare (Square square)
    {
        switch (square.configuration)
        {
            case 0:
                break;


            // 1 points:

            case 1:
                MeshFromPoints(square.centreBottom, square.buttomLeft, square.centreLeft);
                MeshFromPoints(square.centreBottom, square.centreLeft, square.WcentreLeft, square.WcentreBottom);
                break;
            case 2:
                MeshFromPoints(square.centreRight, square.buttomRight, square.centreBottom);
                MeshFromPoints(square.centreRight, square.centreBottom, square.WcentreBottom, square.WcentreRight);
                break;
            case 4:
                MeshFromPoints(square.centreTop, square.topRight, square.centreRight);
                MeshFromPoints(square.centreTop, square.centreRight, square.WcentreRight, square.WcentreTop);
                break;
            case 8:
                MeshFromPoints(square.topLeft, square.centreTop, square.centreLeft);
                MeshFromPoints(square.centreLeft, square.centreTop, square.WcentreTop, square.WcentreLeft);
                break;


            // 2 points:

            case 3:
                MeshFromPoints(square.centreRight, square.buttomRight, square.buttomLeft, square.centreLeft);
                MeshFromPoints(square.centreRight, square.centreLeft, square.WcentreLeft, square.WcentreRight);
                break;
            case 6:
                MeshFromPoints(square.centreTop, square.topRight, square.buttomRight, square.centreBottom);
                MeshFromPoints(square.centreTop, square.centreBottom, square.WcentreBottom, square.WcentreTop);
                break;
            case 9:
                MeshFromPoints(square.topLeft, square.centreTop, square.centreBottom, square.buttomLeft);
                MeshFromPoints(square.centreBottom, square.centreTop, square.WcentreTop, square.WcentreBottom);
                break;
            case 12:
                MeshFromPoints(square.topLeft, square.topRight, square.centreRight, square.centreLeft);
                MeshFromPoints(square.centreLeft, square.centreRight, square.WcentreRight, square.WcentreLeft);
                break;
            case 5:
                MeshFromPoints(square.centreTop, square.topRight, square.centreRight, square.centreBottom, square.buttomLeft, square.centreLeft);
                MeshFromPoints(square.centreTop, square.centreLeft, square.WcentreRight, square.WcentreTop);
                MeshFromPoints(square.centreBottom, square.centreRight, square.WcentreRight, square.WcentreBottom);
                break;
            case 10:
                MeshFromPoints(square.topLeft, square.centreTop, square.centreRight, square.buttomRight, square.centreBottom, square.centreLeft);
                MeshFromPoints(square.centreRight, square.centreTop, square.WcentreTop, square.WcentreRight);
                MeshFromPoints(square.centreLeft, square.centreBottom, square.WcentreBottom, square.WcentreLeft);
                break;


            // 3 points:

            case 7:
                MeshFromPoints(square.centreTop, square.topRight, square.buttomRight, square.buttomLeft, square.centreLeft);
                MeshFromPoints(square.centreTop, square.centreLeft, square.WcentreLeft, square.WcentreTop);
                break;
            case 11:
                MeshFromPoints(square.topLeft, square.centreTop, square.centreRight, square.buttomRight, square.buttomLeft);
                MeshFromPoints(square.centreRight, square.centreTop, square.WcentreTop, square.WcentreRight);
                break;
            case 13:
                MeshFromPoints(square.topLeft, square.topRight, square.centreRight, square.centreBottom, square.buttomLeft);
                MeshFromPoints(square.centreBottom, square.centreRight, square.WcentreRight, square.WcentreBottom);
                break;
            case 14:
                MeshFromPoints(square.topLeft, square.topRight, square.buttomRight, square.centreBottom, square.centreLeft);
                MeshFromPoints(square.centreLeft, square.centreBottom, square.WcentreBottom, square.WcentreLeft);
                break;


            // 4 points:

            case 15:
                MeshFromPoints(square.topLeft, square.topRight, square.buttomRight, square.buttomLeft);
                break;
        }
    }


    /**/ void MeshFromPoints(params Node[] points)
    {
        AssignVertices(points);

        if (points.Length >= 3)
            CreateTringle(points[0], points[1], points[2]);
        if (points.Length >= 4)
            CreateTringle(points[0], points[2], points[3]);
        if (points.Length >= 5)
            CreateTringle(points[0], points[3], points[4]);
        if (points.Length >= 6)
            CreateTringle(points[0], points[4], points[5]);
    }


    /**/ void AssignVertices(Node[] points)
    {
        for (int i = 0; i < points.Length; i++)
        {

            if (points[i].vertexIndex == -1)
            {
                points[i].vertexIndex = vertices.Count;
                vertices.Add(points[i].position);
            }
        }
    }


    /**/ void CreateTringle (Node a, Node b, Node c)
    {
        triangles.Add(a.vertexIndex);
        triangles.Add(b.vertexIndex);
        triangles.Add(c.vertexIndex);
    }

    
    void OnDrawGizmos()
    {
       /* if (squareGrid != null)
        {

            for (int x = 0; x < squareGrid.squares.GetLength(0); x++)
                for (int y = 0; y < squareGrid.squares.GetLength(1); y++)
                {
                    Gizmos.color = (squareGrid.squares[x, y].topLeft.active) ? Color.black : Color.white;
                    Gizmos.DrawCube(squareGrid.squares[x, y].topLeft.position, Vector3.one * 0.4f);


                    Gizmos.color = (squareGrid.squares[x, y].topRight.active) ? Color.black : Color.white;
                    Gizmos.DrawCube(squareGrid.squares[x, y].topRight.position, Vector3.one * 0.4f);

                    Gizmos.color = (squareGrid.squares[x, y].buttomRight.active) ? Color.black : Color.white;
                    Gizmos.DrawCube(squareGrid.squares[x, y].buttomRight.position, Vector3.one * 0.4f);

                    Gizmos.color = (squareGrid.squares[x, y].buttomLeft.active) ? Color.black : Color.white;
                    Gizmos.DrawCube(squareGrid.squares[x, y].buttomLeft.position, Vector3.one * 0.4f);


                    Gizmos.color = Color.yellow;
                    Gizmos.DrawCube(squareGrid.squares[x, y].centreTop.position, Vector3.one * 0.15f);
                    Gizmos.DrawCube(squareGrid.squares[x, y].centreRight.position, Vector3.one * 0.15f);
                    Gizmos.DrawCube(squareGrid.squares[x, y].centreBottom.position, Vector3.one * 0.15f);
                    Gizmos.DrawCube(squareGrid.squares[x, y].centreLeft.position, Vector3.one * 0.15f); 

                }
        } */
    }
    


   

    public class SquareGrid
    {
        public Square[,] squares;
        public float scale = 6f;

        public SquareGrid(int[,] map, float squareSize)
        {
            int nodeCountX = map.GetLength(0);
            int nodeCountY = map.GetLength(1);

            float mapWidth = nodeCountX * squareSize;
            float mapHeight = nodeCountY * squareSize;

            ControlNode[,] controlNodes = new ControlNode[nodeCountX, nodeCountY];

            for (int x = 0; x < nodeCountX; x++)
                for (int y = 0; y < nodeCountY; y++)
                {
                    Vector3 pos = new Vector3(-mapWidth / 2 + x * squareSize + squareSize / 2, Mathf.PerlinNoise(x / scale, y / scale), -mapHeight / 2 + y * squareSize + squareSize);
                    controlNodes[x, y] = new ControlNode(pos, map[x, y] == 1, squareSize);
                }

            squares = new Square[nodeCountX - 1, nodeCountY - 1];
            for (int x = 0; x < nodeCountX - 1; x++)
                for (int y = 0; y < nodeCountY - 1; y++)
                {
                    squares[x, y] = new Square(controlNodes[x, y + 1], controlNodes[x + 1, y + 1], controlNodes[x + 1, y], controlNodes[x, y]);
                }
        }
    }


    public class Square
    {
        public ControlNode topLeft, topRight, buttomRight, buttomLeft;
        public Node centreTop, centreRight, centreBottom, centreLeft;
        public Node WcentreTop, WcentreRight, WcentreBottom, WcentreLeft;
        public int configuration;

        public Square(ControlNode _topLeft, ControlNode _topRight, ControlNode _buttomRight, ControlNode _buttomLeft)
        {
            topLeft = _topLeft;
            topRight = _topRight;
            buttomRight = _buttomRight;
            buttomLeft = _buttomLeft;

            centreTop = topLeft.right;
            centreRight = buttomRight.above;
            centreBottom = buttomLeft.right;
            centreLeft = buttomLeft.above;

            WcentreTop = topLeft.wallRight;
            WcentreRight = buttomRight.wallAbove;
            WcentreBottom = buttomLeft.wallRight;
            WcentreLeft = buttomLeft.wallAbove;

            if (topLeft.active)
                configuration += 8;
            if (topRight.active)
                configuration += 4;
            if (buttomRight.active)
                configuration += 2;
            if (buttomLeft.active)
                configuration += 1;
        }
    }





    public class Node
    {
        public Vector3 position;
        public int vertexIndex = -1;

        public Node(Vector3 _pos)
        {
            position = _pos;
        }
    }



    public class ControlNode : Node
    {
        public bool active;
        public Node above, right, wallAbove, wallRight;

        public ControlNode(Vector3 _pos, bool _active, float squareSize) : base(_pos)
        {
            active = _active;
            above = new Node(position + Vector3.forward * squareSize / 2f);
            right = new Node(position + Vector3.right * squareSize / 2f);
            wallAbove = new Node(position + Vector3.forward * squareSize / 2f - new Vector3(0, position.y + 0.5f, 0));
            wallRight = new Node(position + Vector3.right * squareSize / 2f - new Vector3(0, position.y + 0.5f, 0));
        }
    }
}