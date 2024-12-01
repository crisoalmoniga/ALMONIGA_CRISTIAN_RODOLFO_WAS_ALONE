using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] // Esto asegura que el script se ejecute en modo de edici�n
public class TriangleMesh : MonoBehaviour
{
    private Mesh mesh;

    void OnEnable()
    {
        // Crear una nueva malla solo una vez
        mesh = new Mesh();

        // Definir los v�rtices del tri�ngulo
        Vector3[] vertices = new Vector3[3];
        vertices[0] = new Vector3(0, 0, 0);    // V�rtice inferior izquierdo
        vertices[1] = new Vector3(1, 0, 0);    // V�rtice inferior derecho
        vertices[2] = new Vector3(0.5f, 1, 0); // V�rtice superior

        // Definir los tri�ngulos
        int[] triangles = new int[3];
        triangles[0] = 0; // Primer v�rtice
        triangles[1] = 1; // Segundo v�rtice
        triangles[2] = 2; // Tercer v�rtice

        // Asignar los v�rtices y tri�ngulos a la malla
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // Calcular las normales para iluminaci�n
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

        // A�adir material b�sico
        renderer.material = new Material(Shader.Find("Standard"));
    }

    // Esto asegura que el tri�ngulo tambi�n sea visible en la escena en modo edici�n
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (mesh != null)
        {
            Gizmos.DrawMesh(mesh);
        }
    }
}
