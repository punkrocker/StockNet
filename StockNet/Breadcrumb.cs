using System;
using System.Collections.Generic;

namespace StockNet
{
    public class Breadcrumb : List<Crumb>
    {
        public Breadcrumb(IEnumerable<Crumb> crumbs)
            : base(crumbs)
        {
        }
    }
}