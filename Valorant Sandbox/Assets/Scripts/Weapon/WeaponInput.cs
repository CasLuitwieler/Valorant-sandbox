using UnityEngine;

public class WeaponInput : MonoBehaviour, IWeaponInput
{
    [SerializeField] private KeyCode _fireKey = KeyCode.Mouse0;

    public bool FireKeyDown()
    {
        return Input.GetKeyDown(_fireKey);
    }

    public bool FireKeyPressed()
    {
        return Input.GetKey(_fireKey);
    }
}
