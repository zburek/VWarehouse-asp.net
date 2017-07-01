using System;

namespace MVC.Models.ItemViewModels
{
    public class ItemOnStockViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
    }
}