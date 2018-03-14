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
        public float biasChanger;

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

        public bool Send(bool youcandie)
        {
            if(youcandie == false)
            {
                bool result = GameManager.Instance.processor.ProccesNews(this);
                return result;
            }
            
            if(youcandie == true)
            {
                Destroy(gameObject);
            }
            return false;
        }
    }
}