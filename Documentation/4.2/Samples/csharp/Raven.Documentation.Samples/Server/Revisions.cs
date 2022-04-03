﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Raven.Client;
using Raven.Client.Documents;
using Raven.Client.Documents.Operations.Indexes;
using Raven.Client.Documents.Operations.Revisions;
using Raven.Client.Documents.Session;
using Raven.Client.Json;
using Raven.Documentation.Samples.Orders;

namespace Raven.Documentation.Samples.Server
{
    public class Versioning
    {
        private class User
        {
            public string Name { get; set; }
        }

        private class Loan
        {
            public string Id { get; set; }
        }

        public async Task Sample()
        {
            using (var store = new DocumentStore())
            {
                #region configuration
                await store.Maintenance.SendAsync(new ConfigureRevisionsOperation(new RevisionsConfiguration
                {
                    Default = new RevisionsCollectionConfiguration
                    {
                        Disabled = false,
                        PurgeOnDelete = false,
                        MinimumRevisionsToKeep = 5,
                        MinimumRevisionAgeToKeep = TimeSpan.FromDays(14),
                    },
                    Collections = new Dictionary<string, RevisionsCollectionConfiguration>
                    {
                        {"Users", new RevisionsCollectionConfiguration {Disabled = true}},
                        {"Orders", new RevisionsCollectionConfiguration {Disabled = false}}
                    }
                }));
                #endregion

                #region store
                using (var session = store.OpenAsyncSession())
                {
                    await session.StoreAsync(new User
                    {
                        Name = "Ayende Rahien"
                    });

                    await session.SaveChangesAsync();
                }
                #endregion

                Loan loan = new Loan { Id = "loans/1" };

                using (var session = store.OpenAsyncSession())
                {
                    #region get_revisions
                    List<User> revisions = await session
                        .Advanced
                        .Revisions
                        .GetForAsync<User>("users/1", start: 0, pageSize: 25);

                    List<MetadataAsDictionary> revisionsMetadata = await session
                        .Advanced
                        .Revisions
                        .GetMetadataForAsync("users/1", start: 0, pageSize: 25);

                    User revison = await session
                        .Advanced
                        .Revisions
                        .GetAsync<User>(revisionsMetadata[0].GetString(Constants.Documents.Metadata.ChangeVector));

                    User revisonAtYearAgo = await session
                        .Advanced
                        .Revisions
                        .GetAsync<User>("users/1", DateTime.Now.AddYears(-1));
                    #endregion
                }
            }
        }

        public async Task ForceRevisionCreationForSample()
        {
            using (var store = new DocumentStore())
            {
                string companyId;
                using (var session = store.OpenSession())
                {
                    #region ForceRevisionCreationByEntity
                    // Force revision creation by entity
                    var company = new Company { 
                            Name = "CompanyProfile" 
                        };
                    session.Store(company);
                    companyId = company.Id;
                    session.SaveChanges();

                    // Forcing the creation of a revision by entity can be performed 
                    // only when the entity is tracked, after the document is stored.
                    session.Advanced.Revisions.ForceRevisionCreationFor<Company>(company);
                    #endregion
                }

                using (var session = store.OpenSession())
                {
                    #region ForceRevisionCreationByID
                    // Force revision creation by ID
                    session.Advanced.Revisions.ForceRevisionCreationFor(companyId);
                    session.SaveChanges();
                    #endregion

                    var revisionsCount = session.Advanced.Revisions.GetFor<Company>(companyId).Count;
                }
            }
        }

        public class Company
        {
            public string Id { get; set; }
            public string ExternalId { get; set; }
            public string Name { get; set; }
            public Contact Contact { get; set; }
            public Address Address { get; set; }
            public string Phone { get; set; }
            public string Fax { get; set; }
        }

    }
}
