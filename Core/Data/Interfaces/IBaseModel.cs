using System;

namespace Core
{
    public interface IBaseModel
    {
        int Id { get; set; }

        DateTimeOffset CreatedOn { get; set; }

        DateTimeOffset ModifiedOn { get; set; }
    }
}
