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

    [Space]

    [SerializeField] private Color bottomColor;
    [SerializeField] private Color middleColor;
    [SerializeField] private Color topColor;

    [SerializeField] private float middleColorCutOff, topColorCutOff;

    [Space]

    [SerializeField] private bool island;
    [SerializeField] private Vector2Int islandCenter;
    [SerializeField] private float islandCurve;

    public void Regenerate()
    {
        terrain = new Terrain(bottomColor, middleColor, topColor, middleColorCutOff, topColorCutOff);
        if (randomMap) heightMap = terrain.GenerateHeightMap(vertexSize, terrainSize, island, islandCenter, islandCurve);
        Mesh mesh = terrain.Regenerate(vertexSize, mapSize, mirrored, textureSize, heightMap, maxHeight, island, islandCenter, islandCurve);
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
