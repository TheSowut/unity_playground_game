using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPatrol : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private Transform _targetA = null, _targetB = null;
    private bool _canChangeDirection = false;

    void FixedUpdate()
    {
        if (!_canChangeDirection)
        {
            transform.position = changeDirection(_targetA);
        }
        else if (_canChangeDirection)
        {
            transform.position = changeDirection(_targetB);
        }

        if (transform.position == _targetA.position)
        {
            _canChangeDirection = true;
        }
        else if (transform.position == _targetB.position)
        {
            _canChangeDirection = false;
        }
    }

    Vector2 changeDirection(Transform target)
    {
        return Vector2.MoveTowards(
            transform.position,
            target.position,
            _speed * Time.deltaTime
        );
    }
}
