using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XuatBangId_Danang_Haiphong.DAO
{
    class GetDataSource
    {
        public static DataTable GetTable01()
        {
            return DBConnection.GetDataByQuery($"call lay_du_lieu_01()");
            
        }
        public static DataTable GetTable02(string _idkhach, string _hoten)
        {
            return DBConnection.GetDataByQuery($"call lay_du_lieu_02('{_idkhach}','{_hoten}')");

        }
    }
}
