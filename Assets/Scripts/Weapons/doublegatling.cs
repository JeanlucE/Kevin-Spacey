using UnityEngine;
using System.Collections;

public class doublegatling : gun {

    public gatling left, right;

    override public void StartShooting()
    {
        left.StartShooting();
        right.StartShooting();
    }

    override public void StopShooting()
    {
        left.StopShooting();
        right.StopShooting();
    }
}
