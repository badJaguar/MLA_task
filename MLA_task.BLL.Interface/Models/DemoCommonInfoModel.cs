using System.Collections.Generic;

namespace MLA_task.BLL.Interface.Models
{
    public class DemoCommonInfoModel
    {
            public int Id { get; set; }
            public string CommonInfo { get; set; }
            public ICollection<DemoModel> DemoModels { get; set; }
    }
}