using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance { get; private set; }

    public static bool HasShild;

    [SerializeField] private Transform aimTransform;
    [SerializeField] private Transform shildSpawnPoint;
    [SerializeField] private float shootTimerMax;
    private float shootTimer;
    private Transform shellSpawnPoint;
    private Transform cannonAim;
    private Shild shildTransform;
    private Vector3 touchStartPosition;
    private Touch touch;


    private void Awake()
    {
        Instance = this;
        
        cannonAim = transform.Find("cannonAim");
        shellSpawnPoint = cannonAim.Find("shellSpawnPosition");
    }
    private void Update()
    {    
        ShotDelay();
        GetMouseInput();
        GetTouchInput();
    }

    private void GetMouseInput()
    {
        if(Input.GetMouseButton(0))
        {
            GetFolowAngleFromVector(UtilClass.GetMouseWorldPosition());
        }
        if(Input.GetMouseButtonUp(0))
        {
            HandleShooting();
        }
    }
    private void ShotDelay()
    {
        shootTimer -= Time.deltaTime;
        if (Mathf.Abs(shootTimer) > shootTimerMax)
            shootTimer = shootTimerMax;
    }
    private void GetTouchInput()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            touchStartPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchStartPosition.z = 0f;

            switch (touch.phase)
            {
                case TouchPhase.Moved:                   
                    GetFolowAngleFromVector(touchStartPosition);
                    break;
                case TouchPhase.Ended:
                    HandleShooting();
                    break;
            }
        }     
    }
    private void GetFolowAngleFromVector(Vector3 vectorPos)
    {
        Vector3 aimDirection = (vectorPos - cannonAim.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        cannonAim.eulerAngles = new Vector3(0, 0, angle);
    }
    private void HandleShooting()
    {
        if(shootTimer <= 0f)
        {
            shootTimer += shootTimerMax;
            CannonShell.Create(shellSpawnPoint.position, GetShotAimPosition(), transform.tag);
        }  
    }

    public void EnableShild()
    {
        if (HasShild != true && shildTransform == null)
        {
            HasShild = true;
            Transform pfPlayerShild = GameAssets.Instance.pfPlayerShild;
            shildTransform = Shild.Create(shildSpawnPoint.position, pfPlayerShild);
            Debug.Log("Shild Activated!");
        }
    }
    public Vector3 GetShotAimPosition()
    {
        return aimTransform.position;
    }
}