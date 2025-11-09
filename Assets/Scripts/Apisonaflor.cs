using UnityEngine;

public class Apisonaflor : PlantaBase
{
    public float daño = 50f;

    protected override void Update()
    {
        base.Update();

        Collider[] zombies = Physics.OverlapSphere(transform.position, rangoAtaque);
        foreach (Collider c in zombies)
        {
            ZombieController z = c.GetComponent<ZombieController>();
            if (z != null)
            {
                z.RecibirDaño(daño);
                Debug.Log("💥 Apisonaflor golpeó un zombie!");
            }
        }
    }
}
