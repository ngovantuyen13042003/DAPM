using DAPM.Data;
using DAPM.Models;

namespace DAPM.Services
{
    public class DanhMucService
    {
        private readonly DbLuLutHoaVangContext mydb;
        public DanhMucService(DbLuLutHoaVangContext Mydb)
        {
            mydb = Mydb;
        }
        public List<TbDanhMuc> getAll()
        {
            var data = mydb.TbDanhMucs.ToList();
            return data;
        }
    }
}
