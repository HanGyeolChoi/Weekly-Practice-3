using System;
using UnityEngine;

public interface IDamagable
{
    public void TakeDamage(float amount);
}
public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;
    private Condition health { get { return uiCondition.health; } }
    private Condition stamina {  get { return uiCondition.stamina; } }
    private Condition mana { get { return uiCondition.mana; } }

    //public event Action OnDie;


    private void Update()
    {
        health.Add(health.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);
        mana.Add(mana.passiveValue * Time.deltaTime);

        if(health.curValue <= 0)
        {
            //OnDie?.Invoke();
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        stamina.Add(amount);
    }

    public void ManaGain(float amount)
    {
        mana.Add(amount);
    }

    public bool UseStamina(float amount)
    {
        if (stamina.curValue < amount) return false;
        stamina.Add(-amount);
        return true;
    }

    public bool UseMana(float amount)
    {
        if (mana.curValue < amount) return false;
        mana.Add(-amount);
        return true;
    }

    private void Die()
    {
        
    }

    public void TakeDamage(float amount)
    {
        health.Add(-amount);
    }
}
