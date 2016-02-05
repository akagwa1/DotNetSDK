using CB;
using CB.Exception;
using net_sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDot_Net
{
   public class CloudObjectTest
    {
      public void initialize()
       {
           CloudApp.init("bengi1234",
                   "3UVnLf/HANIYE+IxP2ZxHg==");


       }

       public void SaveData() {
		 initialize();
		CloudObject obj = new CloudObject("NOTIFICATION_QUERY_0");

		obj.Set("name", "samplexyz");
        obj.SaveAsync();
			
		
		
	}
       public void ShouldNotSaveStringIntoDate(){
		initialize();
		CloudObject obj = new CloudObject("NOTIFICATION_QUERY_2");

		obj.Set("createdAt", "abcd");
		obj.Set("name", "abcd");
		obj.SaveAsync();
	}
       public void ShouldNotSaveWithoutRequiredColumn(){
		initialize();
		CloudObject obj = new CloudObject("NAME_REQUIRED");

        obj.SaveAsync();
	}
       public void ShouldNotSaveWithWrongDataType(){
		initialize();
		CloudObject obj = new CloudObject("NOTIFICATION_QUERY_3");

		obj.Set("name", 10);
        obj.SaveAsync();
	}
       public void ShouldNotSaveDuplicateValue() {
		initialize();
		CloudObject obj = new CloudObject("UNIQUE_NAME");
	    String text = PrivateValidation._makeString();
		obj.Set("name", text);
		obj.SaveAsync();
				
	}
       public void shouldUpdateVersionOnUpdate() {
		initialize();
		CloudObject obj = new CloudObject("NOTIFICATION_QUERY_4");
     	obj.Set("name", "sample");
     	obj.SaveAsync();
			
	}
       public void updateAfterSave() {
		initialize();
		CloudObject obj = new CloudObject("NOTIFICATION_QUERY_5");
     	obj.Set("name", "sample");
     	obj.SaveAsync();
	}
       public void deleteAfterSave(){
		initialize();
		CloudObject obj = new CloudObject("NOTIFICATION_QUERY_6");
     	obj.Set("name", "sample");
        obj.SaveAsync();
			
	}
       public void saveArray(){
		initialize();
		CloudObject obj = new CloudObject("DATA_1");
		obj.Set("name", "sample");
		string text = PrivateValidation._makeString();
		string[] stringval = {text , text};
        obj.Set("stringArray", stringval);
		obj.SaveAsync();
	}
       public void saveArrayWithWrongDataType(){
		initialize();
		CloudObject obj = new CloudObject("DATA_1");
		int[] stringval = {23, 45};
        obj.Set("stringArray", stringval);
		obj.SaveAsync();
	}
       public void saveArrayWithJSONObject(){
		initialize();
		CloudObject obj = new CloudObject("DATA_1");
		obj.Set("name", "sample");
		Dictionary<string,Object> obj1 = new Dictionary<string,Object>();
		try {
			obj1.Add("sample", "sample");
			
		} catch (CloudBoostException e) {
			
			throw new CloudBoostException(e.Message);
		}
		Dictionary<string,Object> stringObj = new Dictionary<string,Object>();
		//stringObj.Add(obj1);
		//stringObj.Add(obj1);
        obj.Set("objectArray", stringObj);
		obj.SaveAsync();
	}

    }
}
