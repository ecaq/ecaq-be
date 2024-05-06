using Ecaq.Models;
using Ecaq.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Ecaq.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CityController : ControllerBase
{
    private readonly IConfiguration _configuration;

    private readonly IWebHostEnvironment _hostingEnv;
    private readonly IEmailSenderApp _emailSender;
    private readonly string SECRET_KEY = "6LemEtoaAAAAADtXSsjeZGH2lpZ3LnrYWNvFDc_X";
    private readonly JsonSerializerOptions _jsonOptions;
    public CityController(IConfiguration configuration, IWebHostEnvironment hostingEnv, IEmailSenderApp emailSender)
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
    //[Authorize]
    [HttpGet("Get-Cities")]
    public async Task<ActionResult<Cities>> GetCities()
    {
        string contentRootPath = _hostingEnv.ContentRootPath;
        string webRootPath = _hostingEnv.WebRootPath;

        //var contentRoot = Directory.GetCurrentDirectory();
        var folderPath = Path.Combine(webRootPath, "data\\");

        string fullPath = folderPath + "cities.json";

        if (string.IsNullOrEmpty(fullPath))
        {
            throw new ArgumentException("File path cannot be null or empty.");
        }

        try
        {
            // Read existing JSON file content
            var jsonObject = await Ecaq.Helpers.Utility.GetJsonFileToObjecAsync(fullPath);

            return await Task.FromResult(Ok(jsonObject.cities));
        }
        catch (JsonException ex)
        {
            throw new JsonException("Error while serializing the object to JSON.", ex);
        }
        catch (IOException ex)
        {
            throw new IOException("Error while writing to the JSON file.", ex);
        }
    }


    [AllowAnonymous]
    //[Authorize]
    [HttpGet("Get-City/{id}")]
    public async Task<ActionResult<Cities>> GetCity(int id)
    {
        string contentRootPath = _hostingEnv.ContentRootPath;
        string webRootPath = _hostingEnv.WebRootPath;

        //var contentRoot = Directory.GetCurrentDirectory();
        var folderPath = Path.Combine(webRootPath, "data\\");

        string fullPath = folderPath + "cities.json";

        if (string.IsNullOrEmpty(fullPath))
        {
            throw new ArgumentException("File path cannot be null or empty.");
        }

        try
        {
            // Read existing JSON file content
            var jsonObject = await Ecaq.Helpers.Utility.GetJsonFileToObjecAsync(fullPath);
            var cityObject = jsonObject.cities.Where(x => x.id == id).FirstOrDefault();

            return await Task.FromResult(Ok(cityObject));
        }
        catch (JsonException ex)
        {
            throw new JsonException("Error while serializing the object to JSON.", ex);
        }
        catch (IOException ex)
        {
            throw new IOException("Error while writing to the JSON file.", ex);
        }
    }


}
