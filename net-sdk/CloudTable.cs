using CB;
using CB.Exception;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using net_sdk.Util;
using CB.Util;
using System.Collections;


namespace net_sdk
{
    public class CloudTable
    {

       public Dictionary<string,Object> document = new Dictionary<string,Object>();
        public CloudTable(string tableName) {

            try {
                if(string.IsNullOrEmpty(tableName)){

                    throw new CloudBoostException("INVALID TABLE NAME");
                }

                try
                {

                    document.Add("name", tableName);
                    this.document.Add("appId", CloudApp.AppID);
                    this.document.Add("_type", "table");
                    if (tableName.ToLower() == "user")
                    {
                        this.document.Add("type", "user");
                        this.document.Add("maxCount", 1);
                    }
                    else if (tableName.ToLower() == "role")
                    {
                        this.document.Add("type", "role");
                        this.document.Add("maxCount", 1);
                    }
                    else
                    {
                        this.document.Add("type", "custom");
                        this.document.Add("maxCount", 9999);
                    }

                    this.document.Add("columns",this.document["type"]);
                }
                catch (CloudBoostException e2)
                {

                    throw new CloudBoostException(e2.Message);
                }	

            }catch(CloudBoostException e){

                throw new CloudBoostException(e.Message);
            }

        }
        	
        public CloudTable()
        {
            // TODO: Complete member initialization
        }
        public string getType()
        {
            try
            {
                return this.document["_type"].ToString();
            }
            catch (CloudBoostException e)
            {
                throw new CloudBoostException(e.Message);
                return null;
            }
        }
        public void setTableName(string tableName)
        {
            try
            {
                this.document.Add("name", tableName);
            }
            catch (CloudBoostException e)
            {

                throw new CloudBoostException(e.Message); ;
            }
        }
        public string getTableName()
        {
            try
            {
                return this.document["name"].ToString();
            }
            catch (CloudBoostException e)
            {

                throw new CloudBoostException(e.Message);
                return null;
            }
        }
        public string getTableType()
        {
            try
            {
                return this.document["type"].ToString();
            }
            catch (CloudBoostException e)
            {

                throw new CloudBoostException(e.Message);
                return null;
            }
        }       
        public void save()
        {

            if (string.IsNullOrEmpty(CloudApp.AppID))
            {
                throw new CloudBoostException("AppId is null");
            }
        	
        Dictionary<string,Object> parames = new Dictionary<string,Object>();
		CloudTable thisObj = this;
		try {
		parames.Add("data", document);		
		parames.Add("key", CloudApp.AppKey);
        String url = CloudApp.ApiUrl + "/" + CloudApp.AppID+"/table/" + this.document["name"];
		CBResponse response=CBParser.callJson(url, "PUT", parames);
			if(response.getStatusCode() == 200){
                JObject body = new JObject();
                body = (JObject)response.getResponseBody();
                thisObj.document = Serializer.Deserialize(body);
				
			}else{
				CloudBoostException e = new CloudBoostException(response.getError());
				
			}
		} catch (JsonException e) {
            throw new JsonException(e.Message);
		}
	}        
        public void addColumn(Column column) {
        //if(!PrivateMethod._columnValidation(column, this)){
        //    throw new CloudException("Invalid Column Found, Do Not Use Reserved Column Names");
        //}
		try{

            ArrayList columnList = new ArrayList((ArrayList)this.document["columns"]);
            columnList.Add(column.document);
		    this.document.Add("columns", columnList);
		} catch (CloudBoostException e) {

            throw new CloudBoostException(e.Message);
		}	
	}
        public void setColumn(Column column)
        {
            //if (!PrivateMethod._columnValidation(column, this))
            //{
            //    throw new CloudException("Invalid Column Found, Do Not Use Reserved Column Names");
            //}
            try
            {
                string name = column.getColumnName();
                ArrayList columnList = new ArrayList((ArrayList)this.document["columns"]);

                if (columnList.Contains(name))
                {
                    int index = columnList.IndexOf(name);
                    columnList.Remove(name);
                    columnList.Insert(index, column.document);

                }
                

                    this.document.Add("columns", columnList);
                
            }
            catch (CloudBoostException e)
            {

                throw new CloudBoostException(e.Message); ;
            }
        }
        private void deleteColumn(Column column) {
            try {
                string name = column.getColumnName();
                ArrayList columnList = new ArrayList((ArrayList)this.document["columns"]);
                if (columnList.Contains(name)) {

                    columnList.Remove(name);

                
                }
            
            }catch(CloudBoostException e){

                throw new CloudBoostException(e.Message);
            }
        
        }
        public void delete() { 
        try{
        if(CloudApp.AppID == null){
        throw new CloudBoostException("App id is null");

        }
        //if(this.document["_id"]==null){
        //    throw new CloudBoostException("Cannot delete a table which is not saved.");
        //}
        }catch(CloudBoostException e){
        throw new CloudBoostException(e.Message);
        
        }

            Dictionary<string,Object> parames = new Dictionary<string,Object>();
            try {

                parames.Add("data", document);
                parames.Add("key",CloudApp.AppKey);
                string url = CloudApp.ServiceUrl + "/" + CloudApp.AppID + "/table/"+"bengi";//this.document["name"];
                CBResponse response = CBParser.callJson(url,"DELETE",parames);
                if (response.getStatusCode() == 200)
                {
                    Console.WriteLine("SUCCESS");
                }
                else
                {
                    CloudBoostException e = new CloudBoostException(response.getResponseBody());
                    throw e;
                }

            }catch(CloudBoostException e){

                throw new CloudBoostException(e.Message);
            }catch(KeyNotFoundException e){
            throw new Exception("KEY VALUE PAIR DOES NOT EXIST");
            }


        
        }




    }
}
