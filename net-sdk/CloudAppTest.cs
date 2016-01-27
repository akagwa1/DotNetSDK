using CB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net_sdk
{
    public class CloudAppTest
    {

        static void Main(string[] args) {
            shouldCreateATable();
        
        
        }
        public static void shouldCreateATable() {
            CloudApp.init("bengi1234", "3UVnLf/HANIYE+IxP2ZxHg==");
		CloudTable table=new CloudTable("firstAppTb");
        table.save();

        }
    }
}
