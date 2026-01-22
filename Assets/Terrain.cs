using UnityEngine;


public class Terrain
{


    public Terrain() {

    }

    public Mesh Regenerate(Vector2Int vertexSize, Vector2Int mapSize, bool mirrorGenerate, float textureSize, Texture2D heightMap,float maxHeight)
    {
        Mesh mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        Vector3[] verticies = new Vector3[vertexSize.x * vertexSize.y];
        Color[] colors = new Color[verticies.Length];
        Vector2[] uvs = new Vector2[verticies.Length];
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
                float vertexHeight = heightMap.GetPixel((heightMap.width / vertexSize.x) * x, (heightMap.height / vertexSize.y) * y).r;
                Vector3 vertexPos = new Vector3((x - vertexSize.x / 2f) * mapSize.x, vertexHeight * maxHeight, (y - vertexSize.y / 2f) * mapSize.y);
                verticies[vertexIndex] = vertexPos;
                colors[vertexIndex] = GetColorAtHeight(vertexHeight);

                uvs[vertexIndex] = new Vector2((x / (float)vertexSize.x) * textureSize, (y / (float)vertexSize.y) * textureSize);

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
        mesh.SetUVs(0, uvs);
        
        //mesh.RecalculateNormals();
        //mesh.RecalculateBounds();
        
        mesh.colors = colors;
        return mesh;
    }

    private void GenerateTriangles(bool mirrorGenerate, int[] triangles, int vertexIndex, int triangleIndex, Vector2Int vertexSize)
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

    private Color GetColorAtHeight(float vertexHeight) {
        if(vertexHeight <= 0.3f) {
            return Color.red;
        } else if (vertexHeight <= 0.7f) {
            return Color.yellow;
        }

        return Color.green;
    }

    public Texture2D GenerateHeightMap(Vector2Int textureSize, float terrainSize)
    {
        Texture2D heightMap = new Texture2D(textureSize.x, textureSize.y);
        int randomSeed = Random.Range(-10000, 10000);
        
        for (int y = 0; y < textureSize.y; y++)
        {
            for (int x = 0; x < textureSize.x; x++)
            {
                float heightValue = Mathf.PerlinNoise(((float)x + randomSeed)/ textureSize.x * terrainSize, ((float)y + randomSeed)/ textureSize.y * terrainSize);
                Color pixelColor = new Color(heightValue, heightValue, heightValue);
                heightMap.SetPixel(x, y, pixelColor);
            }
        }
        
        heightMap.Apply();
        return heightMap;
    }
}
