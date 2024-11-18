using TMPro;
using UnityEngine;

public class StageFlowManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] levelIndicators;
    [SerializeField] private TextMeshProUGUI levelIOpenWin;
    [SerializeField] private TextMeshProUGUI currentLevelDisplay;

    private int activeLevel;

    private void Awake()
    {
        LoadCurrentLevel();
        UpdateLevelDisplay();
    }

    private void LoadCurrentLevel()
    {
        activeLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
    }

    private void UpdateLevelDisplay()
    {
        foreach (var levelText in levelIndicators)
        {
            levelText.text = "Level " + activeLevel;
        }
        currentLevelDisplay.text = "Level " + activeLevel;
        levelIOpenWin.text = "Level " + (activeLevel + 1);
        if (activeLevel>=10)
        {
            currentLevelDisplay.text = "Level " + 10;
            levelIOpenWin.text = "Level " + 10;
            foreach (var levelText in levelIndicators)
            {
                levelText.text = "Level " + 10;
            }
        }
    }

    public void WinGame()
    {
        PlayerPrefs.SetInt("CurrentLevel", activeLevel + 1);
        PlayerPrefs.Save();

        var levelTracker = FindObjectOfType<TierProgressArchitect>();
        levelTracker.MarkLevelAsCompleted(activeLevel);
    }
}