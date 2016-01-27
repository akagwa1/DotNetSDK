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


namespace net_sdk
{
    public class CloudTable
    {

       public JObject document = new JObject();
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

                    this.document.Add("columns",this.document.GetValue("type"));
                }
                catch (JsonException e2)
                {

                    throw new JsonException(e2.Message);
                }	

            
            
            
            
            }catch(CloudBoostException e){

                throw new CloudBoostException(e.Message);
            }

            //try
            //{
            //    this.document = new JObject();
            //    this.document.Add("name", "firtappTb");
            //    this.document.Add("appId", CloudApp.AppID);
            //    this.document.Add("_type", "table");
            //    if (tableName.ToLower() == "user")
            //    {
            //        this.document.Add("type", "user");
            //        this.document.Add("maxCount", 1);
            //    }
            //    else if (tableName.ToLower() == "role")
            //    {
            //        this.document.Add("type", "role");
            //        this.document.Add("maxCount", 1);
            //    }
            //    else
            //    {
            //        this.document.Add("type", "custom");
            //        this.document.Add("maxCount", 9999);
            //    }

            //    this.document.Add("columns", this.document.GetValue("type"));
            //}
            //catch (CloudBoostException e2)
            //{

            //    throw new CloudBoostException(e2.Message);
            //}	
        }
        	
        public CloudTable()
        {
            // TODO: Complete member initialization
        }
        public String getType()
        {
            try
            {
                return this.document.GetValue("_type").ToString();
            }
            catch (JsonException e)
            {
                return null;
            }
        }
        public void save()
        {

            if (CloudApp.AppID == null)
            {
                throw new CloudBoostException("App Id is null");
            }
        	JObject parames  = new JObject();
		CloudTable thisObj = this;
		try {
		parames.Add("data", document);		
		parames.Add("key", CloudApp.AppKey);
		String url = CloudApp.ServerUrl+"/"+CloudApp.AppID+"/table/"+this.document.GetValue("name");
		CBResponse response=CBParser.callJson(url, "PUT", parames);
			if(response.getStatusCode() == 200){
				JObject body = new JObject(response.getResponseBody());
				thisObj.document = body;
				//.done(thisObj, null);
			}else{
				CloudBoostException e = new CloudBoostException(response.getError());
				//callbackObject.done((CloudTable)null, e);
				
			}
		} catch (JsonException e) {
            throw new JsonException(e.Message);
		}
	}
        public void setTableName(String tableName)
        {
            try
            {
                this.document.Add("name", tableName);
            }
            catch (JsonException e)
            {

                throw new JsonException(e.Message);
            }
        }
        public String getTableName()
        {
            try
            {
                return this.document.GetValue("name").ToString();
            }
            catch (JsonException e)
            {

                throw new JsonException(e.Message);
                return null;
            }
        }
        String getTableType()
        {
            try
            {
                return this.document.GetValue("type").ToString();
            }
            catch (JsonException e)
            {

                throw new JsonException(e.Message);
                return null;
            }
        }
        public void addColumn(Column column) {
        //if(!PrivateMethod._columnValidation(column, this)){
        //    throw new CloudException("Invalid Column Found, Do Not Use Reserved Column Names");
        //}
		try{
		JArray columnList = new JArray( this.document.GetValue("columns").ToString());
		columnList.Add(column.document);
		this.document.Add("columns", columnList);
		} catch (JsonException e2) {

            throw new JsonException(e2.Message);
		}	
	}
    //    public void setColumn(Column column){
    //    //if(!PrivateMethod._columnValidation(column, this)){
    //    //    throw new CloudException("Invalid Column Found, Do Not Use Reserved Column Names");
    //    //}
    //    try{
    //        String name=column.getColumnName();
    //    JArray columnList = new JArray( this.document.GetValue("columns").ToString());
    //    for(int i=0; i<columnList.ToString().Length; i++){
    //        if(columnList.ToDictionary().gegetString("name").equals(name)){
    //            columnList.remove(i);
    //            columnList.Add(i, column.document);
    //            break;
    //        }
    //    }
    //    this.document.Add("columns", columnList);
    //    } catch (JSONException e2) {
			
    //        e2.printStackTrace();
    //    }	
    //}


    }
}
