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
        if(!PrivateValidation._tableValidation(tableName)){
			try {
				throw new CloudBoostException("Invalid Table Name");
			} catch (CloudBoostException e) {
				throw new CloudBoostException("Invalid Table Name");
			}
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
		//CloudTable thisObj = this;
		try {
		parames.Add("data", document);		
		parames.Add("key", CloudApp.AppKey);
        String url = CloudApp.ServiceUrl + "/" + CloudApp.AppID+"/table/" + this.document["name"];
		CBResponse response=CBParser.callJson(url, "PUT", parames);
			if(response.getStatusCode() == 200){
                JObject body = new JObject();
                body = (JObject)response.getResponseBody();
                this.document = Serializer.Deserialize(body);
				
			}else{
				CloudBoostException e = new CloudBoostException(response.getError());
				
			}
		} catch (JsonException e) {
            throw new JsonException(e.Message);
		}
	}        
        public void addColumn(Column column) {
            if (!PrivateValidation._columnValidation(column, this))
            {
                throw new CloudBoostException("Invalid Column Found, Do Not Use Reserved Column Names");
            }
		try{

            //List<string> columnList = new List<string>();
           // columnList.Add(this.document["columns"].ToString());
            this.document.Remove("columns");
		   // this.document.Add("columns", columnList);
            this.document.Add("columns", column);
		} catch (CloudBoostException e) {

            throw new CloudBoostException(e.Message);
		}	catch(ArgumentException e2){

            throw new ArgumentException(e2.Message);
        }
	}
        public void addColumn(Column[] column){
		for(int i=0; i<column.Length; i++){
            try
            {
                this.addColumn(column[i]);
            }catch(CloudBoostException e){

                throw new CloudBoostException(e.Message);
            }
		}
	}
        public void setColumn(Column column)
        {
            if (!PrivateValidation._columnValidation(column, this))
            {
                throw new CloudBoostException("Invalid Column Found, Do Not Use Reserved Column Names");
            }
            try
            {
                string name = column.getColumnName();
                ArrayList columnList = new ArrayList((ArrayList)this.document["columns"]);

                if (columnList.Contains(name))
                {
                    int index = columnList.IndexOf(name);
                    columnList.Remove(name);
                    columnList.Insert(index, column.dictionaryDoc);

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
        public void updateColumn(Column column)
        {
            Column col = null;
            try
            {
                List<Object> columnList = new List<Object>(this.document.Select(columns => columns.Value));
                for (int i = 0; i < columnList.Count; i++)
                {
                    //to be revised
                    if (columnList.ElementAt(i) == column.getColumnName())
                    {
                        columnList.Insert(i, column);
                        this.document.Add("columns", columnList);
                        break;
                    }
                }
            }
            catch (CloudBoostException e)
            {

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
        public static void get(CloudTable table){
		if(CloudApp.AppID == null){
			throw new CloudBoostException("App Id is missing");
		}
		
		Dictionary<string,Object> parames = new Dictionary<string,Object>();
		try {
		parames.Add("key", CloudApp.AppKey);
		parames.Add("appId", CloudApp.AppID);
		String url = CloudApp.ServiceUrl+"/"+CloudApp.AppID+"/table/"+table.getTableName();
		CBResponse response=CBParser.callJson(url, "POST", parames);

			if(response.getStatusCode() == 200){
                Dictionary<string,Object> respObjectDictionary = new Dictionary<string,Object>();
				//JObject body = new JObject(response.getResponseBody());
               // respObjectDictionary = body;
				//CloudTable obj = new CloudTable(body["name"].ToString());
				//obj.document = body;
				
			}else{
				
				throw new CloudBoostException(response.getResponseBody());
				
			}
			
		} catch (CloudBoostException e) {
			throw new CloudBoostException("Failed to get table, may be inexistent");
			
		}
	}
	




    }
}
