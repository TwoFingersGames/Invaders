using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LevelData", menuName = "ScriptableObjects/LevelData", order = 51)]
public class LevelData : ScriptableObject
{
    [SerializeField] private Vector2Int _levelSize;
    [SerializeField] private int _planetsCount;
    [SerializeField] private int _playersCount;
    [SerializeField] private int _enemiesCount;
    [SerializeField] private int _startUnitsCount;
    [SerializeField] private List<PlanetData> _planetData;
    [SerializeField] private List<UnitData> _unitData;
    [SerializeField] private List<FractionData> _fractionData;

    public Vector2Int LevelSize { get => _levelSize; }
    public int PlanetsCount { get => _planetsCount; }
    public int PlayersCount { get => _playersCount; }
    public int EnemiesCount { get => _enemiesCount; }
    public int StartUitsCount { get => _startUnitsCount; }
    public List<PlanetData> PlanetData { get => _planetData; }
    public List<UnitData> UnitData { get => _unitData; }
    public List<FractionData> FractionData { get => _fractionData; }
}
