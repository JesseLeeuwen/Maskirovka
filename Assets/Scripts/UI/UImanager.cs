using UnityEngine;
using Maskirovka.News;
using Maskirovka.Selector;

namespace Maskirovka.UI
{
    public class UImanager : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private NewsPanel newsPanel; 

        public void ReceiveSelection( ISelectable selected )
        {
            SelectableType type = selected.GetType();
            if( type == SelectableType.News )
            {
            }
            else if ( type == SelectableType.Country )
            {

            }
        }
    }
}