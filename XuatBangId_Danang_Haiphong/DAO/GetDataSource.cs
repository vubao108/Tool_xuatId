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
        public static DataTable GetTable_ketqua()
        {
            return DBConnection.GetDataByQuery($"call lay_du_lieu_ketqua()");

        }

        public static void Update_dachon_01(string _id)
        {
            DBConnection.ExecuteQuery($"call update_da_chon_01('{_id}')");
        }
        public static void Update_dachon_02(string _id)
        {
            DBConnection.ExecuteQuery($"call update_da_chon_02('{_id}')");
        }
        public static void Insert_id(string idkhach01, string idkhach02, string id01, string id02)
        {
            DBConnection.ExecuteQuery($"call insert_id_to_ketqua('{idkhach01}','{idkhach02}','{id01}','{id02}')");
        }
    }
}
