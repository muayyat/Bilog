using System;
using System.Collections.Generic;

namespace Bilog.Data.Models
{
    public partial class CategorysPaged
    {
        public List<CategoryDTO> Categorys { get; set; }
        public int CategoryCount { get; set; }
    }
}