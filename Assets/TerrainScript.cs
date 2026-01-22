using UnityEngine;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TerrainScript : MonoBehaviour
{
    private Terrain terrain;

    [SerializeField] private Vector2Int vertexSize;
    [SerializeField] private Vector2Int mapSize;
    [SerializeField] private float maxHeight;
    [SerializeField] private bool mirrored;

    [Space]

    [SerializeField] private bool randomMap;
    [SerializeField] private float terrainSize;
    [SerializeField] private Texture2D heightMap;

    [Space]

    [SerializeField] private Texture texture;
    [SerializeField] private float textureSize;

    public void Regenerate()
    {
        if (terrain == null) terrain = new Terrain();
        if (randomMap) heightMap = terrain.GenerateHeightMap(vertexSize, terrainSize);
        Mesh mesh = terrain.Regenerate(vertexSize, mapSize, mirrored, textureSize, heightMap, maxHeight);
        mesh.name = "TerrainMesh";
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().sharedMaterial.mainTexture = texture;
    }
        

    
    void Start()
    {
        Regenerate();
    }

    
    void Update()
    {
    }
}
