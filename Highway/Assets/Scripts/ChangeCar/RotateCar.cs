using UnityEngine;
using UnityEngine.EventSystems;

public class RotateCar : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] Transform PlayerCarHolder;
    Touch touch;
    bool Pressed;

    void Update()
    {
        if(Pressed)
        {
            if(Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);

                if(touch.phase == TouchPhase.Moved)
                {
                    PlayerCarHolder.Rotate(0, -touch.deltaPosition.x, 0);
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }
}
