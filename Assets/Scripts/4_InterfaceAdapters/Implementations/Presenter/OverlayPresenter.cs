using System;
using UnityEngine;
using UniRx;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public class OverlayPresenter : IOverlayPresenter, IDisposable
    {
        public IObservable<Color> OnTriggerAsObservable => onTriggerSubject;
        
        private Subject<Color> onTriggerSubject = new Subject<Color>();
        
        public void Trigger(Color color)
        {
            onTriggerSubject.OnNext(color);
        }

        public void Dispose()
        {
            onTriggerSubject.Dispose();
        }
    }

}