using System;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;
using UnityEngine;
using UniRx;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    /// <summary>
    /// Controllerで呼び出されるので、一番最初に生成する
    /// </summary>
    public class OverlayUseCase : IOverlayUseCase, IDisposable
    {
        public IObservable<Color> OnOverlayTriggeredAsObservable => onTriggerSubject;

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