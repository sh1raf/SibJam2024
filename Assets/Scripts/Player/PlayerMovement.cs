using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    [Space]
    [Header("Drunk")]

    [SerializeField] private float drunkTime;

    public Vector2 MovementDirection { get { return _movementDirection; } private set { } }

    private float _currentSpeed;

    private Vector2 _inputVector;
    private Vector2 _movementDirection;

    private Animator _animator;

    private Vector2 _inputMulti;
    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _inputMulti = new Vector2(1,1);
        _currentSpeed = speed;
    }

    private void Update()
    {
        _inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * _inputMulti;
        _movementDirection = _inputVector.normalized;
        _movementDirection *= speed * Time.deltaTime;

        _animator.SetFloat("SpeedX", _movementDirection.x);
        _animator.SetFloat("SpeedY", _movementDirection.y);


        transform.Translate(_movementDirection);
    }



    public void Drunk()
    {
        StartCoroutine(Drunking());
    }

    public IEnumerator Drunking()
    {
        _inputMulti *= -1;

        yield return new WaitForSeconds(drunkTime);

        _inputMulti *= -1;
    }
}
