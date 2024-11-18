using UnityEngine;
using TMPro;

public class PointsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] pointsText; // Текст для отображения очков
    private const string PlayerPointsKey = "PlayerPoints"; // Ключ для очков в PlayerPrefs

    void Start()
    {
        UpdatePointsText(); // Обновляем текст при запуске
    }

    public void UpdatePointsText()
    {
        int currentPoints = PlayerPrefs.GetInt(PlayerPointsKey, 0); // Получаем текущие очки
        for (int i = 0; i < pointsText.Length; i++)
        {
            pointsText[i].text = $"{currentPoints} points"; // Обновляем текст
        }
        
    }
}