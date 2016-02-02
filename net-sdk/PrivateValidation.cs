using CB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace net_sdk
{
   public class PrivateValidation
    {

       public static ArrayList _defaultColumns(string type)
       {
           ArrayList col = new ArrayList();
           Column id = new Column("id", Column.DataType.Id, true, true);
           id.setIsRenamable(false);
           id.setIsDeletable(false);
           id.setIsEditable(false);

           Column expires = new Column("expires", Column.DataType.DateTime, false, false);
           expires.setIsDeletable(false);
           expires.setIsEditable(false);

           Column createdAt = new Column("createdAt", Column.DataType.DateTime, true, false);
           createdAt.setIsDeletable(false);
           createdAt.setIsEditable(false);

           Column updatedAt = new Column("updatedAt", Column.DataType.DateTime, true, false);
           updatedAt.setIsDeletable(false);
           updatedAt.setIsEditable(false);

           Column acl = new Column("ACL", Column.DataType.ACL, true, false);
           acl.setIsDeletable(false);
           acl.setIsEditable(false);

           col.Add(id.document);
           col.Add(expires.document);
           col.Add(createdAt.document);
           col.Add(updatedAt.document);
           col.Add(acl.document);

           if (type == "custom")
           {
               return col;
           }
           else if (type == "user")
           {
               Column username = new Column("username", Column.DataType.Text, true, true);
               username.setIsDeletable(false);
               username.setIsEditable(false);

               Column email = new Column("email", Column.DataType.Email, false, true);
               email.setIsDeletable(false);
               email.setIsEditable(false);

               Column password = new Column("password", Column.DataType.EncryptedText, true, false);
               password.setIsDeletable(false);
               password.setIsEditable(false);

               Column roles = new Column("roles", Column.DataType.List, false, false);
               CloudTable role = new CloudTable("Role");
               roles.setRelatedTo(role);
               roles.setRelatedToType("role");
               roles.setRelationType("table");
               roles.setIsDeletable(false);
               roles.setIsEditable(false);
               col.Add(username.document);
               col.Add(roles.document);
               col.Add(password.document);
               col.Add(email.document);
               return col;
           }
           else if (type == "role")
           {
               Column name = new Column("name", Column.DataType.Text, true, true);
               name.setIsDeletable(false);
               name.setIsEditable(false);
               col.Add(name.document);
               return col;
           }
           return col;
       }
       public static bool _tableValidation(string tableName)
       {
           char c = tableName.ElementAt(0);
           bool isDigit = (c >= '0' && c <= '9');
           if (isDigit)
           {
               return false;
           }

           if (tableName.Contains("^S+$"))
           {
               return false;
           }


           Regex pattern = new Regex("[~`!#$%\\^&*+=\\-\\[\\]\\';,/{}|\":<>\\?]");
           MatchCollection matches = pattern.Matches(tableName);
           if (matches.Count > 0)
           {
               return false;
           }
           
               return true;
           
       }
       public static bool isValidURL(string pUrl)
       {

           Uri u = null;
           try
           {
               u = new Uri(pUrl);
           }
           catch(UriFormatException e)
           {
               return false;
           }
           try
           {
               if (!u.IsWellFormedOriginalString()) {
                   return false;
               }
           }
           catch (UriFormatException e)
           {
               return false;
           }
           return true;
       }
       public static bool _columnNameValidation(String columnName)
       {
           char c = columnName.ElementAt(0);
           bool isDigit = (c >= '0' && c <= '9');
           if (isDigit)
           {
               return false;
           }

           if (columnName.Contains("^S+$"))
           {
               return false;
           }

           Regex pattern = new Regex("[~`!#$%\\^&*+=\\-\\[\\]\\';,/{}|\":<>\\?]");
           MatchCollection matches = pattern.Matches(columnName);
           if (matches.Count > 0)
           {
               return false;
           }
           
               return true;
           
       }
       public static bool _columnValidation(Column column, CloudTable table)
       {
           List<String> defaultColumns = new List<String>();
           defaultColumns.Add("id");
           defaultColumns.Add("createdAt");
           defaultColumns.Add("updatedAt");
           defaultColumns.Add("ACL");
           if (table.getTableType().ToLower() == "user")
           {
               defaultColumns.Add("username");
               defaultColumns.Add("email");
               defaultColumns.Add("password");
               defaultColumns.Add("roles");
           }
           else if (table.getTableType().ToLower() == "role")
           {
               defaultColumns.Add("name");
           }

           String colName = column.getColumnName().ToLower();
           int index = defaultColumns.IndexOf(colName);

           if (index > -1)
           {
               return false;
           }

           return true;
       }
       public static void _isModified(CloudObject obj, string columnName){
	
		List<Object> modifiedColumns = new List<Object>();
        List<Object> col = new List<Object>();
		try{
		col = obj.dictionary.Select(_modifiedColumns => _modifiedColumns.Value).ToList();
            
       
		} catch(IndexOutOfRangeException e2) {
			
			throw e2;
		}
		for(int i=0;i < col.Count;i++){
			try {
				modifiedColumns.Add( col.ElementAt(i));
			} catch (IndexOutOfRangeException e) {
				
				throw new IndexOutOfRangeException(e.Message);
			}catch(Exception e){

                throw new Exception(e.Message);
            }
		}
		try {
			obj.dictionary.Add("_isModified", true);
		} catch (Exception e1) {

            throw new Exception(e1.Message); ;
		}
		
		if(modifiedColumns.Contains(columnName)){
			modifiedColumns.Clear();
			modifiedColumns.Add(columnName);
		}else{
			modifiedColumns.Add(columnName);
		}
		try {
			obj.dictionary.Add("_modifiedColumns", modifiedColumns);
		} catch (IndexOutOfRangeException e) {
			
			throw new IndexOutOfRangeException(e.Message);
		}
	}
       public static String _makeString()
       {
           char[] chars = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
           StringBuilder sb = new StringBuilder();
           Random random = new Random();
           for (int i = 0; i < 8; i++)
           {
               char c = chars[random.Next(chars.Length)];
               sb.Append(c);
           }
           return sb.ToString();
       }
       public static String _getSessionId()
       {
           String session = CloudApp.SESSION_ID;
           return session;
       }
       public static void _setSessionId(String session)
       {
           CloudApp.SESSION_ID = session;
       }
       public static void _deleteSessionId()
       {
           CloudApp.SESSION_ID = null;
       }
       static List<String> _toStringArray(List<string> obj1)
       {
           List<String> obj = new List<String>();
           for (int i = 0; i < obj1.Count; i++)
           {
               try
               {
                   obj.Add(obj1.ElementAt(i));
               }
               catch (IndexOutOfRangeException e)
               {

                   throw new IndexOutOfRangeException(e.Message);
               }catch(Exception e){

                   throw new Exception(e.Message);
               }
           }
           return obj;
       }
       static List<Object> _toObjectArray(List<Object> obj1)
       {
           List<Object> obj = new List<Object>();
           for (int i = 0; i < obj1.Count; i++)
           {
               try
               {
                   obj.Add(obj1.ElementAt(i));
               }
               catch (IndexOutOfRangeException e)
               {

                   throw new IndexOutOfRangeException(e.Message);
               }catch(Exception e){
                   throw new Exception(e.Message);
               }
           }
           return obj;
       }

    }
}
