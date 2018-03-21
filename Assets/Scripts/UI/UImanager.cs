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
        private ReputationTable countryInfo;
        [SerializeField]
        private GameObject countryInfoContainer;
        [SerializeField]
        private float targetFocus;
        [SerializeField]
        private InvasionManager invasion;
        [SerializeField]
        private float speed;
        private float focus;
        
        private bool isInvasionMode;

        public void ToggleInvasionMode()
        {
            isInvasionMode = !isInvasionMode;
        }

        public bool IsInvasionMode()
        {
            return isInvasionMode;
        }

        public void ReceiveSelection( ISelectable selected )
        {
            SelectableType type = selected.GetType();
            if( isInvasionMode == true && type == SelectableType.Country )
            {
                invasion.InvadeAttempt( (Country)selected );
                return;        
            }
            
            if( type == SelectableType.NewsFeedItem && newsPanel.gameObject.activeInHierarchy == false )
            {
                newsPanel.Init( (NewsFeedItem)selected );
                targetFocus = 0;
            }
            else if ( type == SelectableType.Country )
            {
                targetFocus = 1;
                countryInfo.Init( (Country)selected );
            }

            countryInfoContainer.SetActive( type == SelectableType.Country );
        }

        void Update()
        {
            focus = Mathf.MoveTowards( focus, targetFocus, speed * Time.deltaTime);
            animator.SetFloat("Focus", focus);
        }
    }
}