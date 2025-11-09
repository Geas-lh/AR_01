using UnityEngine;

public class Girasol : PlantaBase
{
    [Header("Generación de Soles")]
    [SerializeField] private GameObject sunPrefab;       // Prefab del Sol (arrástralo en el inspector)
    [SerializeField] private Transform spawnPoint;       // Punto desde donde saldrá el sol
    [SerializeField] private float tiempoEntreSoles = 5f; // Cada cuántos segundos genera un sol

    private float tiempoUltimoSol;

    protected override void Start()
    {
        base.Start();
        tiempoUltimoSol = -tiempoEntreSoles; // para generar uno pronto al iniciar
    }

    protected override void Update()
    {
        base.Update();

        // Cada cierto tiempo genera un sol
        if (Time.time - tiempoUltimoSol >= tiempoEntreSoles)
        {
            GenerarSol();
            tiempoUltimoSol = Time.time;
        }
    }

    private void GenerarSol()
    {
        if (sunPrefab == null)
        {
            Debug.LogWarning("🌻 Girasol no tiene asignado el prefab de Sol en el inspector.");
            return;
        }

        // Si no hay un punto asignado, lo genera justo encima de la planta
        Vector3 puntoSpawn = spawnPoint != null ? spawnPoint.position : transform.position + Vector3.up * 1.5f;

        // Instancia el sol en la escena
        Instantiate(sunPrefab, puntoSpawn, Quaternion.identity);

        Debug.Log("🌻 Girasol generó un sol.");
    }
}
