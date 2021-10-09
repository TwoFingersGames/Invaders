using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private Planet _target;
    private Unit _unit;
    private Rigidbody2D _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _unit = GetComponent<Unit>();
    }

    private void Update()
    {
        if (GameManager.Instance.GameStates == GameStates.Play)
        {
            _rigidbody.AddForce(_rigidbody.GetVector(Vector2.up * _unit.Speed));
        }
        
        
    }

    public IEnumerator FlyTo(Planet target)
    {
        _target = target;
        var speed = 1 / _unit.Speed;
        while (true)
        {
            if (GameManager.Instance.GameStates == GameStates.Play)
            {
                var direction1 = _rigidbody.velocity.normalized;
                var direction2 = transform.position - _target.transform.position;
                _rigidbody.transform.rotation = Quaternion.FromToRotation(direction1, direction2);

                RaycastHit2D hit = Physics2D.Raycast(transform.position, _rigidbody.velocity.normalized, 2f);
                if (hit.collider.gameObject.TryGetComponent(out Planet planet))
                {
                    if (planet.Id != _target.Id)
                    {
                        _rigidbody.AddForce(hit.normal.normalized * _unit.Speed, ForceMode2D.Force);
                    }
                }
                yield return new WaitForSeconds(speed);
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }
        
    }

    
}
