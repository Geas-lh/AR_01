using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;

    private bool isGameOver = false;

    // Método público que puede ser llamado desde otros scripts
    public void TriggerGameOver()
    {
        if (isGameOver) return; // Evita llamarlo dos veces

        isGameOver = true;

        // Pausar el juego
        Time.timeScale = 0f;

        // Mostrar UI del Game Over
        if (gameOverUI != null)
            gameOverUI.SetActive(true);
    }
}
