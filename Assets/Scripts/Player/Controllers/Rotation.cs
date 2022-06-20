using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
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
        Plane plane = new Plane(Vector3.up, transform.position);

        if (plane.Raycast(ray, out float distance))
        {
            var worldPos = ray.GetPoint(distance);
            var targetRotation = Quaternion.LookRotation(worldPos - transform.position);
            targetRotation.x = 0f;
            targetRotation.z = 0f;
            transform.rotation = Quaternion.Lerp(transform.rotation,targetRotation,_sensitivity * Time.deltaTime);
        }
    }
}

   

