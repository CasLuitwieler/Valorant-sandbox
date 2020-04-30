using System;

public interface IFireMode
{
    event Action OnFire;
    bool CanFire();
    void Fire();
}
