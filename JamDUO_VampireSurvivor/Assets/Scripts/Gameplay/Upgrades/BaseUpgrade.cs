using System;


[Serializable]
public abstract class BaseUpgrade
{
    public abstract void Execute(PlayerController player);
}

