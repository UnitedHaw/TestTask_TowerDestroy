using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;
    private float angle;
    private Transform shellSpawnPosition;
    private Transform cannonAim;
    private bool isUp;
    private bool isDown;

    

    private void Awake()
    {
        cannonAim = transform.Find("cannonAim");
        shellSpawnPosition = cannonAim.Find("shellSpawnPosition");
        isDown = true;
        isUp = false;
    }

    private void Update()
    {
        CannonRotationHandler();
    }

    private void CannonRotationHandler()
    {
        if (isDown == true && cannonAim.rotation.z >= -0.9f)
        {
            cannonAim.Rotate(Vector3.back * Time.deltaTime * rotationSpeed);
            if (cannonAim.rotation.z <= -0.9f)
            {
                isDown = false;
                isUp = true;
            }
        }

        if (isUp == true && cannonAim.rotation.z <= 0f)
        {
            cannonAim.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
            if (cannonAim.rotation.z >= 0f)
            {
                isDown = true;
                isUp = false;
            }
        }
    }
}
