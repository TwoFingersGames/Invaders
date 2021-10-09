using UnityEngine;

public class UnitCollisionSystem : MonoBehaviour
{
    private Unit _unit;

    private void Start()
    {
        _unit = GetComponent<Unit>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Unit unit))
        {
            if(unit.FractionData.Name != _unit.FractionData.Name)
            {
                unit.ReduceHealth(_unit.Damage);
            }
        }
    }
}
