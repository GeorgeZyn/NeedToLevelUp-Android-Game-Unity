using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
   public Vector2 joystickVector;
   public RectTransform backgroundJoystick, handleJoystick;
   public float handleRange;

   public void OnPointerUp(PointerEventData eventData)
   {
      joystickVector = Vector2.zero;
      handleJoystick.anchoredPosition = Vector2.zero;
   }

   public void OnPointerDown(PointerEventData eventData)
   {
      OnDrag(eventData);
   }

   public void OnDrag(PointerEventData eventData)
   {
      if(RectTransformUtility.ScreenPointToLocalPointInRectangle(backgroundJoystick, eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
      {
         var sizeBackgroundJoystick = backgroundJoystick.sizeDelta;
         localPoint /= sizeBackgroundJoystick;

         joystickVector = new Vector2(localPoint.x * 3, localPoint.y * 3);
         joystickVector = joystickVector.magnitude > 1f ? joystickVector.normalized : joystickVector;

         float handlePos = sizeBackgroundJoystick.x / 2 * handleRange;
         handleJoystick.anchoredPosition = new Vector2(joystickVector.x * handlePos, joystickVector.y * handlePos);
      }
   }
}
