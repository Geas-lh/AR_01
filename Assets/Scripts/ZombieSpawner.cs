using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform homePoint;
    public Transform[] spawnPoints;
    public float tiempoPrimerZombie = 15f;
    public float intervaloSpawn = 10f;

    private bool primerZombieLanzado = false;

    public void IniciarSpawns()
    {
        if (!primerZombieLanzado)
            StartCoroutine(SpawnRoutine());
        primerZombieLanzado = true;
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(tiempoPrimerZombie);

        while (true)
        {
            SpawnZombieAleatorio();
            yield return new WaitForSeconds(intervaloSpawn);
        }
    }

    void SpawnZombieAleatorio()
    {
        if (zombiePrefab == null || spawnPoints.Length == 0) return;

        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject z = Instantiate(zombiePrefab, spawn.position, spawn.rotation);
        ZombieController ctrl = z.GetComponent<ZombieController>();

        if (ctrl != null)
            ctrl.AsignarObjetivo(homePoint);

        ZombieManager.Instance?.RegistrarZombie(z);
        Debug.Log("🧟 Zombie creado en " + spawn.name);
    }
}
