# map.wsu.edu
WSU map

[![Build Status](https://travis-ci.org/washingtonstateuniversity/map.wsu.edu.svg?branch=master)](https://travis-ci.org/washingtonstateuniversity/map.wsu.edu)

##How to run locally
This repo doesn't include the `Map.Web/secrets.config` file which is what is used to set the database connection.  You'll have to create your own by making a copy of `Map.Web/secrets.config.default` and renaming it `Map.Web/secrets.config`.  This will look like:

```xml
<?xml version="1.0" encoding="utf-8"?>
<!-- rename this file to secrets.config and fill in your connection strings -->
<connectionStrings>
	<add name="mapLive" connectionString="[YOUR CONNECTION STRING]" providerName="System.Data.SqlClient"  />
	<add name="mapDev"  connectionString="[YOUR CONNECTION STRING]" providerName="System.Data.SqlClient"  />
</connectionStrings>
```

Just replace [YOUR CONNECTION STRING] with your connection string.
