# NetCoreHelpers

**NetCoreHelpers** is a lightweight utility library that provides common helper methods and extensions to simplify and enhance operations in .NET Core applications. This package is designed to improve code readability, reduce boilerplate, and offer easy-to-use methods for common tasks.

## Features

- **Null/Default Checks**: Simplify null and default value checks.
- **Dictionary Conversions**: Convert `JArray` or `JObject` to a `Dictionary<string, string>`.
- **GUID Validation**: Easily validate if a string is a valid GUID.
- **Stream Extensions**: Extensions to read streams to the end or convert objects to byte arrays.
- **DataTable Utilities**: Utilities to create `DataTable` from collections and convert to CSV format.
- **Excel Column Naming**: Generate Excel-style column names programmatically.
- **String Extensions**: A set of extensions for string manipulation and validation.

## Installation

To install NetCoreHelpers, run the following command in the NuGet Package Manager Console:

```sh
Install-Package NetCoreHelpers
```

Alternatively, you can install it via the .NET CLI:

```sh
dotnet add package NetCoreHelpers
```

## Usage

### Null/Default Checks

Check if a value is null or its default value.

```csharp
int? nullableInt = null;
bool isNullOrDefault = nullableInt.IsNullOrDefault(); // true
```

### Dictionary Conversions

Convert `JObject` or `JArray` to `Dictionary<string, string>`.

```csharp
JObject jsonObject = JObject.Parse(@"{ 'Key1': 'Value1', 'Key2': 'Value2' }");
Dictionary<string, string> dictionary = jsonObject.ToDictionary();
```

### GUID Validation

Check if a string is a valid GUID.

```csharp
string guidString = "d3b07384-d9a0-4c9b-8f8b-7f2b8b8b8b8b";
bool isValidGuid = guidString.IsValidGuid(); // true
```

### Stream Extensions

Convert a stream to a byte array.

```csharp
using (var stream = new MemoryStream())
{
    byte[] byteArray = stream.ToByteArray();
}
```

### DataTable Utilities

Convert a list to a `DataTable` and export it to CSV.

```csharp
List<MyClass> myList = new List<MyClass>();
DataTable dataTable = myList.ToDataTable();
string csv = dataTable.ToCsv();
```

### Excel Column Naming

Generate an Excel-style column name from an integer index.

```csharp
string columnName = ExcelHelper.GetColumnName(1); // "A"
```

### String Extensions

#### IsNullOrWhiteSpace

Check if a string is null, empty, or consists only of white-space characters.

```csharp
string str = " ";
bool result = str.IsNullOrWhiteSpace(); // true
```

#### IsNotNullOrWhiteSpace

Check if a string is not null, empty, or consists only of white-space characters.

```csharp
string str = "Hello";
bool result = str.IsNotNullOrWhiteSpace(); // true
```

#### IsNullOrEmpty

Check if a string is null or empty.

```csharp
string str = "";
bool result = str.IsNullOrEmpty(); // true
```

#### IsNotNullOrEmpty

Check if a string is not null or empty.

```csharp
string str = "Hello";
bool result = str.IsNotNullOrEmpty(); // true
```

#### ToEnum<T>

Convert a string to an enum of type `T`.

```csharp
string str = "Ascending";
SortOrder order = str.ToEnum<SortOrder>(); // SortOrder.Ascending
```

#### ToInt

Convert a string to an integer.

```csharp
string str = "123";
int number = str.ToInt(); // 123
```

#### ToLong

Convert a string to a long integer.

```csharp
string str = "123456789";
long number = str.ToLong(); // 123456789
```

#### ToJsonString

Convert an object to a JSON string.

```csharp
var obj = new { Name = "John", Age = 30 };
string jsonString = obj.ToJsonString(); // {"Name":"John","Age":30}
```

#### ConvertToModel

Convert a JSON string to a model of type `T`.

```csharp
string jsonString = "{\"Name\":\"John\",\"Age\":30}";
var person = jsonString.ConvertToModel<Person>(); // Person object with Name="John" and Age=30
```
```