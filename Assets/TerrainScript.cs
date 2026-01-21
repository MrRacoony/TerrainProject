using UnityEngine;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TerrainScript : MonoBehaviour
{
    private Terrain terrain;

    [SerializeField] private Vector2Int vertexSize;
    
    public void Regenerate()
    {
        if (terrain == null) terrain = new Terrain();

        Mesh mesh = terrain.Regenerate(vertexSize);
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
