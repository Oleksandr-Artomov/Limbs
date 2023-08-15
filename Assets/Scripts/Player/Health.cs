using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider _healthSlider;

    public float _health;

    public float _maxHealth;

    public void SetMaxHealth()
    {
        _healthSlider.maxValue = _maxHealth;
        _healthSlider.value = _health;
    }

    public void TakeDamage()
    {
        _health = _health - 5.0f;
    }
}
