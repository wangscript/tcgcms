using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TCG.Entity;

namespace TCG.Handlers
{
    public interface IPropertiesHandlers
    {

        DataTable GetPropertiesByCIdWithOutCaching(string cid);

        int PropertiesManage(Properties cp);

        int PropertiesDEL(int cpid);

        int GetMaxProperties();

        DataTable GetPropertiesCategoriesBySkinId(string skinid);

        int PropertiesCategoriesManage(PropertiesCategorie cp);

        int GetMaxPropertiesCategrie();
    }
}
