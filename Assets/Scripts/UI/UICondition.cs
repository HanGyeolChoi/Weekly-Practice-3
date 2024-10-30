﻿using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;
    public Condition stamina;
    public Condition mana;

    private void Start()
    {
        CharacterManager.Instance.Player.condition.uiCondition = this;
    }
}