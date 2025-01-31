using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private GameObject human;
    [SerializeField] private GameObject center;
    [SerializeField] private float detectionMeterIncrement = .3f;

    // Update is called once per frame
    void Update()
    {
        human.transform.RotateAround(center.transform.position, Vector3.up, 15f * Time.deltaTime);
    }

    // if something stays in the cone, make a raycast and see if the tag of the first object is the gun
    // its not precise as it could be hitting the cast on a corner while you're clearly in view
    // it is also 2 AM, fuck this I got close
    private void OnTriggerStay(Collider other)
    {
        RaycastHit hitPoint;
        Ray ray = new Ray(other.gameObject.transform.position, transform.position);
        if (Physics.Raycast(ray, out hitPoint))
        {
            if (hitPoint.collider.tag == "Pistol" || hitPoint.collider.tag == "Shotgun" || hitPoint.collider.tag == "Rifle")
            {
                EventManager.current.OnIncrementDetectionMeter(detectionMeterIncrement);
            }
        }
    }
}
