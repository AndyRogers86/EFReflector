# EFReflector

Read data via reflection from Entity Framwork

Usage:

`DataTable dt = EFReflection.ReflectData.GetData("EntityFrameworkNamespace", "DataSource", new List<string>() { "Col1", "Col2", "Col3" });`

Example:

`DataTable dt = EFReflection.ReflectData.GetData("EFReflectionDemo.Model.ReflectDemoEntities", "T_Person", new List<string>() { "ID", "Name", "DOB" });`

A demo is included within this VS 2017 solution along with a database backup file. You will need to update the app.config file with your database connection for it to work.
