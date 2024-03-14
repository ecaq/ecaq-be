using Ecaq.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Ecaq.Helpers
{
    public sealed class RestClient : IAsyncDisposable
    {
        public ValueTask DisposeAsync()
        {
            DisposeAsync();
            return default;
        }


        //public async Task<string> PostAsyncToSAP<T>(T t, SapToken cookieToken, string serviceURL, string u, string p)
        //{
        //    var json = JsonSerializer.Serialize(t);
        //    //string csrfToken = await GetCookiesWithCSRF(WebServiceUrl, userName, pwd);            
        //    //CookieContainer cookieContainer = new CookieContainer();
        //    //foreach (var cookie in _responseCookies)
        //    //{
        //    //    cookieContainer.Add(new Uri(WebServiceUrl), cookie);
        //    //}
        //    //HttpClientHandler handler = new HttpClientHandler();
        //    //handler.CookieContainer = cookieContainer;

        //    //TokenModel cookieToken = await RestToken.GetCookiesWithCSRF(WebServiceUrl, userName, pwd);

        //    CookieContainer cookieContainer = new CookieContainer();
        //    foreach (var cookie in cookieToken.ResponseCookies)
        //    {
        //        cookieContainer.Add(new Uri(serviceURL), cookie);
        //    }
        //    HttpClientHandler handler = new HttpClientHandler();
        //    handler.CookieContainer = cookieContainer;
        //    using (var client = new HttpClient(handler))
        //    {
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
        //                                                        "Basic", Convert.ToBase64String(
        //                                                            System.Text.Encoding.ASCII.GetBytes(
        //                                                                 string.Format("{0}:{1}", u, p))));


        //        //payloadJson = JsonConvert.SerializeObject(userStorage);
        //        HttpContent httpContent = new StringContent(json);
        //        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //        client.DefaultRequestHeaders.Add("x-csrf-token", cookieToken.CsrfToken);
        //        try
        //        {
        //            //response = await client.PostAsync(serviceURL, content);
        //            //if (response.IsSuccessStatusCode)
        //            //{
        //            //    jsonResponse = await response.Content.ReadAsStringAsync();
        //            //    return jsonResponse;
        //            //    //do something with json response here                    
        //            //}
        //            //else
        //            //{
        //            //    return null;
        //            //}

        //            var result = await client.PostAsync(serviceURL, httpContent);

        //            if (result.IsSuccessStatusCode)
        //            {
        //                return "success";
        //            }
        //            else
        //            {
        //                return "Prospect server exception error.";
        //            }

        //        }
        //        catch (Exception e)
        //        {
        //            string error = e.GetBaseException().ToString() + "---" + e.Message;
        //            //Could not connect to server
        //            return error;
        //        }
        //    }
        //}



        public async Task<ReCaptchaResponse> GRecaptchaAsync(string serviceURL)
        {

            using (var client = new HttpClient())
            {

                //payloadJson = JsonConvert.SerializeObject(userStorage);
                HttpContent httpContent = new StringContent(string.Empty);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                try
                {
                    //response = await client.PostAsync(serviceURL, content);
                    //if (response.IsSuccessStatusCode)
                    //{
                    //    jsonResponse = await response.Content.ReadAsStringAsync();
                    //    return jsonResponse;
                    //    //do something with json response here                    
                    //}
                    //else
                    //{
                    //    return null;
                    //}

                    var res = await client.PostAsync(serviceURL, httpContent);

                    if (res.IsSuccessStatusCode)
                    {

                        var cSharpObject = await res.Content.ReadFromJsonAsync<ReCaptchaResponse>();

                        // the same as above but two way process.
                        string jsonString = await res.Content.ReadAsStringAsync();
                        var cSharpObject2 = JsonSerializer.Deserialize<ReCaptchaResponse>(jsonString);

                        return cSharpObject;
                    }

                }
                catch (Exception e)
                {
                    string error = e.GetBaseException().ToString() + "---" + e.Message;
                }
            }

            return null;
        }



    }
}
