using UnityEngine;
using Maskirovka.News;
using Maskirovka.Selector;

namespace Maskirovka
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance; // instance of gamemanager used by Instance property
        public NewsProcessor processor; // current news processor
        public NewsManager newsManager;
        public NewsFeed feed;

        // properties
        public static GameManager Instance{ // static instance of the gameManager
            get { return instance; }
        }

        void Awake()
        {
            instance = this;
        }
    }
}