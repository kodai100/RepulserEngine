using System;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.Domain.Entity;

namespace ProjectBlue.RepulserEngine.UseCaseInterfaces
{
    public interface ITimecodeDisplayUseCase
    {
        IObservable<TimecodeData> OnTimecodeUpdatedAsObservable { get; }
    }
}