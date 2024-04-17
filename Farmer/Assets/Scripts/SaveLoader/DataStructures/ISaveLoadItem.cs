using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.SaveLoader
{
    public interface ISaveLoadItem
    {
        IItemData GetData();
    }
}
