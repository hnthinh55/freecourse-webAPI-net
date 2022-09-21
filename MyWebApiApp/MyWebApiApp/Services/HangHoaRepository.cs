using Microsoft.EntityFrameworkCore;
using MyWebApiApp.Data;
using MyWebApiApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyWebApiApp.Services
{
    public class HangHoaRepository : IHangHoaRepository
    {
        private readonly MyDbContext _context;
        private static int PAGE_SIZE { get; set; } = 5;
        public HangHoaRepository(MyDbContext context)
        {
            _context = context;
        }
        public List<HangHoaModel> GetAll(string search, double? from, double? to, string sortBy, int page = 1)
        {
            var allProduct = _context.HangHoas.Include(hh => hh.Loai).AsQueryable();
            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allProduct = allProduct.Where(hh => hh.TenHh.Contains(search));
            }
            if (from.HasValue)
            {
                allProduct = allProduct.Where(hh => hh.DonGia >= from);
            }
            if (to.HasValue)
            {
                allProduct = allProduct.Where(hh => hh.DonGia <= to);
            }
            #endregion

            #region Sorting
            // Default sort by Name(TenHh)
            allProduct = allProduct.OrderBy(hh =>hh.TenHh);
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "ten_desc": allProduct = allProduct.OrderByDescending(hh => hh.TenHh);
                        break;
                    case "gia_asc":
                        allProduct = allProduct.OrderBy(hh => hh.DonGia);
                        break;
                    case "gia_desc":
                        allProduct = allProduct.OrderByDescending(hh => hh.DonGia);
                        break;
                }
            }
            #endregion

            //#region Paging
            //allProduct = allProduct.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            //#endregion
            //var result = allProduct.Select(hh => new HangHoaModel
            //{
            //    MaHangHoa = hh.MaHh,
            //    TenHangHoa = hh.TenHh,
            //    DonGia = hh.DonGia,
            //    TenLoai = hh.Loai.TenLoai
            //});

            //return result.ToList();
            var result = PaginatedList<MyWebApiApp.Data.HangHoa>.Create(allProduct, page, PAGE_SIZE);
            return result.Select(hh => new HangHoaModel
            {
                MaHangHoa = hh.MaHh,
                TenHangHoa = hh.TenHh,
                DonGia = hh.DonGia,
                TenLoai = hh.Loai?.TenLoai
            }).ToList();
        }
    }
}
