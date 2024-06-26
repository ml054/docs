# Get Index Terms Operation

---

{NOTE: }

* Use `GetTermsOperation` to retrieve the **terms of an index-field**.  

* In this page:
    * [Get Terms example](../../../../client-api/operations/maintenance/indexes/get-terms#get-terms-example)
    * [Syntax](../../../../client-api/operations/maintenance/indexes/get-terms#syntax)

{NOTE/}

---

{PANEL: Get Terms example}

{CODE-TABS}
{CODE-TAB:csharp:Sync get_index_terms@ClientApi\Operations\Maintenance\Indexes\GetIndexTerms.cs /}
{CODE-TAB:csharp:Async get_index_terms_async@ClientApi\Operations\Maintenance\Indexes\GetIndexTerms.cs /}
{CODE-TABS/}

{PANEL/}

{PANEL: Syntax}

{CODE get_index_terms_syntax@ClientApi\Operations\Maintenance\Indexes\GetIndexTerms.cs /}

| Parameters | Type | Description |
| - | - | - |
| **indexName** | `string` | Name of an index to get terms for |
| **field** | `string` | Name of index-field to get terms for |
| **fromValue** | `string` | The starting term from which to return results.<br>This term is not included in the results.<br>`null` - start from first term. |
| **pageSize** | `int?` | Number of terms to get.<br>`null` - return all terms.  |

| Return value of `store.Maintenance.Send(getTermsOp)` | Description |
| - |- |
| string[] | List of terms for the requested index-field. <br> Alphabetically ordered. |

{PANEL/}

## Related Articles

### Indexes

- [What are Indexes](../../../../indexes/what-are-indexes)
- [Creating and Deploying Indexes](../../../../indexes/creating-and-deploying)
- [Index Administration](../../../../indexes/index-administration)
