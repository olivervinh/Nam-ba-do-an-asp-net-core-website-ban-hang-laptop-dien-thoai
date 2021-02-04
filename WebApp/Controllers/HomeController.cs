using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApp.Areas.Admin.Data;
using WebApp.Areas.Admin.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly DataProviderContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public HomeController(DataProviderContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        //hiển thị category 
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ViewBag.ListLSP = _context.LoaiSPs.ToList();
            ViewBag.Thuonghieu = _context.Thuonghieus.ToList();
            ViewBag.Product = _context.SanPhams.ToList();
          
            base.OnActionExecuted(context);
        }
        public IActionResult cart()
        {
            return View();
        }
        public IActionResult _thuonghieuAA()
        {
            return View("_homePartial");
        }
        public IActionResult _homePartial()
        {
            return View("_homePartial");
        }
        public IActionResult _productAreas()
        {

            return View("_homePartial");
        }

        public IActionResult _categoryPartial()
        {

            return View("_homePartial");
        }
        public IActionResult _productinCategory(int id)
        {
            ViewBag.cate = _context.SanPhams.Where(sp => sp.MaLoai == id);

            return View();
        }
        public IActionResult _productinBrand(int id)
        {
            ViewBag.brand = _context.SanPhams.Where(sp => sp.MaThuonghieu == id);

            return View();
        }
        public IActionResult _productdetail(int id)
        {
            ViewBag.pro = _context.SanPhams.Where(a => a.Ma == id);
            return View();
        }
        public IActionResult _searchfollowCategoryPartial()
        {

            return View("_homePartial");
        }
        public IActionResult _thanhbentraiCategory()
        {

            return View("_homePartial");
        }

        /// Thêm sản phẩm vào cart
        [Route("addcart/{productid:int}", Name = "addcart")]

        public IActionResult AddToCart([FromRoute] int productid)
        {

            var product = _context.SanPhams
                .Where(p => p.Ma == productid)
                .FirstOrDefault();
            if (product == null)
                return NotFound("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.Ma == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new CartItem() { quantity = 1, product = product });
            }

            // Lưu cart vào Session
            SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart
            return RedirectToAction(nameof(_homePartial));
        }
        /// xóa item trong cart
        [Route("/removecart/{productid:int}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] int productid)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.Ma == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }
        /// Cập nhật
        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.Ma == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity = quantity;
            }
            SaveCartSession(cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();
        }

        // Hiện thị giỏ hàng
        [Route("/cart", Name = "cart")]
        public IActionResult Cart()
        {

            return View(GetCartItems());
        }

        // Key lưu chuỗi json của Cart
        public const string CARTKEY = "cart";

        // Lấy cart từ Session (danh sách CartItem)
        List<CartItem> GetCartItems()
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }

        // Xóa cart khỏi session
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        // Lưu Cart (Danh sách CartItem) vào session
        void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }
        [Route("/checkout")]
        public async Task<IActionResult> CheckOut()
        {
            var cart = GetCartItems();
            ViewBag.cart = GetCartItems();
            ViewBag.size = cart.Count;
            return View();
        }

       
        [HttpPost]
        [Route("/checkout")]
        public async Task<IActionResult> CheckOut(ChitietDonHang chitiet,HDvaCTHD hDvaCTHD, int dem, Donhang donhang, List<ChitietDonHang> chitietDonHang /*[Bind("TongTien")] Donhang donhang, [Bind("Id,Quantity,ThanhTien,MaSP")] ChitietDonHang chitietDonHang*/)
        {
           
            var cart = GetCartItems();
            ViewBag.cart = GetCartItems();
            ViewBag.kb = from hd in _context.DonHangs
                     join cthd in _context.ChitietDonHangs
                     on hd.Id equals cthd.Madonhang
                     select new { };

            var dh = from d in _context.DonHangs
                     select d;
         
            ViewBag.size = cart.Count;
         
            if (ModelState.IsValid)
            {
                var Sender = await _userManager.GetUserAsync(HttpContext.User);
                if (User.Identity.IsAuthenticated)
                {
                    donhang.MaUser = Sender.Id;
                }
                else
                {
                    return RedirectToAction("_homePartial", "Home");
                }
                var oderdetail = new List<ChitietDonHang>();
                donhang.Datecheckout = DateTime.Now;
                decimal tam=0;
               foreach(var item in cart)
                {
                    oderdetail.Add(new ChitietDonHang()
                    {
                        Quantity = item.quantity,
                        MaSP = item.product.Ma,
                        ThanhTien= item.product.Gia*item.quantity,
                    
                    });
                    tam +=Convert.ToDecimal(item.product.Gia * item.quantity);
                }
                var co = new Donhang()
                {
                    MaUser = Sender.Id,
                    Datecheckout = DateTime.Now,
                    LstDH = oderdetail,
                    TongTien=tam,

                };

                _context.Add(co);
                await _context.SaveChangesAsync();
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(_homePartial));
            }

            return RedirectToAction(nameof(CheckOut));
        }
    }
}

