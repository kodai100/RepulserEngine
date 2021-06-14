using System;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public interface IOverlayController
    {
        // void Trigger(Color color);
        IObservable<Color> OnOverlayTriggeredAsObservable { get; }
    }
}