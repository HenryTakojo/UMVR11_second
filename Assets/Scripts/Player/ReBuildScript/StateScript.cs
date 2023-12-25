using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public abstract class StateScript
{
    public abstract void Enter();

    public abstract void Tick(float deltaTime);
    public abstract void Exit();

}
