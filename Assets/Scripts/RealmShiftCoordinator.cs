using UnityEngine;
using UnityEngine.SceneManagement;

public class RealmShiftCoordinator : MonoBehaviour
{
    public void LoadTransitionScene()
    {
        LoadSceneByName(AudioCueRegistry.TransitionScene);
    }

    public void LoadGameplayScene()
    {
        LoadSceneByName(AudioCueRegistry.GameplayScene);
    }

    private void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}