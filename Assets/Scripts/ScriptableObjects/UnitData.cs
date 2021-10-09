using UnityEngine;

[CreateAssetMenu(fileName = "New UnitData", menuName = "ScriptableObjects/UnitData", order = 51)]
public class UnitData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite; 
    [SerializeField] private int _health, _damage;
    [SerializeField] private float _speed;
    
    public string Name { get => _name; }
    public Sprite Sprite
    {
        get => _sprite;
    }

    public int Health
    {
        get => _health;
    }

    public int Damage
    {
        get => _damage;
    }

    public float Speed
    {
        get => _speed;
    }

}
