using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Ltc;

namespace Tests
{
    public class TimecodeTest
    {
        
        [Test]
        public void LessThanTest()
        {
            var timecode1 = new Timecode {
                dropFrame = false,
                hour = 1,
                minute = 0,
                second = 5,
                frame = 0
            };

            var timecode2 = new Timecode
            {
                dropFrame = false,
                hour = 1,
                minute = 1,
                second = 5,
                frame = 0
            };

            var result = timecode1 > timecode2;
            Assert.AreEqual(false, result);

            result = timecode2 > timecode1;
            Assert.AreEqual(true, result);

        }

        [Test]
        public void LargerThanTest()
        {
            var timecode1 = new Timecode
            {
                dropFrame = false,
                hour = 1,
                minute = 0,
                second = 5,
                frame = 0
            };

            var timecode2 = new Timecode
            {
                dropFrame = false,
                hour = 1,
                minute = 1,
                second = 5,
                frame = 0
            };

            var result = timecode1 < timecode2;
            Assert.AreEqual(true, result);

            result = timecode2 < timecode1;
            Assert.AreEqual(false, result);
        }
    }
}
