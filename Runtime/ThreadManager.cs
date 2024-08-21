using System;
using System.Collections.Generic;
using UnityEngine;


//TODO: DS 24/05/24 Remove from namespace once decided if in master template
namespace Nimlok.Phidgets
{
    public class ThreadManager : MonoBehaviour
    {
        public static ThreadManager instance;
        Queue<Action> executeOnMainThread = new();
        Action currentAction;

        private void Awake()
        {
            instance ??= this;
        
            if (instance != this)
            {
                Destroy(this); 
            }
        }

        private void FixedUpdate() => UpdateMainThread();

        void UpdateMainThread()
        {
            while (executeOnMainThread.Count > 0)
            {
                currentAction = null;
                lock (executeOnMainThread)
                {
                    if (executeOnMainThread.Count > 0)
                    {
                        currentAction = executeOnMainThread.Dequeue();
                    }
                }
            
                currentAction?.Invoke();
            }
        }

        public void AddToMainThread(Action action)
        {
            lock (executeOnMainThread)
            {
                executeOnMainThread.Enqueue(action);
            }
        }
    }
}


