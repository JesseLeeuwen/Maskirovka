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

        [SerializeField]
        private float targetFocus;
        [SerializeField]
        private float speed;
        private float focus;

        public void ReceiveSelection( ISelectable selected )
        {
            SelectableType type = selected.GetType();
            if( type == SelectableType.News )
            {
                newsPanel.Init( (News.News)selected );
                targetFocus = 0;
            }
            else if ( type == SelectableType.Country )
            {
                targetFocus = 1;
            }
        }

        void Update()
        {
            focus = Mathf.MoveTowards( focus, targetFocus, speed * Time.deltaTime);
            animator.SetFloat("Focus", focus);
        }
    }
}