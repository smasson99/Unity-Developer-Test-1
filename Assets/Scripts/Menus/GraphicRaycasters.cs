using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Values;

namespace Menus
{
    public class GraphicRaycasters : MonoBehaviour
    {
        private List<GraphicRaycaster> graphicRaycasters;
        private PointerEventData pointerEventData;
        private EventSystem eventSystem;
        
        public TransformValue EventSystemTransform;
        
        private void Start()
        {
            graphicRaycasters = GetComponentsInChildren<GraphicRaycaster>().ToList();
            eventSystem = EventSystemTransform.Value.GetComponent<EventSystem>();
        }

        public bool IsPointerOnUI()
        {
            pointerEventData = new PointerEventData(eventSystem) {position = Mouse.current.position.ReadValue()};

            var raycastResults = new List<RaycastResult>();
            
            graphicRaycasters.ForEach(x => x.Raycast(pointerEventData, raycastResults));

            // foreach (RaycastResult result in raycastResults)
            // {
            //     Debug.Log("Hit " + result.gameObject.name);
            // }
            //
            // Debug.Log($"Length : {raycastResults.Count}");
            
            return raycastResults.Count > 0;
        }
    }
}
