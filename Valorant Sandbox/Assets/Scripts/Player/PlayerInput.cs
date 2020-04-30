using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float XInput { get; private set; }
    public float YInput { get; private set; }
    public float ZInput { get; private set; }

    public float MouseX { get; private set; }
    public float MouseY { get; private set; }

    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;

    private void Update()
    {
        XInput = Input.GetAxisRaw("Horizontal");
        ZInput = Input.GetAxisRaw("Vertical");

        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");

        if (Input.GetKeyDown(_jumpKey))
            YInput = 1f;
        else
            YInput = 0f;
    }
}
