# Listeners : What are store listeners and how to work with them?

In order execute a custom action before or after a document is stored the `IDocumentStoreListener` needs to be implemented:

{CODE:java document_store_interface@ClientApi\Listeners\Store.java /}

##Example

To prevent anyone from adding documents with a certain key, you can create `FilterForbiddenKeysDocumentListener`. In result the document with the specified forbidden id will not be stored.

{CODE:java document_store_example@ClientApi\Listeners\Store.java /}
