using UnityEngine;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TerrainScript : MonoBehaviour
{
    private Terrain terrain;

    [SerializeField] private Vector2Int vertexSize;
    [SerializeField] private Vector2 mapSize;
    [SerializeField] private bool mirrored;

    [SerializeField] private Texture2D heightMap;


    public void Regenerate()
    {
        if (terrain == null) terrain = new Terrain();
        if (heightMap == null) heightMap = terrain.GenerateHeightMap(vertexSize);
        Mesh mesh = terrain.Regenerate(vertexSize, mapSize, mirrored, heightMap);
        mesh.name = "TerrainMesh";
        GetComponent<MeshFilter>().mesh = mesh;
    }
        

    
    void Start()
    {
        Regenerate();
    }

    
    void Update()
    {
    }
}
