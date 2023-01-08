using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaserGroup : MonoBehaviour
{
    private List<Laser> laserList = new List<Laser>();
    // Start is called before the first frame update
    void Start()
    {
        laserList = GetComponentsInChildren<Laser>().ToList();
    }

    public void Activate()
    {
        foreach (Laser l in laserList)
        {
            l.Activate();
        }
    }

    public void Deactivate()
    {
        foreach (Laser l in laserList)
        {
            l.Deactivate();
        }
    }


}
