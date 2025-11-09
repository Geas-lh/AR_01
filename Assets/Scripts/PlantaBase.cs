using UnityEngine;

public abstract class PlantaBase : MonoBehaviour
{
    [Header("Estadísticas base")]
    public float vida = 100f;
    public float rangoAtaque = 10f;
    public float tiempoEntreAtaques = 1.5f;

    [Header("Objetivo")]
    public Transform puntoZombies; // punto hacia donde apuntará (por carril)

    protected float tiempoUltimoAtaque;

    protected virtual void Start()
    {
        tiempoUltimoAtaque = -tiempoEntreAtaques;
    }

    protected virtual void Update()
    {
        if (puntoZombies != null)
        {
            Vector3 objetivo = new Vector3(puntoZombies.position.x, transform.position.y, puntoZombies.position.z);
            transform.LookAt(objetivo);
        }
    }

    public virtual void RecibirDaño(float cantidad)
    {
        vida -= cantidad;
        if (vida <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
