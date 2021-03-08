using System;
using ProjectBlue.RepulserEngine.Domain.Model;

namespace ProjectBlue.RepulserEngine.Repository
{

    public interface ITimecodeDecoderRepository
    {
        IObservable<TimecodeData> OnTimecodeUpdatedAsObservable { get; }
    }
    
}