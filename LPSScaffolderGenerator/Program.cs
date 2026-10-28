
using Microsoft.Data.SqlClient;
using System.Text;

const string connectionString =
    @"Server=localhost\SQLEXPRESS01;Database=LPSBookTest;Trusted_Connection=True;TrustServerCertificate=True;";

var tables = new[]
{
    (TableName: "JenisBuku", ClassName: "MasterJenisBuku"),
    (TableName: "Buku", ClassName: "BukuModel"),
    (TableName: "BukuHistory", ClassName: "BukuHistoryModel")
};

// Simpan file GeneratedModels di root project generator.
string outputDirectory = Path.GetFullPath(
    Path.Combine(
        AppContext.BaseDirectory,
        "..", "..", "..",
        "GeneratedModels"));

Directory.CreateDirectory(outputDirectory);

await using var connection =
    new SqlConnection(connectionString);

await connection.OpenAsync();

foreach (var table in tables)
{
    var columns = new List<ColumnInfo>();

    await using (var command = connection.CreateCommand())
    {
        command.CommandText = @"
            SELECT
                c.name AS ColumnName,
                ty.name AS SqlType,
                c.is_nullable AS IsNullable
            FROM sys.tables t
            INNER JOIN sys.columns c
                ON t.object_id = c.object_id
            INNER JOIN sys.types ty
                ON c.user_type_id = ty.user_type_id
            WHERE t.name = @TableName
              AND SCHEMA_NAME(t.schema_id) = 'dbo'
            ORDER BY c.column_id;";

        command.Parameters.AddWithValue(
            "@TableName", table.TableName);

        await using var reader =
            await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            columns.Add(new ColumnInfo(
                reader.GetString(0),
                reader.GetString(1),
                reader.GetBoolean(2)));
        }
    }

    if (columns.Count == 0)
    {
        Console.WriteLine(
            $"Tabel {table.TableName} tidak ditemukan.");

        continue;
    }

    var code = new StringBuilder();

    code.AppendLine("#nullable enable");
    code.AppendLine("using System;");
    code.AppendLine();
    code.AppendLine("namespace Models");
    code.AppendLine("{");
    code.AppendLine($"    public class {table.ClassName}");
    code.AppendLine("    {");

    foreach (var column in columns)
    {
        string typeName = MapSqlType(column.SqlType);

        if (column.IsNullable)
        {
            typeName += "?";
        }

        code.Append(
            $"        public {typeName} {column.ColumnName} "
            + "{ get; set; }");

        if (!column.IsNullable && typeName == "string")
        {
            code.Append(" = string.Empty;");
        }
        else if (!column.IsNullable && typeName == "byte[]")
        {
            code.Append(" = Array.Empty<byte>();");
        }

        code.AppendLine();
    }

    code.AppendLine("    }");
    code.AppendLine("}");

    string filePath = Path.Combine(
        outputDirectory,
        $"{table.ClassName}.cs");

    await File.WriteAllTextAsync(
        filePath,
        code.ToString(),
        new UTF8Encoding(false));

    Console.WriteLine(
        $"Berhasil menghasilkan: {filePath}");
}

Console.WriteLine("\nModel Generator selesai.");

static string MapSqlType(string sqlType)
{
    return sqlType.ToLowerInvariant() switch
    {
        "int" => "int",
        "bigint" => "long",
        "smallint" => "short",
        "tinyint" => "byte",
        "bit" => "bool",

        "varchar" or "nvarchar" or
        "char" or "nchar" or
        "text" or "ntext" or "xml" => "string",

        "datetime" or "datetime2" or
        "smalldatetime" or "date" => "DateTime",

        "time" => "TimeSpan",
        "datetimeoffset" => "DateTimeOffset",

        "decimal" or "numeric" or
        "money" or "smallmoney" => "decimal",

        "float" => "double",
        "real" => "float",
        "uniqueidentifier" => "Guid",

        "binary" or "varbinary" or
        "image" or "timestamp" or
        "rowversion" => "byte[]",

        _ => throw new NotSupportedException(
            $"Tipe SQL belum didukung: {sqlType}")
    };
}

record ColumnInfo(
    string ColumnName,
    string SqlType,
    bool IsNullable);
