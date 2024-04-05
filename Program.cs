using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
namespace Srikanth
{
	public class Program
	{
		private static int CompareEmployee(Employee x, Employee y)
		{
			if (x.Id < y.Id) return 1;
			else if (x.Id > y.Id) return -1;
			else return 0;
		}
		public static void Main()
		{
			string filePath = @"C:\Users\Falcon\source\repos\EmployeeCSVApp\data.csv";
			// Reading initial data from csv
			List<Employee> employees = new List<Employee>();
			string[] lines = File.ReadAllLines(filePath);
			for(int i = 1; i < lines.Length; i++)
			{
				string[] values = lines[i].Split(',');
				employees.Add(new Employee(int.Parse(values[0]), values[1], values[2], values[3], int.Parse(values[4])));
			}
			string userResponse;
			do
			{
				Console.WriteLine("*-*-*-*-*-MENU-*-*-*-*-*");
				Console.WriteLine("1. Add Employee");
				Console.WriteLine("2. Edit Employee");
				Console.WriteLine("3. Delete Employee");
				Console.WriteLine("4. View Employee");
				Console.WriteLine("5. View All");
				Console.WriteLine("6. Sort Employees");
				Console.WriteLine("7. Exit");
				Console.Write("Enter your choice: ");
				string userInput;
				int Choice = 0;
				bool isValidResponse;
				do
				{
					userInput = Console.ReadLine();
					isValidResponse = int.TryParse(userInput, out Choice);
					if (!isValidResponse || Choice < 1 || Choice > 7)
					{
						Console.WriteLine("Please enter a valid number between 1 to 7");
						Console.Write("Enter your choice: ");
					}
				} while (!isValidResponse || Choice < 1 || Choice > 7);
				switch (Choice)
				{
					case 1:
						Console.WriteLine("Add Employee Details");
						int empId;
						string userInputEmpId;
						bool isValidEmpId, isEmpIdExists = true;
						Console.Write("Enter Employee ID: ");
						do
						{
							userInputEmpId = Console.ReadLine();
							isValidEmpId = int.TryParse(userInputEmpId, out empId);
							if (isValidEmpId)
							{
								isEmpIdExists = employees.Exists(x => x.Id == empId);
								if (isEmpIdExists)
								{
									Console.Write("This Employee Id already exist, Please enter another ID: ");
								}
								else
								{
									break;
								}
							}
							else
							{
								Console.WriteLine("Please enter a valid integer");
								Console.Write("Re-Enter Employee ID: ");
							}
						} while (isValidEmpId || isEmpIdExists);
						Console.Write("Enter Employee Name: ");
						string empName = Console.ReadLine();
						Console.Write("Enter Employee Role: ");
						string empRole = Console.ReadLine();
						Console.Write("Enter Employee Department: ");
						string empDept = Console.ReadLine();
						Console.Write("Enter Employee Age: ");
						int empAge;
						string userInputEmpAge;
						bool isValidEmpAge;
						do
						{
							userInputEmpAge = Console.ReadLine();
							isValidEmpAge = int.TryParse(userInputEmpAge, out empAge);
							if (!isValidEmpAge || empAge < 18 || empAge > 60)
							{
								Console.WriteLine("Please enter a valid integer and Employee age should be greater than 18 and less than 60");
								Console.Write("Re-Enter Employee Age: ");
							}
						} while (!isValidEmpAge || empAge < 18 || empAge > 60);
						Employee newEmp = new Employee(empId, empName, empRole, empDept, empAge);
						employees.Add(newEmp);
						using(StreamWriter sw = File.AppendText(filePath))
						{
							sw.WriteLine(newEmp.ToString());
							sw.Close();
						}
						Console.WriteLine("Employee Added Successfully");
						break;
					case 2:
						string[] linesCase2 = File.ReadAllLines(filePath);
						if(linesCase2.Length == 1)
						{
							Console.WriteLine("There are no employees to Edit, Please add new Employee");
							break;
						}
						Console.Write("Enter Employee ID to Edit: ");
						int editEmpId;
						string userInputEditEmpId;
						bool isValidEditEmpId, isEditEmpIdExists = false;
						do
						{
							userInputEditEmpId = Console.ReadLine();
							isValidEditEmpId = int.TryParse(userInputEditEmpId, out editEmpId);
							if (isValidEditEmpId)
							{
								isEditEmpIdExists = employees.Exists(x => x.Id == editEmpId);
								if (isEditEmpIdExists)
								{
									Console.WriteLine("Employee Found!!");
									break;
								}
								else
								{
									Console.Write("Employee Id Not Found, Please Re-Enter: ");
								}
							}
							else
							{
								Console.WriteLine("Please enter a valid integer");
								Console.Write("Re-Enter Employee ID: ");
							}
						} while (!isValidEditEmpId || !isEditEmpIdExists);
						int editUserChoice;
						bool isValidEditUserChoice;
						Console.WriteLine("1. Name");
						Console.WriteLine("2. Role");
						Console.WriteLine("3. Department");
						Console.WriteLine("4. Age");
						Console.Write("Enter your choice to edit: ");
						do
						{
							isValidEditUserChoice = int.TryParse(Console.ReadLine(), out editUserChoice);
							if (!isValidEditUserChoice || editUserChoice > 4 || editUserChoice < 1)
							{
								Console.WriteLine("Invalid Choice, Please enter a integer between 1 to 4");
								Console.Write("Please Re-Enter you choice: ");
							}
						} while (!isValidEditUserChoice || editUserChoice > 4 || editUserChoice < 1);
						switch (editUserChoice)
						{
							case 1:
								Console.Write("Enter new Employee Name: ");
								string newEmpName = Console.ReadLine();
								Employee editNameEmp = employees.Find(x => x.Id == editEmpId);
								editNameEmp.Name = newEmpName;
								string[] linesName = File.ReadAllLines(filePath);
								for(int i = 1; i < linesName.Length; i++)
								{
									if (linesName[i].Split(',')[0] == editEmpId.ToString())
									{
										linesName[i] = editNameEmp.ToString();
										break;
									}
								}
								File.WriteAllLines(filePath, linesName);
								Console.WriteLine("Employee Name updated Successfully");
								break;
							case 2:
								Console.Write("Enter new Employee Role: ");
								string newEmpRole = Console.ReadLine();
								Employee editRoleEmp = employees.Find(x => x.Id == editEmpId);
								editRoleEmp.Role = newEmpRole;
								string[] linesRole = File.ReadAllLines(filePath);
								for (int i = 1; i < linesRole.Length; i++)
								{
									if (linesRole[i].Split(',')[0] == editEmpId.ToString())
									{
										linesRole[i] = editRoleEmp.ToString();
										break;
									}
								}
								File.WriteAllLines(filePath, linesRole);
								Console.WriteLine("Employee Role updated Successfully");
								break;
							case 3:
								Console.Write("Enter new Employee Department: ");
								string newEmpDept = Console.ReadLine();
								Employee editDeptEmp = employees.Find(x => x.Id == editEmpId);
								editDeptEmp.Department = newEmpDept;
								string[] linesDept = File.ReadAllLines(filePath);
								for (int i = 1; i < linesDept.Length; i++)
								{
									if (linesDept[i].Split(',')[0] == editEmpId.ToString())
									{
										linesDept[i] = editDeptEmp.ToString();
										break;
									}
								}
								File.WriteAllLines(filePath, linesDept);
								Console.WriteLine("Employee Department updated Successfully");
								break;
							case 4:
								Console.Write("Enter new Employee Age: ");
								int newEditEmpAge;
								string userInputEditEmpAge;
								bool isValidEditEmpAge;
								do
								{
									userInputEditEmpAge = Console.ReadLine();
									isValidEditEmpAge = int.TryParse(userInputEditEmpAge, out newEditEmpAge);
									if (!isValidEditEmpAge || newEditEmpAge < 18 || newEditEmpAge > 60)
									{
										Console.WriteLine("Please enter a valid integer and Employee age should be greater than 18 and less than 60");
										Console.Write("Re-Enter Employee Age: ");
									}
								} while (!isValidEditEmpAge || newEditEmpAge < 18 || newEditEmpAge > 60);
								Employee editAgeEmp = employees.Find(x => x.Id == editEmpId);
								editAgeEmp.Age = newEditEmpAge;
								string[] linesAge = File.ReadAllLines(filePath);
								for (int i = 1; i < linesAge.Length; i++)
								{
									if (linesAge[i].Split(',')[0] == editEmpId.ToString())
									{
										linesAge[i] = editAgeEmp.ToString();
										break;
									}
								}
								File.WriteAllLines(filePath, linesAge);
								Console.WriteLine("Employee Age updated Successfully");
								break;
						}
						break;
					case 3:
						string[] linesDelete = File.ReadAllLines(filePath);
						if(linesDelete.Length == 1)
						{
							Console.WriteLine("There are no employees to Delete!!, Please add new employees");
							break;
						}
						Console.Write("Enter Employee ID to Delete: ");
						int deleteEmpId;
						string userInputDeleteEmpId;
						bool isValidDeleteEmpId, isDeleteEmpIdExists = false;
						do
						{
							userInputDeleteEmpId = Console.ReadLine();
							isValidDeleteEmpId = int.TryParse(userInputDeleteEmpId, out deleteEmpId);
							if (isValidDeleteEmpId)
							{
								isDeleteEmpIdExists = employees.Exists(x => x.Id == deleteEmpId);
								if (isDeleteEmpIdExists)
								{
									Console.WriteLine("Employee Found!!");
									break;
								}
								else
								{
									Console.Write("Employee Id Not Found, Please Re-Enter: ");
								}
							}
							else
							{
								Console.WriteLine("Please enter a valid integer");
								Console.Write("Re-Enter Employee ID: ");
							}
						} while (!isValidDeleteEmpId || !isDeleteEmpIdExists);
						Employee empDelete = employees.Find(x => x.Id == deleteEmpId);
						bool isDeleted = employees.Remove(empDelete);
						if (isDeleted)
						{
							using (StreamWriter sw = new StreamWriter(filePath))
							{
								sw.WriteLine("Id,Name,Role,Department,Age");
								foreach (Employee e in employees)
								{
									sw.WriteLine(e.ToString());
								}
								sw.Close();
							}
							Console.WriteLine("Employee Successfully Deleted");
						}
						else
						{
							Console.WriteLine("Some issue in deleting the Employee");
						}
						break;
					case 4:
						string[] linesCase4 = File.ReadAllLines(filePath);
						if(linesCase4.Length == 1)
						{
							Console.WriteLine("There are no Employees to Print!!");
							break;
						}
						Console.Write("Enter Employee Id to View: ");
						int viewEmpId;
						string userInputViewEmpId;
						bool isValidViewEmpId, isViewEmpIdExists = false;
						do
						{
							userInputViewEmpId = Console.ReadLine();
							isValidViewEmpId = int.TryParse(userInputViewEmpId, out viewEmpId);
							if (isValidViewEmpId)
							{
								isViewEmpIdExists = employees.Exists(x => x.Id == viewEmpId);
								if (isViewEmpIdExists)
								{
									Console.WriteLine("Employee Found!!");
									break;
								}
								else
								{
									Console.Write("Employee Id Not Found, Please Re-Enter: ");
								}
							}
							else
							{
								Console.WriteLine("Please enter a valid integer");
								Console.Write("Re-Enter Employee ID: ");
							}
						} while (!isValidViewEmpId || !isViewEmpIdExists);
						using(StreamReader sr = new StreamReader(filePath))
						{
							string firstLine = sr.ReadLine();
							string line;
							while((line = sr.ReadLine()) != null)
							{
								string[] values = line.Split(',');
								if (values[0] == viewEmpId.ToString())
								{
									Console.WriteLine(line);
								}
							}
							sr.Close();
						}
						break;
					case 5:
						string[] linesCase5 = File.ReadAllLines(filePath);
						if(linesCase5.Length == 1)
						{
							Console.WriteLine("There are no Employees to Print!!");
							break;
						}
						Console.WriteLine("Printing all employees ...");
						using (StreamReader sr = new StreamReader(filePath))
						{
							string firstLine = sr.ReadLine();
							string line;
							while ((line = sr.ReadLine()) != null)
							{
								Console.WriteLine(line);
							}
							sr.Close();
						}
						break;
					case 6:
						string[] linesCase6 = File.ReadAllLines(filePath);
						if(linesCase6.Length == 1)
						{
							Console.WriteLine("There are no Employees to Sort, Please add new Employees");
							break;
						}
						Console.WriteLine("Enter \"A\" for ascending sort and \"D\" for descending");
						Console.Write("Enter your sort choice: ");
						bool isValidSortChoice = false;
						string sortChoice;
						do
						{
							sortChoice = Console.ReadLine().ToUpper();
							isValidSortChoice = sortChoice.Equals("A") || sortChoice.Equals("D");
							if (!isValidSortChoice)
							{
								Console.WriteLine("Please enter valid sort choice \"A\" or \"D\"");
								Console.Write("Re-Enter your sort choice: ");
							}
							else
							{
								break;
							}
						} while (!isValidSortChoice || sortChoice != "A" || sortChoice != "D");
						if (sortChoice == "A")
						{
							Console.WriteLine("You have chosen ascending sort");
							employees.Sort();
							using(StreamWriter sw = new StreamWriter(filePath))
							{
								sw.WriteLine("Id,Name,Role,Department,Age");
								foreach(Employee e in employees)
								{
									sw.WriteLine(e.ToString());
								}
								sw.Close();
							}
							Console.WriteLine("Ascending Sort Successful");
						}
						else
						{
							Console.WriteLine("You have chosen descending sort");
							Comparison<Employee> employeeComparer = new Comparison<Employee>(CompareEmployee);
							employees.Sort(employeeComparer);
							using (StreamWriter sw = new StreamWriter(filePath))
							{
								sw.WriteLine("Id,Name,Role,Department,Age");
								foreach (Employee e in employees)
								{
									sw.WriteLine(e.ToString());
								}
								sw.Close();
							}
							Console.WriteLine("Descending Sort Successful");
						}
						break;
					case 7:
						Environment.Exit(0);
						break;
					default:
						break;
				}
				do
				{
					Console.Write("Do you wish to continue - YES (y) or NO (n): ");
					userResponse = Console.ReadLine().ToUpper();
				} while (userResponse != "YES" && userResponse != "NO" && userResponse != "Y" && userResponse != "N");
			} while (userResponse == "YES" || userResponse == "Y");
		}
	}
	public class Employee : IComparable<Employee>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Role { get; set; }
		public string Department { get; set; }
		public int Age { get; set; }
		public Employee(int id, string name, string role, string department, int age)
		{
			this.Id = id;
			this.Name = name;
			this.Role = role;
			this.Department = department;
			this.Age = age;
		}
		public override string ToString()
		{
			return this.Id + "," + this.Name + "," + this.Role + "," + this.Department + "," + this.Age;
		}
		public int CompareTo(Employee obj)
		{
			if (this.Id > obj.Id) return 1;
			else if (this.Id < obj.Id) return -1;
			else return 0;
		}
	}
}