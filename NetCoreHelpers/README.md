# NetCoreHelpers

**NetCoreHelpers** is a lightweight library offering a variety of helper methods and extensions to simplify common operations in .NET Core applications. These utilities aim to improve code readability and reduce boilerplate.

## Features

- **Null/Default Checks**: Easily determine if values are null or set to their default.
- **Dictionary Conversions**: Convert `JArray` or `JObject` to `Dictionary<string, string>`.
- **XML and XDocument Utilities**: Seamlessly convert between `XmlDocument` and `XDocument`.
- **GUID Validation**: Validate if a string is a valid GUID.
- **Stream Extensions**: Read streams to the end or convert objects to byte arrays.
- **DataTable Utilities**: Create `DataTable` from collections or convert it to CSV.
- **Excel Column Naming**: Generate Excel-style column names programmatically.

## Installation

To install the package, run the following command in the NuGet Package Manager Console:

```sh
Install-Package NetCoreHelpers
```



## Usage

Below are some examples of how to use the provided extension methods.

### Null/Default Checks

```csharp
int? nullableInt = null;
bool isNullOrDefault = nullableInt.IsNullOrDefault(); // true
```

### Dictionary Conversions

```csharp
JObject jsonObject = JObject.Parse(@"{ 'Key1': 'Value1', 'Key2': 'Value2' }");
Dictionary<string, string> dictionary = jsonObject.ToDictionary();
```

### XML and XDocument Utilities

```csharp
XmlDocument xmlDocument = new XmlDocument();
xmlDocument.LoadXml("<root><element>value</element></root>");
XDocument xDocument = xmlDocument.ToXDocument();
```

### GUID Validation

```csharp
string guidString = "d3b07384-d9a0-4c9b-8f8b-7f2b8b8b8b8b";
bool isValidGuid = guidString.IsValidGuid(); // true
```

### Stream Extensions

```csharp
using (var stream = new MemoryStream())
{
    byte[] byteArray = stream.ToByteArray();
}
```

### DataTable Utilities

```csharp
List<MyClass> myList = new List<MyClass>();
DataTable dataTable = myList.ToDataTable();
string csv = dataTable.ToCsv();
```

### Excel Column Naming

```csharp
string columnName = ExcelHelper.GetColumnName(1); // "A"
```

```markdown
### String Extensions

The `StringExtension` class provides several useful extension methods for strings.

#### IsNullOrWhiteSpace

Checks if a string is null, empty, or consists only of white-space characters.

```csharp
string str = " ";
bool result = str.IsNullOrWhiteSpace(); // true
```

#### IsNotNullOrWhiteSpace

Checks if a string is not null, empty, or consists only of white-space characters.

```csharp
string str = "Hello";
bool result = str.IsNotNullOrWhiteSpace(); // true
```

#### IsNullOrEmpty

Checks if a string is null or empty.

```csharp
string str = "";
bool result = str.IsNullOrEmpty(); // true
```

#### IsNotNullOrEmpty

Checks if a string is not null or empty.

```csharp
string str = "Hello";
bool result = str.IsNotNullOrEmpty(); // true
```

#### ToEnum

Converts a string to an enum of type `T`.

```csharp
string str = "Ascending";
SortOrder order = str.ToEnum<SortOrder>(); // SortOrder.Ascending
```

#### ToInt

Converts a string to an integer.

```csharp
string str = "123";
int number = str.ToInt(); // 123
```

#### ToLong

Converts a string to a long integer.

```csharp
string str = "123456789";
long number = str.ToLong(); // 123456789
```

#### ToJsonString

Converts an object to a JSON string.

```csharp
var obj = new { Name = "John", Age = 30 };
string jsonString = obj.ToJsonString(); // {"Name":"John","Age":30}
```

#### ConvertToModel

Converts a JSON string to a model of type `T`.

```csharp
string jsonString = "{\"Name\":\"John\",\"Age\":30}";
var person = jsonString.ConvertToModel<Person>(); // Person object with Name="John" and Age=30
```