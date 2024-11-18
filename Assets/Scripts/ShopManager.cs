using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI pointsText; // Текст для отображения очков
    private const string PlayerPointsKey = "PlayerPoints"; // Ключ для сохранения очков в PlayerPrefs

    void Start()
    {
        UpdatePointsText(); // Обновляем текст при загрузке сцены
    }

    public void SpendPoints(int amount)
    {
        int currentPoints = PlayerPrefs.GetInt(PlayerPointsKey, 0); // Получаем текущие очки

        if (currentPoints >= amount)
        {
            currentPoints -= amount; // Уменьшаем очки
            PlayerPrefs.SetInt(PlayerPointsKey, currentPoints); // Сохраняем изменения
            PlayerPrefs.Save(); // Принудительное сохранение
            UpdatePointsText(); // Обновляем текст
        }
        else
        {
            Debug.Log("Недостаточно очков!");
        }
    }

    private void UpdatePointsText()
    {
        int currentPoints = PlayerPrefs.GetInt(PlayerPointsKey, 0); // Получаем текущие очки
        pointsText.text = $"{currentPoints.ToString("D4")} points"; // Форматируем с лидирующими нулями
    }
}