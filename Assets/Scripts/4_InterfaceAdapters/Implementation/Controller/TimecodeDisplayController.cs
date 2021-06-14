using System;
using kodai100.TimeCodeCalculation;
using ProjectBlue.RepulserEngine.Domain.DataModel;
using ProjectBlue.RepulserEngine.Domain.Entity;
using ProjectBlue.RepulserEngine.Domain.UseCase;
using ProjectBlue.RepulserEngine.UseCaseInterfaces;
using UniRx;

namespace ProjectBlue.RepulserEngine.Controllers
{
    public class TimecodeDisplayController : ITimecodeDisplayController, IDisposable
    {
        public IObservable<TimecodeData> OnUpdateTimecodeAsObservable => onUpdateTimcodeSubject;

        private Subject<TimecodeData> onUpdateTimcodeSubject = new Subject<TimecodeData>();

        private ITimecodeDisplayUseCase timecodeDisplayUseCase;
        private IGlobalFrameOffsetSettingUseCase globalFrameOffsetSettingUseCase;

        private CompositeDisposable disposable = new CompositeDisposable();

        public TimecodeDisplayController(ITimecodeDisplayUseCase timecodeDisplayUseCase,
            IGlobalFrameOffsetSettingUseCase globalFrameOffsetSettingUseCase)
        {
            this.timecodeDisplayUseCase = timecodeDisplayUseCase;
            this.globalFrameOffsetSettingUseCase = globalFrameOffsetSettingUseCase;

            this.timecodeDisplayUseCase.OnTimecodeUpdatedAsObservable.Subscribe(t =>
            {
                var offsetTimeCode = OffsetFilter(t);
                onUpdateTimcodeSubject.OnNext(offsetTimeCode);
            }).AddTo(disposable);
        }

        // TODO: combine to decode use case (same code in SignalTriggerController)
        private TimecodeData OffsetFilter(TimecodeData inputTimecode)
        {
            var setting = globalFrameOffsetSettingUseCase.GetCurrent();
            var info = new FrameRateInfo((FrameRateType) Enum.ToObject(typeof(FrameRateType), setting.FrameRateType));
            var timecodeForCalculator = new TimeCode
            {
                DropFrame = info.DropFrame, Hour = inputTimecode.hour, Minute = inputTimecode.minute,
                Second = inputTimecode.second,
                Frame = inputTimecode.frame
            };
            var num = TimeCodeCalculator.TimeCodeToNumber(timecodeForCalculator, info);
            num += setting.Offset;

            // if smaller than zero by minus offset, return zero
            if (num < 0)
            {
                return new TimecodeData(0, 0, 0, 0, inputTimecode.dropFrame);
            }

            var filteredTc = TimeCodeCalculator.FrameNumberToTimeCode(num, info);

            // avoid reference data override
            var result = new TimecodeData(filteredTc.Hour, filteredTc.Minute, filteredTc.Second, filteredTc.Frame,
                info.DropFrame);

            return result;
        }


        public void Dispose()
        {
            disposable.Dispose();
        }
    }
}