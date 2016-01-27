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
		CloudApp.init("bengi123", "MjFWX9D3JqTa76tcEHt9GL2ITB8Gzsp68S1+3oq7CBE=");
		CloudTable table=new CloudTable("firstAppTb");
        table.save();

        }
    }
}
