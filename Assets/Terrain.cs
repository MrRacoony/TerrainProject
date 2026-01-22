using UnityEngine;


public class Terrain
{   
    public Mesh Regenerate(Vector2Int vertexSize, Vector2Int mapSize, bool mirrorGenerate, Texture2D heightMap)
    {
        Mesh mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

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
                Vector3 vertexPos = new Vector3((x - vertexSize.x / 2f) * mapSize.x, heightMap.GetPixel(heightMap.width/x,heightMap.height/y).r, (y - vertexSize.y / 2f) * mapSize.y);
                Debug.Log(heightMap.width/x + " : " + heightMap.height/y);
                verticies[vertexIndex] = vertexPos;

                //Debug.Log(verticies[vertexIndex]);

                if (x != vertexSize.x && y != vertexSize.y)
                {
                    GenerateTriangles(mirrorGenerate, triangles, vertexIndex, triangleIndex, vertexSize);
                    triangleIndex += 6;
                }

                vertexIndex++;
            }
        }

        // Debug.Log(triangles.Length);
        // Debug.Log(verticies.Length);
        
        mesh.SetVertices(verticies);
        mesh.SetIndices(triangles, MeshTopology.Triangles, 0);
        
        //mesh.RecalculateNormals();
        //mesh.RecalculateBounds();
        
        return mesh;
    }

    public void GenerateTriangles(bool mirrorGenerate, int[] triangles, int vertexIndex, int triangleIndex, Vector2Int vertexSize)
    {
        if (mirrorGenerate)
        {
            // First triangle
            triangles[triangleIndex] = vertexIndex;
            triangles[triangleIndex + 1] = vertexIndex + vertexSize.x;
            triangles[triangleIndex + 2] = vertexIndex + 1;

            // Second triangle
            triangles[triangleIndex + 3] = vertexIndex + vertexSize.x;
            triangles[triangleIndex + 4] = vertexIndex + vertexSize.x + 1;
            triangles[triangleIndex + 5] = vertexIndex + 1;
        }
        else
        {
            // First triangle
            triangles[triangleIndex] = vertexIndex;
            triangles[triangleIndex + 1] = vertexIndex + 1 + vertexSize.x;
            triangles[triangleIndex + 2] = vertexIndex + 1;

            // Second triangle
            triangles[triangleIndex + 3] = vertexIndex;
            triangles[triangleIndex + 4] = vertexIndex + vertexSize.x;
            triangles[triangleIndex + 5] = vertexIndex + vertexSize.x + 1;
        }
    }

    public Texture2D GenerateHeightMap(Vector2Int textureSize)
    {
        Texture2D heightMap = new Texture2D(textureSize.x, textureSize.y);
        
        for (int y = 0; y < textureSize.y; y++)
        {
            for (int x = 0; x < textureSize.x; x++)
            {
                float heightValue = Mathf.PerlinNoise((float)x / textureSize.x * 10f, (float)y / textureSize.y * 10f);
                Color pixelColor = new Color(heightValue, heightValue, heightValue);
                heightMap.SetPixel(x, y, pixelColor);
            }
        }
        
        heightMap.Apply();
        return heightMap;
    }
}
