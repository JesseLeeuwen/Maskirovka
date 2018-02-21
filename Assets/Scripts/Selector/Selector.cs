using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Maskirovka.Selector
{
    public class Selector : MonoBehaviour
    {
        [SerializeField]
        private UIManager manager;

        [SerializeField]
        private ISelectable selected;


        public ISelectable GetSelected()
        {
            return selected;
        }

        void Update()
        {
        

        }
    }

    public interface ISelectable
    {
        SelectableType GetType();
    }
}