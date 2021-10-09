using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    private Planet _planet;
    private List<Vector3> _spawnPoints;
    private List<Quaternion> _spawnRotations;
    private List<Unit> _units;
    private void Init()
    {
        if (_planet == null)
        {
            TryGetComponent(out Planet planet);
            _planet = planet;
        }
    }

    public void CreateUnits()
    {
        Init();
        CreateSpawnPoints();
        StartCoroutine(SpawnUnits(_planet.PlanetData.SpawnUnitsFrequency));
    }
    private void CreateSpawnPoints()
    {
        var count = _planet.UnitCount / 2;

        if (count > 60)
        {
            var cicleCount = count / 60;

            for (int i = 0; i < cicleCount; i++)
            {
                CreateRoundPoints(60);
            }

            CreateRoundPoints(count % 60);
        }
        else
        {
            CreateRoundPoints(count);
        }
        
    }
    private void CreateRoundPoints(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float angle = i * Mathf.PI * 2 / count;
            float x = Mathf.Cos(angle) * 1.5f;
            float y = Mathf.Sin(angle) * 1.5f;
            Vector3 pos = _planet.transform.position + new Vector3(x, y, 0);
            float angleDegrees = -angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
            _spawnPoints.Add(pos);
            _spawnRotations.Add(rot);
        }
    }
    private IEnumerator SpawnUnits(float spawnUnitsFrequency)
    {
        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            var unit = (Unit)new GameObject().AddComponent<Unit>();
            unit.gameObject.name = "Unit" + i;
            unit.gameObject.transform.position = _spawnPoints[i];
            unit.gameObject.transform.rotation = _spawnRotations[i];
            unit.Modify(LevelCreator.Instance.LevelData.UnitData[0]);
            unit.SetFraction(_planet.FractionData);
            _units.Add(unit);
            yield return new WaitForSeconds(1 / spawnUnitsFrequency);
        }
    }
}
