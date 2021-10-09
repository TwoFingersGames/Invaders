using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCollisionSystem : MonoBehaviour
{
    private Planet _planet;

    private void Start()
    {
        _planet = GetComponent<Planet>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Unit unit))
        {
            if (unit.FractionData.Name != _planet.FractionData.Name)
            {
                _planet.ChangeUnits(-unit.Damage);
            }
            else
            {
                _planet.ChangeUnits(unit.Damage);
            }

            if (_planet.UnitCount <= 0)
            {
                _planet.SetFraction(unit.FractionData);
            }
            

            Destroy(unit.gameObject);
        }
    }
}
