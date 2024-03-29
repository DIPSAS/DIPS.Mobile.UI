﻿namespace DIPS.Mobile.UI.Converters.ValueConverters
{
    /// <summary>
    /// This class is used to control the current time.
    /// </summary>
    internal static class Clock
    {
#nullable disable
        private static IClock s_clock;
#nullable enable

        static Clock()
        {
            Reset();
        }

        public static void Reset()
        {
            s_clock = new CurrentTime();
        }

        public static IDisposable OverrideClock(IClock clock)
        {
            s_clock = clock;

            return new ClockCleanup();
        }

        public static IDisposable OverrideClock(DateTime fixedDateTime)
        {
            s_clock = new FixedTime(fixedDateTime);

            return new ClockCleanup();
        }

        private sealed class ClockCleanup : IDisposable
        {
            public void Dispose()
            {
                Reset();
            }
        }

        /// <summary>
        /// Now property that can be used in the same fashion as DateTime.Now -> Clock.Now.
        /// </summary>
        public static DateTime Now
        {
            get { return s_clock.Now; }
        }

        /// <summary>
        /// Now property that can be used in the same fashion as DateTime.Today -> Clock.Today.
        /// </summary>
        public static DateTime Today
        {
            get { return s_clock.Today; }
        }

        /// <summary>
        /// Now property that can be used in the same fashion as DateTime.UtcNow -> Clock.UtcNow.
        /// </summary>
        public static DateTime UtcNow
        {
            get { return s_clock.UtcNow; }
        }

        internal class CurrentTime : IClock
        {
            DateTime IClock.Now => DateTime.Now;

            DateTime IClock.Today => DateTime.Today;

            DateTime IClock.UtcNow => DateTime.UtcNow;
        }

        internal class FixedTime : IClock
        {
            private readonly DateTime m_fixedTime;

            public FixedTime(DateTime fixedTime)
            {
                this.m_fixedTime = fixedTime;
            }

            DateTime IClock.Now { get { return this.m_fixedTime; } }

            DateTime IClock.Today { get { return this.m_fixedTime.Date; } }

            DateTime IClock.UtcNow { get { return this.m_fixedTime.ToUniversalTime(); } }
        }
    }

    
        /// <summary>
        /// Interface to implement a Test Double to control the current time.
        /// </summary>
        internal interface IClock
        {
            DateTime Now { get; }
            DateTime Today { get; }
            DateTime UtcNow { get; }
        }
}