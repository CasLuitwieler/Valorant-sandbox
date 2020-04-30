using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f, _rotationSpeed = 25f;

    private PlayerInput _inputController;
    private CharacterController _charController;

    private void Awake()
    {
        _charController = GetComponent<CharacterController>();
        _inputController = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        Vector3 forwardMovement = transform.forward * _inputController.ZInput;
        Vector3 strafeMovement = transform.right * _inputController.XInput;

        Vector3 move = forwardMovement + strafeMovement;
        _charController.Move(move.normalized * _moveSpeed * Time.fixedDeltaTime);
    }

    private void Rotate()
    {
        float yRotation = _inputController.MouseX * _rotationSpeed;
        transform.Rotate(0f, yRotation * Time.fixedDeltaTime, 0f);
    }
}
