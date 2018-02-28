using System;
using UnityEngine;

using Maskirovka.Selector;

namespace Maskirovka.News
{
    public class News : MonoBehaviour, ISelectable
    {
        public float value;
        public Catagorie catagorie;
        public Country country;
        public int chanceOfSucces;
        public bool playerChanged;
 
        public void Init( float value, Catagorie catagorie, Country country )
        {
            this.value = value;
            this.catagorie = catagorie;
            this.country = country;
        }

        public new SelectableType GetType()
        {
            return SelectableType.News;
        }

        public void Send()
        {
            bool result = GameManager.Instance.processor.ProccesNews( this );
            Destroy( gameObject );
        }
    }
}