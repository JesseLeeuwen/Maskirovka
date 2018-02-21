using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

using Maskirovka.UI;

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
            if( Input.GetMouseButtonUp(0) )
            {
                RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward, 20, 1 << 8);
                if( hitInfo.collider != null )
                    manager.ReceiveSelection( hitInfo.collider.GetComponent<ISelectable>() ) ;
            }
        }
    }

    public interface ISelectable
    {
        SelectableType GetType();
    }
}