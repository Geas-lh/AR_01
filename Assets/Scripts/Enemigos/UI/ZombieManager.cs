using UnityEngine;
using System.Collections.Generic;

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager Instance;
    private List<GameObject> zombies = new List<GameObject>();

    void Awake()
    {
        Instance = this;
    }

    public void RegistrarZombie(GameObject z)
    {
        if (!zombies.Contains(z))
            zombies.Add(z);
    }

    public void EliminarZombie(GameObject z)
    {
        zombies.Remove(z);
    }

    public GameObject ObtenerZombieMasCercano(Vector3 origen, float rango)
    {
        GameObject objetivo = null;
        float minDist = Mathf.Infinity;

        foreach (var z in zombies)
        {
            if (z == null) continue;
            float dist = Vector3.Distance(origen, z.transform.position);
            if (dist < minDist && dist <= rango)
            {
                minDist = dist;
                objetivo = z;
            }
        }

        return objetivo;
    }

    public bool HayZombies()
    {
        return zombies.Count > 0;
    }
}
