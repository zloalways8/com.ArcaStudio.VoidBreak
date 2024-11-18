using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ImpactNode : MonoBehaviour
{
    [SerializeField] private AudioSource audioComponentIncision;
    
    public TextMeshProUGUI blockText; // Текст на блоке
    public Image blockImage; // Изображение блока
    public Sprite[] blockSprites; // Массив изображений для разных состояний

    public int health; // Количество жизней блока
    private int currentHealth;

    private DifficultyManager difficultyManager; // Ссылка на менеджер сложности
    private CoreGameplayDirector coreGameplayDirector; // Ссылка на GameController

    private const string PlayerPointsKey = "PlayerPoints"; // Ключ для сохранения очков в PlayerPrefs

    void Awake()
    {
        // Найдем менеджер сложности на сцене
        difficultyManager = FindObjectOfType<DifficultyManager>();
        coreGameplayDirector = FindObjectOfType<CoreGameplayDirector>();

        // Устанавливаем количество жизней блока
        if (difficultyManager != null)
        {
            // Получаем список доступных уровней здоровья в зависимости от сложности
            var availableHealthLevels = difficultyManager.GetHealthLevels();
            health = availableHealthLevels[Random.Range(0, availableHealthLevels.Count)];
            
            if (GameSettingsManager.Instance.doubleHealthEnabled)
            {
                health = 1 ; // Удваиваем здоровье
            }
        }

        currentHealth = health;
        UpdateBlockText();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            audioComponentIncision.Play();
            AddPoints(10); // Добавляем очки за уничтожение блока
            TakeDamage(1); // Каждый удар шарика отнимает 1 единицу здоровья
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            DestroyBlock();
        }
        else
        {
            UpdateBlockText(); // Обновляем текст с количеством оставшихся жизней
            UpdateBlockImage(); // Обновляем изображение блока, если необходимо
        }
    }

    void UpdateBlockText()
    {
        blockText.text = currentHealth.ToString(); // Показываем оставшиеся жизни
    }

    void UpdateBlockImage()
    {
        if (blockSprites.Length > 0)
        {
            // Пример: меняем изображение в зависимости от количества здоровья
            int spriteIndex = Mathf.Clamp(blockSprites.Length - (health - currentHealth), 0, blockSprites.Length - 1);
            blockImage.sprite = blockSprites[spriteIndex];
        }
    }

    void DestroyBlock()
    {
        // Уведомляем GameController о том, что блок был уничтожен
        if (coreGameplayDirector != null)
        {
            coreGameplayDirector.BlockDestroyed(); // Уменьшаем количество оставшихся блоков
        }
        
        Destroy(gameObject); // Уничтожаем блок
    }

    void AddPoints(int points)
    {
        int currentPoints = PlayerPrefs.GetInt(PlayerPointsKey, 0); // Получаем текущие очки
        currentPoints += points; // Увеличиваем очки
        PlayerPrefs.SetInt(PlayerPointsKey, currentPoints); // Сохраняем их
        PlayerPrefs.Save(); // Принудительное сохранение

        // Обновляем текст очков
        PointsDisplay pointsDisplay = FindObjectOfType<PointsDisplay>();
        if (pointsDisplay != null)
        {
            pointsDisplay.UpdatePointsText();
        }
    }
}
