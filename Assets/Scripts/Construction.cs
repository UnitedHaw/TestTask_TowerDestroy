using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform enemyTransform;
    private HealthSystem healthSystem;
    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.OnDied += HealthSystem_OnDied;
    }
    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        if (transform == playerTransform || enemyTransform)
        {
            UIController.Instance.GameOver();
        }

        Shild shild = GetComponent<Shild>();
        if(shild != null)
        {
            if (shild.CompareTag("Player"))
            {
                PlayerControl.HasShild = false;
            }
        }
        
        Instantiate(GameAssets.Instance.pfTowerDestroyedParticles, transform.position, Quaternion.identity);
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroy);
        Destroy(gameObject);
    }
}
