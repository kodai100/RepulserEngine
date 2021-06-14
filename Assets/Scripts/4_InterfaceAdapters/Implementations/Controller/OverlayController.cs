using System;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.Controllers
{
    /// <summary>
    /// ひとまずPresenterの役割
    /// </summary>
    public class OverlayController : IOverlayController
    {
        public IObservable<Color> OnOverlayTriggeredAsObservable => useCase.OnOverlayTriggeredAsObservable;

        private IOverlayUseCase useCase;

        public OverlayController(IOverlayUseCase useCase)
        {
            this.useCase = useCase;
        }

        // public void Trigger(Color color)
        // {
        //     useCase.Trigger(color);
        // }
    }
}