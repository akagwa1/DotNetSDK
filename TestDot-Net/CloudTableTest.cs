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
        employee.save();
        CloudTable company = createCompany();
        company.save();
        CloudTable address = createAddress();
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
        


	
	public void deleteEmployeeTable() {
		initialize();

	}

	
	public void createEmployeeTable() {
		initialize();

	}

        public void createCompanyTable() {
		initialize();

	}

	
	public void createAddressTable() {
		initialize();

	}

	
	public void updateCompanySchema()  {
		initialize();

	}

    public void duplicateTable() {
        Utils.initMaster();
        
		string tableName = PrivateValidation._makeString();
		CloudTable obj = new CloudTable(tableName);
        obj.save();
    
    }
    public void createAndDeleteTable() {

        initialize();
        String tableName = PrivateValidation._makeString();
        CloudTable obj = new CloudTable(tableName);
        obj.save();
        obj.delete();

    
    }
    public void getTable() {

        initialize();
        String name = PrivateValidation._makeString();
        CloudTable obj = new CloudTable(name);
        obj.save();
        CloudTable.get(name);
       

    
    }
    public void shouldGetAllTable() {
        initialize();
        CloudTable.getAll();
    
    
    }
    public void GetAllTable() { 
    
        initialize();
        CloudTable.getAll();

    }
    public void updateNewColumnIntoTable() { 
    
    initialize();
	String tableName1 = PrivateValidation._makeString();
    String tableName2 = PrivateValidation._makeString();
	CloudTable obj = new CloudTable(tableName1);
    CloudTable obj1 = new CloudTable(tableName2);
    obj.save();
    CloudTable.get(obj);
    CloudTable getTable = new CloudTable();
        Column column1 = new Column("Name",net_sdk.Column.DataType.Relation, true, false);
								column1.setRelatedTo(getTable);
								getTable.addColumn(column1);
                                getTable.save();
                                CloudTable newTable = new CloudTable();
                                Column column2 = new Column("Name",
                                                                        net_sdk.Column.DataType.Relation, true, false);
                                newTable.deleteColumn(column2);
                                newTable.save();

    
    }
    public void shouldCreateAndDeleteTable(){
    initialize();
   String tableName = PrivateValidation._makeString();
   CloudTable obj = new CloudTable(tableName);
   CloudTable table = new CloudTable();
   table.delete();

        
        }
    public void addColumnAfterSave() { 
    
    initialize();
		String tableName = PrivateValidation._makeString();
		CloudTable table = new CloudTable(tableName);
        table.save();
        CloudTable newTable = new CloudTable();
        Column column1 = new Column("Name1", net_sdk.Column.DataType.Text, false,
							false);
					newTable.addColumn(column1);
                    newTable.save();

    
    }
    public void setTableType() { 
    
    initialize();
		CloudTable table = new CloudTable(PrivateValidation._makeString());
        table.save();
        CloudTable newTable = new CloudTable();
        try
        {
            newTable.document.Add("_type", "newType");
        }
        catch (CloudBoostException e1)
        {
            throw new CloudBoostException(e1.Message);
        }
        newTable.save();
    
    }
    public void addColumnToExistingTable() { 
    
    initialize();
		CloudTable table = new CloudTable(PrivateValidation._makeString());
        table.save();
       String name = PrivateValidation._makeString();
        CloudTable newTable = new CloudTable();
						Column col = new Column(name, net_sdk.Column.DataType.Text, false, false);
						newTable.addColumn(col);
						newTable.save();
    }
    public void renameTableFails() { 
    
        //net_sdk.Util.initKisenyiMaster();
        //CloudTable table = new CloudTable(PrivateMethod._makeString());
        //table.save();
    
    }
    public void createListTable() { 
    
    initialize();
	    String name = PrivateValidation._makeString();
		CloudTable obj = new CloudTable(name);
		Column subject = new Column("subject", net_sdk.Column.DataType.List, false, false);
		subject.setRelatedTo(net_sdk.Column.DataType.Text);
		Column age = new Column("age", net_sdk.Column.DataType.Number, false, false);
		obj.addColumn(subject);
		obj.addColumn(age);
        obj.save();
        }

    }
}
