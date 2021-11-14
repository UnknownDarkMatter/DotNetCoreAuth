#avec notepadd++ convertir les sauts de ligne au format UNIX
for i in {1..90};
do
    /opt/mssql-tools/bin/sqlcmd -l 30 -S localhost -U sa -P P@ssword1! -i /home/IdentityServer/Migrations/Scripts/Update/20211113105032_CreateDatabase.sql
    if [ $? -eq 0 ]
    then
        echo "setup.sql completed"
        break
    else
        echo "not ready yet..."
        sleep 1
    fi
done