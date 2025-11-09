using UnityEngine;

public class CanonCoco : PlantaBase
{
    public GameObject prefabCoco;
    public Transform puntoDisparo;

    protected override void Update()
    {
        base.Update();

        if (Time.time - tiempoUltimoAtaque >= tiempoEntreAtaques)
        {
            DispararCoco();
            tiempoUltimoAtaque = Time.time;
        }
    }

    void DispararCoco()
    {
        Instantiate(prefabCoco, puntoDisparo.position, puntoDisparo.rotation);
        Debug.Log("💣 Cañón Coco disparó!");
    }
}
