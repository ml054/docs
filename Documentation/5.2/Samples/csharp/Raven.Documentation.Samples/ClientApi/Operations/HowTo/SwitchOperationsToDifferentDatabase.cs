﻿using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Documents.Operations.Revisions;
using Raven.Documentation.Samples.Orders;

namespace Raven.Documentation.Samples.ClientApi.Operations
{
    public class SwitchOperationsToDifferentDatabase
    {
        private interface OperationsForDatabaseSyntax
        {
            #region syntax_1
            OperationExecutor ForDatabase(string databaseName);
            #endregion
        }
        
        private interface MaintenanceForDatabaseSyntax
        {
            #region syntax_2
            MaintenanceOperationExecutor ForDatabase(string databaseName);
            #endregion
        }

        public void SwitchOperationToDifferentDatabase()
        {
            #region for_database_1
            var documentStore = new DocumentStore
            {
                Database = "DefaultDB" // Define default database on the store
            }.Initialize();
            
            using (documentStore)
            {
                // Get operation executor for another database
                OperationExecutor opExecutor = documentStore.Operations.ForDatabase("AnotherDB");
                
                // Send the operation, e.g. 'GetRevisionsOperation' will be executed on "AnotherDB"
                var revisionsInAnotherDB = 
                    opExecutor.Send(new GetRevisionsOperation<Order>("Orders/1-A"));
             
                // Without 'ForDatabase', the operation is executed "DefaultDB"
                var revisionsInDefaultDB =
                    documentStore.Operations.Send(new GetRevisionsOperation<Company>("Company/1-A"));
            }
            #endregion
        }
        
        public void SwitchMaintenanceOperationToDifferentDatabase()
        {
            #region for_database_2
            var documentStore = new DocumentStore
            {
                Database = "DefaultDB" // Define default database on the store
            }.Initialize();
            
            using (documentStore = new DocumentStore())
            {
                // Get maintenance operation executor for another database
                MaintenanceOperationExecutor opExecutor = documentStore.Maintenance.ForDatabase("AnotherDB");
                
                // Send the operation, e.g. get database stats for "AnotherDB"
                var statsForAnotherDB =
                    opExecutor.Send(new GetStatisticsOperation());
                
                // Without 'ForDatabase', the stats are retrieved for "DefaultDB"
                var statsForDefaultDB =
                    documentStore.Maintenance.Send(new GetStatisticsOperation());
            }
            #endregion
        }
    }
}
