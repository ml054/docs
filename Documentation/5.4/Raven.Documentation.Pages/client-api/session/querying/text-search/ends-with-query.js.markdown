﻿# Ends-With Query

---

{NOTE: }

* Use `whereEndsWith` to query for documents having a field that ends with some specified string.  

* Unless explicitly specified, the string comparisons are case-insensitive by default.

* **Note**:  
  This postfix search causes the server to perform a full index scan.  
  Instead, consider using a static index that indexes the field in reverse order  
  and then query with a [prefix search](../../../../client-api/session/querying/text-search/starts-with-query), which is much faster.

* In this page:
    * [whereEndsWith](../../../../client-api/session/querying/text-search/ends-with-query#whereendswith)
    * [whereEndsWith (case-sensitive)](../../../../client-api/session/querying/text-search/ends-with-query#whereendswith-(case-sensitive))
    * [Negate whereEndsWith](../../../../client-api/session/querying/text-search/ends-with-query#negate-whereendswith)
    * [Syntax](../../../../client-api/session/querying/text-search/ends-with-query#syntax)

{NOTE/}

---

{PANEL: whereEndsWith}

{CODE-TABS}
{CODE-TAB:nodejs:Query endsWith_1@client-api\session\querying\textSearch\endsWith.js /}
{CODE-TAB-BLOCK:sql:RQL}
from "Products"
where endsWith(Name, "Lager")
{CODE-TAB-BLOCK/}
{CODE-TABS/}

{PANEL/}

{PANEL: whereEndsWith (case-sensitive)}

{CODE-TABS}
{CODE-TAB:nodejs:Query endsWith_2@client-api\session\querying\textSearch\endsWith.js /}
{CODE-TAB-BLOCK:sql:RQL}
from "Products"
where exact(endsWith(Name, "Lager"))
{CODE-TAB-BLOCK/}
{CODE-TABS/}

{PANEL/}

{PANEL: Negate whereEndsWith}

{CODE-TABS}
{CODE-TAB:nodejs:Query endsWith_3@client-api\session\querying\textSearch\endsWith.js /}
{CODE-TAB-BLOCK:sql:RQL}
from "Products"
where exists(Name) and not endsWith(Name, "Lager")
{CODE-TAB-BLOCK/}
{CODE-TABS/}

{PANEL/}

{PANEL: Syntax}

{CODE:nodejs syntax@client-api\session\querying\textSearch\StartsWith.js /}

| Parameter     | Type    | Description                                                               |
|---------------|---------|---------------------------------------------------------------------------|
| **fieldName** | string  | The field name in which to search                                         |
| **value**     | string  | The **postfix** string to search by                                       |
| **exact**     | boolean | `false` - search is case-insensitive<br>`true` - search is case-sensitive |


{PANEL/}

## Related Articles

### Session

- [Query overview](../../../../client-api/session/querying/how-to-query)
- [Starts-With query](../../../../client-api/session/querying/text-search/starts-with-query)
- [Full-text search](../../../../client-api/session/querying/text-search/full-text-search)

### Indexes

- [map indexes](../../../../indexes/map-indexes)
