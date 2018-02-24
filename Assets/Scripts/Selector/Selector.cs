using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

using Maskirovka.UI;
using Maskirovka.Utility;

namespace Maskirovka.Selector
{
    public class Selector : MonoBehaviour
    {
        [SerializeField]
        private UImanager manager;
        [SerializeField]
        private ISelectable selected;

        public ISelectable GetSelected()
        {
            return selected;
        }

        void Update()
        {
            if( ExtendedStandaloneInputModule.OnUI(0,1) == true )
                return;

            if( Input.GetMouseButtonUp(0) )
            {
                RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward, 20, 1 << 8 | 1 << 5);
                if( hitInfo.collider != null )
                {
                    ISelectable selected = hitInfo.collider.GetComponent<ISelectable>();
                    if( selected != null )
                        manager.ReceiveSelection( hitInfo.collider.GetComponent<ISelectable>() ) ;
                }
            }
        }
    }

    public interface ISelectable
    {
        SelectableType GetType();
    }
}