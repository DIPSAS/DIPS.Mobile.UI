﻿using System;
using DIPS.Mobile.UI.Converters.ValueConverters;

namespace DIPS.Mobile.UI.Extensions
{
    /// <summary>
    /// Extensions class for DateTime
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Checks if the date occurred to day
        /// </summary>
        /// <param name="dateTime">The datetime to check</param>
        /// <returns>Boolean value</returns>
        public static bool IsToday(this DateTime dateTime) => dateTime.Date == Clock.Today.Date;

        /// <summary>
        /// Checks if the date occurred yesterday
        /// </summary>
        /// <param name="dateTime">The datetime to check</param>
        /// <returns>Boolean value</returns>
        public static bool IsYesterday(this DateTime dateTime)
        {
            return dateTime.Date == Clock.Now.AddDays(-1).Date;
        }

        /// <summary>
        /// Checks if the date occurs tomorrow
        /// </summary>
        /// <param name="dateTime">The datetime to check</param>
        /// <returns>Boolean value</returns>
        public static bool IsTomorrow(this DateTime dateTime)
        {
            return dateTime.Date == Clock.Now.AddDays(1).Date;
        }

        /// <summary>
        /// Gets the correct english day suffix for a date
        /// </summary>
        /// <param name="dateTime">The datetime to get the suffix from</param>
        /// <returns>a string with the correct suffix</returns>
        public static string GetEnglishDaySuffix(this DateTime dateTime)
        {
            switch (dateTime.Day)
            {
                case 1:
                case 21:
                case 31:
                    return "st";
                case 2:
                case 22:
                    return "nd";
                case 3:
                case 23:
                    return "rd";
                default:
                    return "th";
            }
        }
        
        public static DateTime ConvertDate(this DateTime dateTime, bool ignoreLocalTime)
        {
            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                return dateTime;
            }
            var convertedDateTime = ignoreLocalTime ? dateTime.ToUniversalTime() : dateTime.ToLocalTime();

            return convertedDateTime;
        }

        public static DateTime ConvertDate(this DateTime dateTime, DateTimeKind kind)
        {
            if (kind is DateTimeKind.Unspecified)
                return DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
            
            return kind is DateTimeKind.Local ? dateTime.ToLocalTime() : dateTime.ToUniversalTime();
        }
    }
    
}