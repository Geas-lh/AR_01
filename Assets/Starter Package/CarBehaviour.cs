using System.Collections;
using UnityEngine;
using TMPro;

public class CarBehaviour : MonoBehaviour
{
    public ReticleBehaviour Reticle;
    public float Speed = 1.2f;

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public GameObject gameOverUI;

    private int score = 0;
    private float timer = 20f;
    private float currentTime = 0f;
    private bool timerActive = false;
    private float minTime = 1f;

    private void Update()
    {
        if (Reticle == null) return;

        var trackingPosition = Reticle.transform.position;

        if (Vector3.Distance(trackingPosition, transform.position) > 0.1f)
        {
            var lookRotation = Quaternion.LookRotation(trackingPosition - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            transform.position = Vector3.MoveTowards(transform.position, trackingPosition, Speed * Time.deltaTime);
        }

        // 🔹 Control del temporizador
        if (timerActive)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                timerActive = false;
                gameOverUI?.SetActive(true);
            }
            UpdateTimerUI();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var package = other.GetComponent<PackageBehaviour>();
        if (package != null)
        {
            Destroy(other.gameObject);
            score++;
            UpdateScoreUI();

            // 🔹 Iniciar el temporizador si aún no empezó
            if (!timerActive)
                timerActive = true;

            // 🔹 Reiniciar el tiempo, reduciendo 1 segundo hasta el mínimo
            timer = Mathf.Max(minTime, timer - 1f);
            currentTime = timer;
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = $"Time: {Mathf.Ceil(currentTime)}";
    }
}
