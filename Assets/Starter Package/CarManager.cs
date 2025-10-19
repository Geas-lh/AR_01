using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class CarManager : MonoBehaviour
{
    public GameObject CarPrefab;
    public ReticleBehaviour Reticle;
    public DrivingSurfaceManager DrivingSurfaceManager;

    public GameManager gameManager; // 👈 Nueva referencia al GameManager
    public CarBehaviour Car;

    private void Update()
    {
        if (Car == null && WasTapped() && Reticle.CurrentPlane != null)
        {
            // Spawn our car at the reticle location.
            var obj = Instantiate(CarPrefab);
            Car = obj.GetComponent<CarBehaviour>();
            Car.Reticle = Reticle;
            Car.transform.position = Reticle.transform.position;

            // 🔹 Asignar referencias automáticamente
            Car.scoreText = GameObject.Find("ScoreText")?.GetComponent<TMP_Text>();
            Car.timerText = GameObject.Find("TimerText")?.GetComponent<TMP_Text>();
            Car.gameManager = gameManager; // 👈 Ahora pasamos la referencia del GameManager

            DrivingSurfaceManager.LockPlane(Reticle.CurrentPlane);
        }
    }

    private bool WasTapped()
    {
        if (Input.GetMouseButtonDown(0))
            return true;

        if (Input.touchCount == 0)
            return false;

        var touch = Input.GetTouch(0);
        return touch.phase == TouchPhase.Began;
    }
}
