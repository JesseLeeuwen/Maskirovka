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
                // to prevent stupid missing ref error
                try{
                    if( selected != null )
                        ((MonoBehaviour)selected).SendMessage("Select", 0, SendMessageOptions.DontRequireReceiver);
                }catch( MissingReferenceException e){ Debug.LogWarning("unity fucked up again"); }

                // get new selected Object
                PointerEventData data = ExtendedStandaloneInputModule.GetPointerEventData(-1);
                selected = null;

                if( data.pointerPressRaycast.gameObject != null )
                {
                    if( data.pointerPressRaycast.gameObject.layer != 8 )
                        return;
                    selected = data.pointerPressRaycast.gameObject.GetComponent<ISelectable>();                    
                    manager.ReceiveSelection( selected );
                    
                    if( manager.IsInvasionMode() == false )
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