using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Referencias principales")]
    public GameObject panelGameOver;
    public GameObject panelVictoria;
    public ZombieSpawner spawner;

    [Header("Configuración")]
    public int zombisParaGanar = 10;

    private bool primerGirasolPlantado = false;
    private int zombisEliminados = 0;
    private bool juegoTerminado = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (panelGameOver != null) panelGameOver.SetActive(false);
        if (panelVictoria != null) panelVictoria.SetActive(false);
    }

    public void PrimerGirasolColocado()
    {
        if (!primerGirasolPlantado)
        {
            primerGirasolPlantado = true;
            Debug.Log("🌻 Primer girasol colocado: iniciando spawns...");
            if (spawner != null)
                spawner.IniciarSpawns();
        }
    }

    public void OnZombieMuerto()
    {
        if (juegoTerminado) return;

        zombisEliminados++;
        Debug.Log($"🧟 Zombies eliminados: {zombisEliminados}/{zombisParaGanar}");

        if (zombisEliminados >= zombisParaGanar)
        {
            Victoria();
        }
    }

    private void Victoria()
    {
        juegoTerminado = true;
        Time.timeScale = 0f;
        if (panelVictoria != null) panelVictoria.SetActive(true);
        Debug.Log("🎉 ¡Victoria! Has derrotado a todos los zombies!");
    }

    public void OnGameOver()
    {
        if (juegoTerminado) return;

        juegoTerminado = true;
        Time.timeScale = 0f;
        if (panelGameOver != null) panelGameOver.SetActive(true);
        Debug.Log("💀 GAME OVER: un zombi llegó a la casa.");
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
