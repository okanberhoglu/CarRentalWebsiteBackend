using Core.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class CarDetailDto: IDto
    {
        public string BrandName { get; set; }
        public int CarId { get; set; }
        public int BrandId { get; set; }
        public string ColorName { get; set; }
        public int ColorId { get; set; }
    }
}
