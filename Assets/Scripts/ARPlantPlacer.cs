using System.Collections.Generic;   // ✅ <-- agrega esta línea
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class ARPlantPlacer : MonoBehaviour
{
    [Header("Componentes AR")]
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private Camera arCamera;

    [Header("Plantas")]
    [SerializeField] private GameObject plantaSeleccionada;
    private GameObject previewInstance;

    [Header("Offsets y límites")]
    [SerializeField] private Vector3 offset = Vector3.up * 0.05f;
    [SerializeField] private float distanciaMaxima = 10f;

    private static readonly List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update()
    {
        if (plantaSeleccionada == null) return;

        // Tocar pantalla
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Lanzar un raycast al plano AR
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;

                    // Intentar colocar la planta
                    IntentarColocar(hitPose.position);
                }
            }
        }
    }

    private void IntentarColocar(Vector3 posicion)
    {
        if (plantaSeleccionada == null)
            return;

        var costo = plantaSeleccionada.GetComponent<PlantCost>();
        int precio = (costo != null) ? costo.Precio : 0;

        // ✅ Verificar soles antes de colocar
        if (!EconomyManager.Instance.CanAfford(precio))
        {
            Debug.Log("❌ No hay suficientes soles. Planta descartada.");
            if (previewInstance != null) Destroy(previewInstance);
            plantaSeleccionada = null;
            return;
        }

        // ✅ Instanciar la planta en la superficie AR
        GameObject nuevaPlanta = Instantiate(plantaSeleccionada, posicion + offset, Quaternion.identity);
        EconomyManager.Instance.TrySpendSoles(precio);

        // Avisar al GameManager si es un girasol (para iniciar oleada de zombies)
        if (nuevaPlanta.GetComponent<Girasol>() != null)
            GameManager.Instance.PrimerGirasolColocado();

        // Limpiar selección
        plantaSeleccionada = null;
    }

    public void SeleccionarPlanta(GameObject prefab)
    {
        plantaSeleccionada = prefab;
    }
}
