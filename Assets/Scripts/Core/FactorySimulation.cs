using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FactorySimulation : MonoBehaviour
{
    public static FactorySimulation Instance;
    public float tickInterval = 0.2f;

    private float _timer;
    private readonly List<Building> _buildings = new();

    void Awake() => Instance = this;

    public void Register(Building b)
    {
        if (!_buildings.Contains(b)) _buildings.Add(b);
    }

    public void Unregister(Building b)
    {
        _buildings.Remove(b);
    }

    void Update()
    {
        _timer += Time.deltaTime;
        while (_timer >= tickInterval)
        {
            _timer -= tickInterval;
            TickAll();
        }
    }

    void TickAll()
    {
        foreach (var bld in _buildings)
        {
            bld.Tick(tickInterval);
        }
    }
}