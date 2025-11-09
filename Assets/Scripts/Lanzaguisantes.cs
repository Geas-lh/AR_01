using UnityEngine;

public class Peashooter : MonoBehaviour
{
    [Header("Ataque")]
    public GameObject peaPrefab;
    public Transform muzzle;
    public float rangoAtaque = 8f;
    public float fireRate = 1.5f;
    public float velocidadRotacion = 5f;

    private float nextFireTime;
    private GameObject objetivoActual;

    void Update()
    {
        objetivoActual = ZombieManager.Instance?.ObtenerZombieMasCercano(transform.position, rangoAtaque);

        if (objetivoActual != null)
        {
            // Rotar suavemente hacia el zombi
            Vector3 direccion = (objetivoActual.transform.position - transform.position).normalized;
            direccion.y = 0; // evitar inclinación
            Quaternion rotacionDeseada = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotacionDeseada, Time.deltaTime * velocidadRotacion);

            // Disparar si toca
            if (Time.time >= nextFireTime)
            {
                Disparar();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void Disparar()
    {
        if (peaPrefab != null && muzzle != null)
        {
            Instantiate(peaPrefab, muzzle.position, muzzle.rotation);
            Debug.Log("🌱 Lanzaguisantes disparó!");
        }
    }
}
