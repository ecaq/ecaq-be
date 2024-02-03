
using Microsoft.AspNetCore.Components.Forms;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Ecaq.Helpers
{
    public class Utility
    {
        public static string Encrypt(string password)
        {
            var provider = MD5.Create();
            string salt = "S0m3R@nd0mSalt";
            byte[] bytes = provider.ComputeHash(Encoding.UTF32.GetBytes(salt + password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        public static bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public static string RemoveLeadingZeros(string numbers)
        {
            if (Int32.TryParse(numbers, out int number))
            {
                // etc...
                return number.ToString();
            }
            return numbers;
        }


        public static async Task<Tuple<string, string>> FileInputServiceAsync(InputFileChangeEventArgs e, string fileName)
        {
            string fName = "";
            string base64 = "";

            var file = e.GetMultipleFiles().FirstOrDefault();
            string ext = Path.GetExtension(file.Name);
            var format = "image/*";
            if (file != null)
            {
                var resizedImageFile = await file.RequestImageFileAsync(format, 400, 400);
                Stream stream = resizedImageFile.OpenReadStream();
                var ms = new MemoryStream();
                await stream.CopyToAsync(ms);
                stream.Close();
                fName = fileName + ext;  //file.Name;
                //base64 = ConvertImageToDisplay(ms.ToArray());
                //base64 = Base64Encode(ConvertImageToDisplay(ms.ToArray()));
                base64 = Convert.ToBase64String(ms.ToArray());
            }









            return new Tuple<string, string>(fName, base64);
        }
        static string ConvertImageToDisplay(byte[] img)
        {
            if (img != null)
            {
                var base64 = Convert.ToBase64String(img);
                var fs = string.Format("data:image/jpg;base64,{0}", base64);
                return fs;
            }
            return "";
        }
        static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }













        //public static CustomerObjectDto _XmlConvertSelectNode(string xmlString, string nodeName)
        //{

        //    var xmldoc = new XmlDocument();
        //    xmldoc.LoadXml(xmlString);
        //    XmlNodeList nodes = xmldoc.GetElementsByTagName(nodeName);

        //    //foreach (XmlNode node in nodes)
        //    //{
        //    //    Console.WriteLine("Node={0}", node.Name);
        //    //    foreach (XmlNode child in node.SelectNodes("content"))
        //    //    {
        //    //        Console.WriteLine("Name={0} type attribute={1}", child.Name, child.Attributes["type"].Value);
        //    //    }
        //    //}

        //    var fromXml = JsonConvert.SerializeObject(nodes);
        //    var fromJson = JsonConvert.DeserializeObject<List<CustomerObjectDto>>(fromXml);


        //    return fromJson.FirstOrDefault();
        //}
        //public static IndividualCustomerCollectionSet XmlConvertObject(string json, string nodeName)
        //{
        //    XmlDocument doc = JsonConvert.DeserializeXmlNode(json);
        //    //var xmldoc = new XmlDocument();
        //    //xmldoc.LoadXml(xmlString);
        //    XmlNodeList nodes = doc.GetElementsByTagName(nodeName);

        //    //foreach (XmlNode node in nodes)
        //    //{
        //    //    Console.WriteLine("Node={0}", node.Name);
        //    //    foreach (XmlNode child in node.SelectNodes("content"))
        //    //    {
        //    //        Console.WriteLine("Name={0} type attribute={1}", child.Name, child.Attributes["type"].Value);
        //    //    }
        //    //}

        //    var fromXml = JsonConvert.SerializeObject(nodes);
        //    var fromJson = JsonConvert.DeserializeObject<List<IndividualCustomerCollectionSet>>(fromXml);


        //    return fromJson.FirstOrDefault();
        //}



        /// <summary>
        /// Source https://www.learmoreseekmore.com/2020/10/blazor-webassembly-fileupload.html
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static async ValueTask<Tuple<bool, string?>> OnImageSelectionAsync(InputFileChangeEventArgs e)
        {
            long MAXALLOWEDSIZE = 1000000;

            //int ctr = 1;
            foreach (IBrowserFile file in e.GetMultipleFiles())
            {
                if (file.ContentType.ToLower() == "image/jpeg" || file.ContentType.ToLower() == "image/png")
                {
                    if (file.Size < MAXALLOWEDSIZE)
                    {

                        var buffers = new byte[file.Size];
                        await file.OpenReadStream(MAXALLOWEDSIZE).ReadAsync(buffers);
                        string fileType = file.ContentType;
                        string fileUrl = $"data:{fileType};base64,{Convert.ToBase64String(buffers)}";

                        return Tuple.Create(true, fileUrl)!;
                        //return new Tuple<bool, string>(true, fileUrl)!;
                    }
                    else
                    {
                        return Tuple.Create(false, "Only one mega byte file size is allowed.")!;
                        //return new Tuple<bool, string>(false, "Only one mega byte file size is allowed.")!;
                    }
                }
                else
                {
                    return Tuple.Create(false, "Only jpg and png file format is allowed.")!;
                    //return new Tuple<bool, string>(false, "Only jpg and png file format is allowed.")!;
                }
            }

            return Tuple.Create(false, "Value cannot be null")!;
            //return new Tuple<bool, string>(false, "Value cannot be null")!;

        }

    }
}
