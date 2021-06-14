using System;
using ProjectBlue.RepulserEngine.Domain.Entity;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public interface ITimecodeDecodeUseCase
    {
        IObservable<TimecodeData> OnTimecodeUpdatedAsObservable { get; }
    }
}