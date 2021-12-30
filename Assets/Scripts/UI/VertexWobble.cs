using UnityEngine;
using TMPro;

public class VertexWobble : MonoBehaviour
{
    public TMP_Text textMesh;

    Mesh mesh;

    Vector3[] vertices;

    public float x;
    public float y;
    

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 offset = Wobble(Time.time + i);

            vertices[i] = vertices[i] + offset +  Vector3.one;
        }

        mesh.vertices = vertices;
        textMesh.canvasRenderer.SetMesh(mesh);
    }

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * x), Mathf.Cos(time * y));
    }
}
