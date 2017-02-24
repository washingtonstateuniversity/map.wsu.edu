using System;
using Castle.ActiveRecord;
using Map.Models;

namespace Map.Models
{
    public interface ICheckedOutBy
    {
        authors checked_out_by { get; set; }
        int id { get; set; }
    }
}
