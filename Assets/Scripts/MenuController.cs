using UnityEngine;
using UnityEngine.SceneManagement;

public class CargarEscena : MonoBehaviour
{
    [Header("Nombre de la escena a cargar")]
    [SerializeField] private string nombreEscena;

    // Método público que puedes asignar al botón
    public void Cargar()
    {
        if (!string.IsNullOrEmpty(nombreEscena))
        {
            SceneManager.LoadScene(nombreEscena);
        }
        else
        {
            Debug.LogWarning("No se ha asignado un nombre de escena en el inspector.");
        }
    }
}
