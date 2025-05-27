using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Gameplay UI")]
    public TextMeshProUGUI ammoText;
    public Image gunIcon;
    public Slider healthBar;
    public GameObject crosshair;
    public GameObject equipPrompt;

    [Header("Weapon Inventory UI")]
    public TextMeshProUGUI weaponInventoryText;

    [Header("Pause Menu")]
    public GameObject pauseMenu;
    public Button resumeButton;
    public Button restartButton;
    public Button quitButton;

    [Header("Win/Lose Screens")]
    public GameObject winScreen;
    public GameObject loseScreen;
    public Button winRestartButton;
    public Button winQuitButton;
    public Button loseRestartButton;
    public Button loseQuitButton;

    private bool isGamePaused = false;
    private Coroutine blinkCoroutine;
    private Color defaultAmmoColor;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (ammoText != null)
            defaultAmmoColor = ammoText.color;
    }

    private void OnEnable()
    {
        WeaponBase.OnAmmoChanged += UpdateAmmoUI;
        WeaponInventory.OnWeaponSwitched += UpdateGunIcon;
        WeaponInventory.OnInventoryUpdated += UpdateWeaponInventoryUI;
        PlayerHealth.OnPlayerHealthChanged += UpdateHealthBar;
        PlayerHealth.OnPlayerDeath += ShowLoseScreen;
        EnemyManager.OnAllEnemiesDead += ShowWinScreen;

        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
        winRestartButton.onClick.AddListener(RestartGame);
        winQuitButton.onClick.AddListener(QuitGame);
        loseRestartButton.onClick.AddListener(RestartGame);
        loseQuitButton.onClick.AddListener(QuitGame);
    }

    private void OnDisable()
    {
        WeaponBase.OnAmmoChanged -= UpdateAmmoUI;
        WeaponInventory.OnWeaponSwitched -= UpdateGunIcon;
        WeaponInventory.OnInventoryUpdated -= UpdateWeaponInventoryUI;
        PlayerHealth.OnPlayerHealthChanged -= UpdateHealthBar;
        PlayerHealth.OnPlayerDeath -= ShowLoseScreen;
        EnemyManager.OnAllEnemiesDead -= ShowWinScreen;
    }

    private void UpdateAmmoUI(int currentAmmo, int totalAmmo)
    {
        ammoText.text = $"{currentAmmo}/{totalAmmo}";

        if (currentAmmo == 0)
        {
            if (blinkCoroutine == null)
                blinkCoroutine = StartCoroutine(BlinkAmmoText());
        }
        else
        {
            if (blinkCoroutine != null)
            {
                StopCoroutine(blinkCoroutine);
                blinkCoroutine = null;
                ammoText.color = defaultAmmoColor;
            }
        }
    }

    private IEnumerator BlinkAmmoText()
    {
        while (true)
        {
            ammoText.color = Color.red;
            yield return new WaitForSeconds(0.25f);
            ammoText.color = defaultAmmoColor;
            yield return new WaitForSeconds(0.25f);
        }
    }

    private void UpdateGunIcon(Sprite icon)
    {
        gunIcon.sprite = icon;
    }

    private void UpdateHealthBar(int currentHealth)
    {
        healthBar.value = currentHealth;
    }

    private void UpdateWeaponInventoryUI(List<WeaponData> weapons)
    {
        weaponInventoryText.text = "";
        int weaponIndex = 1;
        foreach (WeaponData weapon in weapons)
        {
            weaponInventoryText.text += $"{weaponIndex} - {weapon.weaponName}\n";
            weaponIndex++;
        }
    }

    public void ShowEquipPrompt(bool show, string weaponName = "")
    {
        if (equipPrompt != null)
        {
            equipPrompt.SetActive(show);
            if (show)
            {
                TextMeshProUGUI textComponent = equipPrompt.GetComponent<TextMeshProUGUI>();
                if (textComponent != null)
                    textComponent.text = $"Press E to Equip {weaponName}";
            }
        }
    }

    private void ShowWinScreen()
    {
        GameplayUIActive(false);
        winScreen.SetActive(true);
        Time.timeScale = 0;
        FindFirstObjectByType<PlayerContoller>()?.UnlockCursor();
    }

    private void ShowLoseScreen()
    {
        GameplayUIActive(false);
        loseScreen.SetActive(true);
        Time.timeScale = 0;
        FindFirstObjectByType<PlayerContoller>()?.UnlockCursor();
    }

    private void GameplayUIActive(bool isActive)
    {
        ammoText.gameObject.SetActive(isActive);
        gunIcon.gameObject.SetActive(isActive);
        healthBar.gameObject.SetActive(isActive);
        crosshair.SetActive(isActive);
        weaponInventoryText.gameObject.SetActive(isActive);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused) ResumeGame();
            else PauseGame();
        }
    }

    private void PauseGame()
    {
        isGamePaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        FindFirstObjectByType<PlayerContoller>()?.UnlockCursor();
    }

    private void ResumeGame()
    {
        isGamePaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        FindFirstObjectByType<PlayerContoller>()?.LockCursor();
    }

    private void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void QuitGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}
