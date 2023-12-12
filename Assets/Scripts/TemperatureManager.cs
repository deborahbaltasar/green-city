using System;
using UnityEngine;

public class TemperatureManager
{
    private float currentTemperature;
    private const float OptimalTempMin = 18.0f;
    private const float OptimalTempMax = 24.0f;

    public TemperatureManager(float initialTemperature)
    {
        currentTemperature = initialTemperature;
    }

    public float CurrentTemperature
    {
        get => currentTemperature;
        set => currentTemperature = Mathf.Clamp(value, -10.0f, 50.0f);
    }

    public void IncreaseTemperature(float amount)
    {
        CurrentTemperature += amount;
    }

    public void DecreaseTemperature(float amount)
    {
        CurrentTemperature -= amount;
    }

    public void SetTemperature(float amount)
    {
        CurrentTemperature = amount;
    }

    public float CalculateTemperatureEffect(StructureBaseSO structure)
    {
        float baseEffect =
            (currentTemperature < OptimalTempMin || currentTemperature > OptimalTempMax)
                ? 0.8f
                : 1.0f;
        return baseEffect * structure.temperatureEffect;
    }
}
