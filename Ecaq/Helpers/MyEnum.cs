using System.ComponentModel.DataAnnotations;

namespace Ecaq.Helpers
{
    public enum CrudOperation
    {

        [Display(Name = "Add")]
        Add = 1,

        [Display(Name = "Edit")]
        Edit = 2,

        [Display(Name = "Delete")]
        Delete = 3
    }
}
