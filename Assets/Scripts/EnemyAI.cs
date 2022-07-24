using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;
    private float angle;
    private Transform shellSpawnPosition;
    private Transform cannonAim;
    private bool isStoped;

    

    private void Awake()
    {
        cannonAim = transform.Find("cannonAim");
        shellSpawnPosition = cannonAim.Find("shellSpawnPosition");
    }

    private void Update()
    {
        Debug.Log(Mathf.Atan(cannonAim.rotation.z) *Mathf.Rad2Deg);
        angle = Mathf.Atan(cannonAim.rotation.z) * Mathf.Rad2Deg;
        Mathf.Clamp(angle, 0, 40);

        if (angle < -40 && isStoped == false)
        {
            isStoped = true;
            cannonAim.Rotate(Vector3.forward * 2 * rotationSpeed * rotationSpeed);
        }
        else
        {
            cannonAim.Rotate(Vector3.back * rotationSpeed * rotationSpeed);
            isStoped = false;
        }



        //if (cannonAim.rotation.z >= 0f)
        //    cannonAim.Rotate(Vector3.back * rotationSpeed * rotationSpeed);

        //if (cannonAim.rotation.z <= -120f)
        //    cannonAim.Rotate(Vector3.forward * rotationSpeed * rotationSpeed);

    }
}
