using UnityEngine;

public class Sun : MonoBehaviour
{
    [Header("Valor del sol (en soles)")]
    [SerializeField] private int valor = 25;
    [Header("Auto destroy después de X segundos")]
    [SerializeField] private float tiempoVida = 10f;

    private void Start()
    {
        Destroy(gameObject, tiempoVida);
    }

    // Recolectar con click
    private void OnMouseDown()
    {
        Collect();
    }

    // Llamar desde el Collector de UI para recoger todos
    public void Collect()
    {
        EconomyManager.Instance.AddSoles(valor);
        Destroy(gameObject);
    }

    // Opcional: método público toString
    public int GetValue() => valor;
}
