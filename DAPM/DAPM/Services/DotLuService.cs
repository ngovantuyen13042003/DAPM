using DAPM.Data;
using DAPM.Models;

namespace DAPM.Services
{
    public class DotLuService
    {
        private readonly DbLuLutHoaVangContext mydb;

        public DotLuService(DbLuLutHoaVangContext mydb)
        {
            this.mydb = mydb;
        }

        public List<TbDotLu> GetAll()
        {
            var data = mydb.TbDotLus.ToList();
            return data;
        }
    }
}
