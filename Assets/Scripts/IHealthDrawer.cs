using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IHealthDrawer
{
    public void OnHealthChanged(int healthValue);
}

public class HealthDrawerInterface : InterfaceComponent<IHealthDrawer> { }


