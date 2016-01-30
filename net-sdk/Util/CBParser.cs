using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using net_sdk.Util;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;
using CB;
using System.Web;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using CB.Exception;


namespace net_sdk.Util
{
    public class CBParser
    {
        static Random random = new Random();
        static string boundary1 = "---------" + randomString() + randomString() + randomString();
        public static CBResponse callJson(string myUrl,string httpMethod,Dictionary<string,Object> parameters) {
          
            System.Net.ServicePointManager.ServerCertificateValidationCallback = RemoteCertificateValidation;
            System.Net.ServicePointManager.Expect100Continue = false;

            try { 
            
                parameters.Add("sdk","C#");
            
            }catch(CloudBoostException e){


                throw new CloudBoostException(e.Message);
            }
            string parames = JsonConvert.SerializeObject(parameters);

            HttpWebRequest     r = (HttpWebRequest)System.Net.WebRequest.Create(myUrl);
           
            r.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";
            int respCode = 0;
            string respMsg = null;
            r.Timeout = 100000;

            r.ContentType = "application/json";
           
           string inputString = null;
           string sid = null;

           try
           {

               StreamReader bodyreader;
               string bodytext = "";
               byte[] contentToWrite = Encoding.ASCII.GetBytes(parames);

               r.Method = httpMethod;
            
               using ( var requestStream = r.GetRequestStream())
               {
                   requestStream.Write(contentToWrite, 0, contentToWrite.Length);
               }

            
               try
               {
                   var responseString = r.GetResponse() as HttpWebResponse;//GetResponseStream()).ReadToEnd();

               }catch(IOException e){}catch(Exception e){

                   Console.WriteLine(e.Message);
               
               }
               if (respCode != 200)
               {

                   string error = "";
                   CBResponse res = new CBResponse(respMsg, respMsg, respCode, sid);
                   res.setError(error);
                   return res;
               }

           }catch(IOException e){

               CBResponse resp = new CBResponse(respMsg,respMsg,respCode,sid);
               return resp;
           }
           catch(WebException e){
           }

           CBResponse respo = new CBResponse(respMsg,respMsg,respCode,sid);
           return respo;
        }
        private static bool RemoteCertificateValidation(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private static void newline() {

            write("\r\n");
        }

        private static void writeName(string name) {

            try {
                newline();
                write("Content-Disposition: form-data; name=\"");
                write(name);
                write("\"");
                


            }catch(IOException e){
                throw new IOException(e.Message);
            }

        
        }

        public static Stream dos = null;
        public static HttpWebRequest req = null;
        protected static void write(string s) {

            dos.WriteByte(byte.Parse(s));

            
        }

        protected static void boundary() {

            write("--");
            writeName(boundary1);
        }

        protected static void writeln(string s) {

            try {

                write(s);
                newline();

            
            }catch(IOException e){}
        
        }
        public static void setParameter(string name, string value) {

            try {

                boundary();
                writeName(name);
                newline(); newline();
                writeln(value);
            
            }catch(IOException e){}

        }

        public static CBResponse postFormData(string myurl,string httpMethod, JObject parames, Stream inputstream) {

            Uri url = null;
            try {
                url = new Uri(myurl);
            
            
            }catch(IOException e){

                throw new IOException(e.Message);
            }
            HttpWebRequest req = (HttpWebRequest)System.Net.WebRequest.Create(url);
            req.Method = httpMethod;
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:26.0) Gecko/20100101 Firefox/26.0";
            req.ContentType = "multipart/form-data; boundary=" + "boundary"; //boundary;
            HttpWebResponse resp = null;
            resp = (HttpWebResponse)req.GetResponse();
             dos = resp.GetResponseStream();
             setParameter("key",CloudApp.AppKey);
             setParameter("fileObj",parames.ToString());
             setParameter("sdk","java");
             setFile("fileToUpload","blob",inputstream);
             Stream stream = post();
            int code = int.Parse(resp.StatusCode.ToString());
            string msg = resp.StatusDescription;
             string respon = inputStreamToString(stream);
             CBResponse ress = new CBResponse(respon, msg, code, null);
             return ress;
        }

        private static string inputStreamToString(Stream stream)
        {
            try {

                StringBuilder responseInBuffer = new StringBuilder();
	        byte[] b = new byte[4028];
	        while (true) {
	            try {
                    int n =  stream.Read(b,0,b.Length);
	                if (n == -1) {
	                    break;
	                }
	                responseInBuffer.Append(b.ToString(), 0, n);
	            } catch (IOException e) {
                    throw new IOException(e.Message);
	            } catch (Exception e2) {
                    throw new Exception(e2.Message);
	            }
	        }
	        return responseInBuffer.ToString();
            
            }catch(IOException e){

                throw new IOException(e.Message);
            }
        }

        public static void setFile(String name, String filename, Stream inputstream)  {

            try
            {
                boundary();
                writeName(name);
                write("; filename=\"");
                write(filename);
                write("\"");
                newline();
                write("Content-Type: ");
                String type = null;
                if (type == null) type = "application/octet-stream";
                writeln(type);
                newline();
                pipe(inputstream, dos);
                newline();
            }catch(IOException e){


                throw new IOException(e.Message);
            }
      }

        private static void pipe(Stream inputstream, Stream dos)
        {
            try {
            
            byte[] buf = new byte[500000];
            int nread;
            int navailable;
            int total = 0;
            while((nread = inputstream.Read(buf,0,buf.Length)) >= 0)
            {

                    dos.Write(buf,0,nread);
                    total += nread;
                
               
            }
            dos.Flush();
           // Buffer = null;

                
            
            }catch(IOException e){

                throw new IOException(e.Message);
            
            }
        }
        protected static String randomString()
        {
            Random random = new Random();
            string randomStr = random.Next(1000).ToString();
           return randomStr;
        }


        public static Stream post() {
            try{
                WebResponse resp = null;
                HttpWebRequest req =null;
        boundary();
        writeln("--");
        dos.Close();
       resp = req.GetResponse();
        
        return resp.GetResponseStream();
            }catch(IOException e){

                throw new IOException(e.Message);
            }
      }

    }
}
