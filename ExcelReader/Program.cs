using ExcelReader;
using OfficeOpenXml;


ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

var file = new FileInfo("EmployeeSampleData.xlsx");

Console.WriteLine("Reading the Excel file ...");
List<EmployeeModel> employeeList = await ExcelReaderService.LoadExcelFile(file);
Thread.Sleep(1000);

using (var context = new ExcelReaderContext())
{
	context.Database.EnsureDeleted();
	Thread.Sleep(500);
	Console.WriteLine("Creating database ...");
	context.Database.EnsureCreated();
	Thread.Sleep(500);

	Console.WriteLine("Populating database with the Excel data ...");
	foreach (EmployeeModel employee in employeeList)
	{
		ExcelReaderService.AddData(employee);
	}
	Thread.Sleep(2000);
}
Console.Clear();
ExcelReaderService.PrintTable();