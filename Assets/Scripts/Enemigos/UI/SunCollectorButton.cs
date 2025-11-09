using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SunCollectorButton : MonoBehaviour
{
    [Header("UI opcional (texto TMP para feedback)")]
    public TextMeshProUGUI feedbackText;

    [Header("Sonido opcional al recoger (AudioSource con clip)")]
    public AudioSource sonidoRecoleccion;

    // Este método se llama desde el evento OnClick() del botón
    public void RecogerTodosLosSoles()
    {
        Sun[] soles = FindObjectsOfType<Sun>();
        int totalRecolectados = 0;

        foreach (Sun s in soles)
        {
            s.Collect();  // Usa el método del script Sun.cs
            totalRecolectados++;
        }

        // Feedback
        if (totalRecolectados > 0)
        {
            Debug.Log($"☀️ Recolectados {totalRecolectados} soles en total.");
            if (feedbackText != null)
                feedbackText.text = $"+{totalRecolectados * 25} ☀️";

            if (sonidoRecoleccion != null)
                sonidoRecoleccion.Play();
        }
        else
        {
            if (feedbackText != null)
                feedbackText.text = "No hay soles para recolectar.";
        }
    }
}
