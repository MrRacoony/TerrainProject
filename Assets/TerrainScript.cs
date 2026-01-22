using UnityEngine;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TerrainScript : MonoBehaviour
{
    private Terrain terrain;

    [SerializeField] private Vector2Int vertexSize;
    [SerializeField] private Vector2Int mapSize;
    [SerializeField] private bool mirrored;

    [SerializeField] private bool randomMap;
    [SerializeField] private float terrainSize;
    [SerializeField] private Texture2D heightMap;


    public void Regenerate()
    {
        if (terrain == null) terrain = new Terrain();
        if (randomMap) heightMap = terrain.GenerateHeightMap(vertexSize, terrainSize);
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
