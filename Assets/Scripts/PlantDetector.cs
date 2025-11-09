using UnityEngine;
using System.Collections.Generic;

public class PlantDetector : MonoBehaviour
{
    private List<GameObject> plantasRegistradas = new List<GameObject>();

    void Update()
    {
        // Buscar todas las plantas en la escena
        GameObject[] todasLasPlantas = GameObject.FindGameObjectsWithTag("Planta");

        foreach (GameObject planta in todasLasPlantas)
        {
            if (!plantasRegistradas.Contains(planta))
            {
                // Nueva planta detectada
                VerificarPlanta(planta);
                plantasRegistradas.Add(planta);
            }
        }

        // Limpieza (por si alguna planta fue destruida)
        plantasRegistradas.RemoveAll(p => p == null);
    }

    private void VerificarPlanta(GameObject planta)
    {
        PlantCost costo = planta.GetComponent<PlantCost>();
        int precio = (costo != null) ? costo.Precio : 0;

        if (precio > 0)
        {
            // Si el jugador no tiene suficiente dinero → eliminar planta sin costo
            if (!EconomyManager.Instance.CanAfford(precio))
            {
                Debug.Log($"❌ Planta {planta.name} cuesta {precio}, pero no hay soles suficientes. Eliminada.");
                Destroy(planta);
                return;
            }

            // Si sí tiene suficiente → descontar el costo
            EconomyManager.Instance.TrySpendSoles(precio);
            Debug.Log($"🌱 Planta {planta.name} colocada. Coste: {precio} soles.");

            // Si es girasol → activar spawn de zombis
            if (planta.GetComponent<Girasol>() != null)
            {
                GameManager.Instance.PrimerGirasolColocado();
            }
        }
    }
}
