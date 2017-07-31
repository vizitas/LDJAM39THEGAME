using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPower : Singleton<PlayerPower>
{
    private float power = 1;
    public float Power { get { return power; } }
    [SerializeField]
    float decayAmmount = 0.005f;
    float damageCoolDown = 3f;
    float lastDamage;
    [SerializeField]
    StatusBar powerBar;
    [SerializeField]
    Blinker powerBarBlinker;
    // Use this for initialization
    void Start()
    {
        InputController.letterDestroyed.AddListener(LetterDecay);
        InvokeRepeating("Decay", 0, 1);
    }
    private void Update()
    {
        if (Time.time > lastDamage + damageCoolDown)
        {
            powerBarBlinker.enabled = false;
        }
    }
    void LetterDecay(LetterController letter)
    {
        power -= decayAmmount;
        powerBar.SetStatus(power);
    }
    void Decay()
    {
        power -= decayAmmount;
        powerBar.SetStatus(power);
    }
    public void InflictPowerDamage(float ammount)
    {
        if (Time.time > lastDamage + damageCoolDown)
        {
            lastDamage = Time.time;
            ChangePower(-ammount);
            powerBarBlinker.enabled = true;
        }
    }
    public void ChangePower(float ammount)
    {
        if (ammount > 0 && power >= 1)
            return;
        power += ammount;
    }
}
