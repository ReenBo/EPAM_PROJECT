using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewInputSystem : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;

    private NewMove _input;

    private void Awake()
    {
        _input = new NewMove();
    }
    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 direction = _input.Player.Move.ReadValue<Vector2>();

        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;

        Vector3 moveDirecion = new Vector3(direction.x, 0, direction.y);
        transform.position += moveDirecion * scaledMoveSpeed;
    }
}
