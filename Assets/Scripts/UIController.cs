using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public Button pauseBtn;
    public Button shildBtn;

    private void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        pauseBtn = root.Q<Button>("pauseBtn");
        shildBtn = root.Q<Button>("shildBtn");

        pauseBtn.clicked += PauseButtonPressed;
        shildBtn.clicked += ShildButtonPressed;
    }

    private void PauseButtonPressed()
    {
        Time.timeScale = 0f;      
    }
    private void ShildButtonPressed()
    {
        Transform pfShild = Resources.Load<Transform>("pfPlayerShild");
        Instantiate(pfShild);
    }
}
