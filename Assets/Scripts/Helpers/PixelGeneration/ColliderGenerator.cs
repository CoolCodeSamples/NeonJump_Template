using System.Collections.Generic;
using UnityEngine;

public class ColliderGenerator : MonoBehaviour
{
    public static void Generate(Transform parent)
    {
        GenerateInternal(parent, is3D: true);
    }

    public static void Generate2D(Transform parent)
    {
        GenerateInternal(parent, is3D: false);
    }

    private static void GenerateInternal(Transform parent, bool is3D)
    {
        /* 
         * The transform needs to be set to the default values and recreated afterwards.
         * Otherwise the collider will have an unwanted offset.
         */

        Vector3 startPos = parent.localPosition;
        Quaternion startRotation = parent.localRotation;
        Vector3 startScale = parent.localScale;
        ResetTransform(parent);

        if (is3D) GenerateMeshCollider(parent);
        else GeneratePolygonCollider2D(parent);

        RecreateTransform(parent, startPos, startRotation, startScale);
    }

    private static void ResetTransform(Transform t)
    {
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        t.localScale = Vector3.one;
    }

    private static void RecreateTransform(Transform original, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        original.localPosition = position;
        original.localRotation = rotation;
        original.localScale = scale;
    }

    private static void GenerateMeshCollider(Transform parent)
    {
        MeshFilter[] meshFilters = parent.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        for (int i = meshFilters.Length - 1; i >= 0; i--)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
        }

        MeshCollider collider = parent.gameObject.AddComponent<MeshCollider>();
        collider.sharedMesh = new Mesh();
        collider.sharedMesh.CombineMeshes(combine);
        collider.convex = true;
    }

    private static void GeneratePolygonCollider2D(Transform parent)
    {
        // Add a new PolygonCollider2D component to the parent
        PolygonCollider2D collider = parent.gameObject.AddComponent<PolygonCollider2D>();

        // Temporary list to store all paths
        List<Vector2> allPaths = new List<Vector2>();

        // Combine all 2D polygons (assume each child has a SpriteRenderer with a polygon sprite)
        foreach (Transform child in parent)
        {
            allPaths.Add(child.transform.position);
            //SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            //if (spriteRenderer != null && spriteRenderer.sprite != null)
            //{
            //    Vector2[] spriteVertices = spriteRenderer.sprite.vertices;
            //    Vector2[] worldVertices = new Vector2[spriteVertices.Length];

            //    // Transform the vertices to world space
            //    for (int i = 0; i < spriteVertices.Length; i++)
            //    {
            //        Vector3 worldPoint = child.TransformPoint(spriteVertices[i]);
            //        worldVertices[i] = parent.InverseTransformPoint(worldPoint);
            //    }

            //    // Add path to the temporary list
            //    allPaths.Add(worldVertices);
            //}
        }

        // Assign all paths to the collider
        collider.pathCount = allPaths.Count;
        collider.SetPath(0, points: allPaths);
    }

}
