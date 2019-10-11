using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using paytm;
using PaytmIntegration.Models;

namespace PaytmIntegration.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatePayment(RequestData data)
        {
            Random random = new Random();
            var orderId = random.Next();

            String merchantKey = Key.MerchantKey ;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("MID", Key.MerchantId);
            parameters.Add("CHANNEL_ID", "WEB");
            parameters.Add("INDUSTRY_TYPE_ID", "Retail");
            parameters.Add("WEBSITE", "WEBSTAGING");
            parameters.Add("EMAIL", data.email);
            parameters.Add("MOBILE_NO", data.mobileNumber);
            parameters.Add("CUST_ID", "1");
            parameters.Add("ORDER_ID", Convert.ToString(orderId));
            parameters.Add("TXN_AMOUNT", data.amount);
            parameters.Add("CALLBACK_URL", "http://localhost:50745/Home/PaytmResponse"); //This parameter is not mandatory. Use this to pass the callback url dynamically.

            string checksum = CheckSum.generateCheckSum(merchantKey, parameters);

            string paytmURL = "https://securegw-stage.paytm.in/theia/processTransaction?orderid=" + Convert.ToString(orderId);

            string outputHTML = "<html>";
            outputHTML += "<head>";
            outputHTML += "<title>Merchant Check Out Page</title>";
            outputHTML += "</head>";
            outputHTML += "<body>";
            outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
            outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
            outputHTML += "<table border='1'>";
            outputHTML += "<tbody>";
            foreach (string key in parameters.Keys)
            {
                outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
            }
            outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            outputHTML += "<script type='text/javascript'>";
            outputHTML += "document.f1.submit();";
            outputHTML += "</script>";
            outputHTML += "</form>";
            outputHTML += "</body>";
            outputHTML += "</html>";

            Session["htmlData"] = outputHTML;
            var urlBuilder = new UrlHelper(Request.RequestContext);
            var url = urlBuilder.Action("PaymentPage", "Home");
            return Json(new { status = "success", redirectUrl = url });
            //return View("PaymentPage");
        }
        [HttpGet]

        public ActionResult PaymentPage()
        {

            ViewBag.htmlData = Session["htmlData"];
            return View();
        }

        public ActionResult PaytmResponse(PaytmResponse data)
        {
            return View("PaytmResponse",data);
        }
    }
}