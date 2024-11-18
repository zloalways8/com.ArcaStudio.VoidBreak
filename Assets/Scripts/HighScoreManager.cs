using UnityEngine;
using System.Collections.Generic;

public class HighScoreManager : MonoBehaviour
{
    private const int MaxScores = 5; // Максимальное количество рекордов

    // Метод для сохранения нового результата
    public static void SaveHighScore(string levelName, float time)
    {
        // Загружаем текущие рекорды
        List<float> scores = LoadHighScores(levelName);

        // Добавляем новый результат
        scores.Add(time);

        // Сортируем рекорды по убыванию
        scores.Sort((a, b) => b.CompareTo(a));

        // Оставляем только 5 лучших
        if (scores.Count > MaxScores)
        {
            scores.RemoveAt(scores.Count - 1);
        }

        // Сохраняем обновленные рекорды
        for (int i = 0; i < scores.Count; i++)
        {
            PlayerPrefs.SetFloat($"{levelName}_HighScore_{i}", scores[i]);
        }

        // Сохраняем количество рекордов
        PlayerPrefs.SetInt($"{levelName}_HighScoreCount", scores.Count);

        PlayerPrefs.Save();
    }

    // Метод для загрузки рекордов
    public static List<float> LoadHighScores(string levelName)
    {
        List<float> scores = new List<float>();

        int count = PlayerPrefs.GetInt($"{levelName}_HighScoreCount", 0);
        for (int i = 0; i < count; i++)
        {
            scores.Add(PlayerPrefs.GetFloat($"{levelName}_HighScore_{i}", 0));
        }

        return scores;
    }
}
