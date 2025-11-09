using UnityEngine;

public class ZombieHitbox : MonoBehaviour
{
    private ZombieController zombieController;

    void Start()
    {
        // Buscar el componente principal del zombi (en el padre)
        zombieController = GetComponentInParent<ZombieController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si choca con un proyectil
        ProjectilePea guisante = other.GetComponent<ProjectilePea>();
        if (guisante != null)
        {
            // Aplica daño al zombi
            if (zombieController != null)
            {
                zombieController.RecibirDaño(guisante.daño);
                Debug.Log($"💥 Guisante impactó a {zombieController.name}, daño: {guisante.daño}");
            }

            // Destruye el proyectil tras el impacto
            Destroy(guisante.gameObject);
        }
    }
}
