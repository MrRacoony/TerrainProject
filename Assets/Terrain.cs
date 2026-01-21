using UnityEngine;


public class Terrain
{
    public Mesh Regenerate()
    {
        Mesh mesh = new Mesh();

        Vector3[] verticies = new Vector3[4];
        verticies[0] = new Vector3(-1, 0, -1);
        verticies[1] = new Vector3(-1, 0, 1);
        verticies[2] = new Vector3(1, 0, 1);
        verticies[3] = new Vector3(1, 0, -1);

        int[] triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;
        
        mesh.SetVertices(verticies);
        mesh.SetIndices(triangles, MeshTopology.Triangles, 0);
        
        return mesh;
    }
}
