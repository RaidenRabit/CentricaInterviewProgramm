SET SERVER="localhost"

SET DB="master"

SET LOGIN="SA"

SET PASSWORD="Password12!"

SET INPUT=%cd%\CreateTablesScript.sql

sqlcmd  -S%SERVER% -d%DB% -U%LOGIN% -P%PASSWORD% -i%INPUT% -b
SET INPUT=%cd%\InsertDataScript.sql
sqlcmd  -S%SERVER% -d%DB% -U%LOGIN% -P%PASSWORD% -i%INPUT% -b