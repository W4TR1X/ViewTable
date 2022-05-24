using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using w4TR1x.ViewTable.Interfaces;

namespace w4TR1x.Aspnet.Interfaces
{
    public interface ICustomHtmlCell : ICell
    {
        void Render(TagBuilder cellTagBuilder, int renderIndex, TagBuilder builder = null);
    }
}
