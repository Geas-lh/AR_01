using UnityEngine;
using TMPro;

public class SunUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoSoles;

    private void Start()
    {
        // Inicializa el texto al valor actual
        ActualizarSoles(EconomyManager.Instance.GetSoles());

        // Suscríbete a los cambios
        EconomyManager.Instance.OnSolesChanged += ActualizarSoles;
    }

    private void OnDestroy()
    {
        // Limpia la suscripción al salir
        if (EconomyManager.Instance != null)
            EconomyManager.Instance.OnSolesChanged -= ActualizarSoles;
    }

    private void ActualizarSoles(int cantidad)
    {
        if (textoSoles != null)
            textoSoles.text = cantidad.ToString();
    }
}
