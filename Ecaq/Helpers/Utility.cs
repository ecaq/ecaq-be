
using Ecaq.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
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

        public static string CreateRandomPassword(int passwordLength)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            char[] chars = new char[passwordLength];
            Random rd = new();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
        public static string CreateRandomNumbers(int numberLength)
        {
            string allowedChars = "01234567899876543210";
            char[] chars = new char[numberLength];
            Random rd = new();

            for (int i = 0; i < numberLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
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






        // <summary>
        // Adds a new object to a JSON file.
        //
        // Parameters:
        // - filePath: The path to the JSON file.
        // - newObject: The object to be added to the JSON file.
        //
        // Exceptions:
        // - Throws an ArgumentException if the filePath is null or empty.
        // - Throws an ArgumentNullException if the newObject is null.
        // - Throws a JsonException if there is an error while serializing the newObject to JSON.
        // - Throws an IOException if there is an error while writing to the JSON file.
        // </summary>
        public static async void AddObjectToJsonFile(string filePath, Cities newObject, JsonSerializerOptions jsonOptions)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.");
            }

            if (newObject == null)
            {
                throw new ArgumentNullException(nameof(newObject), "Object cannot be null.");
            }

            try
            {
                // Read existing JSON file content
                string jsonContent = await File.ReadAllTextAsync(filePath);

                // Deserialize the JSON content into a dynamic object
                var jsonObject = JsonSerializer.Deserialize<CityRoot>(jsonContent)!;

                // Add the new object to the JSON array
                //jsonObject.Add(newObject);
                jsonObject.cities.Add(newObject);

                // Serialize the updated object back to JSON
                string updatedJsonContent = JsonSerializer.Serialize(jsonObject, jsonOptions);

                // Write the updated JSON content back to the file
                File.WriteAllText(filePath, updatedJsonContent);

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

        public static async Task<CityRoot> GetJsonFileToObjecAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.");
            }

            try
            {
                // Read existing JSON file content
                string jsonContent = await File.ReadAllTextAsync(filePath);

                // Deserialize the JSON content into a dynamic object
                var jsonObject = JsonSerializer.Deserialize<CityRoot>(jsonContent)!;

                return jsonObject;
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

        public static async void DeletedToJsonFile(string filePath, int id, JsonSerializerOptions jsonOptions)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.");
            }

            try
            {
                // Read existing JSON file content
                string jsonContent = await File.ReadAllTextAsync(filePath);







                // Deserialize the JSON content into a dynamic object
                var jsonObject = JsonSerializer.Deserialize<CityRoot>(jsonContent)!;
                var cityObject = jsonObject.cities.Where(x => x.id == id).FirstOrDefault();

                // Add the new object to the JSON array
                //jsonObject.Add(newObject);
                jsonObject.cities.Remove(cityObject!);

                // Serialize the updated object back to JSON
                string updatedJsonContent = JsonSerializer.Serialize(jsonObject, jsonOptions);

                // Write the updated JSON content back to the file
                File.WriteAllText(filePath, updatedJsonContent);
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
