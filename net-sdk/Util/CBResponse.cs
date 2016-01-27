using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net_sdk.Util
{
   public class CBResponse
    {

      private string responseBody = null;
      private string statusMessage;
      private int statusCode;
      private string sessionId;
      private string error = null;


      public string getError()
      {

      
              return error;
        

      }


       public void setError(string error){
       
       
       this.error = error;
       
       }
      public string getSessionId() {


              return sessionId;
        
      
      }
      public void setSessionId(string sessionId) {

          this.sessionId = sessionId;
      
      }

      public string toString() {
          string rtn = "test";//"[Response-Body:" + getResponseBody() +",Status-Code:" + getStatusCode().ToString() + ",Status-Message:" + getStatusMessage() + ",Session-ID:" + getSessionId + "]";

          return rtn;
      
      }
      public CBResponse(string responseBody, string statusMessage,int statusCode,string sessionId) {

          this.responseBody = responseBody;
          this.statusMessage = statusMessage;
          this.statusCode = statusCode;
          this.sessionId = sessionId;
          
      }
      public string getResponseBody() {
          return responseBody;
      }

      public void setResponseBody(string responseBody) {

          this.responseBody = responseBody; 
      }
      public string getStatusMessage() {

          return statusMessage;
      
      }
      public void setStatusMessage(string statusMessage) {
          this.statusMessage = statusMessage;
      }
      public int getStatusCode() {

          return statusCode;
      }
      public void setStatusCode(int statusCode) {

          this.statusCode = statusCode;
      }

    }
}
