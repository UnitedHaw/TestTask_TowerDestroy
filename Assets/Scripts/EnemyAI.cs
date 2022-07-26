using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public static bool HasEnemyShild;

    [SerializeField] private Transform shildSpawnPoint;
    [SerializeField] private float rotationSpeed;
    private Transform shellSpawnPosition;
    private Transform cannonAim;
    private WaitForSeconds delay;

    private float shootTargetOffset = 4f;
    private float minRotationAngle = -0.9f;
    private float maxRotationAngle = 0f;
    private float rayDistance = 100f;
    private float shildEnableAtteptInterval = 3f;
    private float shildColdown = 15f;
    private bool inUpperDir;
    private bool inDownDir;

    private float shootTimer;
    private float shootTimerMax = 0.3f;


    private bool ShildTimerEnabled;
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
        EnemyScaner();
        ShotDelay();
    }

    private IEnumerator TryEnableShild()
    {        
        while (true)
        {
            if (HasEnemyShild == false && ShildTimerEnabled == false)
            {
                Debug.Log("������ �������� ���...");

                if (Random.Range(0, 5) == 2)
                {
                    EnableShild();
                    Debug.Log("� ��� � ���!");
                    HasEnemyShild = true;
                }
            }
            yield return delay;

            if (HasEnemyShild)
            {
                for (int i = 0; i <= shildColdown - shildEnableAtteptInterval; i++)
                {
                    yield return new WaitForSeconds(1f);
                    if (i == 15)
                    {
                        ShildTimerEnabled = false;
                    }
                }
            }
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

    private void EnemyScaner()
    {
        RaycastHit2D[] hitsInfo = Physics2D.RaycastAll(cannonAim.position, cannonAim.up, rayDistance);
        Debug.DrawRay(cannonAim.position, cannonAim.up * rayDistance, Color.green);

        for (int i = 0; i < hitsInfo.Length; i++)
        {
            if (hitsInfo[i].collider != null && hitsInfo[i].collider.CompareTag("Player"))
            {
                Debug.DrawRay(cannonAim.position, cannonAim.up * rayDistance, Color.red);
                if(shootTimer < 0)
                {
                    RandomShooting(hitsInfo[i].point * shootTargetOffset);
                    shootTimer += shootTimerMax;
                }     
            }
            else
            {
                Debug.DrawRay(cannonAim.position, cannonAim.up * rayDistance, Color.green);
            }
        }
    }
    private void ShotDelay()
    {
        shootTimer -= Time.deltaTime;
        if (Mathf.Abs(shootTimer) > shootTimerMax)
            shootTimer = shootTimerMax;
    }

    private void RandomShooting(Vector3 target)
    {
        
        int shootChance = 40;
        var rnd = Random.Range(0, 101);
        Debug.Log(rnd);
        if (shootChance > rnd)
        {
            CannonShell.Create(shellSpawnPosition.position, target, transform.tag);
        }
    }

    public void EnableShild()
    {
        HasEnemyShild = true;
        ShildTimerEnabled = true;
        Transform pfPlayerShild = GameAssets.Instance.pfEnemyShild;
        Shild.Create(shildSpawnPoint.position, pfPlayerShild);
    }
}
