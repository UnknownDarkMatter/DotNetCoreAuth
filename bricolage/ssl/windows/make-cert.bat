SET CURRENT_DIR=%~dp0
SET OPENSSL_DIR=C:\OpenSSL
SET PASSWORD=azerty

CD %CURRENT_DIR%
CD ..

%OPENSSL_DIR%\openssl req -new -x509 -sha256 -nodes -newkey rsa:2048 -config localhost.conf -out localhost.crt -keyout localhost.key
%OPENSSL_DIR%\openssl pkcs12 -export -inkey localhost.key -in localhost.crt -out localhost.pfx -passin pass:%PASSWORD% -passout pass:%PASSWORD%


pause
