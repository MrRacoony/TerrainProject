using UnityEngine;


public class Terrain
{
    public Mesh Regenerate(Vector2Int vertexSize)
    {
        Mesh mesh = new Mesh();
        
        Vector3[] verticies = new Vector3[vertexSize.x * vertexSize.y];
        int[] triangles = new int[(vertexSize.x - 1) * (vertexSize.y - 1) * 6];

        int vertexIndex = 0;
        int triangleIndex = 0;
        
        if (triangles.Length <= 0)
        {
            Debug.LogError("Vertex size too small");
            return null;
        }
        
        for (int y = 1; y <= vertexSize.y; y++)
        {
            for (int x = 1; x <= vertexSize.x; x++)
            {
                verticies[vertexIndex] = new Vector3(x, 0, y);
                
                //Debug.Log(verticies[vertexIndex]);

                if (x != vertexSize.x && y != vertexSize.y)
                {
                    // First triangle
                    triangles[triangleIndex] = vertexIndex;
                    triangles[triangleIndex + 1] = vertexIndex + 1 + vertexSize.x;
                    triangles[triangleIndex + 2] = vertexIndex + 1;
                    
                    // Debug.Log($"{triangles[triangleIndex]} + {triangles[triangleIndex + 1]} + {triangles[triangleIndex + 2]}");
                    
                    // Second triangle
                    triangles[triangleIndex + 3] = vertexIndex;
                    triangles[triangleIndex + 4] = vertexIndex + vertexSize.x;
                    triangles[triangleIndex + 5] = vertexIndex + vertexSize.x + 1;

                    // Debug.Log($"{triangles[triangleIndex + 3]} + {triangles[triangleIndex + 4]} + {triangles[triangleIndex + 5]}");
                    
                    triangleIndex += 6;
                }

                vertexIndex++;
            }
        }

        // Debug.Log(triangles.Length);
        // Debug.Log(verticies.Length);
        
        mesh.SetVertices(verticies);
        mesh.SetIndices(triangles, MeshTopology.Triangles, 0);
        
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        
        return mesh;
    }
}
