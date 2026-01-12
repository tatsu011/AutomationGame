using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public Vector2Int GridPosition { get; set; }
    public Direction Facing;

    protected virtual void OnEnable()
    {
        if (FactorySimulation.Instance != null)
            FactorySimulation.Instance.Register(this);
    }

    protected virtual void OnDisable()
    {
        if (FactorySimulation.Instance != null)
            FactorySimulation.Instance.Unregister(this);
    }

    public virtual void Tick(float deltaTime) { }
}