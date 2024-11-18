using UnityEngine;
using TMPro;

public class ShopController : MonoBehaviour
{
    [SerializeField] private GameObject[] purchaseButtons; // Кнопки для покупки
    [SerializeField] private GameObject[] purchasedObjects; // Объекты, которые появляются после покупки
    [SerializeField] private TextMeshProUGUI pointsText; // Текст для отображения очков
    private const string PlayerPointsKey = "PlayerPoints"; // Ключ для очков в PlayerPrefs
    private const int BonusCost = 1000; // Стоимость одного бонуса

    private void Start()
    {
        UpdatePointsText();
        UpdateShopUI();
    }

    public void BuyBonus(int bonusIndex)
    {
        int currentPoints = PlayerPrefs.GetInt(PlayerPointsKey, 0);

        if (currentPoints >= BonusCost && bonusIndex < purchaseButtons.Length)
        {
            
            if (bonusIndex == 2)
            {
                for (int i = 0; i < 10; i++)
                {
                    Debug.Log(i);
                    PlayerPrefs.SetInt("CurrentLevel", i);
                    PlayerPrefs.Save();

                    var levelTracker = FindObjectOfType<TierProgressArchitect>();
                    levelTracker.MarkLevelAsCompleted(i); 
                }
                
            }
            // Снимаем очки
            currentPoints -= BonusCost;
            PlayerPrefs.SetInt(PlayerPointsKey, currentPoints);

            // Скрываем кнопку и активируем купленный объект
            purchaseButtons[bonusIndex].SetActive(false);
            purchasedObjects[bonusIndex].SetActive(true);

            // Сохраняем состояние покупки
            PlayerPrefs.SetInt($"BonusPurchased_{bonusIndex}", 1);

            UpdatePointsText();
        }
        else
        {
            Debug.Log("Недостаточно очков для покупки.");
        }
    }

    private void UpdatePointsText()
    {
        int currentPoints = PlayerPrefs.GetInt(PlayerPointsKey, 0); // Получаем текущие очки
        pointsText.text = $"{currentPoints.ToString("D4")} points"; // Отображаем очки с лидирующими нулями
    }

    private void UpdateShopUI()
    {
        for (int i = 0; i < purchaseButtons.Length; i++)
        {
            bool isPurchased = PlayerPrefs.GetInt($"BonusPurchased_{i}", 0) == 1;

            purchaseButtons[i].SetActive(!isPurchased);
            purchasedObjects[i].SetActive(isPurchased);
        }
    }
}