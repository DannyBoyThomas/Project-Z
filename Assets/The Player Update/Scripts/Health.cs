using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Health : NetworkBehaviour 
{
    [SyncVar]
    public float CurrentHealth = 10;
    [SyncVar]
    public float MaxHealth = 10;

    public delegate void HealthDelegate(float damage);
    public HealthDelegate onDamage;
    public HealthDelegate onHeal;

    public delegate void DeathDelegate();
    public DeathDelegate OnDeath;

    public void OnHit(float damage)
    {
        if (damage > 0)
            onDamage(damage);
        else
            if (damage < 0)
                onHeal(damage);

        CurrentHealth += damage;

        CmdSyncHealth(CurrentHealth, MaxHealth);

    }


    [Command]
    public void CmdSyncHealth(float CurrentHealth, float MaxHealth)
    {
        this.CurrentHealth = CurrentHealth;
        this.MaxHealth = MaxHealth;
    }
}
