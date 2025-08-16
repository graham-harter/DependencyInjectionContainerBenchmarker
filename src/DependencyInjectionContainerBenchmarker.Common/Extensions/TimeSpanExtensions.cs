using System;

namespace DependencyInjectionContainerBenchmarker.Common.Extensions
{
    /// <summary>
    /// Extension methods for the <see cref="TimeSpan"/> class.
    /// </summary>
    public static class TimeSpanExtensions
    {
        #region Public methods

        /// <summary>
        /// Format the <see cref="TimeSpan"/> instance for output.
        /// </summary>
        /// <param name="timespan">
        /// The <see cref="TimeSpan"/> instance to format for output.
        /// </param>
        /// <returns>
        /// The <see cref="string"/> representation of the <see cref="TimeSpan"/> instance.
        /// </returns>
        public static string Format(this TimeSpan timespan)
        {
            var hours = timespan.Hours;
            var minutes = timespan.Minutes;
            var seconds = timespan.Seconds;
            var milliseconds = timespan.Milliseconds;

            return $"{ZeroPad(hours, 2)}:{ZeroPad(minutes, 2)}:{ZeroPad(seconds, 2)}:{ZeroPad(milliseconds, 3)}";
        }

        #endregion // #region Public methods

        #region Private methods

        private static string ZeroPad(int n, int length)
        {
            return n.ToString().PadLeft(length, '0');
        }

        #endregion // #region Private methods
    }
}
