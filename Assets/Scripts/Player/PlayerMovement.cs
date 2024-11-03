using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float defaultSpeed;

    public Vector2 MovementDirection { get { return _movementDirection; } private set { } }

    private Vector2 _inputVector;
    private Vector2 _movementDirection;

    private Animator _animator;

    private Vector2 _inputMulti;

    private bool _canMove = true;
    private float _currentSpeed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _inputMulti = new Vector2(1,1);

        _currentSpeed = defaultSpeed;
    }

    private void Update()
    {
        if (!_canMove)
            return;

        _inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * _inputMulti;
        _movementDirection = _inputVector.normalized;
        _movementDirection *= _currentSpeed * Time.deltaTime;

        if (_movementDirection.x > 0)
        {
            if (_movementDirection.y > 0)
            {
                _animator.Play("Right-up");
            }
            else if (_movementDirection.y < 0)
            {
                _animator.Play("Right-down");
            }
            else
                _animator.Play("Right");
        }
        else if (_movementDirection.x < 0)
        {
            if (_movementDirection.y > 0)
            {
                _animator.Play("Left-up");
            }
            else if (_movementDirection.y < 0)
            {
                _animator.Play("Left-down");
            }
            else
                _animator.Play("Left");
        }
        else
        {
            if (_movementDirection.y > 0)
            {
                _animator.Play("Up");
            }
            else if (_movementDirection.y < 0)
            {
                _animator.Play("Down");
            }
            else
                _animator.Play("Idle");
        }

        transform.Translate(_movementDirection);
    }

    public void Freeze()
    {
        _animator.Play("Idle");
        _canMove = false;
    }

    public void UnFreeze()
    {
        _canMove = true;
    }

    public void SpeedBoost(float multi, float duration)
    {
        StartCoroutine(SpeedBoosting(multi, duration));
    }

    private IEnumerator SpeedBoosting(float multi, float duration)
    {
        _currentSpeed *= multi;

        yield return new WaitForSeconds(duration);

        _currentSpeed *= 1 / multi;
    }

    public void Drunk(float duration)
    {
        StartCoroutine(Drunking(duration));
    }

    public IEnumerator Drunking(float duration)
    {
        _inputMulti *= -1;

        yield return new WaitForSeconds(duration);

        _inputMulti *= -1;
    }
}
