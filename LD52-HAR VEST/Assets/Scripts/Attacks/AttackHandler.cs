using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [SerializeField]
    private Attack[] attacks = new Attack[3];
    InputHandler input;
    PlayerController player;

    private void Start()
    {
        input = GetComponentInChildren<InputHandler>();
        player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if(input.GetSingleAttack() && attacks[0].IsBeingUsed == false)
        {
            StartAttack(attacks[0]);
            return;
        }
        else if (input.GetExplosionAttack() && attacks[1].IsBeingUsed == false)
        {
            StartAttack(attacks[1]);
            return;
        }
        else if (input.GetTeleportAttack() && attacks[2].IsBeingUsed == false)
        {
            StartAttack(attacks[2]);
            return;
        }
    }

    private void StartAttack(Attack activatedAttack)
    {
        int playerdirection = player.Direction;
        activatedAttack.SetDirection(playerdirection);
        activatedAttack.DoAttack();
    }
}
