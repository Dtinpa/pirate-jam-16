using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Use the Cinemachine camera
    [SerializeField] private GameObject camera;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 50;

    private void Start()
    {
        EventManager.current.DisableCurrentGun += DisableCurrentGun;
    }

    private void OnDestroy()
    {
        EventManager.current.DisableCurrentGun -= DisableCurrentGun;
    }

    // Update is called once per frame
    private void Update()
    {
        // forward
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 vector = new Vector3(0, 0, speed);
            rigidbody.AddForce(vector * Time.deltaTime);
        }

        // Left
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 vector = new Vector3(-speed, 0, 0);
            rigidbody.AddForce(vector * Time.deltaTime);
        }

        // Backward
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 vector = new Vector3(0, 0, -speed);
            rigidbody.AddForce(vector * Time.deltaTime);
        }

        // Right
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 vector = new Vector3(speed, 0, 0);
            rigidbody.AddForce(vector * Time.deltaTime);
        }

        // Jump
        if (Input.GetKey(KeyCode.Space))
        {
            if (controller.isGrounded)
            {
                rigidbody.AddForce(rigidbody.transform.up * speed);
            }
        }

        // Mouse click
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            EventManager.current.OnPlayerAimGunToggle();
        }

        // Mouse click
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            EventManager.current.OnPlayerAimGunToggle();
            EventManager.current.OnPlayerFireBullet();
        }
    }

    private void DisableCurrentGun()
    {
        Destroy(this.gameObject);
    }
}
