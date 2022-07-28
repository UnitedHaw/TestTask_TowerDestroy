using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shild : MonoBehaviour
{
    public static Shild Create(Vector3 position, Transform pfShild)
    {
        Transform shildTransform = Instantiate(pfShild, position, Quaternion.identity);
        Shild shild = shildTransform.GetComponent<Shild>();
        SoundManager.Instance.PlaySound(SoundManager.Sound.ShildActivated);
        return shild;
    }
}
