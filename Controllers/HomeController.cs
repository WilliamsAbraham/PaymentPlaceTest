using Microsoft.AspNetCore.Mvc;
using PaymentPlaceTest.Dto;
using PaymentPlaceTest.Models;
using PaymentPlaceTest.Service;
using System.Diagnostics;

namespace PaymentPlaceTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly PaymentService _paymentService;

        public HomeController(ILogger<HomeController> logger, PaymentService paymentService)
        {
            _logger = logger;
            _paymentService = paymentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Payment()
        {
            var mode = new ViewDto();
            return View(mode);
        }
        [HttpPost]
         public async Task<IActionResult> MakePayment(ViewDto viewDto)
        {
            var response = await _paymentService.InitializePaymentAsync(viewDto.Amount, viewDto.Email);

            return Redirect(response.data.authorization_url);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
