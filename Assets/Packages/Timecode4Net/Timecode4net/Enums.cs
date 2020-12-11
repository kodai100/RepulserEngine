using System;
// ReSharper disable InconsistentNaming

namespace Timecode4net
{
    public enum FrameRate
    {
        /// <summary>
        ///     SMPTE 23.98frames/sec.
        /// </summary>
        fps23_98,
        /// <summary>
        ///     SMPTE 24frames/sec.
        /// </summary>
        fps24,
        /// <summary>
        ///     SMPTE 25frames/sec.
        /// </summary>
        fps25,
        /// <summary>
        ///     SMPTE 29.97frames/sec.
        /// </summary>
        fps29_97,
        /// <summary>
        ///     SMPTE 30frames/sec.
        /// </summary>
        fps30,
        /// <summary>
        ///     SMPTE 50frames/sec.
        /// </summary>
        fps50,
        /// <summary>
        ///     SMPTE 59.94frames/sec.
        /// </summary>
        fps59_94,
        /// <summary>
        ///     SMPTE 60frames/sec.
        /// </summary>
        fps60,
        /// <summary>
        ///     milliseconds
        /// </summary>
        msec
    }
}
