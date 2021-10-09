using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlanetCollisionSystem))]
[RequireComponent(typeof(Selector))]
[RequireComponent(typeof(UnitSpawner))]

public class Planet : MonoBehaviour
{
    [SerializeField] private PlanetData _planetData;
    [SerializeField] private FractionData _fractionData;
    [SerializeField] private FractionData _void;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private CircleCollider2D _collider;

    [SerializeField] private int _unitCount;
    [SerializeField] private int _id;

    public PlanetData PlanetData { get => _planetData; }
    public FractionData FractionData { get => _fractionData; } 
    public int UnitCount { get => _unitCount; }

    public int Id
    {
        get
        {
            return _id;
        }

        set
        {
            _id = value;
        }
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void ResetCollider()
    {
        if (TryGetComponent(out CircleCollider2D collider))
        {
            Component.Destroy(collider);
        }

        _collider = gameObject.AddComponent<CircleCollider2D>();
    }

    public void Modify(PlanetData planetData)
    {
        _planetData = planetData;
        gameObject.name = _planetData.Name;
        _spriteRenderer.sprite = _planetData.Sprite;
        ResetCollider();
    }

    public void SetFraction(FractionData fraction)
    {
        if (_void == null)
        {
            _void = fraction;
        }

        if (_unitCount == 0)
        {
            _fractionData = _void;
        }
        else if (_unitCount < 0)
        {
            _unitCount = -_unitCount;
            _fractionData = fraction;
        }
        else
        {
            _fractionData = fraction;
        }


        if (_fractionData != null)
        {
            _spriteRenderer.color = _fractionData.Color;
        }
        
        StartCoroutine(AddUnits(_planetData.CollectUnitsFrequency));
    }

    public void ChangeUnits(int damage)
    {
        _unitCount += damage;
    }

    private IEnumerator AddUnits(float frequency)
    {

        var time = 1f / frequency;

        while (_unitCount > 0)
        {
            if(GameManager.Instance.GameStates == GameStates.Play)
            {
                _unitCount++;
                yield return new WaitForSeconds(time);
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }
    }
}

