using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimedSwitch : MonoBehaviour, IHittable
{
    bool isActivated;
    [SerializeField]
    private UnityEvent OnActivated;
    [SerializeField]
    private UnityEvent OnTimeOver;

    [SerializeField]
    private float amountOfTime;
    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        if (!isActivated) return;
        Countdown();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>() != null || !isActivated)
        {
            Hit();
        }
    }

    public void Hit()
    {
        isActivated = true;
        OnActivated.Invoke();
    }

    public void Reset()
    {
        isActivated=false;
        OnTimeOver.Invoke();
        timer = 0;
    }

    private void Countdown()
    {
        timer += Time.deltaTime;
        if(timer >= amountOfTime)
        {
            Reset();
        }
    }
}
