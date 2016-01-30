using CB;
using net_sdk.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace net_sdk
{
    public class CloudAppTest
    {
        

        static void Main(string[] args) {
           // shouldSaveRecord();
           // save();
           // shouldCreateAnApp();
            find();
           

        }
        public static void shouldSaveRecord() {
            CloudApp.init("bengi123", "MjFWX9D3JqTa76tcEHt9GL2ITB8Gzsp68S1+3oq7CBE=");
		CloudTable table=new CloudTable("firstAppTb");
        table.save();

        }

        public static void find() {
            CloudQuery qq = new CloudQuery("pengi");
            qq.FindOne();
        
        }

      	public static void shouldCreateAnApp(){
            CloudApp.init("bengi123", "MjFWX9D3JqTa76tcEHt9GL2ITB8Gzsp68S1+3oq7CBE=");
            Dictionary<string, Object> parames = new Dictionary<string, Object>();
			 parames.Add("email", "hello@cloudboost.io");
			 parames.Add("password", "sample");
			String url=CloudApp.ServerUrl+"/user/signin";
            CBResponse response = CBParser.callJson(url, "POST", parames);
			if(response.getStatusCode()==200){
				JObject obj=new JObject(response.getResponseBody());
				String url2=CloudApp.ServerUrl+"/app/create";
				String appid="";//PrivateMethod._makeString();
				String appname="";//PrivateMethod._makeString();
				JObject param=new JObject();
                parames.Add("appId", appid);
                parames.Add("name", appname);
                parames.Add("userId", obj.GetValue("_id"));
				CBResponse response2=CBParser.callJson(url2, "POST", parames);
				
				
			}else response.getStatusMessage();
	}
        public static void save() {


            try {

                CloudApp.init("bengi1234", "3UVnLf/HANIYE+IxP2ZxHg==");
                CloudObject obj = new CloudObject("firstAppTb");
                obj.CreatedAt = DateTime.Now;
                obj.IsSearchable = true;
                obj.UpdatedAt = DateTime.Now;
                obj.ID = "bengi1234";
                 obj.SaveAsync();
            
            
            }catch(Exception e){
            
            }
        
        
        }

      






    }
}
