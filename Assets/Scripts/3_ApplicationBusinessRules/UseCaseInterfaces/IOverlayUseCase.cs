using System;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.UseCaseInterfaces
{
    public interface IOverlayUseCase
    {
        void Trigger(Color color);
        IObservable<Color> OnOverlayTriggeredAsObservable { get; }
    }
}