using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Maskirovka.News;
using Maskirovka;
using Maskirovka.Utility;
using Maskirovka.Selector;

namespace Maskirovka.UI
{
    public class SetCorrectLayerTutorial : MonoBehaviour
    {
        void Update()
        {
            if (gameObject.GetComponent<NewsFeedItem>().subject.tag == "Baker")
            {
                gameObject.layer = 8;
            } 
    
    }
    }
}

