using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSafeController : MonoBehaviour
{
    [SerializeField] private string gunTag;
    [SerializeField] private GameObject nextGun;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered Gun Safe");
        // turn off the camera and activate the next gun
        // we still want the gun to physically be there in the safe
        if (other.transform.root.tag == gunTag)
        {
            EventManager.current.OnGameIncrementScore();

            // if the game hasn't ended, move on to the next gun
            if(!GameManager.current.gameEnd)
            {
                EventManager.current.OnDisableCurrentGun();
                nextGun.SetActive(true);
            }
        }
    }
}
