using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupActions : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerstats;
    [SerializeField]
    private Animator carAnimator;
    [SerializeField]
    private GameObject realCar;


    public void BoostStartAction()
    {
        playerstats.speed = 60;
        playerstats.canTakeDamage = false;
    }

    public void BoostEndAction()
    {
        playerstats.speed = 40;
        playerstats.canTakeDamage = true;
    }

    public void GrowStartAction()
    {
        carAnimator.Play("carGrow");
    }

    public void GrowEndAction()
    {
        carAnimator.Play("carShrink");
    }

    public void ThinStartAction()
    {
        carAnimator.Play("carNarrow");
    }
        
    public void ThinEndAction()
    {
        carAnimator.Play("carWiden");
    }
}
