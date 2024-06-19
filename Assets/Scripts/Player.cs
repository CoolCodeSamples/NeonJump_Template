using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Material playerMaterial;

    private void Awake()
    {
        PixelGenerator.Generate(transform, PlayerShapeType.Fridolin, playerMaterial);
    }
}
