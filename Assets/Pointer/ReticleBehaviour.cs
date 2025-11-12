using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ReticleBehaviour : MonoBehaviour
{
    public GameObject GloveModel; // Modelo 3D del guante
    public DrivingSurfaceManager DrivingSurfaceManager;

    public ARPlane CurrentPlane;

    private void Start()
    {
        // Asegura que el guante esté activo desde el inicio
        if (GloveModel != null)
            GloveModel.SetActive(true);
    }

    private void Update()
    {
        // Mantiene el comportamiento original de detección de planos
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        DrivingSurfaceManager.RaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinBounds);

        CurrentPlane = null;
        ARRaycastHit? hit = null;

        if (hits.Count > 0)
        {
            // Si no hay plano bloqueado, usa el primero
            var lockedPlane = DrivingSurfaceManager.LockedPlane;
            hit = lockedPlane == null
                ? hits[0]
                : hits.SingleOrDefault(x => x.trackableId == lockedPlane.trackableId);
        }

        if (hit.HasValue)
        {
            CurrentPlane = DrivingSurfaceManager.PlaneManager.GetPlane(hit.Value.trackableId);

            // Mueve el modelo del guante a la posición detectada
            transform.position = hit.Value.pose.position;
            transform.rotation = hit.Value.pose.rotation;
        }

        // El guante siempre está activo, no se oculta
        if (GloveModel != null && !GloveModel.activeSelf)
            GloveModel.SetActive(true);
    }
}
