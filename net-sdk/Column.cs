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
       public JObject document;

       Dictionary<string, Object> dictionaryDoc = new Dictionary<string, Object>();

       public enum DataType { 
       
       Text,Email,URL,Number,Boolean,DateTime,GeoPoint,File,List,Relation,Object,Id,EncryptedText,ACL
       
       }

       public Column(string columnName, DataType dataType){
           
       
       
      // if(){}

            
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

               this.document = new JObject();
               document = Serializer.Serialize(dictionaryDoc);

           }
           catch (CloudBoostException e)
           {

               throw new CloudBoostException(e.Message);
           }
       
       
       }

       public Column(String columnName,DataType dataType,Boolean required,Boolean unique) {

       
      // if(){}
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

               this.document = new JObject();
               document = Serializer.Serialize(dictionaryDoc);
           }
           catch (CloudBoostException e)
           {
           throw new CloudBoostException(e.Message);
           
           }
       
       }

      public string getColumnName()
       {

           try {
               

             return  this.document.GetValue("name").ToString();
           
           
           }catch(CloudBoostException e){

               throw new CloudBoostException(e.Message);
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

               string datatype = document.GetValue("dataType").ToString();
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

               document.GetValue("required");
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
            document.GetValue("unique");
            return true;
           
           }catch(CloudBoostException e){

               throw new CloudBoostException(e.Message);
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

           JObject table = null;
           CloudTable tableObject= new CloudTable();
           try{
           
           table = (JObject)document.GetValue("relatedTo");
           
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

               return document.GetValue("relatedToType").ToString();
           
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

               return document.GetValue("relationType").ToString();
           
           }catch(CloudBoostException e){

               throw new CloudBoostException(e.Message);
               return null;
           
           
           }
       
       
       }

       void setIsDeletable(bool value) {

           try {

               document.Add("isDeletable",value);
           
           }catch(CloudBoostException e){

               throw new CloudBoostException(e.Message);
           }
       
       }

       bool getIsDeletable() {

           try {

          return  bool.Parse(document.GetValue("isDeletable").ToString());
           
           }catch(CloudBoostException e){

               throw new CloudBoostException(e.Message);
               return false;
           }
       
       
       }

       void setIsEditable(bool value) {

           try {


               document.Add("isEditable",value);
           }catch(CloudBoostException e){

               throw new CloudBoostException(e.Message);
           }
       
       }

       bool getIsEditable() {
           try {

               return bool.Parse(document.GetValue("isEditable").ToString());
           
           
           }catch(CloudBoostException e){

               throw new CloudBoostException(e.Message);
           
           }
       
       }

       void setIsRenamable(bool value) {

           try {

               document.Add("isRenamable",value);
           
           }catch(CloudBoostException e){

               throw new CloudBoostException(e.Message);
           
           }
       
       }

       bool getIsRenamable() {

           try {

               return bool.Parse(document.GetValue("isRenamable").ToString());
           
           }catch(CloudBoostException e){

               throw new CloudBoostException(e.Message);
               return false;
           
           }
       
       }





     
    }
}
