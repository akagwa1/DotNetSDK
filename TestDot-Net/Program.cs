using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDot_Net
{
   public class Program
    {

       static void Main(string[] args)
       {
           try
           {

               CloudTableTest cloudTableTester = new CloudTableTest();
               CloudObjectTest cloudObjectTester = new CloudObjectTest();
               cloudObjectTester.saveArray();
               cloudObjectTester.saveArrayWithWrongDataType();
               cloudObjectTester.SaveData();
               cloudObjectTester.ShouldNotSaveDuplicateValue();
               cloudObjectTester.ShouldNotSaveWithoutRequiredColumn();
               cloudObjectTester.ShouldNotSaveWithWrongDataType();
               cloudObjectTester.shouldUpdateVersionOnUpdate();
               cloudObjectTester.updateAfterSave();
               //cloudTableTester.SequentialTests();
           }catch(Exception ex){

               throw new Exception(ex.Message);
           }




       }
    }
}
