using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : GenericSingletonClass<LevelCreator>
{
    [SerializeField] private LevelData _levelData;
    [SerializeField] private Camera _camera;
    [SerializeField] private List<Vector2> _spawnPoints;
    [SerializeField] private List<Planet> _planets;
    [SerializeField] private int _seed = 1;

    public LevelData LevelData { get => _levelData; }
    public int SetSeed(int value)
    {
        if (value < 0)
        {
            _seed = -value;
        }
        else if (value == 0)
        {
            _seed = 1;
        }
        else
        {
            _seed = value;
        }
        return _seed;
    }
    public void CreateLevel()
    {
        SetupCamera();
        CreateSpawnPoints();
        SpawnPlanets();
    }

    private void SetupCamera()
    {
        _camera = FindObjectOfType<Camera>();
        _camera.orthographicSize = _levelData.LevelSize.x / 2f;
        _camera.transform.position = new Vector3(_levelData.LevelSize.x / 2f, _levelData.LevelSize.y / 2f, -1f);
    }

    private void CreateSpawnPoints()
    {
        _spawnPoints.Clear();
        var levelPoints = new int[_levelData.LevelSize.x, _levelData.LevelSize.y];

        var planetCount = _levelData.PlanetsCount;

        var randomX = new System.Random(_seed * 2);
        var randomY = new System.Random(_seed);

        while (planetCount > 0)
        { 
            int i = randomX.Next(1, _levelData.LevelSize.x - 1);
            int j = randomY.Next(1, _levelData.LevelSize.y - 1);

            if (levelPoints[i, j] == 0)
            {
                _spawnPoints.Add(new Vector2(i, j));
                planetCount--;

                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        levelPoints[i + x, j + y] = 1;
                    }
                }
            }
        }
    }
    
    private void SpawnPlanets()
    {
        foreach (var item in _planets)
        {
            Destroy(item.gameObject);
        }
        _planets.Clear();
        var activeIndexes = new List<int>();
        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            
            var planet = (Planet)new GameObject().AddComponent<Planet>();
            planet.gameObject.name = "Planet " + i;
            planet.Id = i;
            planet.gameObject.transform.position = _spawnPoints[i];
            planet.Modify(_levelData.PlanetData[0]);
            planet.SetFraction(_levelData.FractionData[0]);
            _planets.Add(planet);
            activeIndexes.Add(i);
        }
        var fractionCount = _levelData.PlayersCount + _levelData.EnemiesCount;
        var random = new System.Random(System.DateTime.Now.Millisecond);

        int players = _levelData.PlayersCount;
        
        for (int i = 0; i < fractionCount; i++)
        {
            var randomIndex = random.Next(0, activeIndexes.Count);
            _planets[activeIndexes[randomIndex]].ChangeUnits(_levelData.StartUitsCount);
            _planets[activeIndexes[randomIndex]].SetFraction(_levelData.FractionData[i+1]);
            if (players > 0)
            {
                _planets[activeIndexes[randomIndex]].gameObject.AddComponent<Player>();
                players--;
            }
            activeIndexes.Remove(activeIndexes[randomIndex]);
        }
    }
}
