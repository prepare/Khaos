using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RenderEngine
{
    public enum TargetEnum
    {
        _tab,
        _blank,
        _parent,
        _search,
        _self,
        _top
    }

    public interface IPageHost
    {
        void Navigate(string uri, TargetEnum target);
    }
}
