using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class GeometryPreprocess : MonoBehaviour
{
    private Dictionary<Mesh, Mesh> _meshTable = new Dictionary<Mesh, Mesh>();

    private void Start()
    {
        var meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = GetGeometryMesh(meshFilter);
    }

    private Mesh GetGeometryMesh(MeshFilter meshFilter)
    {
        Mesh mesh;
        _meshTable.TryGetValue(meshFilter.sharedMesh, out mesh);
        if (mesh == null)
        {
            mesh = GenerateGeometryMesh(meshFilter);
            _meshTable.Add(meshFilter.sharedMesh, mesh);
        }
        return mesh;
    }

    private Mesh GenerateGeometryMesh(MeshFilter meshFilter)
    {
        var sharedMesh = meshFilter.sharedMesh;

        // 三角ポリゴンの数 * 3頂点 = sharedIndicesの数
        var sharedIndices = sharedMesh.GetIndices(0);

        var vertices = new List<Vector3>();
        var indices = new int[sharedIndices.Length];
        var normals = new List<Vector3>();
        var tangents = new List<Vector4>();

        Vector3[] cachedVertices = sharedMesh.vertices;
        Vector3[] cachedNormals = sharedMesh.normals;
        Vector4[] cachedTangents = sharedMesh.tangents;
        Vector2[] cachedUv = sharedMesh.uv;

        // UV
        var uv1 = new List<Vector2>();
        // 三角ポリゴン単位でのIndex
        var uv2 = new List<Vector2>();
        // 三角ポリゴンの中心
        var uv3 = new List<Vector3>();

        for(int i = 0; i < sharedIndices.Length / 3; i++)
        {
            // 三角ポリゴン単位での処理(GeometryShaderの代わり)
            // sharedIndicesで取得できる頂点は三角ポリゴンの3頂点順に格納されている
            for(int j = 0; j < 3; j++)
            {
                int n = 3 * i + j;
                int index = sharedIndices[n];
                indices[n] = n;
                vertices.Add(cachedVertices[index]);
                normals.Add(cachedNormals[index]);
                tangents.Add(cachedTangents[index]);
                uv1.Add(cachedUv[index]);
                uv2.Add(new Vector2(i, i));
                uv3.Add((
                    cachedVertices[sharedIndices[3 * i + 0]] +
                    cachedVertices[sharedIndices[3 * i + 1]] +
                    cachedVertices[sharedIndices[3 * i + 2]]) / 3f);
            }
        }

        var mesh = new Mesh();
        mesh.name = sharedMesh.name + " (Geometry)";
        mesh.SetVertices(vertices);
        mesh.SetIndices(indices, MeshTopology.Triangles, 0);
        mesh.SetNormals(normals);
        mesh.SetTangents(tangents);
        mesh.SetUVs(0, uv1);
        mesh.SetUVs(1, uv2);
        mesh.SetUVs(2, uv3);
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        return mesh;
    }
}
