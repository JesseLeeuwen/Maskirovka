using UnityEngine;
using System.Collections.Generic;

namespace Maskirovka
{
    [CreateAssetMenu(menuName="NewsFeed")]
    public class NewsFeed : ScriptableObject
    {
        public delegate void NewsEventFunc( Change change );
        private NewsEventFunc eventFunc;

        [SerializeField]
        private Queue<Change> changes = new Queue<Change>();

        public Queue<Change> PullUpdates()
        {
            Queue<Change> tempChanges = new Queue<Change>(changes);
            changes.Clear();

            return tempChanges;
        }

        public void PushUpdate(Change change)
        {
            changes.Enqueue( change );
            
            eventFunc.Invoke(change);
        }

        public void UnSubNewsEvent( NewsEventFunc func )
        {
            eventFunc -= func;
        }

        public void SubToNewsEvents(NewsEventFunc func)
        {
            if( eventFunc != null )
            {
                eventFunc = new NewsEventFunc( func );
                return;
            }
            eventFunc += func;
        }
    }
}
