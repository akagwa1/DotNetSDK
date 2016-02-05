using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using net_sdk;
using net_sdk.Util;
using CB.Exception;

namespace TestDot_Net
{
    public class CloudTableTest

    {
    private static string COMPANY = PrivateValidation._makeString();
	private static string EMPLOYEE = PrivateValidation._makeString();
    private static string ADDRESS = PrivateValidation._makeString();
    public void initialize()
    {
        try
        {
            Utils.initMaster();
        }catch(Exception e){
            throw new Exception(e.Message);
        }
      
    }

    public void SequentialTests() { 
    
    
        initialize();

		CloudTable employee = createEmployee();
        Console.WriteLine(employee.document.Keys.ToString());
        employee.save();
        CloudTable company = createCompany();
        company.save();
        CloudTable address = createAddress();
        //save
        address.save();
    
    }
        public CloudTable createEmployee()
    {
        Column age = new Column("age", net_sdk.Column.DataType.Number, false, false);
        Column name = new Column("name", net_sdk.Column.DataType.Text, false, false);
        Column dob = new Column("dob", net_sdk.Column.DataType.DateTime, false, false);
        Column password = new Column("password", net_sdk.Column.DataType.EncryptedText, false,
                false);
        CloudTable table = new CloudTable(EMPLOYEE);
        try
        {
            table.addColumn(new Column[] { age, name, dob, password });
        }
        catch (CloudBoostException e)
        {

           throw new CloudBoostException(e.Message);
        }
        return table;

    }

    public CloudTable createCompany()
    {
        Column revenue = new Column("Revenue", net_sdk.Column.DataType.Number, false, false);
        Column name = new Column("Name", net_sdk.Column.DataType.Text, false, false);
        Column file = new Column("File", net_sdk.Column.DataType.File, false, false);
        CloudTable table = new CloudTable(COMPANY);
        try
        {
            table.addColumn(new Column[] { revenue, name, file });
        }
        catch (CloudBoostException e)
        {

            throw new CloudBoostException(e.Message);
        }
        return table;

    }

    public CloudTable createAddress()
    {
        Column city = new Column("City", net_sdk.Column.DataType.Text, false, false);
        Column pincode = new Column("PinCode", net_sdk.Column.DataType.Number, false, false);
        CloudTable table = new CloudTable(ADDRESS);
        try
        {
            table.addColumn(new Column[] { city, pincode });
        }
        catch (CloudBoostException e)
        {

            throw new CloudBoostException(e.Message);
        }
        return table;

    }
        public void updateEmployeeSchema() {
		CloudTable table = new CloudTable(EMPLOYEE);
		try {
			
					Column company = new Column(COMPANY, net_sdk.Column.DataType.Relation,
							false, false);
					company.setRelatedTo(new CloudTable(COMPANY));
					Column address = new Column(ADDRESS, net_sdk.Column.DataType.Relation,
							false, false);
					address.setRelatedTo(new CloudTable(ADDRESS));
					table.addColumn(new Column[] { company, address });
					table.save();

				
		
		} catch (CloudBoostException e) {
			
			throw new CloudBoostException(e.Message);
		}
	}


	public void deleteAddressTable(){
		initialize();

	}

    }
}
