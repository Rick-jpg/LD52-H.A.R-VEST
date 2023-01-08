using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
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

    [Header("Materials")]
    [SerializeField]
    Material deactivated;
    [SerializeField]
    Material activated;

    Renderer renderer;
    ParticleSystem particleSystem;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

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
            Debug.Log("Hit");
            Hit();
        }
    }

    public void Hit()
    {
        ChangeMaterial(activated);
        isActivated = true;
        OnActivated.Invoke();
        particleSystem.Play();
    }

    public void Reset()
    {
        particleSystem.Stop();
        ChangeMaterial(deactivated);
        isActivated =false;
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

    private void ChangeMaterial(Material mat)
    {
        renderer.material = mat;
    }
}
