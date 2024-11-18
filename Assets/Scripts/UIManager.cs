using UnityEngine;

public class UIManager : MonoBehaviour
{
    private DeflectorPanelManager deflectorPanelManager;

    void Start()
    {
        deflectorPanelManager = FindObjectOfType<DeflectorPanelManager>();
        if (deflectorPanelManager == null)
        {
            Debug.LogError("PaddleController не найден!");
        }
    }

    public void OnLeftButtonDown()
    {
        deflectorPanelManager.SetMoveDirection(-1); // Движение влево
    }

    public void OnRightButtonDown()
    {
        deflectorPanelManager.SetMoveDirection(1); // Движение вправо
    }

    public void OnButtonUp()
    {
        deflectorPanelManager.SetMoveDirection(0); // Остановка движения
    }
}