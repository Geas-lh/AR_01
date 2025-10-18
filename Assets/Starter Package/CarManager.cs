using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro; // Importante para los textos TMP

/**
 * Spawns a <see cref="CarBehaviour"/> when a plane is tapped.
 */
public class CarManager : MonoBehaviour
{
    public GameObject CarPrefab;
    public ReticleBehaviour Reticle;
    public DrivingSurfaceManager DrivingSurfaceManager;

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
            Car.gameOverUI = GameObject.Find("GameOverUI");

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
