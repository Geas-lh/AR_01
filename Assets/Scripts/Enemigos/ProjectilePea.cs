using UnityEngine;

public class ProjectilePea : MonoBehaviour
{
    public float velocidad = 8f;
    public float daño = 20f;
    public float tiempoDeVida = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * velocidad;
        Destroy(gameObject, tiempoDeVida);
    }
}
