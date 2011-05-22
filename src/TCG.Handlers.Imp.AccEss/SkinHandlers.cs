using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TCG.Entity;
using TCG.Utils;
using TCG.Handlers;

namespace TCG.Handlers.Imp.AccEss
{
    public class SkinHandlers : ISkinHandlers
    {
       
        /// <summary>
        /// 从数据库中获得皮肤记录集
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllSkinWithOutCaching()
        {

            string Sql = "SELECT * FROM Skin";
            return AccessFactory.conn.DataTable(Sql);
        }

        /// <summary>
        /// 创建皮肤
        /// </summary>
        /// <param name="skin">皮肤实体</param>
        /// <returns></returns>
        public int CreateSkin(Skin skin)
        {
            int count = objectHandlers.ToInt(AccessFactory.conn.ExecuteScalar("SELECT COUNT(1) FROM Skin WHERE id = '" + skin.Id + "'"));

            string sql = "UPDATE Skin SET [Name]='" + skin.Name + "',Pic='" + skin.Pic + "',WebDescription='" + skin.WebDescription + "',Filename='" + skin.Filename
                + "',IndexPage='" + skin.IndexPage + "',WebKeyWords='" + skin.WebKeyWords + "' WHERE id = '" + skin.Id + "'";
            if (count == 0)
            {
                sql = "INSERT INTO Skin (ID,[NAME],PIC,WebDescription,Filename,IndexPage,WebKeyWords) VALUES('" + skin.Id + "','" + skin.Name + "','"
                    + skin.Pic + "','" + skin.WebDescription + "','" + skin.Filename + "','" + skin.IndexPage + "','" + skin.WebKeyWords + "')";
            }

            AccessFactory.conn.Execute(sql);
           
            return 1;
        }

        /// <summary>
        /// 修改皮肤
        /// </summary>
        /// <param name="skin">皮肤实体</param>
        /// <returns></returns>
        public int UpdateSkin(Skin skin)
        {
            return -19000000;
        }
    }
}
