using UnityEngine;
using System;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager Instance;

    public event Action<int> OnSolesChanged;

    [SerializeField] private int solesIniciales = 50;
    private int solesActuales;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        solesActuales = solesIniciales;
    }

    public void AddSoles(int cantidad)
    {
        solesActuales += cantidad;
        OnSolesChanged?.Invoke(solesActuales);
    }

    public bool TrySpendSoles(int cantidad)
    {
        if (solesActuales >= cantidad)
        {
            solesActuales -= cantidad;
            OnSolesChanged?.Invoke(solesActuales);
            return true;
        }
        return false;
    }

    public bool CanAfford(int cantidad)
    {
        return solesActuales >= cantidad;
    }

    public int GetSoles() => solesActuales;
}
