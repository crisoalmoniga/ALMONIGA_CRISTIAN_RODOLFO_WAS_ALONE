using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] // Esto asegura que el script se ejecute en modo de edición
public class TriangleMesh : MonoBehaviour
{
    private Mesh mesh;

    void OnEnable()
    {
        // Crear una nueva malla solo una vez
        mesh = new Mesh();

        // Definir los vértices del triángulo
        Vector3[] vertices = new Vector3[3];
        vertices[0] = new Vector3(0, 0, 0);    // Vértice inferior izquierdo
        vertices[1] = new Vector3(1, 0, 0);    // Vértice inferior derecho
        vertices[2] = new Vector3(0.5f, 1, 0); // Vértice superior

        // Definir los triángulos
        int[] triangles = new int[3];
        triangles[0] = 0; // Primer vértice
        triangles[1] = 1; // Segundo vértice
        triangles[2] = 2; // Tercer vértice

        // Asignar los vértices y triángulos a la malla
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // Calcular las normales para iluminación
        mesh.RecalculateNormals();

        // Asignar el Mesh al MeshFilter del objeto
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            meshFilter = gameObject.AddComponent<MeshFilter>();
        }
        meshFilter.mesh = mesh;

        // Asegurarse de tener un MeshRenderer para mostrar la malla
        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
        if (renderer == null)
        {
            renderer = gameObject.AddComponent<MeshRenderer>();
        }

        // Añadir material básico
        renderer.material = new Material(Shader.Find("Standard"));
    }

    // Esto asegura que el triángulo también sea visible en la escena en modo edición
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (mesh != null)
        {
            Gizmos.DrawMesh(mesh);
        }
    }
}
