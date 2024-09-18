


using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagment.Models
{

    public class AddItem
    {
        public int? Id { get; set; }
        public string ItemName { get; set; }
        public string ItemPrice { get; set; }
        public string  ItemType { get; set; }
    }
}
