// File provided for Reference Use Only by Microsoft Corporation (c) 2007.
// Copyright (c) Microsoft Corporation. All rights reserved.
using System; 
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq; 
using System.Data.Common;
using System.Linq.Expressions; 
using System.IO; 
using System.Linq;
using System.Text; 
using System.Transactions;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;
 
namespace System.Data.Linq.Provider {
 
    /// 
 
    /// A data provider implements this interface to hook into the LINQ to SQL framework.
    /// 
 
    internal interface IProvider : IDisposable {
        /// 

        /// Initializes the database provider with the data services object and connection.
        /// 
 
        /// 
        /// A connection string, connection object or transaction object 
        /// used to seed the provider with database connection information. 
        void Initialize(IDataServices dataServices, object connection);
 
        /// 

        /// The text writer used by the provider to output information such as query and commands
        /// being executed.
        /// 
 
        TextWriter Log { get; set; }
 
        /// 
 
        /// The connection object used by the provider when executing queries and commands.
        /// 
 
        DbConnection Connection { get; }

        /// 

        /// The transaction object used by the provider when executing queries and commands. 
        /// 

        DbTransaction Transaction { get; set; } 
 
        /// 

        /// The command timeout setting to use for command execution. 
        /// 

        int CommandTimeout { get; set; }

        /// 
 
        /// Clears the connection of any current activity.
        /// 
 
        void ClearConnection(); 

        /// 
 
        /// Creates a new database instance (catalog or file) at the location specified by the connection
        /// using the metadata encoded within the entities or mapping file.
        /// 

        void CreateDatabase(); 

        /// 
 
        /// Deletes the database instance at the location specified by the connection. 
        /// 

        void DeleteDatabase(); 

        /// 

        /// Returns true if the database specified by the connection object exists.
        /// 
 
        /// 
        bool DatabaseExists(); 
 
        /// 

        /// Executes the query specified as a LINQ expression tree. 
        /// 

        /// 
        /// A result object from which you can obtain the return value and output parameters.
        IExecuteResult Execute(Expression query); 

        /// 
 
        /// Compiles the query specified as a LINQ expression tree. 
        /// 

        ///  
        /// A compiled query instance.
        ICompiledQuery Compile(Expression query);

        /// 
 
        /// Translates a DbDataReader into a sequence of objects (entity or projection) by mapping
        /// columns of the data reader to object members by name. 
        /// 
 
        /// The type of the resulting objects.
        ///  
        /// 
        IEnumerable Translate(Type elementType, DbDataReader reader);

        /// 
 
        /// Translates an IDataReader containing multiple result sets into sequences of objects
        /// (entity or projection) by mapping columns of the data reader to object members by name. 
        /// 
 
        /// 
        ///  
        IMultipleResults Translate(DbDataReader reader);

        /// 

        /// Returns the query text in the database server's native query language 
        /// that would need to be executed to perform the specified query.
        /// 
 
        /// The query 
        /// 
        string GetQueryText(Expression query); 

        /// 

        /// Return an IDbCommand object representing the translation of specified query.
        /// 
 
        /// 
        ///  
        DbCommand GetCommand(Expression query); 
    }
 
    /// 

    /// A compiled query.
    /// 

    internal interface ICompiledQuery { 
        /// 

        /// Executes the compiled query using the specified provider and a set of arguments. 
        /// 
 
        /// The provider that will execute the compiled query.
        /// Argument values to supply to the parameters of the compiled query, 
        /// when the query is specified as a LambdaExpression.
        /// 
        IExecuteResult Execute(IProvider provider, object[] arguments);
    } 

    internal static class DataManipulation { 
        /// 
 
        /// The method signature used to encode an Insert command.
        /// The method will throw a NotImplementedException if called directly. 
        /// 

        /// 
        /// 
        ///  
        /// 
        ///  
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "item", Justification = "[....]: The method is being used to represent a method signature")] 
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "resultSelector", Justification = "[....]: The method is being used to represent a method signature")]
        public static TResult Insert(TEntity item, Func resultSelector) { 
            throw new NotImplementedException();
        }
        /// 

        /// The method signature used to encode an Insert command. 
        /// The method will throw a NotImplementedException if called directly.
        /// 
 
        ///  
        /// 
        ///  
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "item", Justification = "[....]: The method is being used to represent a method signature")]
        public static int Insert(TEntity item) {
            throw new NotImplementedException();
        } 
        /// 

        /// The method signature used to encode an Update command. 
        /// The method will throw a NotImplementedException if called directly. 
        /// 

        ///  
        /// 
        /// 
        /// 
        ///  
        /// 
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "item", Justification = "[....]: The method is being used to represent a method signature")] 
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "check", Justification = "[....]: The method is being used to represent a method signature")] 
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "resultSelector", Justification = "[....]: The method is being used to represent a method signature")]
        public static TResult Update(TEntity item, Func check, Func resultSelector) { 
            throw new NotImplementedException();
        }
        /// 

        /// The method signature used to encode an Update command. 
        /// The method will throw a NotImplementedException if called directly.
        /// 
 
        ///  
        /// 
        ///  
        /// 
        /// 
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "item", Justification = "[....]: The method is being used to represent a method signature")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "resultSelector", Justification = "[....]: The method is being used to represent a method signature")] 
        public static TResult Update(TEntity item, Func resultSelector) {
            throw new NotImplementedException(); 
        } 
        /// 

        /// The method signature used to encode an Update command. 
        /// The method will throw a NotImplementedException if called directly.
        /// 

        /// 
        ///  
        /// 
        ///  
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "item", Justification = "[....]: The method is being used to represent a method signature")] 
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "check", Justification = "[....]: The method is being used to represent a method signature")]
        public static int Update(TEntity item, Func check) { 
            throw new NotImplementedException();
        }
        /// 

        /// The method signature used to encode an Update command. 
        /// The method will throw a NotImplementedException if called directly.
        /// 
 
        ///  
        /// 
        ///  
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "item", Justification = "[....]: The method is being used to represent a method signature")]
        public static int Update(TEntity item) {
            throw new NotImplementedException();
        } 
        /// 

        /// The method signature used to encode a Delete command. 
        /// The method will throw a NotImplementedException if called directly. 
        /// 

        ///  
        /// 
        /// 
        /// 
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "item", Justification = "[....]: The method is being used to represent a method signature")] 
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "check", Justification = "[....]: The method is being used to represent a method signature")]
        public static int Delete(TEntity item, Func check) { 
            throw new NotImplementedException(); 
        }
        /// 
 
        /// The method signature used to encode a Delete command.
        /// The method will throw a NotImplementedException if called directly.
        /// 

        ///  
        /// 
        ///  
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "item", Justification = "[....]: The method is being used to represent a method signature")] 
        public static int Delete(TEntity item) {
            throw new NotImplementedException(); 
        }
    }
}

