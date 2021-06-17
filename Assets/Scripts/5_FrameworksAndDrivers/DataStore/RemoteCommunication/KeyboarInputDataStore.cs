
using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace ProjectBlue.RepulserEngine.DataStore
{

    public class KeyboardInputDataStore : IKeyboardInputDataStore, IDisposable, ITickable
    {
        public IObservable<string> OnInputAsObservable => onKeyboardInputSubject;
        private Subject<string> onKeyboardInputSubject = new Subject<string>();
        
        public void Tick()
        {
            if (!Input.anyKeyDown) return;
            
            foreach (KeyCode code in Enum.GetValues(typeof(KeyCode))) {
                if (Input.GetKeyDown (code)) {
                    onKeyboardInputSubject.OnNext(code.ToString());
                    break;
                }
            }
        }

        public void Dispose()
        {
            onKeyboardInputSubject.Dispose();
        }
    }
    
}