using UnityEngine;

public class DeflectorPanelManager : MonoBehaviour
{
    public float movementSpeed = 5f; // Скорость движения платформы
    public float screenBoundary; // Границы экрана
    private Vector3 startPosition;
    private bool canMove = false; // Управление разрешено только после запуска шарика
    private int moveDirection = 0; // -1 для влево, 1 для вправо, 0 для остановки

    void Start()
    {
        float halfPaddleWidth = transform.localScale.x / 2;
        screenBoundary = Camera.main.aspect * Camera.main.orthographicSize - halfPaddleWidth;
        startPosition = transform.position;
    }

    void Update()
    {
        if (canMove)
        {
            MovePaddle();
        }
    }

    public void EnableMovement(bool enable)
    {
        canMove = enable;
    }

    public void ResetPosition()
    {
        canMove = false;
        moveDirection = 0;
        transform.position = startPosition;
    }

    public void SetMoveDirection(int direction)
    {
        moveDirection = direction;
    }

    void MovePaddle()
    {
        // Перемещаем платформу
        Vector3 newPosition = transform.position + Vector3.right * moveDirection * movementSpeed * Time.deltaTime;

        // Ограничиваем движение в пределах экрана
        newPosition.x = Mathf.Clamp(newPosition.x, -screenBoundary, screenBoundary);

        transform.position = newPosition;
    }
}