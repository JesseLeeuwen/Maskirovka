using UnityEngine;
using System.Collections.Generic;

namespace Maskirovka
{
    public class NewsFeed : MonoBehaviour
    {
        public Queue<Change> PullUpdates()
        {
            return new Queue<Change>();
        }

        public void PushUpdate(Change change)
        {

        }
    }
}
