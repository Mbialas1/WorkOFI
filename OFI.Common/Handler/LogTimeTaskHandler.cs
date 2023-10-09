using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OFI.Common.Handler
{
    public class LogTimeTaskHandler
    {
        /// <summary>
        /// Function for check is correct parameters about time
        /// D - Day/s
        /// H - Houer/s
        /// M - Minute/s
        /// S - second/s
        /// </summary>
        /// <param name="timeText"></param>
        /// <returns></returns>
        public static bool HasCorrectLoggetTime(string timeText)
        {
            var regex = new Regex(@"^[0-9hdms]+$", RegexOptions.IgnoreCase);
            if(regex.IsMatch(timeText))
                return Regex.IsMatch(timeText, @"^(\d*d)?(\d*h)?(\d*m)?(\d*s)?$");

            return false;
        }

        public static TimeOnly ConvertTimeTextToRealyTime(string timeText)
        {
            TimeOnly timeOnly = new TimeOnly();
            try
            {
                int index = timeText.IndexOf("d", StringComparison.OrdinalIgnoreCase);
                if (index > -1)
                {
                    int days = Int32.Parse(timeText.Substring(0, index));
                    timeText = timeText.Remove(0, index + 1);
                    timeOnly = timeOnly.AddHours(days * 24);
                }
                index = timeText.IndexOf('h', StringComparison.OrdinalIgnoreCase);
                if (index > -1)
                {
                    int hours = Int32.Parse(timeText.Substring(0, index));
                    timeText = timeText.Remove(0, index + 1);
                    timeOnly = timeOnly.AddHours(hours);
                }
                index = timeText.IndexOf('m', StringComparison.OrdinalIgnoreCase);
                if (index > -1){
                    int minutes = Int32.Parse(timeText.Substring(0, index));
                    timeText = timeText.Remove(0, index + 1);
                    timeOnly = timeOnly.AddMinutes(minutes);
                }

                return timeOnly;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
