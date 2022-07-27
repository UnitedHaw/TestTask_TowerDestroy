using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    [SerializeField] private HealthSystem playerHealthSystem;
    [SerializeField] private HealthSystem enemyHealthSystem;
    private Transform hudtransform;
    private Transform pauseMenuTransform;
    public Button pauseBtn;
    public Button resumeBtn;
    public Button restartBtn;
    public Button shildActivationBtn;
    public VisualElement hudRoot;
    public VisualElement pauseMenuRoot;
    public VisualElement shildBtnDisabledContainer;
    public VisualElement playerHealthBar;
    public VisualElement enemyHealthBar;
    public Label shildActivationTimer;

    private float shildDisableTimer = 15f;
    private float shildDisableTimeLeft;
    private string formatedTimer;
    private bool timerEnabled;

    private void Awake()
    {
        hudtransform = transform.GetChild(0);
        pauseMenuTransform = transform.GetChild(1);
    }
    private void Start()
    {
        hudRoot = hudtransform.GetComponent<UIDocument>().rootVisualElement;
        pauseMenuRoot = pauseMenuTransform.GetComponent<UIDocument>().rootVisualElement;

        pauseBtn = hudRoot.Q<Button>("pauseBtn");
        shildActivationBtn = hudRoot.Q<Button>("shildBtn");
        shildBtnDisabledContainer = hudRoot.Q<VisualElement>("shildBtnDisabledContainer");
        shildActivationTimer = hudRoot.Q<Label>("timerText");
        resumeBtn = pauseMenuRoot.Q<Button>("resumeBtn");
        restartBtn = pauseMenuRoot.Q<Button>("restartBtn");


        playerHealthBar = hudRoot.Q<VisualElement>("playerHealthBar");
        playerHealthSystem.OnDamaged += PlayerHealthSystem_OnDamaged;

        enemyHealthBar = hudRoot.Q<VisualElement>("enemyHealthBar");
        enemyHealthSystem.OnDamaged += EnemyHealthSystem_OnDamaged;

        Hide(pauseMenuRoot);

        pauseBtn.clicked += (() => {
            Time.timeScale = 0f;
            Show(pauseMenuRoot);
            Hide(hudRoot);
        });

        shildActivationBtn.clicked += (() =>
        {
            Hide(shildActivationBtn);
            Show(shildBtnDisabledContainer);
            PlayerControl.Instance.EnableShild();

            timerEnabled = true;
            shildDisableTimeLeft = shildDisableTimer;
        });

        resumeBtn.clicked += (() => {
            Time.timeScale = 1f;
            Show(hudRoot);
            Hide(pauseMenuRoot);
        });

        restartBtn.clicked += (() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        });
    }

    private void EnemyHealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        enemyHealthBar.style.width = new StyleLength(Length.Percent(enemyHealthSystem.GetHealthAmount()));
    }

    private void PlayerHealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        playerHealthBar.style.width = new StyleLength(Length.Percent(playerHealthSystem.GetHealthAmount()));
    }

    private void Update()
    {
        ShildTimer(timerEnabled);
    }
    private void ShildTimer(bool timerEnabled)
    {
        if (timerEnabled)
        {
            if (shildDisableTimeLeft > 0f)
            {
                shildDisableTimeLeft -= Time.deltaTime;
                formatedTimer = string.Format("{0:0}:{1:00}", 0, shildDisableTimeLeft);
                shildActivationTimer.text = formatedTimer;
            }
            if (shildDisableTimeLeft <= 0f)
            {
                timerEnabled = false;
                Hide(shildBtnDisabledContainer);
                Show(shildActivationBtn);
            }
        }
    }

    public void Show(VisualElement visualElement)
    {
        visualElement.style.display = DisplayStyle.Flex;
    }
    public void Hide(VisualElement visualElement)
    {
        visualElement.style.display = DisplayStyle.None;
    }
}