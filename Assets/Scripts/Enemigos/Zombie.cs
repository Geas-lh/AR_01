using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidad = 1.5f;
    [SerializeField] private float distanciaParaDestruir = 0.5f;

    private Transform objetivo;

    private void Start()
    {
        // Buscar el objetivo automáticamente
        GameObject targetObj = GameObject.FindGameObjectWithTag("ObjetivoZombie");
        if (targetObj != null)
        {
            objetivo = targetObj.transform;
            Debug.Log($"🧭 ObjetivoZombie encontrado: {objetivo.name}");
        }
        else
        {
            Debug.LogWarning("❌ No se encontró un objeto con el tag 'ObjetivoZombie'.");
        }

        ZombieManager.Instance?.RegistrarZombie(gameObject);
    }

    private void Update()
    {
        if (objetivo == null)
        {
            Debug.LogWarning("⛔ No hay objetivo asignado al zombi.");
            return;
        }

        // Calcular dirección hacia el objetivo
        Vector3 direccion = (objetivo.position - transform.position).normalized;

        // Moverse hacia él
        transform.position += direccion * velocidad * Time.deltaTime;
        transform.LookAt(objetivo);

        // Debug: línea visible en escena
        Debug.DrawLine(transform.position, objetivo.position, Color.red);

        // Llegó al objetivo
        if (Vector3.Distance(transform.position, objetivo.position) <= distanciaParaDestruir)
        {
            LlegarAlObjetivo();
        }
    }

    private void LlegarAlObjetivo()
    {
        Debug.Log("🧟 El zombi llegó al objetivo.");
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        ZombieManager.Instance?.EliminarZombie(gameObject);
    }
}
