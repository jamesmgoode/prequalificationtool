using System;

namespace PrequalificationTool
{
    public interface IDateTimeHelper
    {
        DateTime Now();
    }

    public class DateTimeHelper : IDateTimeHelper
    {
        public virtual DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
