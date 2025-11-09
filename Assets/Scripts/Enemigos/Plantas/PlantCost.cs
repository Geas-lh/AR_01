using UnityEngine;

[DisallowMultipleComponent]
public class PlantCost : MonoBehaviour
{
    [Header("Precio de la planta")]
    [SerializeField] private int precio = 50;

    public int Precio => precio;
}
