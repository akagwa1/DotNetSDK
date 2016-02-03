using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CB.Exception;
using CB.Util;


namespace net_sdk
{
   public class Column
    {
       public Dictionary<string,Object> document;

       public Dictionary<string, Object> dictionaryDoc = new Dictionary<string, Object>();

       public enum DataType { 
       
       Text,Email,URL,Number,Boolean,DateTime,GeoPoint,File,List,Relation,Object,Id,EncryptedText,ACL
       
       }

       public Column(string columnName, DataType dataType){



           if (!PrivateValidation._columnNameValidation(columnName))
           {
               try
               {
                   throw new CloudBoostException("Invalid Column Name");
               }
               catch (CloudBoostException e)
               {
                   throw new CloudBoostException(e.Message); ;
               }
           }

            
           try {

               this.dictionaryDoc = new Dictionary<string, object>();

               dictionaryDoc.Add("name", columnName);
               dictionaryDoc.Add("dataType", dataType);
               dictionaryDoc.Add("_type", "column");
               dictionaryDoc.Add("required", false);
               dictionaryDoc.Add("unique", false);
               dictionaryDoc.Add("relatedTo", null);
               dictionaryDoc.Add("relationType", null);
               dictionaryDoc.Add("isDeletable", true);
               dictionaryDoc.Add("isEditable", true);
               dictionaryDoc.Add("isRenamable", false);

              

           }
           catch (CloudBoostException e)
           {

               throw new CloudBoostException(e.Message);
           }
       
       
       }

       public Column(String columnName,DataType dataType,Boolean required,Boolean unique) {


           if (!PrivateValidation._columnNameValidation(columnName))
           {
               try
               {
                   throw new CloudBoostException("Invalid Column Name");
               }
               catch (CloudBoostException e)
               {
                   throw new CloudBoostException(e.Message);
               }
           }
           try
           {
               this.dictionaryDoc = new Dictionary<string, object>();
               dictionaryDoc.Add("name", columnName);
               dictionaryDoc.Add("dataType", dataType);
               dictionaryDoc.Add("_type", "column");
               dictionaryDoc.Add("required", required);
               dictionaryDoc.Add("unique", unique);
               dictionaryDoc.Add("relatedTo", null);
               dictionaryDoc.Add("relationType", null);
               dictionaryDoc.Add("isDeletable", true);
               dictionaryDoc.Add("isEditable", true);
               dictionaryDoc.Add("isRenamable", false);

              // this.document = new JObject();
              // document = Serializer.Serialize(dictionaryDoc);
           }
           catch (CloudBoostException e)
           {
           throw new CloudBoostException(e.Message);
           
           }
       
       }

      public string getColumnName()
       {

           try {
               

             return  this.dictionaryDoc["name"].ToString();
           
           
           }catch(CloudBoostException e){

               throw new CloudBoostException(e.Message);
           }catch(ArgumentNullException e2){
           
           
           throw new ArgumentNullException(e2.Message);
           }


       }

       public void setColumnName(string value) {
           Dictionary<string, Object> dictionary = new Dictionary<string, Object>();

           try {

              dictionaryDoc.Add("name",value);
           
           }catch(CloudBoostException e){}
       
       }

       public string getDataType() {

          
           try {

               string datatype = document["dataType"].ToString();
               //return (DataType)datatype;
               return datatype;
           
           
           }catch(CloudBoostException e){

               throw new CloudBoostException(e.Message);
               
           }
       
       }

       public void setDataType(DataType type) {

           try {

              dictionaryDoc.Add("dataType",type);
           
           
           }catch(CloudBoostException e){


               throw new CloudBoostException(e.Message);
           
           }
       
       
       }

       public void setRequired(bool required) {

           try {

               document.Add("required",required);

           
           }catch(CloudBoostException e){

               throw new CloudBoostException(e.Message);
           
           }
       
       }

       public bool getRequired() {

           try {

               bool.Parse(document["required"].ToString());
               return true;
           
           
           }catch(CloudBoostException e){

               throw new CloudBoostException(e.Message);
               return false;
           }
       
       }

       public void setUnique(bool unique) {

           try {

               document.Add("unique",unique);
           
           }catch(CloudBoostException e){


               throw new CloudBoostException(e.Message);
           }

           
       }

       public bool getUnique() {


           try { 
            bool.Parse(document["unique"].ToString());
            return true;
           
           }catch(CloudBoostException e){

               throw new CloudBoostException(e.Message);
               return false;
           }
       
       
       
       }

       public void setRelatedTo(CloudTable table) {

           try {

               document.Add("relatedTo",table.document.ToString());
           
           }catch(CloudBoostException e){
               throw new CloudBoostException(e.Message);
           
           }

       
       }

       public CloudTable getRelatedTo() {

           Dictionary<string,Object> table = null;
           CloudTable tableObject= new CloudTable();
           try{

               table = (Dictionary<string, Object>)document["relatedTo"];
           
           }catch(CloudBoostException e){
           throw new CloudBoostException(e.Message);
           
           }
           tableObject.document = table;
           return tableObject;
       
       
       }

       public void setRelatedToType(string value) {


           try {

               document.Add("relatedToType",value);
           
           
           }catch(CloudBoostException e){

               throw new CloudBoostException(e.Message);
           }
       }

       public string getRelatedToType() {

           try {

               return document["relatedToType"].ToString();
           
           }
           catch(CloudBoostException e){

               throw new CloudBoostException(e.Message);
               return null;
           }
       
       
       }

       public void setRelationType(string value) {

           try {

               document.Add("relationType",value);

           }catch(CloudBoostException e){
           
           
           throw new CloudBoostException(e.Message);
           
           }
       
       }

       public string setRelationType() {

           try {

               return document["relationType"].ToString();
           
           }catch(CloudBoostException e){

               throw new CloudBoostException(e.Message);
               return null;
           
           
           }
       
       
       }

       public void setIsDeletable(bool value) {

           try {

               document.Add("isDeletable",value);
           
           }catch(CloudBoostException e){

               throw new CloudBoostException(e.Message);
           }
       
       }

      public bool getIsDeletable() {

           try {

          return  bool.Parse(document["isDeletable"].ToString());
           
           }catch(CloudBoostException e){

               throw new CloudBoostException(e.Message);
               return false;
           }
       
       
       }

      public void setIsEditable(bool value) {

           try {


               document.Add("isEditable",value);
           }catch(CloudBoostException e){

               throw new CloudBoostException(e.Message);
           }
       
       }

      public bool getIsEditable() {
           try {

               return bool.Parse(document["isEditable"].ToString());
           
           
           }catch(CloudBoostException e){

               throw new CloudBoostException(e.Message);
           
           }
       
       }

      public void setIsRenamable(bool value) {

           try {

               document.Add("isRenamable",value);
           
           }catch(CloudBoostException e){

               throw new CloudBoostException(e.Message);
           
           }
       
       }

      public bool getIsRenamable() {

           try {

               return bool.Parse(document["isRenamable"].ToString());
           
           }catch(CloudBoostException e){

               throw new CloudBoostException(e.Message);
               return false;
           
           }
       
       }


     
    }
}
