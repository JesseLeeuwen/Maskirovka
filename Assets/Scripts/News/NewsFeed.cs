using UnityEngine;
using System.Collections.Generic;

namespace Maskirovka
{
    [CreateAssetMenu(menuName="NewsFeed")]
    public class NewsFeed : ScriptableObject
    {
        [SerializeField]
        private Queue<Change> changes = new Queue<Change>();

        public Queue<Change> PullUpdates()
        {
            Queue<Change> tempChanges = changes;
            changes.Clear();

            return tempChanges;
        }

        public void PushUpdate(Change change)
        {
            changes.Enqueue( change );
        }
    }
}
