using System;
using ProjectBlue.RepulserEngine.Domain.Entity;
using ProjectBlue.RepulserEngine.Repository;

namespace ProjectBlue.RepulserEngine.Domain.UseCase
{
    public class TimecodeDecodeUseCase : ITimecodeDecodeUseCase
    {
        private ITimecodeDecoderRepository repository;

        public IObservable<TimecodeData> OnTimecodeUpdatedAsObservable => repository.OnTimecodeUpdatedAsObservable;

        public TimecodeDecodeUseCase(ITimecodeDecoderRepository repo)
        {
            this.repository = repository;
        }
    }
}