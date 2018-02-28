using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

using Maskirovka.UI;
using Maskirovka.Utility;

namespace Maskirovka.Selector
{
    public interface ISelectable
    {
        SelectableType GetType();
    }

    public class Selector : MonoBehaviour
    {
        [SerializeField]
        private UImanager manager;
        [SerializeField]
        private ISelectable selected;

        void Update()
        {
            if( ExtendedStandaloneInputModule.OnUI(0,1) == true )
                return;

            if( Input.GetMouseButtonUp(0) )
            {
                // deselect previous object
                if( selected != null )                
                    ((MonoBehaviour)selected).SendMessage("Select", 0, SendMessageOptions.DontRequireReceiver);
                
                // get new selected Object
                PointerEventData data = ExtendedStandaloneInputModule.GetPointerEventData(-1);
                selected = data.pointerPressRaycast.gameObject.GetComponent<ISelectable>();
                if( selected != null )
                {
                    manager.ReceiveSelection( selected );
                    ((MonoBehaviour)selected).SendMessage("Select", 1, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    
        public ISelectable GetSelected()
        {
            return selected;
        }
    }
}