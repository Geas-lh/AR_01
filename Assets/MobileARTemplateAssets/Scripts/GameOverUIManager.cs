using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class GameOverUIManager : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject gameOverPanel;
    public Button restartButton;
    public Button exitButton; // opcional

    private void Start()
    {
        // Aseguramos que el panel esté oculto al inicio
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);

        if (exitButton != null)
            exitButton.onClick.AddListener(ExitGame);
    }

    // 🔹 Mostrar el panel de Game Over (llamado desde GameManager)
    public void ShowGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f; // Pausa el juego
    }

    // 🔹 Reinicia la partida
    public void RestartGame()
    {
        Time.timeScale = 1f; // Reanudar el tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // 🔹 Salir del juego (opcional, útil en build)
    public void ExitGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
