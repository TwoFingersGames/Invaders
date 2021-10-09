using UnityEngine;

[CreateAssetMenu(fileName = "New PlanetData", menuName = "ScriptableObjects/PlanetData", order = 51)]
public class PlanetData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private float _collectUnitsFrequency;
    [SerializeField] private float _spawnUnitsFrequency;

    public string Name { get => _name; }
    public Sprite Sprite { get => _sprite; }
    public float CollectUnitsFrequency { get => _collectUnitsFrequency; }
    public float SpawnUnitsFrequency { get => _spawnUnitsFrequency; }

}
