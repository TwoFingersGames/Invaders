using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(UnitCollisionSystem))]
[RequireComponent(typeof(Mover))]

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitData _unitData;
    [SerializeField] private FractionData _fractionData;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private PolygonCollider2D _polygonCollider2D;

    [SerializeField] private int _health;
    
    
    public int Health { get => _health; }
    public int Damage { get => _unitData.Damage; }
    public float Speed { get => _unitData.Speed; }
    
    public UnitData UnitData { get => _unitData; }
    public FractionData FractionData { get => _fractionData; }
    

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = 0;
        Modify(_unitData);
        SetFraction(_fractionData);
    }

    private void ResetCollider()
    {
        if (TryGetComponent(out PolygonCollider2D collider))
        {
            Component.Destroy(collider);
        }

        _polygonCollider2D = gameObject.AddComponent<PolygonCollider2D>();
    }

    public void SetFraction(FractionData fraction)
    {
        _fractionData = fraction;
        _spriteRenderer.color = _fractionData.Color;
        
    }

    public void Modify(UnitData unitData)
    {
        _unitData = unitData;
        gameObject.name = _unitData.Name;
        _health = _unitData.Health;
        _spriteRenderer.sprite = _unitData.Sprite;
        ResetCollider();
    }

    public void ReduceHealth(int damage)
    {
        _health -= damage;

        if(_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetData(UnitData unitData, FractionData fraction)
    {
        _unitData = unitData;
        _fractionData = fraction;
    }
}
