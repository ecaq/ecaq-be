using Ecaq.Data;
using Ecaq.Helpers;
using Ecaq.Models;
using Ecaq.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Ecaq.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ServiceController : ControllerBase
{
    private readonly IConfiguration _configuration;

    private readonly IWebHostEnvironment _hostingEnv;
    private readonly IEmailSenderApp _emailSender;
    private readonly string SECRET_KEY = "6LemEtoaAAAAADtXSsjeZGH2lpZ3LnrYWNvFDc_X";
    private readonly JsonSerializerOptions _jsonOptions;
    public ServiceController(IConfiguration configuration, IWebHostEnvironment hostingEnv, IEmailSenderApp emailSender)
    {
        _configuration = configuration;
        _hostingEnv = hostingEnv;
        _emailSender = emailSender;

        _jsonOptions = new JsonSerializerOptions { WriteIndented = true };
    }


    [AllowAnonymous]
    [HttpGet("Test")]
    public async Task<ActionResult<string>> ApiTest()
    {
        string ScheduledTime = "20:00";
        //Set the Default Time.
        DateTime scheduledTime = DateTime.MinValue;
        scheduledTime = DateTime.Parse(ScheduledTime).AddDays(-1);
        if (DateTime.Now.AddDays(-1) > scheduledTime)
        {
            scheduledTime = scheduledTime.AddDays(1);
        }

        TimeSpan timeSpan = scheduledTime.Subtract(DateTime.Now.AddDays(-1));
        string schedule = string.Format("{0} day(s) {1} hour(s) {2} minute(s) {3} seconds(s)", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);


        //Get the difference in Minutes between the Scheduled and Current Time.
        int dueTime = Convert.ToInt32(timeSpan.TotalMilliseconds);




        //string webRootPath = _hostingEnv.WebRootPath;
        //string body = string.Empty;
        ////using streamreader for reading my htmltemplate
        //using (StreamReader reader = new StreamReader(webRootPath + "/emailtemplate/" + "Greetings" + ".html"))
        //{
        //    body = reader.ReadToEnd();
        //}


        //DateTime d = DateTime.Now;
        //string newTime = d.ToString("HH:mm:ss").Replace(":", "");

        try
        {
            return await Task.FromResult("Your API is online! ||| " + "Simple Service scheduled to run after: " + schedule + " and dutime: " + dueTime);
        }
        catch (Exception ex)
        {
            return await Task.FromResult($"Your API has error of: {ex.Message}");
        }
    }

    [AllowAnonymous]
    [HttpPost("verify")]
    public async Task<ActionResult<string>> Verify(ReCaptchaToken reCaptchaToken)
    {

        var url = $"https://www.google.com/recaptcha/api/siteverify?secret={SECRET_KEY}&response={reCaptchaToken.GRecaptchaResponse}";

        RestClient omasterRestClient = new();
        var oMaster = await omasterRestClient.GRecaptchaAsync(url);

        string jsonString = JsonSerializer.Serialize(oMaster);


        return await Task.FromResult(jsonString);
    }

    [AllowAnonymous]
    [HttpPost("CreateToken")]
    public async Task<ActionResult> GetToken(ApplicationUser user)
    {
        //if (user.Username == _configuration["Account:Usr"] && user.Password == _configuration["Account:Pwd"])
        //{
        //    var issuer = _configuration["Jwt:Issuer"];
        //    var audience = _configuration["Jwt:Audience"];
        //    var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[]
        //        {
        //            new Claim("Id", Guid.NewGuid().ToString()),
        //            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
        //            new Claim(JwtRegisteredClaimNames.Email, user.Username),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //        }),
        //        Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:Expiry"]!)),
        //        Issuer = issuer,
        //        Audience = audience,
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        //    };
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    var jwtToken = tokenHandler.WriteToken(token);
        //    return await Task.FromResult(Ok(jwtToken));
        //}
        return await Task.FromResult(Unauthorized());
    }



    [AllowAnonymous]
    //[Authorize]
    [HttpPost("Add-City")]
    public async Task<ActionResult> AddCity(Cities city, string token = "")
    {
        city.id = int.Parse(Ecaq.Helpers.Utility.CreateRandomNumbers(8));
        string contentRootPath = _hostingEnv.ContentRootPath;

        //var contentRoot = Directory.GetCurrentDirectory();
        var folderPath = Path.Combine(contentRootPath, "data\\");

        string fullPath = folderPath + "cities.json";

        Ecaq.Helpers.Utility.AddObjectToJsonFile(fullPath, city, _jsonOptions);


        //var url = $"https://www.google.com/recaptcha/api/siteverify?secret={SECRET_KEY}&response={token}";
        //RestClient omasterRestClient = new();
        //var oMaster = await omasterRestClient.GRecaptchaAsync(url);

        //if (!oMaster.success)
        //{
        //    return BadRequest();
        //}

        //if (string.IsNullOrEmpty(fm.FormSubject))
        //{
        //    fm.FormSubject = "Iquiry from Contact Forms of AAMotors Website";
        //}
        //var result = await ComposeMessage(fm);

        return await Task.FromResult(Ok());
    }


    [Authorize]
    [HttpPost("SendEmail")]
    public async Task<ActionResult> SendEmai(FormsModel fm, string token)
    {
        var url = $"https://www.google.com/recaptcha/api/siteverify?secret={SECRET_KEY}&response={token}";
        RestClient omasterRestClient = new();
        var oMaster = await omasterRestClient.GRecaptchaAsync(url);

        if (!oMaster.success)
        {
            return BadRequest();
        }

        if (string.IsNullOrEmpty(fm.FormSubject))
        {
            fm.FormSubject = "Iquiry from Contact Forms of AAMotors Website";
        }
        var result = await ComposeMessage(fm);

        return await Task.FromResult(Ok(result));
    }

    private async Task<string> ComposeMessage(FormsModel fm)
    {
        string msgToCustomer = "Dear " + fm.FirstName + " " + fm.LastName + "," + System.Environment.NewLine + System.Environment.NewLine +
                    "Thank you for contacting us with your inquiry." + System.Environment.NewLine + System.Environment.NewLine +
                    "One of our Customer Care Representatives will shortly approach you to assist further." + System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine +
                    "We really value your time and if you need any urgent assistance, please feel free to contact us at 1234 5678" + System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine +
                    "Best Regards," + System.Environment.NewLine + System.Environment.NewLine +
                    "Customer Care Team" + System.Environment.NewLine +
                    "Email: customercare@khyrosfx.com" + System.Environment.NewLine +
                    "Website: www.khyrosfx.com";
        var resultToCustomer = await _emailSender.SendEmailAsync(fm.FirstName + " " + fm.LastName, fm.Email, "Acknowledgement", msgToCustomer);

        if (resultToCustomer.ToLower() != "success")
        {
            return await Task.FromResult(resultToCustomer);
        }

        string msgToCRM = "Dear Customer Care Team," + System.Environment.NewLine + System.Environment.NewLine +
                    "A new customer feedback has been received from KhyrosFx website. Please find below the customer details for your further action:" + System.Environment.NewLine + System.Environment.NewLine +
                    "Date : " + DateTime.UtcNow + System.Environment.NewLine + System.Environment.NewLine +
                    "Customer Name : " + fm.FirstName + " " + fm.LastName + System.Environment.NewLine + System.Environment.NewLine +
                    "Phone : " + fm.Mobile + System.Environment.NewLine + System.Environment.NewLine +
                    "Email : " + fm.Email + System.Environment.NewLine + System.Environment.NewLine +
                    "Additional Message : " + fm.MessageBody + System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine +
                    "Thank you and regards," + System.Environment.NewLine + System.Environment.NewLine +
                    "Khyros Fx Team";

        var resultToCRM = await _emailSender.SendEmailAsync(fm.FirstName + " " + fm.LastName, _configuration["Smtp:toEmail"]?.ToString()!, fm.FormSubject, msgToCRM);
        //var result = await _emailSender.SendEmailAsHtmlAsync(fm.FirstName + " " + fm.LastName, fm.Email, fm.FormSubject, fm.MessageBody);
        if (resultToCRM.ToLower() != "success")
        {
            return await Task.FromResult(resultToCRM);
        }
        return await Task.FromResult(resultToCRM);
    }

}
