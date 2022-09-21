using MyWebApiApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyWebApiApp.Services
{
    public class LoaiRepositoryInMemory : ILoaiRepository
    {
        static List<LoaiVM> loais = new List<LoaiVM>
        {
            new LoaiVM{MaLoai = 1, TenLoai = "Tivi"},
            new LoaiVM{MaLoai = 2, TenLoai = "Tu lanh"},
            new LoaiVM{MaLoai = 3, TenLoai = "Dieu hoa"},
            new LoaiVM{MaLoai = 4, TenLoai = "Mat giat"}
        };
        public LoaiVM Add(LoaiModel model)
        {
            var _data = new LoaiVM
            {
                MaLoai = loais.Max(x => x.MaLoai) + 1,
                TenLoai = model.TenLoai
            };

            loais.Add(_data);
            return _data;
        }

        public void Delete(int id)
        {
            var _loai = loais.SingleOrDefault(lo => lo.MaLoai == id);
            loais.Remove(_loai);
        }

        public List<LoaiVM> GetAll()
        {
            return loais;
        }

        public LoaiVM GetById(int id)
        {
            return loais.SingleOrDefault(lo => lo.MaLoai == id);
        }

        public void Update(LoaiVM loai)
        {
            var _loai = loais.SingleOrDefault(lo => lo.MaLoai == loai.MaLoai);
            if( _loai != null)
            {
                _loai.TenLoai = loai.TenLoai;
            }

        }
    }
}
