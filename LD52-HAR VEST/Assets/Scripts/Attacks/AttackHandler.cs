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

    bool isAttacking;

    private void Start()
    {
        input = GetComponentInChildren<InputHandler>();
        player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        for (int i = 0; i < attacks.Length; i++)
        {
            if (attacks[i].IsBeingUsed)
            {
                isAttacking = true;
                Debug.Log("attacking " + i);
                break;
            }

            isAttacking = false;
        }

        if (input.GetSingleAttack() && !isAttacking)
        {
            StartAttack(attacks[0]);
            return;
        }
        else if (input.GetLightningAttack() && !isAttacking)
        {
            StartAttack(attacks[1]);
            return;
        }
        else if (input.GetTeleportAttack() && !isAttacking)
        {
            StartAttack(attacks[2]);
            return;
        }
    }

    private void StartAttack(Attack activatedAttack)
    {
        if (activatedAttack.EnergyCost > player.GetCurrentEnergy())
            return;

        int playerdirection = player.Direction;
        activatedAttack.SetDirection(playerdirection);
        activatedAttack.DoAttack();
    }

    public bool GetAttackisBeingUsed(int attack)
    {
        return attacks[attack].IsBeingUsed;
    }
}
