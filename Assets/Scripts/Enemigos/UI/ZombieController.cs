using UnityEngine;

public class ZombieController : MonoBehaviour
{
    [Header("Estadísticas")]
    [SerializeField] private float velocidad = 1.5f;
    [SerializeField] private float vida = 100f;
    [SerializeField] private float distanciaParaDestruir = 0.5f;

    private Transform objetivo;
    private bool muerto = false;

    public void AsignarObjetivo(Transform nuevoObjetivo)
    {
        objetivo = nuevoObjetivo;
    }

    void Start()
    {
        ZombieManager.Instance?.RegistrarZombie(gameObject);
    }

    void Update()
    {
        if (muerto || objetivo == null) return;

        // Movimiento hacia la casa
        Vector3 direccion = (objetivo.position - transform.position).normalized;
        transform.position += direccion * velocidad * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(direccion);

        if (Vector3.Distance(transform.position, objetivo.position) <= distanciaParaDestruir)
        {
            // Si llega a la casa -> Game Over
            GameManager.Instance.OnGameOver();
            Morir(false);
        }
    }

    public void RecibirDaño(float daño)
    {
        if (muerto) return;

        vida -= daño;
        Debug.Log($"🧟 Recibió daño: {daño}, vida restante: {vida}");

        if (vida <= 0)
        {
            Morir(true);
        }
    }

    private void Morir(bool porAtaque)
    {
        if (muerto) return;
        muerto = true;

        // Quitar del manager global
        ZombieManager.Instance?.EliminarZombie(gameObject);

        // Avisar al GameManager si fue eliminado por planta
        if (porAtaque)
            GameManager.Instance.OnZombieMuerto();

        // Efecto visual o animación opcional
        Destroy(gameObject, 0.1f);
    }
}
