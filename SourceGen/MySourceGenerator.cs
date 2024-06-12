using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace SourceGen
{
    [Generator]
    public class MySourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            //var classNames = new List<string>() { "Class1", "Class2", "Class3" };

            var classNames = new PostgreDataProvider().GetSimpleData();

            foreach (var className in classNames)
            {
                var code =  @"
                     using FluentNHibernate.Mapping;

                    namespace TestSolution.Models.Mappings
                   {
                    public class " + className + @"
                    {
                    public virtual long Id { get; set; }
                    }

                    public class " + className + @"Map: ClassMap<" + className + @">
                    {
                       public " + className + @"Map()
                       {
                        this.Id(x => x.Id);            
                       }
                    }
                 }";

                context.AddSource($"{className}Map.cs", code);
            }
        }
        public void Initialize(GeneratorInitializationContext context)
        {

        }
    }
}
