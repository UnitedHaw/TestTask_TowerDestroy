using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction : MonoBehaviour
{
    private HealthSystem healthSystem;
    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.OnDied += HealthSystem_OnDied;
    }
    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        Shild shild = GetComponent<Shild>();
        if(shild != null)
        {
            if (shild.CompareTag("Player"))
            {
                PlayerControl.HasShild = false;
            }

            if (shild.CompareTag("Enemy"))
            {
                EnemyAI.HasEnemyShild = false;
            }
        } 
        Instantiate(GameAssets.Instance.pfTowerDestroyedParticles, transform.position, Quaternion.identity);
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroy);
        Destroy(gameObject);       
    }
}
