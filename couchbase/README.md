# CouchbaseFakeIt

This folder provides configuration for Couchbase which is applied upon startup,
including bucket and index creation and generating fake data. For documentation,
see [CouchbaseFakeIt](https://github.com/brantburnett/couchbasefakeit).

- `buckets.json`: Defines buckets to create
- `default/models`: Contains YAML files defining fake data to generate in the `default` bucket
- `default/indexes`: Contains YAML files defining indexes to create in the `default` bucket

It is also possible to define FTS indexes and Eventing functions. Environment variables may
be used to configure other cluster details, such as RAM allocations and the services which are enabled.
