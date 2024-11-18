using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class HighScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI[] highScoreTexts; // Массив текстовых полей для отображения рекордов
    public string levelName; // Название уровня, рекорды которого нужно загрузить

    void Start()
    {
        DisplayHighScores();
    }

    void DisplayHighScores()
    {
        List<float> scores = HighScoreManager.LoadHighScores(levelName);

        for (int i = 0; i < highScoreTexts.Length; i++)
        {
            if (i < scores.Count)
            {
                highScoreTexts[i].text = $"{i + 1}. {scores[i].ToString()} sec"; // Форматируем время до двух знаков после запятой
            }
            else
            {
                highScoreTexts[i].text = $"{i + 1}. ---"; // Если рекорд отсутствует
            }
        }
    }
}