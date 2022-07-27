using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public static bool HasEnemyShild;

    [SerializeField] private Transform shildSpawnPoint;
    [SerializeField] private float rotationSpeed;
    private Transform shellSpawnPosition;
    private Transform cannonAim;
    private WaitForSeconds delay;

    private float shootTargetOffset = 5f;
    private float minRotationAngle = -0.9f;
    private float maxRotationAngle = 0f;
    private float rayDistance = 100f;
    private float shildEnableAtteptInterval = 2f;
    private bool inUpperDir;
    private bool inDownDir;


    private void Awake()
    {
        cannonAim = transform.Find("cannonAim");
        shellSpawnPosition = cannonAim.Find("shellSpawnPosition");
        inDownDir = true;
        inUpperDir = false;
    }
    private void Start()
    {
        delay = new WaitForSeconds(shildEnableAtteptInterval);
        StartCoroutine(TryEnableShild());
    }

    private void Update()
    {
        CannonRotationHandler();

        RaycastHit2D[] hitsInfo = Physics2D.RaycastAll(cannonAim.position, cannonAim.up, rayDistance);
        Debug.DrawRay(cannonAim.position, cannonAim.up * rayDistance, Color.green);

        for(int i = 0; i < hitsInfo.Length; i++)
        {
            if (hitsInfo[i].collider != null)
            {        
                Debug.DrawRay(cannonAim.position, cannonAim.up * rayDistance, Color.red);
                if (hitsInfo[i].collider.CompareTag("Player"))
                {
                    RandomShooting(hitsInfo[i].point * shootTargetOffset);
                }
            }
        }    
    }

    private void RandomShooting(Vector3 target)
    {
        int rnd = Random.Range(0, 1000);
        int shootChance = 3;
        
        if (shootChance > rnd)
        {
            CannonShell.Create(shellSpawnPosition.position, target, transform.tag);
        }
    }

    private void CannonRotationHandler()
    {
        if (inDownDir == true && cannonAim.rotation.z >= minRotationAngle)
        {
            cannonAim.Rotate(Vector3.back * Time.deltaTime * rotationSpeed);
            if (cannonAim.rotation.z <= minRotationAngle)
            {
                inDownDir = false;
                inUpperDir = true;
            }
        }

        if (inUpperDir == true && cannonAim.rotation.z <= maxRotationAngle)
        {
            cannonAim.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
            if (cannonAim.rotation.z >= maxRotationAngle)
            {
                inDownDir = true;
                inUpperDir = false;
            }
        }
    }
    private IEnumerator TryEnableShild()
    {
        while(true)
        {
            if (HasEnemyShild == false)
            {
                var rnd = Random.Range(0, 10);
                Debug.Log(rnd);
                if (rnd == 2)
                {
                    EnableShild();
                    HasEnemyShild = true;
                }
                yield return delay;
            }
        }     
    }
    public void EnableShild()
    {
         HasEnemyShild = true;
         Transform pfPlayerShild = Resources.Load<Transform>("pfEnemyShild");
         Shild.Create(shildSpawnPoint.position, pfPlayerShild);
    }
}
