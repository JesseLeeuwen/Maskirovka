using UnityEngine;
 using UnityEngine.UI;
 using UnityEngine.EventSystems;
 
 [RequireComponent(typeof(RectTransform))]
 public class DraggableWindow : MonoBehaviour,
 IDragHandler
 
 {
     private RectTransform rect;
 
     public void Awake()
     {
         rect = GetComponent<RectTransform>();
     }
 
     public void OnDrag(PointerEventData eventData)
     {
 
         Vector3 currentPosition = rect.position;
         currentPosition.x += eventData.delta.x/50;
         currentPosition.y += eventData.delta.y/50;
         rect.position = currentPosition;
		 Debug.Log("Moved UI windows");
     }
 }