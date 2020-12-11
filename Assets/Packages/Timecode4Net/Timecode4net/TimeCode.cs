using System;
using System.Text.RegularExpressions;

namespace Timecode4net
{
    public class Timecode
    {
        public static Timecode FromFrames(int totalFrames, FrameRate frameRate, bool isDropFrame)
        {
            FrameRateSanityCheck(frameRate, isDropFrame);

            var tc = new Timecode(frameRate, isDropFrame) {TotalFrames = totalFrames};
            tc.UpdateByTotalFrames();
            return tc;
        }

        private const string TimeCodePattern = @"^(?<hours>[0-2][0-9]):(?<minutes>[0-5][0-9]):(?<seconds>[0-5][0-9])[:|;|\.](?<frames>[0-9]{2,3})$";

        public static Timecode FromString(string input, FrameRate frameRate, bool isDropFrame)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException(nameof(input));
            }
            FrameRateSanityCheck(frameRate, isDropFrame);

            var tcRegex = new Regex(TimeCodePattern);
            var match = tcRegex.Match(input);
            if (!match.Success)
            {
                throw new ArgumentException("Input text was not in valid timecode format.", nameof(input));
            }

            var tc = new Timecode(frameRate, isDropFrame)
            {
                Hours = int.Parse(match.Groups["hours"].Value),
                Minutes = int.Parse(match.Groups["minutes"].Value),
                Seconds = int.Parse(match.Groups["seconds"].Value),
                Frames = int.Parse(match.Groups["frames"].Value)
            };
            tc.UpdateTotalFrames();

            return tc;
        }

        public static Timecode FromTimeSpan(TimeSpan ts, FrameRate frameRate, bool isDropFrame)
        {
            FrameRateSanityCheck(frameRate, isDropFrame);

            var tc = new Timecode(frameRate, isDropFrame)
            {
                TotalFrames = (int)Math.Round(ts.TotalSeconds * frameRate.ToDouble(), MidpointRounding.AwayFromZero)
            };
            tc.UpdateByTotalFrames();
            return tc;
        }

        private static void FrameRateSanityCheck(FrameRate frameRate, bool isDropFrame)
        {
            if (isDropFrame && frameRate != FrameRate.fps29_97 && frameRate != FrameRate.fps59_94)
            {
                throw new ArgumentException("Dropframe is supported with 29.97 or 59.94 fps.", nameof(isDropFrame));
            }
            if (!Enum.IsDefined(typeof(FrameRate), frameRate))
                throw new ArgumentOutOfRangeException(nameof(frameRate),
                    "Value should be defined in the FrameRate enum.");
        }

        private Timecode(FrameRate frameRate, bool isDropFrame)
        {
            this._isDropFrame = isDropFrame;
            this._rawFrameRate = frameRate;
            this._frameRate = frameRate.ToInt();
        }

        private readonly bool _isDropFrame;

        private readonly FrameRate _rawFrameRate;

        private readonly int _frameRate;

        public int TotalFrames { get; private set; }
    
        public int Hours { get; private set; }

        public int Minutes { get; private set; }

        public int Seconds { get; private set; }

        public int Frames { get; private set; }

        public Timecode AddHours(double hours)
        {
            throw new NotImplementedException();
        }

        public Timecode AddMinutes(double minutes)
        {
            throw new NotImplementedException();
        }

        public Timecode AddSeconds(double seconds)
        {
            throw new NotImplementedException();
        }

        public Timecode AddFrames(uint frames)
        {
            throw new NotImplementedException();
        }

        public TimeSpan ToTimeSpan()
        {
            var framesInMsec = this.TotalFrames * FrameRate.msec.ToInt() / this._rawFrameRate.ToDouble();
            return TimeSpan.FromMilliseconds(framesInMsec);
        }

        public override string ToString()
        {
            var frameSeparator = this._isDropFrame ? ";" : ":";
            return $"{this.Hours:D2}:{this.Minutes:D2}:{this.Seconds:D2}{frameSeparator}{this.Frames:D2}";
        }

        private void UpdateTotalFrames()
        {
            var frames = this.Hours * 3600;
            frames += this.Minutes * 60;
            frames += this.Seconds;
            frames *= this._frameRate;
            frames += this.Frames;
            if (this._isDropFrame)
            {
                var totalMinutes = this.Hours * 60 + this.Minutes;
                var dropFrames = this._rawFrameRate == FrameRate.fps29_97 ? 2 : 4;
                frames -= dropFrames * (totalMinutes - totalMinutes / 10);
            }
            this.TotalFrames = frames;
        }

        private void UpdateByTotalFrames()
        {
            const int secondsInHour = 3600;
            const int secondsInMinutes = 60;
            
            var frameCount = this.TotalFrames;
            if (this._isDropFrame)
            {
                var fps = this._rawFrameRate.ToDouble();
                var dropFrames = Math.Round(fps * 0.066666, MidpointRounding.AwayFromZero);
                var framesPerHour = Math.Round(fps * secondsInHour, MidpointRounding.AwayFromZero);
                var framesPer24H = framesPerHour * 24;
                var framesPer10M = Math.Round(fps * secondsInMinutes * 10, MidpointRounding.AwayFromZero);
                var framesPerMin = Math.Round(fps * secondsInMinutes, MidpointRounding.AwayFromZero);

                frameCount %= (int) framesPer24H;
                if (frameCount < 0)
                {
                    frameCount = (int) (framesPer24H + frameCount);
                }

                var d = Math.Floor(frameCount / framesPer10M);
                var m = frameCount % framesPer10M;
                if (m > dropFrames)
                {
                    frameCount += (int) (dropFrames * 9 * d + dropFrames * Math.Floor((m - dropFrames) / framesPerMin));
                }
                else
                {
                    frameCount += (int) (dropFrames * 9 * d);
                }

                this.Hours = (int) Math.Floor(Math.Floor(Math.Floor(frameCount / (double)this._frameRate) / secondsInMinutes) / secondsInMinutes);
                this.Minutes = (int) Math.Floor(Math.Floor(frameCount / (double) this._frameRate) / secondsInMinutes) % secondsInMinutes;
                this.Seconds = (int) Math.Floor(frameCount / (double) this._frameRate) % secondsInMinutes;
                this.Frames = frameCount % this._frameRate;
            }
            else
            {
                this.Hours = frameCount / (secondsInHour * this._frameRate);
                if (this.Hours > 23)
                {
                    this.Hours %= 24;
                    frameCount -= 23 * secondsInHour * this._frameRate;
                }
                this.Minutes = frameCount % (secondsInHour * this._frameRate) / (secondsInMinutes * this._frameRate);
                this.Seconds = frameCount % (secondsInHour * this._frameRate) % (secondsInMinutes * this._frameRate) / this._frameRate;
                this.Frames = frameCount % (secondsInHour * this._frameRate) % (secondsInMinutes * this._frameRate) % this._frameRate;
            }
        }
    }
}
