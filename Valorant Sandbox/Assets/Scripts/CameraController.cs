using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 25f;

    [SerializeField] private bool _lockCursor = true, _cursorVisible = false;

    private PlayerInput _inputController;
    private void Awake()
    {
        _inputController = GetComponentInParent<PlayerInput>();

        ApplyCursorSettings();
    }

    private void FixedUpdate()
    {
        if(_inputController == null) { Debug.LogWarning("CameraController couldn't get player input"); return; }

        Rotate();
    }

    private void Rotate()
    {
        float xRotation = _inputController.MouseY * _rotationSpeed * -1f;
        transform.Rotate(xRotation * Time.fixedDeltaTime, 0f, 0f);
    }

    private void ApplyCursorSettings()
    {
        Cursor.lockState = _lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = _cursorVisible ? true : false;
    }
}
