using System;

namespace GreenerGrain.Framework.Swagger
{
    /// <summary>
    /// Swagger attribute to declare a method parameter
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class SwaggerParameterAttribute : Attribute
    {
        /// <summary>
        /// Parameter Name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Parameter data type
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// Parameter type
        /// </summary>
        public string ParameterType { get; set; }

        /// <summary>
        /// Parameter description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Indicates if parameter is required
        /// </summary>
        public bool Required { get; set; } = false;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="description">Parameter description</param>
        public SwaggerParameterAttribute(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
