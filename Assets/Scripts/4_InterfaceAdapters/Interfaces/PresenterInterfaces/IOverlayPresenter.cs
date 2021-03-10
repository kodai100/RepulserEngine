using System;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Presentation
{

    public interface IOverlayPresenter
    {
        void Trigger(Color color);
        IObservable<Color> OnTriggerAsObservable { get; }
    }
    
}