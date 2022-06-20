using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    [SerializeField] private NewMove _rotation;
    [SerializeField] float _sensitivity = 10f;
    private Camera _cam;

    private void Awake()
    {
        _rotation = new NewMove();
        _cam = Camera.main;
    }

    private void OnEnable()
    {
        _rotation.Enable();
    }

    private void OnDisable()
    {
        _rotation.Disable();
    }

    private void FixedUpdate()
    {
        Turning();
    }

    private void Turning()
    {
        Vector2 mouseScreenPosition = _rotation.Player.MousePosition.ReadValue<Vector2>();
        Ray ray = _cam.ScreenPointToRay(mouseScreenPosition);
        Plane plane = new Plane(Vector3.up, 0);

        if (plane.Raycast(ray, out float distance))
        {
            var worldPos = ray.GetPoint(distance);
            var targetRotation = worldPos - transform.position;
            targetRotation.x = 0f;
            targetRotation.z = 0f;
            transform.rotation = Quaternion.EulerRotation(0f, targetRotation.y, 0f);
        }
    }
}



