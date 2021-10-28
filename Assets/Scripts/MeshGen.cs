using System.Collections.Generic;
using UnityEngine;
namespace sc_t3
{
    public class MeshGen
    {

        private double[,] vertices;
        private int size;

        public MeshGen(double[,] vertices) => this.vertices = vertices;
        public MeshGen(int size)
        {
            vertices = new double[size, size];
            this.size = size;
        }
        
        public void SetVertice(int x, int y, double value)
        {
            if (x < vertices.GetLength(0) && y < vertices.GetLength(1))
                vertices[x, y] = value;
        }
        public void CreateMesh()
        {
            List<Vector3> verts = new List<Vector3>();
            List<int> tris = new List<int>();
            
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    verts.Add(new Vector3(i, (float)vertices[i, j], j));
                    if (i == 0 || j == 0) continue;

                    tris.Add(size * i + j); //Top right
                    tris.Add(size * i + j - 1); //Bottom right
                    tris.Add(size * (i - 1) + j - 1); //Bottom left - First triangle
                    tris.Add(size * (i - 1) + j - 1); //Bottom left 
                    tris.Add(size * (i - 1) + j); //Top left
                    tris.Add(size * i + j); //Top right - Second triangle
                }

            }
            Vector2[] uvs = new Vector2[verts.Count];
            for (var i = 0; i < uvs.Length; i++) //Give UV coords X,Z world coords
                uvs[i] = new Vector2(verts[i].x, verts[i].z);
            GameObject plane = new GameObject("map");
            plane.AddComponent<MeshFilter>();
            plane.AddComponent<MeshRenderer>();
            Mesh procMesh = new Mesh();
            procMesh.vertices = verts.ToArray();
            procMesh.triangles = tris.ToArray();
            procMesh.uv = uvs;
            procMesh.RecalculateNormals();
            plane.GetComponent<MeshFilter>().mesh = procMesh;
            plane.GetComponent<Renderer>().material = Resources.Load<Material>("Standart");

        }

    }
}