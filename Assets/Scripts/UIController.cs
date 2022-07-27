using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Button pauseBtn;
    public Button resumeBtn;
    public Button restartBtn;

    private void Start()
    {

        var hud = transform.GetChild(0);
        var pauseMenu = transform.GetChild(1);

        

        VisualElement hudRoot = hud.GetComponent<UIDocument>().rootVisualElement;
        VisualElement pauseMenuRoot = pauseMenu.GetComponent<UIDocument>().rootVisualElement;

        pauseBtn = hudRoot.Q<Button>("pauseBtn");

        resumeBtn = pauseMenuRoot.Q<Button>("resumeBtn");
        restartBtn = pauseMenuRoot.Q<Button>("restartBtn");

        Hide(pauseMenu);


        pauseBtn.clicked += (() => {
            Time.timeScale = 0f;
            Show(pauseMenu);
            Hide(hud);
        });

        resumeBtn.clicked += (() => {
            Time.timeScale = 1f;
            Show(hud);
            Hide(pauseMenu);
        });

        restartBtn.clicked += (() =>
        {
            SceneManager.LoadScene(0);
        });


        //VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        //var backgorund = root.Q("background");

        //pauseBtn = root.Q<Button>("pauseBtn");
        //shildBtn = root.Q<Button>("shildBtn");

        //pauseBtn.clicked += PauseButtonPressed;
        //shildBtn.clicked += ShildButtonPressed;
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
    public void Show(Transform transform)
    {
        transform.gameObject.SetActive(true);
    }
    public void Hide(Transform transform)
    {
        transform.gameObject.SetActive(false);
    }
}
