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


namespace net_sdk.Util
{
    public class CBParser
    {
        static Random random = new Random();
        static string boundary1 = "---------" + randomString() + randomString() + randomString();
        public static CBResponse callJson(string myUrl,string httpMethod,JObject parameters) {

            try { 
            
                parameters.Add("sdk","C#");
            
            }catch(JsonException e){


                throw new JsonException(e.Message);
            }

            string parames = parameters.ToString();

            
            Uri url = null;

            try {
            
            url = new Uri(myUrl);
            
            }catch(UriFormatException e){

                throw new UriFormatException(e.Message);

            }

         
            HttpWebRequest r =(HttpWebRequest) System.Net.WebRequest.Create(url);
            r.Timeout = 10000;
            r.Method = httpMethod;
            r.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:26.0) Gecko/20100101 Firefox/26.0";
            r.ContentType = "application/json";
           Stream dos = null;
           string respMsg = null;
           int respCode = 0;
           string inputString = null;
           string sid = null;

           try
           {

               WebResponse resp = null;
               resp = r.GetResponse();
               dos = resp.GetResponseStream();
               dos.Flush();
               dos.Close();
               StreamReader rdr = new StreamReader(dos);
               // string feedback = rdr.ReadToEnd();
               //respCode
               //respMsg
               if (respCode != 200)
               {

                   string error = "";
                   CBResponse res = new CBResponse(respMsg, respMsg, respCode, sid);
                   res.setError(error);
                   return res;
               }

               inputString = rdr.ReadToEnd();

           }catch(IOException e){

               CBResponse resp = new CBResponse(respMsg,respMsg,respCode,sid);
               return resp;
           }

           CBResponse respo = new CBResponse(respMsg,respMsg,respCode,sid);
           return respo;
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
