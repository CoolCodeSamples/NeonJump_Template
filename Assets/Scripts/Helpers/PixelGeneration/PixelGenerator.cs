using System;
using UnityEngine;

public class PixelGenerator : MonoBehaviour
{
    // 3D Generation

    public static GameObject Generate(Transform parent, PlayerShapeType shapeType, Material material = null)
    {
        string[] shapeString = PlayerShapes.GetShape(shapeType);

        GameObject pixelPrefab = CreatePixelPrefab(material);
        GameObject pixels = CreatePixels(pixelPrefab, parent, shapeString);
        ColliderGenerator.Generate(parent);

        Destroy(pixelPrefab);
        
        return pixels;
    }
    
    public static GameObject Generate(Transform parent, string shapeTypeName, Material material = null)
    {
        PlayerShapeType shapeType = (PlayerShapeType)Enum.Parse(typeof(PlayerShapeType), shapeTypeName);
        return Generate(parent, shapeType, material);
    }

    private static GameObject CreatePixelPrefab(Material material)
    {
        GameObject pixel = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pixel.GetComponent<Collider>().enabled = false;
        Rigidbody rb = pixel.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        if (material != null) pixel.GetComponent<MeshRenderer>().material = material;

        return pixel;
    }

    // 2D Generation

    public static GameObject Generate2D(Transform parent, string shapeTypeName)
    {
        PlayerShapeType shapeType = (PlayerShapeType)Enum.Parse(typeof(PlayerShapeType), shapeTypeName);
        return Generate2D(parent, shapeType, Color.white);
    }

    public static GameObject Generate2D(Transform parent, string shapeTypeName, Color color)
    {
        PlayerShapeType shapeType = (PlayerShapeType)Enum.Parse(typeof(PlayerShapeType), shapeTypeName);
        return Generate2D(parent, shapeType, color);
    }

    public static GameObject Generate2D(Transform parent, PlayerShapeType shapeType)
    {
        return Generate2D(parent, shapeType, Color.white);
    }

    public static GameObject Generate2D(Transform parent, PlayerShapeType shapeType, Color color)
    {
        string[] shapeString = PlayerShapes.GetShape(shapeType);

        GameObject pixelPrefab = CreatePixelPrefab2D(color);
        GameObject pixels = CreatePixels(pixelPrefab, parent, shapeString);
        ColliderGenerator.Generate2D(parent);

        Destroy(pixelPrefab);

        return pixels;
    }

    private static GameObject CreatePixelPrefab2D(Color color)
    {
        GameObject pixel = new GameObject("Pixel2D");
        SpriteRenderer renderer = pixel.AddComponent<SpriteRenderer>();

        // Creating the texture at 100x100, because that ends up as the same size as the 3D pixel
        Texture2D texture = new Texture2D(100, 100);
        texture.SetPixel(0, 0, color);
        texture.Apply();

        Collider2D collider = pixel.AddComponent<BoxCollider2D>();
        collider.enabled = false;

        Rigidbody2D rb = pixel.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        // Create a sprite from the texture
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        renderer.sprite = sprite;

        return pixel;
    }

    // Helpers

    private static GameObject CreatePixels(GameObject pixelPrefab, Transform parent, string[] playerShape)
    {
        Transform pixelHolder = new GameObject("Pixels").transform;
        pixelHolder.gameObject.AddComponent<Explode>();

        for (int y = 0; y < playerShape.Length; y++)
        {
            char[] chars = playerShape[y].ToCharArray();
            Vector3 spawnOffset = new Vector3(-chars.Length / 2, playerShape.Length / 2, 0);

            for (int x = 0; x < chars.Length; x++)
            {
                if (chars[x] != ' ')
                {
                    Vector3 localPosition = new Vector3(x, -y, 0) + spawnOffset;
                    Transform currentPixel = Instantiate(pixelPrefab, pixelHolder).transform;
                    currentPixel.localPosition = localPosition;
                }
            }

            SetParentAndReset(parent, pixelHolder);
        }

        return pixelHolder.gameObject;
    }

    private static void SetParentAndReset(Transform parent, Transform child)
    {
        child.SetParent(parent);
        child.localScale = Vector3.one;
        child.localPosition = Vector3.zero;
        child.localRotation = Quaternion.identity;
    }
}
