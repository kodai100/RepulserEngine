using System;

namespace ProjectBlue.RepulserEngine.UseCaseInterfaces
{

    public interface ITimecodeEvaluationUseCase
    {
        
        /// <summary>
        /// returns command id
        /// </summary>
        IObservable<string> OnTriggerPulsedAsObservable { get; }
        
    }
    
}