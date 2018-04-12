using System;

namespace Core.Api
{
    public class BaseResult : IBaseResult
    {
        public string TimeStamp { get; set; }

        public string Message { get; set; }
    }
}
