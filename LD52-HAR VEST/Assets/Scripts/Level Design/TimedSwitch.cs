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

    Renderer meshRenderer;
    ParticleSystem activationParticle;

    private void Start()
    {
        meshRenderer = GetComponent<Renderer>();
        activationParticle = GetComponentInChildren<ParticleSystem>();
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
            AudioManager.Instance.PlaySound(2, 4);
            Hit();
        }
    }

    public void Hit()
    {
        ChangeMaterial(activated);
        isActivated = true;
        OnActivated.Invoke();
        activationParticle.Play();
    }

    public void Reset()
    {
        activationParticle.Stop();
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
        meshRenderer.material = mat;
    }
}
