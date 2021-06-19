using System;
using ProjectBlue.RepulserEngine.Domain.Entity;

namespace ProjectBlue.RepulserEngine.Repository
{
    public interface ITimecodeDecoderRepository
    {
        IObservable<TimecodeData> OnTimecodeUpdatedAsObservable { get; }
    }
}