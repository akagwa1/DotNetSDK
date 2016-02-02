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
           // find();
            delete();
           

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

      	public static void delete(){
            CloudApp.init("bengi123", "MjFWX9D3JqTa76tcEHt9GL2ITB8Gzsp68S1+3oq7CBE=");

            CloudTable ct = new CloudTable();
            ct.delete();
           
				
			
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
