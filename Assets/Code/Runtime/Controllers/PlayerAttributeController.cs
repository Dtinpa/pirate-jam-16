using UnityEngine;
using System.Collections;

namespace Code.Runtime
{
    public class PlayerAttributeController : MonoBehaviour
    {
        // Keeps track of bullets, reloading, gun physics, and gun aiming mechanics
        [SerializeField] private int numOfBullets = 8;
        [SerializeField] private float reloadingTime = 1;

        [SerializeField] private float knockbackForce = 200f;
        [SerializeField] private float upperKnockbackForce = 150f;

        [SerializeField] private Camera camera;

        public int totalBullets { get; private set; }

        public static PlayerAttributeController current;

        private bool reloading = false;
        private bool aiming = false;

        private void Start()
        {
            current = this;
            totalBullets = numOfBullets;

            EventManager.current.PlayerFireBullet += PlayerFireBullet;
            EventManager.current.PlayerAimGunToggle += PlayerAimGunToggle;

            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }

        private void FixedUpdate()
        {
            // if we're currently aiming the gun to move, start tracking the mouse's rotation
            if (aiming && !reloading)
            {
                SetGunRotation();
            }
        }

        private void OnDestroy()
        {
            EventManager.current.PlayerFireBullet -= PlayerFireBullet;
            EventManager.current.PlayerAimGunToggle -= PlayerAimGunToggle;
        }

        private void PlayerAimGunToggle()
        {
            aiming = !aiming;
            if (!reloading)
            {
                ToggleKinematic();
            }
        }

        private void ToggleKinematic()
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = aiming;
        }

        // syncs the rotation of the gun when aiming to match where the camera is facing
        private void SetGunRotation()
        {
            this.transform.eulerAngles = new Vector3(
                camera.transform.eulerAngles.x,
                camera.transform.eulerAngles.y,
                camera.transform.eulerAngles.z
            );
        }

        private void PlayerFireBullet()
        { 
            // if we're in the process of reloading, we don't want to increment the UI and num of bullets, or start another coroutine
            if (totalBullets <= 0 && !reloading)
            {
                StartCoroutine(Reload());
                totalBullets = numOfBullets;
            }
            else if(!reloading)
            {
                totalBullets -= 1;
                EventManager.current.OnUpdateBulletUI(totalBullets);

                this.gameObject.GetComponent<Rigidbody>().AddForce(-transform.forward * knockbackForce * 3f);
                this.gameObject.GetComponent<Rigidbody>().AddForce(-transform.up * upperKnockbackForce * 3f);
            }
        }

        // set the reloading state before and after it waits for the reloading time
        IEnumerator Reload()
        {
            reloading = true;
            yield return new WaitForSecondsRealtime(reloadingTime);

            EventManager.current.OnUpdateBulletUI(totalBullets);
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            reloading = false;
        }
    }
}