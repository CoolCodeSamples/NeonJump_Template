using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect;
    private float speed = 30;

    public void Activate()
    {
        GameObject parent = transform.parent.gameObject;
        transform.SetParent(null);
        Destroy(parent);

        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        if (is3D())
        {
            Explode3D();
        }
        else
        {
            Explode2D();
        }

    }

    private void Explode3D()
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        Collider[] colliders = GetComponentsInChildren<Collider>();
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].isKinematic = false;
            Vector3 awayFromCenter = (rigidbodies[i].transform.position - transform.position).normalized;
            rigidbodies[i].velocity = awayFromCenter * speed;
        }
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = true;
        }
    }

    private void Explode2D()
    {
        Rigidbody2D[] rigidbodies = GetComponentsInChildren<Rigidbody2D>();
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].bodyType = RigidbodyType2D.Dynamic;
            Vector3 awayFromCenter = (rigidbodies[i].transform.position - transform.position).normalized;
            rigidbodies[i].velocity = awayFromCenter * speed;
        }
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = true;
        }
    }

    private bool is3D()
    {
        MeshRenderer renderer = GetComponentInChildren<MeshRenderer>();
        return renderer != null;
    }
}
